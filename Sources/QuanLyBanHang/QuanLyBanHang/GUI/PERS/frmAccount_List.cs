﻿using EntityModel.DataModel;
using QuanLyBanHang.BLL.PERS;
using System;
using System.Windows.Forms;

namespace QuanLyBanHang.GUI.PER
{
    public partial class frmAccount_List : frmBase
    {
        #region Variables
        #endregion

        #region Form Events
        public frmAccount_List()
        {
            InitializeComponent();
        }
        protected override void frmBase_Load(object sender, EventArgs e)
        {
            base.frmBase_Load(sender, e);
            LoadData(0);
            CustomForm();
        }
        #endregion

        #region Grid Events
        private void grvAccountList_DoubleClick(object sender, EventArgs e)
        {
            MouseEventArgs mouse = e as MouseEventArgs;
            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hi = grvAccountList.CalcHitInfo(mouse.Location);
            if (grvAccountList.FocusedRowHandle >= 0 && (hi.InRow || hi.InRowCell))
            {
                UpdateEntry();
            }
        }

        private void gctAccountList_MouseClick(object sender, MouseEventArgs e)
        {
            base.ShowGridPopup(sender, e, true, true, true, false, true, true);
        }
        #endregion

        #region Base Button Events
        protected override void btnAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InsertEntry();
        }

        protected override void btnRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            RefreshEntry();
        }

        protected override void btnEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UpdateEntry();
        }

        protected override void btnDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DeleteEntry();
        }

        protected override void bbpAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InsertEntry();
        }

        protected override void bbpEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UpdateEntry();
        }

        protected override void bbpDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DeleteEntry();
        }

        protected override void bbpRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            RefreshEntry();
        }
        #endregion

        #region Methods
        public override void LoadData(object KeyID)
        {
            gctAccountList.DataSource = clsAccount.Instance.SearchAccount(true, 0);
            grvAccountList.FocusedRowHandle = grvAccountList.LocateByValue("IDPersonnel", KeyID);
        }

        public override void InsertEntry()
        {
            using (frmAccount _frm = new frmAccount())
            {
                _frm.Text = "Thêm mới tài khoản";
                _frm.fType = eFormType.Add;
                _frm._ReloadData = this.LoadData;
                _frm.ShowDialog();
            }
        }

        public override void UpdateEntry()
        {
            if (grvAccountList.RowCount > 0 && grvAccountList.FocusedRowHandle >= 0)
            {
                try
                {
                    using (frmAccount _frm = new frmAccount())
                    {
                        xAccount _eEntry = (xAccount)grvAccountList.GetRow(grvAccountList.FocusedRowHandle);
                        _frm._iEntry = _eEntry;
                        _frm.Text = "Cập nhật tài khoản";
                        _frm.fType = eFormType.Edit;
                        _frm._ReloadData = this.LoadData;
                        _frm.ShowDialog();
                    }
                }
                catch (Exception ex)
                {
                    clsGeneral.showErrorException(ex, "Exception");
                }
            }
        }

        public override void DeleteEntry()
        {
            //if (grvAccountList.RowCount > 0 && grvAccountList.FocusedRowHandle >= 0 && clsGeneral.showConfirmMessage("Xác nhận xóa dữ liệu".Translation("msgConfirmDelete", this.Name)))
            //{
            //    try
            //    {
            //        if (clsAccount.Instance.deleteEntry(((xAccount)grvAccountList.GetRow(grvAccountList.FocusedRowHandle)).IDPersonnel))
            //        {
            //            loadData(0);
            //        }
            //        else
            //            clsGeneral.showMessage("Xóa dữ liệu không thành công.\r\nVui lòng kiểm tra lại".Translation("msgDeleteFailed", this.Name));

            //    }
            //    catch (Exception ex)
            //    {
            //        clsGeneral.showErrorException(ex, "Exception");
            //    }
            //}
        }

        public override void RefreshEntry()
        {
            LoadData(0);
        }
        #endregion
    }
}