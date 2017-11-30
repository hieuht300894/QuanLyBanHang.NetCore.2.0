using Client.Module;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Docking2010.Views;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraBars.Ribbon.Internal;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client.GUI.Common
{
    public partial class frmMain : XtraForm
    {
        #region Variable

        #endregion

        #region Form
        public frmMain()
        {
            InitializeComponent();

            Load -= FrmMain_Load;
            Load += FrmMain_Load;
        }
        #endregion

        #region Method
        private void LoadDataForm()
        {
            AddItemClick();
        }
        private async void AddDocument(XtraForm _xtrForm)
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
        private async void AddItemClick()
        {
            // Duyệt từng page trong ribbon
            try
            {
                await Task.Factory.StartNew(() => { });

                foreach (RibbonPage page in rcMain.Pages)
                {
                    foreach (RibbonPageGroup group in page.Groups)
                    {
                        foreach (RibbonPageGroupItemLink item in group.ItemLinks)
                        {
                            if (item is BarButtonItemLink)
                            {
                                BarButtonItemLink bbi = item as BarButtonItemLink;
                                if (bbi.Item.Name.StartsWith("frm"))
                                {
                                    bbi.Item.ItemClick += bbi_ItemClick;
                                }
                            }
                        }
                    }
                }
            }
            catch { }
        }
        #endregion

        #region Event
        private void FrmMain_Load(object sender, EventArgs e)
        {
            LoadDataForm();
        }
        private void bbi_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                XtraForm frm = clsCallForm.CreateNewForm(e.Item.Name);
                if (frm != null)
                    AddDocument(frm);
            }
            catch { }
        }
        #endregion



    }
}
