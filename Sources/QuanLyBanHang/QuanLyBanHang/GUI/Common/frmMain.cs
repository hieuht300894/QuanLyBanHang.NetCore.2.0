﻿using DevExpress.LookAndFeel;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Docking2010.Views;
using DevExpress.XtraBars.Helpers;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using QuanLyBanHang.Module;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyBanHang.GUI.Common
{
    public partial class frmMain : RibbonForm
    {
        // Timer tmClock;
        public frmMain()
        {
            InitializeComponent();
            SkinHelper.InitSkinGallery(bbiSkin);
            UserLookAndFeel.Default.SkinName = Properties.Settings.Default["SkinName"].ToString();
        }

        /// <summary>
        /// Hàm dùng chung, dùng để load form vào DocumentManager khi bấm vào menu trên ribbon menu
        /// </summary>
        /// <param name="_xtrForm"></param>
        private async void addDocument(XtraForm _xtrForm)
        {
            clsGeneral.CallWaitForm(_xtrForm);
            await Task.Factory.StartNew(() =>
            {
                Invoke(new Action(() =>
                  {
                      BaseDocument document = docManager.GetDocument(_xtrForm);
                      if (document != null)
                          tbvMain.Controller.Activate(document);
                      else
                      {
                          _xtrForm.Text = _xtrForm.Text;
                          _xtrForm.MdiParent = this;
                          _xtrForm.Show();
                      }
                  }));
            });
            clsGeneral.CloseWaitForm();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            LoadDataForm();
        }

        private void LoadDataForm()
        {
            ribbon.Hide();
            ribbonStatusBar.Hide();

            #region Kiểm tra kết nối

            clsGeneral.CallWaitForm(this);
            string _sName, _sDatabase, _sUser, _sPass;
            bool _wAu;
            _wAu = Properties.Settings.Default.IsWindowAuthentication;
            _sName = clsGeneral.Decrypt(Properties.Settings.Default.ServerName);
            _sDatabase = clsGeneral.Decrypt(Properties.Settings.Default.DatabaseName);
            _sUser = clsGeneral.Decrypt(Properties.Settings.Default.UserName);
            _sPass = clsGeneral.Decrypt(Properties.Settings.Default.Password);

            //string _conString = "";
            //if (_wAu)
            //    _conString = "data source={0};initial catalog={1};Integrated Security={2};MultipleActiveResultSets=True;App=EntityFramework";
            //else
            //    _conString = "data source={0};initial catalog={1};Integrated Security={2};user id={3};password={4};MultipleActiveResultSets=True;App=EntityFramework";
            //EntityModel.Module.dbConnectString = string.Format(_conString, _sName, _sDatabase, _wAu, _sUser, _sPass);

            //try
            //{
            //    //EntityModel.Module.InitDefaultData();
            //}
            //catch (Exception ex)
            //{
            //    clsGeneral.CloseWaitForm();
            //    clsGeneral.showErrorException(ex, "Exception");
            //    if (clsGeneral.showConfirmMessage("Thiết lập kết nối máy chủ"))
            //    {
            //        frmDatabase frm = new frmDatabase();
            //        frm.ShowDialog();
            //        loadDataForm();
            //    }
            //    else
            //    {
            //        Application.Exit();
            //    }
            //    return;
            //}
            //clsGeneral.CloseWaitForm();
            #endregion

            #region Thiết lập thông tin
            if (clsGeneral.curPersonnel.KeyID == 0)
            {
                frmLogin frm = new frmLogin();
                if (frm.ShowDialog() == DialogResult.Cancel || clsGeneral.curPersonnel == null)
                {
                    Application.Exit();
                    return;
                }
                else
                {
                    if (Properties.Settings.Default.CurrentCulture.Equals("VN"))
                    {
                        System.Globalization.CultureInfo enCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US");
                        System.Threading.Thread.CurrentThread.CurrentCulture = System.Threading.Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.CreateSpecificCulture("vi-VN");
                        System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat = enCulture.NumberFormat;
                    }

                    clsGeneral.CallWaitForm(this);
                    clsCallForm.InitFormCollection();
                    clsInitData.TaiDuLieu();
                    bsiComputerName.Caption = "PC: " + Properties.Settings.Default.ComputerName;
                    bsiDatabaseName.Caption = "Cơ sở dữ liệu: " + clsGeneral.Decrypt(Properties.Settings.Default.DatabaseName);
                    bsiNhanVien.Caption = clsGeneral.curPersonnel.FullName;
                    bsiClock.Caption = "Công ty phần mềm Tin Tấn © 2017";
                    addItemClick();
                    clsEntity.UpdateFeatures();
                    ribbon.Show();
                    ribbonStatusBar.Show();
                    clsGeneral.CloseWaitForm();
                }
            }
            #endregion
        }

        private async void addItemClick()
        {
            // Duyệt từng page trong ribbon
            try
            {
                foreach (RibbonPage page in ribbon.Pages)
                {
                    page.Visible = false;
                    page.Text = clsEntity.get_Caption(page, page.Name, ribbon.Name, page.Text, 0);

                    foreach (RibbonPageGroup group in page.Groups)
                    {
                        group.Visible = false;
                        group.Text = clsEntity.get_Caption(group, group.Name, page.Name, group.Text, 1);
                        foreach (var item in group.ItemLinks)
                        {
                            if (item is BarButtonItemLink)
                            {
                                BarButtonItemLink bbi = item as BarButtonItemLink;
                                if (bbi.Item.Name.StartsWith("bbi"))
                                {
                                    bbi.Visible = true;
                                    page.Visible = group.Visible = true;
                                }
                                else if (bbi.Item.Name.StartsWith("frm"))
                                {
                                    bbi.Item.Caption = clsEntity.get_Caption(bbi, bbi.Item.Name, group.Name, bbi.Item.Caption, 2);
                                    bbi.Visible = await clsEntity.Check_Role(clsGeneral.curAccount, bbi.Item.Name);
                                    if (bbi.Visible)
                                    {
                                        bbi.Item.ItemClick += bt_ItemClick;
                                        page.Visible = group.Visible = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch { }
        }

        private void bt_ItemClick(object sender, ItemClickEventArgs e)
        {
            //clsGeneral.CallWaitForm(this);
            try
            {
                XtraForm frm = clsCallForm.CreateNewForm(e.Item.Name);
                if (frm != null)
                    addDocument(frm);
            }
            catch { }
            //clsGeneral.CloseWaitForm();
        }

        private void bsiNhanVien_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (clsGeneral.showConfirmMessage("Xác nhận đăng xuất khỏi hệ thống"))
            {
                docManager.View.Controller.CloseAll();
                this.Dispose();
                Application.Restart();
            }
        }

        private void bbiChangePassword_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (frmChangePassword _frm = new frmChangePassword())
            {
                _frm.ShowDialog();
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default["SkinName"] = UserLookAndFeel.Default.SkinName;
            Properties.Settings.Default.Save();
        }

        private void bbiInfomation_ItemClick(object sender, ItemClickEventArgs e)
        {
            using (frmConfiguration _frm = new frmConfiguration())
            {
                _frm.ShowDialog();
            }
        }
    }
}