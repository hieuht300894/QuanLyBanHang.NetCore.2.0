using Client.GUI.Common;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Module
{
    public class clsCallForm
    {
        public static List<FormItem> fList = null;
        public static void InitFormCollection()
        {
            fList = new List<FormItem>();
            try
            {
                System.Reflection.Assembly projectA = System.Reflection.Assembly.GetExecutingAssembly();
                foreach (Type t in projectA.GetTypes())
                {
                    if (t.BaseType == typeof(frmBase))
                    {
                        var emptyCtor = t.GetConstructor(Type.EmptyTypes);
                        if (emptyCtor != null)
                        {
                            var f = (frmBase)emptyCtor.Invoke(new object[] { });
                            fList.Add(new FormItem(f, f.Name));
                        }
                    }
                    if (t.BaseType == typeof(XtraForm))
                    {
                        var emptyCtor = t.GetConstructor(Type.EmptyTypes);
                        if (emptyCtor != null)
                        {
                            var f = (XtraForm)emptyCtor.Invoke(new object[] { });
                            fList.Add(new FormItem(f, f.Name));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsGeneral.showErrorException(ex, "Exception");
            }
        }
        public static XtraForm CreateNewForm(string bbiName)
        {
            if (fList == null)
                return null;
            try { return fList.Find(f => f.BbiName.Equals(bbiName)).xForm; }
            catch { return null; }

        }
    }

    public class FormItem
    {
        XtraForm _frmMain;
        string _bbiName;

        public FormItem(XtraForm _frmMain, string _bbiName)
        {
            this._frmMain = _frmMain;
            this._bbiName = _bbiName;
        }

        public string BbiName { get { return _bbiName; } }

        public XtraForm xForm
        {
            get
            {
                XtraForm frm = _frmMain;
                try
                {

                    if (_frmMain == null || !_frmMain.IsHandleCreated)
                        frm = (XtraForm)Activator.CreateInstance(_frmMain.GetType());
                    return frm;
                }
                catch { return null; }

            }
        }
    }
}
