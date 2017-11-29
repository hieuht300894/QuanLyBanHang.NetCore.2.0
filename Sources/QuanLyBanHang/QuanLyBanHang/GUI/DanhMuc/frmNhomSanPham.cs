﻿using DevExpress.XtraGrid.Views.Grid;
using QuanLyBanHang.BLL.Common;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using EntityModel.DataModel;


namespace QuanLyBanHang.GUI.DanhMuc
{
    public partial class frmNhomSanPham : frmBaseGrid
    {
        BindingList<eNhomSanPham> lstEntries = new BindingList<eNhomSanPham>();
        BindingList<eNhomSanPham> lstEdited = new BindingList<eNhomSanPham>();

        public frmNhomSanPham()
        {
            InitializeComponent();
        }
        protected override void frmBase_Load(object sender, EventArgs e)
        {
            base.frmBase_Load(sender, e);
            LoadData(0);
            CustomForm();
        }

        public async override void LoadData(object KeyID)
        {
            lstEdited = new BindingList<eNhomSanPham>();
            lstEntries = new BindingList<eNhomSanPham>(await clsFunction<eNhomSanPham>.Instance.GetAll());
            await RunMethodAsync(() => { gctDanhSach.DataSource = lstEntries; });
        }
        public override bool ValidationForm()
        {
            grvDanhSach.CloseEditor();
            grvDanhSach.UpdateCurrentRow();
            return base.ValidationForm();
        }
        public async override Task<bool> SaveData()
        {
            bool chk = false;
            chk = await clsFunction<eNhomSanPham>.Instance.AddOrUpdate(lstEdited.ToList());
            return chk;
        }
        public override void CustomForm()
        {
            base.CustomForm();

            gctDanhSach.MouseClick += gctDanhSach_MouseClick;
            grvDanhSach.RowUpdated += grvDanhSach_RowUpdated;
            grvDanhSach.InitNewRow += grvDanhSach_InitNewRow;
        }

        private void grvDanhSach_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            GridView view = (GridView)sender;
            view.SetRowCellValue(e.RowHandle, colKeyID, -lstEdited.Count);
        }
        private void gctDanhSach_MouseClick(object sender, MouseEventArgs e)
        {
            ShowGridPopup(sender, e, true, false, true, true, true, true);
        }
        private void grvDanhSach_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            if (!lstEdited.Any(x => x.KeyID == ((eNhomSanPham)e.Row).KeyID)) lstEdited.Add((eNhomSanPham)e.Row);
        }
    }
}
