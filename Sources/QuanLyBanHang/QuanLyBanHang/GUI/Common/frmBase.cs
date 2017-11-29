﻿using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraLayout;
using DevExpress.XtraTreeList;
using QuanLyBanHang.BLL.Common;
using QuanLyBanHang.BLL.PERS;
using QuanLyBanHang.Module;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyBanHang
{
    public partial class frmBase : XtraForm
    {
        #region Variables
        public string MsgAdd { get; set; } = "Thêm mới dữ liệu";
        public string MsgEdit { get; set; } = "Cập nhật dữ liệu";
        public string MsgDelete { get; set; } = "Xóa dữ liệu";
        public eFormType fType;
        public List<eFormType> fTypes;
        public List<ControlObject> lstChildControls = new List<ControlObject>();
        bool IsLeaveForm = false;
        public delegate void ReloadData(object KeyID);
        public ReloadData _ReloadData;
        #endregion

        #region Form
        public frmBase()
        {
            InitializeComponent();
        }
        #endregion

        #region Methods
        private void BarItemVisibility()
        {
            btnAdd.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            btnEdit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            btnDelete.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            btnRefresh.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            btnSave.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            btnSaveAndAdd.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            btnCancel.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            btnPrintPreview.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            btnExportExcel.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

            foreach (eFormType _fType in fTypes)
            {
                if (_fType == eFormType.Default)
                {
                    btnAdd.Visibility = clsGeneral.curUserFeature.IsAdd ? DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never;
                    btnEdit.Visibility = clsGeneral.curUserFeature.IsEdit ? DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never;
                    btnDelete.Visibility = clsGeneral.curUserFeature.IsDelete ? DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never;
                    btnRefresh.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                }
                if (_fType == eFormType.Add || _fType == eFormType.Edit)
                {
                    if (_fType == eFormType.Edit && clsGeneral.curUserFeature.IsEdit && clsGeneral.curUserFeature.IsSave)
                    {
                        btnSave.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                        btnSaveAndAdd.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    }
                    if (_fType == eFormType.Add && clsGeneral.curUserFeature.IsAdd && clsGeneral.curUserFeature.IsSave)
                    {
                        btnSave.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                        btnSaveAndAdd.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    }
                    btnCancel.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                }
                if (_fType == eFormType.Print)
                {
                    btnPrintPreview.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    btnExportExcel.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                }
            }
        }
        private void SetCaptionButton()
        {
            foreach (var item in barTop.Manager.Items)
            {
                if (item is DevExpress.XtraBars.BarButtonItem)
                {
                    if (!Properties.Settings.Default.CurrentCulture.Equals("VN"))
                    {
                        DevExpress.XtraBars.BarButtonItem btn = (DevExpress.XtraBars.BarButtonItem)item;
                        btn.Caption = btn.Name.AutoSpace();
                    }
                }
            }
        }
        private void LoadAccessForm()
        {
            fTypes = fTypes ?? new List<eFormType>();
            if (!fTypes.Any(x => x == fType)) fTypes.Add(fType);

            if (clsGeneral.curPersonnel.KeyID > 0 && clsGeneral.curAccount.IDPermission > 0 && clsGeneral.curAccount.IDPermission > 0)
                clsGeneral.curUserFeature = clsUserRole.Instance.GetUserFeature(this.Name);
            else if (clsGeneral.curPersonnel.KeyID == 0 && clsGeneral.curAccount.IDPermission == 0)
                clsGeneral.curUserFeature = new EntityModel.DataModel.xUserFeature() { IsAdd = true, IsDelete = true, IsEdit = true, IsSave = true, IsExportExcel = true, IsPrintPreview = true, IsEnable = true };
        }
        private void InitEvents()
        {
            btnAdd.ItemClick += btnAdd_ItemClick;
            btnEdit.ItemClick += btnEdit_ItemClick;
            btnDelete.ItemClick += btnDelete_ItemClick;

            btnSave.ItemClick += btnSave_ItemClick;
            btnSaveAndAdd.ItemClick += btnSaveAndAdd_ItemClick;
            btnCancel.ItemClick += btnCancel_ItemClick;

            btnRefresh.ItemClick += btnRefresh_ItemClick;
            btnPrintPreview.ItemClick += btnPrintPreview_ItemClick;
            btnExportExcel.ItemClick += btnExportExcel_ItemClick;

            bbpAdd.ItemClick += bbpAdd_ItemClick;
            bbpEdit.ItemClick += bbpEdit_ItemClick;
            bbpDelete.ItemClick += bbpDelete_ItemClick;

            bbpSave.ItemClick += bbpSave_ItemClick;
            bbpSaveAndAdd.ItemClick += bbpSaveAndAdd_ItemClick;
            bbpCancel.ItemClick += bbpCancel_ItemClick;

            bbpRefresh.ItemClick += bbpRefresh_ItemClick;
            bbpPrintPreview.ItemClick += bbpPrintPreview_ItemClick;
            bbpExportExcel.ItemClick += bbpExportExcel_ItemClick;
        }
        private void LoadControl()
        {
            lstChildControls = new List<ControlObject>();

            List<FieldInfo> lstFieldInfoes = new List<FieldInfo>(GetType().GetRuntimeFields());
            foreach (FieldInfo fInfo in lstFieldInfoes)
            {
                var Obj = fInfo.GetValue(this);

                if (Obj is LayoutControl)
                    lstChildControls.Add(new ControlObject() { ctrMain = (Control)Obj });
                if (Obj is TextEdit)
                    lstChildControls.Add(new ControlObject() { ctrMain = (Control)Obj });
                if (Obj is SpinEdit)
                    lstChildControls.Add(new ControlObject() { ctrMain = (Control)Obj });
                if (Obj is DateEdit)
                    lstChildControls.Add(new ControlObject() { ctrMain = (Control)Obj });
                if (Obj is LookUpEdit)
                    lstChildControls.Add(new ControlObject() { ctrMain = (Control)Obj });
                if (Obj is GridControl)
                    lstChildControls.Add(new ControlObject() { ctrMain = (Control)Obj });
                if (Obj is TreeList)
                    lstChildControls.Add(new ControlObject() { ctrMain = (Control)Obj });
                if (Obj is SearchLookUpEdit)
                    lstChildControls.Add(new ControlObject() { ctrMain = (Control)Obj });
                if (Obj is MemoEdit)
                    lstChildControls.Add(new ControlObject() { ctrMain = (Control)Obj });

                if (Obj is RepositoryItemDateEdit)
                    lstChildControls.Add(new ControlObject() { repoMain = (RepositoryItem)Obj });
                if (Obj is RepositoryItemSpinEdit)
                    lstChildControls.Add(new ControlObject() { repoMain = (RepositoryItem)Obj });
                if (Obj is RepositoryItemLookUpEdit)
                    lstChildControls.Add(new ControlObject() { repoMain = (RepositoryItem)Obj });
            }
        }
        #endregion

        #region Events
        protected virtual void frmBase_Load(object sender, EventArgs e)
        {
            LoadAccessForm();
            BarItemVisibility();
            SetCaptionButton();
            InitEvents();
            LoadControl();
        }
        private void frmBase_Enter(object sender, EventArgs e)
        {
            if (IsLeaveForm)
            {
                LoadAccessForm();
                IsLeaveForm = !IsLeaveForm;
            }
        }
        private void frmBase_Leave(object sender, EventArgs e)
        {
            if (!IsLeaveForm)
            {
                IsLeaveForm = !IsLeaveForm;
            }
        }
        protected virtual void frmBase_FormClosing(object sender, FormClosingEventArgs e)
        {
            clsGeneral.CallWaitForm(this);
            try
            {
                _ReloadData?.Invoke(0);

                foreach (ControlObject ctrObj in lstChildControls)
                {
                    if (ctrObj.ctrMain != null)
                    {
                        if (ctrObj.ctrMain is GridControl)
                        {
                            ((GridControl)ctrObj.ctrMain).ViewCollection.ToList().ForEach(x => ((GridView)x).SaveLayout(this));
                            ((GridControl)ctrObj.ctrMain).DataSource = null;
                        }
                        if (ctrObj.ctrMain is TreeList)
                        {
                            ((TreeList)ctrObj.ctrMain).SaveLayout();
                            ((TreeList)ctrObj.ctrMain).DataSource = null;
                        }
                        if (ctrObj.ctrMain is SearchLookUpEdit)
                        {
                            ((SearchLookUpEdit)ctrObj.ctrMain).Properties.View.SaveLayout(this);
                            ((SearchLookUpEdit)ctrObj.ctrMain).Properties.DataSource = null;
                        }
                        if (ctrObj.ctrMain is LookUpEdit)
                        {
                            ((LookUpEdit)ctrObj.ctrMain).Properties.DataSource = null;
                        }

                        ctrObj.ctrMain = null;
                    }
                    else if (ctrObj.repoMain != null)
                    {
                        if (ctrObj.repoMain is RepositoryItemLookUpEdit)
                            ((RepositoryItemLookUpEdit)ctrObj.repoMain).DataSource = null;

                        ctrObj.repoMain = null;
                    }
                }

                lstChildControls = null;
                GC.Collect();
            }
            catch { }
            clsGeneral.CloseWaitForm();
        }
        protected virtual void btnAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InsertEntry();
        }
        protected virtual void btnEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UpdateEntry();
        }
        protected virtual void btnDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DeleteEntry();
        }
        protected virtual void btnRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            RefreshEntry();
        }
        protected virtual void btnExportExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
        }
        protected virtual void btnPrintPreview_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
        }
        protected virtual void bbpAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InsertEntry();
        }
        protected virtual void bbpEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UpdateEntry();
        }
        protected virtual void bbpDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DeleteEntry();
        }
        protected virtual void bbpRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            RefreshEntry();
        }
        protected virtual void bbpExportExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
        }
        protected virtual void bbpPrintPreview_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
        }
        protected async virtual void btnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (ValidationForm())
            {
                bool res = await SaveData();
                if (res)
                    DialogResult = DialogResult.OK;
            }
        }
        protected async virtual void btnSaveAndAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (ValidationForm())
            {
                bool res = await SaveData();
                if (res)
                {
                    clsGeneral.showMessage("Lưu dữ liệu thành công.");
                    fType = eFormType.Add;
                    Text = MsgAdd;
                    RenewData();
                    ResetControl();
                    LoadDataForm();
                }
                else
                    clsGeneral.showMessage("Lưu dữ liệu thất bại. Xin vui lòng thử lại.");
            }
        }
        protected virtual void btnCancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ResetControl();
            LoadDataForm();
        }
        protected async virtual void bbpSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (ValidationForm())
            {
                bool res = await SaveData();
                if (res)
                    DialogResult = DialogResult.OK;
            }
        }
        protected async virtual void bbpSaveAndAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (ValidationForm())
            {
                bool res = await SaveData();
                if (res)
                {
                    clsGeneral.showMessage("Lưu dữ liệu thành công.");
                    fType = eFormType.Add;
                    Text = MsgAdd;
                    RenewData();
                    ResetControl();
                    LoadDataForm();
                }
                else
                    clsGeneral.showMessage("Lưu dữ liệu thất bại. Xin vui lòng thử lại.");
            }
        }
        protected virtual void bbpCancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ResetControl();
            LoadDataForm();
        }
        private void btnLoading_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string threadName = clsService.dManageThreads.Select(x => x.Key).FirstOrDefault(x => x.Equals(Name));
            if (!string.IsNullOrEmpty(threadName))
            {
                foreach (var threadObj in clsService.dManageThreads[threadName])
                {
                    threadObj.TokenSource.Cancel();
                }

                clsService.dManageThreads.Remove(threadName);
            }
        }
        private void btnFitComlum_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Action action = new Action(() =>
            {
                try
                {
                    foreach (ControlObject ctrObj in lstChildControls)
                    {
                        if (ctrObj.ctrMain != null)
                        {
                            if (ctrObj.ctrMain is GridControl)
                            {
                                foreach (GridView view in ((GridControl)ctrObj.ctrMain).ViewCollection)
                                {
                                    if (ctrObj.ctrMain.InvokeRequired)
                                    {
                                        ctrObj.ctrMain.Invoke(new Action(() =>
                                        {
                                            view.BestFitMaxRowCount = Properties.Settings.Default.RowsInPage;
                                            view.BestFitColumns();
                                            view.IndicatorWidth = TextRenderer.MeasureText(view.RowCount.ToString(), view.Appearance.FocusedRow.Font).Width + 10;
                                        }));
                                    }
                                    else
                                    {
                                        view.BestFitMaxRowCount = Properties.Settings.Default.RowsInPage;
                                        view.BestFitColumns();
                                        view.IndicatorWidth = TextRenderer.MeasureText(view.RowCount.ToString(), view.Appearance.FocusedRow.Font).Width + 10;
                                    }
                                }
                            }
                            if (ctrObj.ctrMain is TreeList)
                            {
                                TreeList trlMain = (TreeList)ctrObj.ctrMain;
                                if (ctrObj.ctrMain.InvokeRequired)
                                {
                                    ctrObj.ctrMain.Invoke(new Action(() =>
                                    {
                                        trlMain.BestFitColumns();
                                        trlMain.IndicatorWidth = TextRenderer.MeasureText(trlMain.Nodes.Count.ToString(), trlMain.Appearance.FocusedRow.Font).Width + 10;
                                    }));
                                }
                                else
                                {
                                    trlMain.BestFitColumns();
                                    trlMain.IndicatorWidth = TextRenderer.MeasureText(trlMain.Nodes.Count.ToString(), trlMain.Appearance.FocusedRow.Font).Width + 10;
                                }
                            }
                            if (ctrObj.ctrMain is SearchLookUpEdit)
                            {
                                SearchLookUpEdit slokMain = (SearchLookUpEdit)ctrObj.ctrMain;
                                if (ctrObj.ctrMain.InvokeRequired)
                                {
                                    ctrObj.ctrMain.Invoke(new Action(() =>
                                    {
                                        slokMain.Properties.View.BestFitMaxRowCount = Properties.Settings.Default.RowsInPage;
                                        slokMain.Properties.View.BestFitColumns();
                                        slokMain.Properties.View.IndicatorWidth = TextRenderer.MeasureText(slokMain.Properties.View.RowCount.ToString(), slokMain.Properties.View.Appearance.FocusedRow.Font).Width + 10;
                                    }));
                                }
                                else
                                {
                                    slokMain.Properties.View.BestFitMaxRowCount = Properties.Settings.Default.RowsInPage;
                                    slokMain.Properties.View.BestFitColumns();
                                    slokMain.Properties.View.IndicatorWidth = TextRenderer.MeasureText(slokMain.Properties.View.RowCount.ToString(), slokMain.Properties.View.Appearance.FocusedRow.Font).Width + 10;
                                }
                            }
                        }
                    }
                }
                catch { }
            });
            Task.Run(action);
        }
        protected virtual void grv_TopRowChanged<T>(object sender, EventArgs e, IList<T> ListData, string query, SqlParameter[] parameters) where T : class, new()
        {
            GridView view = sender as GridView;
            GridViewInfo vi = view.GetViewInfo() as GridViewInfo;
            List<GridRowInfo> lstRowsInfo = new List<GridRowInfo>(vi.RowsInfo.Where(x => x.VisibleIndex != -1));
            for (int i = lstRowsInfo.Count - 1; i >= 0; i--)
            {
                if (view.IsRowVisible(lstRowsInfo[i].VisibleIndex) != RowVisibleState.Visible || view.IsNewItemRow(lstRowsInfo[i].VisibleIndex))
                    lstRowsInfo.RemoveAt(i);
            }
            int LastRow = GetGridViewLastRow(view);
            int RowCount = ListData.Count - 1;

            if (LastRow == RowCount)
                clsFunction<T>.Instance.SelectAsync(this, view.GridControl, ListData, query, parameters);
        }
        public int GetGridViewLastRow(GridView grvMain)
        {
            GridViewInfo vi = grvMain.GetViewInfo() as GridViewInfo;
            List<GridRowInfo> lstRowsInfo = new List<GridRowInfo>(vi.RowsInfo.Where(x => x.VisibleIndex != -1));
            for (int i = lstRowsInfo.Count - 1; i >= 0; i--)
            {
                if (grvMain.IsRowVisible(lstRowsInfo[i].VisibleIndex) != RowVisibleState.Visible || grvMain.IsNewItemRow(lstRowsInfo[i].VisibleIndex))
                    lstRowsInfo.RemoveAt(i);
            }
            return lstRowsInfo.Select(x => x.VisibleIndex).ToList().DefaultIfEmpty().Max();
        }
        #endregion

        #region Method Base
        #region ShowGridPopup
        protected virtual void ShowGridPopup(object sender, MouseEventArgs e,
            bool IsAdd = false, bool IsEdit = false, bool IsDelete = false,
            bool IsSave = false, bool IsPrintPreview = false, bool IsExportExcel = false)
        {
            if (e.Button == MouseButtons.Right)
            {
                GridControl gctMain = (GridControl)sender;
                GridView grvMain = (GridView)gctMain.DefaultView;
                GridHitInfo hi = grvMain.CalcHitInfo(e.Location);

                if (hi.RowHandle >= 0 && (hi.InRow || hi.InRowCell))
                {
                    foreach (eFormType _fType in fTypes)
                    {
                        if (_fType == eFormType.Default)
                        {
                            bbpAdd.Enabled = clsGeneral.curUserFeature.IsAdd && IsAdd;
                            bbpEdit.Enabled = clsGeneral.curUserFeature.IsEdit && IsEdit;
                            bbpDelete.Enabled = clsGeneral.curUserFeature.IsDelete && IsDelete;
                            bbpRefresh.Enabled = true;
                        }
                        if (_fType == eFormType.Add || _fType == eFormType.Edit)
                        {
                            if (_fType == eFormType.Add && clsGeneral.curUserFeature.IsAdd && clsGeneral.curUserFeature.IsSave)
                            {
                                bbpSave.Enabled = clsGeneral.curUserFeature.IsSave && IsSave;
                                bbpSaveAndAdd.Enabled = clsGeneral.curUserFeature.IsSave && IsSave;
                            }
                            if (_fType == eFormType.Edit && clsGeneral.curUserFeature.IsEdit && clsGeneral.curUserFeature.IsSave)
                            {
                                bbpSave.Enabled = clsGeneral.curUserFeature.IsSave && IsSave;
                                bbpSaveAndAdd.Enabled = clsGeneral.curUserFeature.IsSave && IsSave;
                            }
                            bbpCancel.Enabled = true;
                        }
                        if (_fType == eFormType.Print)
                        {
                            bbpPrintPreview.Enabled = clsGeneral.curUserFeature.IsPrintPreview && IsPrintPreview;
                            bbpExportExcel.Enabled = clsGeneral.curUserFeature.IsExportExcel && IsExportExcel;
                        }
                    }
                }
                else
                {
                    foreach (eFormType _fType in fTypes)
                    {
                        if (_fType == eFormType.Default)
                            bbpRefresh.Enabled = true;
                        if (_fType == eFormType.Add)
                            bbpAdd.Enabled = clsGeneral.curUserFeature.IsAdd && IsAdd;
                    }
                    bbpEdit.Enabled = false;
                    bbpDelete.Enabled = false;
                    bbpSave.Enabled = false;
                    bbpSaveAndAdd.Enabled = false;
                    bbpCancel.Enabled = false;
                    bbpPrintPreview.Enabled = false;
                    bbpExportExcel.Enabled = false;
                }

                bbpAdd.Visibility = bbpAdd.Enabled ? DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never;
                bbpEdit.Visibility = bbpEdit.Enabled ? DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never;
                bbpDelete.Visibility = bbpDelete.Enabled ? DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never;
                bbpRefresh.Visibility = bbpRefresh.Enabled ? DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never;
                bbpSave.Visibility = bbpSave.Enabled ? DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never;
                bbpSaveAndAdd.Visibility = bbpSaveAndAdd.Enabled ? DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never;
                bbpCancel.Visibility = bbpCancel.Enabled ? DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never;
                bbpPrintPreview.Visibility = bbpPrintPreview.Enabled ? DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never;
                bbpExportExcel.Visibility = bbpExportExcel.Enabled ? DevExpress.XtraBars.BarItemVisibility.Always : DevExpress.XtraBars.BarItemVisibility.Never;

                popGridMenu.ShowPopup(MousePosition);
            }
        }
        #endregion

        #region Virtual Method
        public virtual void CustomForm()
        {
            try
            {
                foreach (ControlObject ctrObj in lstChildControls)
                {
                    if (ctrObj.ctrMain != null)
                    {
                        if (ctrObj.ctrMain is LayoutControl)
                            ((LayoutControl)ctrObj.ctrMain).Format();
                        if (ctrObj.ctrMain is TextEdit)
                            ((TextEdit)ctrObj.ctrMain).Format();
                        if (ctrObj.ctrMain is SpinEdit)
                            ((SpinEdit)ctrObj.ctrMain).Format();
                        if (ctrObj.ctrMain is DateEdit)
                            ((DateEdit)ctrObj.ctrMain).Format();
                        if (ctrObj.ctrMain is LookUpEdit)
                            ((LookUpEdit)ctrObj.ctrMain).Format();
                        if (ctrObj.ctrMain is GridControl)
                            ((GridControl)ctrObj.ctrMain).Format();
                        if (ctrObj.ctrMain is TreeList)
                            ((TreeList)ctrObj.ctrMain).Format();
                        if (ctrObj.ctrMain is SearchLookUpEdit)
                            ((SearchLookUpEdit)ctrObj.ctrMain).Format();
                    }
                    else if (ctrObj.repoMain != null)
                    {
                        if (ctrObj.repoMain is RepositoryItemDateEdit)
                            ((RepositoryItemDateEdit)ctrObj.repoMain).Format();
                        if (ctrObj.repoMain is RepositoryItemSpinEdit)
                            ((RepositoryItemSpinEdit)ctrObj.repoMain).Format();
                        if (ctrObj.repoMain is RepositoryItemLookUpEdit)
                            ((RepositoryItemLookUpEdit)ctrObj.repoMain).Format();
                    }
                }
            }
            catch { }
        }
        public virtual void LoadPercent(int Percent)
        {
            betPercent.EditValue = Percent;
        }
        public virtual void LoadMessage(string Msg)
        {
            clsGeneral.showMessage(Msg);
        }
        public virtual void LoadError(Exception Ex)
        {
            clsGeneral.showErrorException(Ex);
        }
        public virtual void OpenProgress()
        {
            Action action = () =>
            {
                barBottom.Visible = true;
                betPercent.EditValue = 0;
                betPercent.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            };
            Invoke(action);
        }
        public virtual void CloseProgress()
        {
            Action action = () =>
            {
                barBottom.Visible = false;
                betPercent.EditValue = 0;
                betPercent.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            };
            Invoke(action);
        }
        public virtual void ShowAlert(string Title = "", string Text = "")
        {
            alertMsg.Show(this, Title, Text);
        }
        public async Task<bool> RunMethodAsync(params Func<bool>[] Funcs)
        {
            Funcs = Funcs ?? new Func<bool>[] { };

            foreach (Func<bool> action in Funcs)
            {
                Task task = Task.Factory.StartNew(action);
                await task;
            }

            Task<bool> taskTemp = Task.Factory.StartNew(() => { return true; });
            return await taskTemp;
        }
        public async Task RunMethodAsync(params Action[] Acts)
        {
            if (!IsHandleCreated) return;

            Task taskTemp = Task.Factory.StartNew(() => { });
            await taskTemp;

            Acts = Acts ?? new Action[] { };
            foreach (Action act in Acts)
            {
                Task task = Task.Factory.StartNew(() => { BeginInvoke(act); });
                await task;
            }
        }
        #endregion

        #region Common Method
        public virtual void LoadData(object KeyID)
        {
        }
        public virtual void InsertEntry()
        {
        }
        public virtual void UpdateEntry()
        {
        }
        public virtual void DeleteEntry()
        {
        }
        public virtual void RefreshEntry()
        {
        }    
        public virtual void LoadDataForm()
        {
        }
        public virtual void SetControlValue()
        {
        }
        public virtual void SetDataSource() {
        }
        public virtual bool ValidationForm()
        {
            return true;
        }
        public async virtual Task<bool> SaveData()
        {
            return await Task<bool>.Factory.StartNew(() => { return true; });
        }
        public virtual void RenewData()
        {
        }
        public virtual void ResetControl()
        {
            lstChildControls.ForEach(x =>
            {
                if (x.ctrMain != null)
                {
                    BaseEdit baseEdit = x.ctrMain as BaseEdit;
                    if (baseEdit != null)
                        baseEdit.DataBindings.Clear();
                }
            });
        }
        #endregion
        #endregion
    }
}