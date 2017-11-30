﻿using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Client.Module
{
    public class ModuleHelper
    {
    }

    public class clsCallForm
    {
        public static List<FormItem> fList = null;
        public async static void InitFormCollection()
        {
            fList = new List<FormItem>();
            try
            {
                await Task.Factory.StartNew(() => { });

                System.Reflection.Assembly projectA = System.Reflection.Assembly.GetExecutingAssembly();
                foreach (Type t in projectA.GetTypes())
                {
                    if (t.BaseType == typeof(XtraForm))
                    {
                        var emptyCtor = t.GetConstructor(Type.EmptyTypes);
                        if (emptyCtor != null)
                        {
                            var f = (XtraForm)emptyCtor.Invoke(new object[] { });
                            fList.Add(new FormItem(f.Name, f));
                        }
                    }
                }
            }
            catch { }
        }
        public static XtraForm CreateNewForm(string bbiName)
        {
            if (fList == null)
                return null;
            try { return fList.Find(f => f.BbiName.Equals(bbiName)).CForm; }
            catch { return null; }

        }
    }

    public class FormItem
    {
        string _bbiName;
        public string BbiName
        {
            get { return _bbiName; }
            set { _bbiName = value; }
        }
        XtraForm _cForm;
        Type _fType;

        public FormItem(string bbiName, XtraForm frmIn)
        {
            this._bbiName = bbiName;
            this._cForm = frmIn;
            this._fType = frmIn.GetType();
        }
        public XtraForm CForm
        {
            get
            {
                if (_cForm == null || !_cForm.IsHandleCreated)
                {
                    _cForm = (XtraForm)Activator.CreateInstance(_fType);
                }
                return _cForm;
            }
            set { _cForm = value; }
        }
    }

    public static class clsGeneral
    {
        #region Enum
        public enum eFormType { Default = 0, List, Add, Edit, Delete, Print };
        #endregion

        #region User
        public static xPersonnel curPersonnel = new xPersonnel();
        public static xAgency curAgency = new xAgency();
        public static xAccount curAccount = new xAccount();
        public static xUserFeature curUserFeature = new xUserFeature();
        #endregion

        #region Mã hóa và giải mã
        static readonly string PasswordHash = "P@@Sw0rd";
        static readonly string SaltKey = "S@LT&KEY";
        static readonly string VIKey = "@1B2c3D4e5F6g7H8";
        /// <summary>
        /// Mã hóa
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public static string Encrypt(string plainText)
        {
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.Zeros };
            var encryptor = symmetricKey.CreateEncryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));

            byte[] cipherTextBytes;

            using (var memoryStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                    cryptoStream.FlushFinalBlock();
                    cipherTextBytes = memoryStream.ToArray();
                    cryptoStream.Close();
                }
                memoryStream.Close();
            }
            return Convert.ToBase64String(cipherTextBytes);
        }

        /// <summary>
        /// Giải mã
        /// </summary>
        /// <param name="encryptedText"></param>
        /// <returns></returns>
        public static string Decrypt(string encryptedText)
        {
            try
            {
                byte[] cipherTextBytes = Convert.FromBase64String(encryptedText);
                byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
                var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.None };

                var decryptor = symmetricKey.CreateDecryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));
                var memoryStream = new MemoryStream(cipherTextBytes);
                var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
                byte[] plainTextBytes = new byte[cipherTextBytes.Length];

                int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                memoryStream.Close();
                cryptoStream.Close();
                return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount).TrimEnd("\0".ToCharArray());
            }
            catch { return string.Empty; }
        }

        #endregion

        #region Image
        public static byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            try
            {
                MemoryStream ms = new MemoryStream();
                imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                return ms.ToArray();
            }
            catch { return null; }
        }

        public static System.Drawing.Image byteArrayToImage(byte[] byteArrayIn)
        {
            try
            {
                MemoryStream ms = new MemoryStream(byteArrayIn);
                System.Drawing.Image returnImage = System.Drawing.Image.FromStream(ms);
                return returnImage;
            }
            catch { return null; }
        }
        #endregion

        #region Hiện thị thông báo
        static GUI.Common.frmMessage _frmMessage;
        static GUI.Common.frmConfirm _frmConfirm;
        static GUI.Common.frmError _frmError;

        /// <summary>
        /// Dùng để thông báo đến người dùng
        /// </summary>
        /// <param name="_message"></param>
        public static void showMessage(string _message)
        {
            if (_frmMessage == null)
                _frmMessage = new GUI.Common.frmMessage();
            if (!_frmMessage.IsHandleCreated)
            {
                _frmMessage.lblMessage.Text = _message;
                _frmMessage.ShowDialog();
            }
        }

        /// <summary>
        /// Dùng để xác nhận thông tin với người dùng
        /// </summary>
        /// <param name="_message"></param>
        /// <returns></returns>
        public static bool showConfirmMessage(string _message)
        {
            if (_frmConfirm == null)
                _frmConfirm = new GUI.Common.frmConfirm();
            if (!_frmConfirm.IsHandleCreated)
            {
                _frmConfirm.lblMessage.Text = _message;
                if (_frmConfirm.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                    return false;
            }
            return true;
        }
        /// <summary>
        /// Dùng để hiển thị Exception
        /// </summary>
        /// <param name="_ex"></param>
        /// <param name="_message"></param>
        public static void showErrorException(Exception _ex, string _message = "Thông báo")
        {
            if (_frmError == null) _frmError = new GUI.Common.frmError();
            if (!_frmError.IsHandleCreated)
            {
                _frmError.lblMessage.Text = _message;
                _frmError.meError.Text = _ex.ToString();
                _frmError.ShowDialog();
            }
        }
        #endregion

        #region Kiểm tra Space
        /// <summary>
        /// Kiểm tra chuổi vừa tạo có khoảng trắng hay không.
        /// Nếu không thì return True. Ngược lại, return False.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool isSpace(string input)
        {
            return input.Contains(" ");
        }
        #endregion

        #region Kiểm tra Unicode
        /// <summary>
        /// Kiểm tra chuổi vừa tạo có phải là kiểu Unicode hay không.
        /// Nếu là Unicode thì return True. Ngược lại, return False.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public static bool isUnicode(string input)
        {
            try
            {
                var asciiBytesCount = Encoding.ASCII.GetByteCount(input);
                var unicodBytesCount = Encoding.UTF8.GetByteCount(input);
                return asciiBytesCount != unicodBytesCount;
            }
            catch (Exception ex)
            {
                showErrorException(ex, "Có lỗi xảy ra khi kiểm tra ký tự Unicode. \r\nKý tự vừa nhập là: " + input);
                return false;
            }
        }

        public static string strNoSign(string strIn)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = strIn.Normalize(NormalizationForm.FormD);
            string nameNosign = regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
            return nameNosign.ToLower();
        }

        #endregion

        #region Kiểm tra email
        public static bool CheckEmail(string strIn)
        {
            Regex check = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            return (check.IsMatch(strIn));//Email đúng            
        }
        #endregion

        #region Kiểm tra và tải bản vá lỗi Admin
        /// <summary>
        /// Kiểm tra và tải bản vá lỗi Admin.
        /// </summary>

        public static bool LiveUpdate()
        {
            bool bRe = false;
            try
            {
                //if (string.IsNullOrEmpty(Properties.Settings.Default.ServicePack))
                //{
                //    return false;
                //}

                //Int32 currentVersion = Int32.Parse(Properties.Settings.Default.ServicePack);
                //string _ftp = Decrypt(Properties.Settings.Default.ftp);
                //string _ftpUser = Decrypt(Properties.Settings.Default.ftp_user);
                //string _ftpPW = Decrypt(Properties.Settings.Default.ftp_pw);
                //string _project = Decrypt(Properties.Settings.Default.ftp_proj);
                //string ftp_dir = ("ftp://" + (_ftp + ("/" + ("LiveUpdate/" + _project))));
                //FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftp_dir));
                //request.Method = WebRequestMethods.Ftp.ListDirectory;
                //request.UseBinary = true;
                //request.Credentials = new NetworkCredential(_ftpUser, _ftpPW);
                //FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                //StreamReader reader = new StreamReader(response.GetResponseStream());
                //string files = "";
                //while (!reader.EndOfStream)
                //{
                //    //Application.DoEvents();
                //    files = reader.ReadLine();
                //}

                //if (((files == null)
                //            || (string.IsNullOrEmpty(files)
                //            || (files == ""))))
                //{
                //    return false;
                //}

                //string fName = Path.GetFileName(files);
                //Int32 version = Int32.Parse(fName.Split((char)'.')[0].Substring(2));
                //if ((version > currentVersion))
                //{
                //    Process p = new Process();
                //    if ((Environment.OSVersion.Version.Major >= 6))
                //    {
                //        p.StartInfo.Verb = "runas";
                //    }

                //    p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                //    p.StartInfo.FileName = (Application.StartupPath + "\\LiveUpdate.exe");
                //    p.StartInfo.UseShellExecute = false;
                //    p.StartInfo.RedirectStandardOutput = true;
                //    p.StartInfo.RedirectStandardError = true;
                //    if (p.Start())
                //    {
                //        Properties.Settings.Default.ServicePack = version.ToString();
                //        Properties.Settings.Default.Save();
                //        p.WaitForExit();
                //        bRe = true;
                //    }
                //}

            }
            catch (Exception ex)
            {
                showErrorException(ex, "Không thể cập nhật bản vá lỗi cho phần mềm. Vui lòng liên hệ với kỹ thuật để được hỗ trợ.");

            }
            return bRe;
        }

        public static bool CheckConnectToFTP(string ftpUrl, string ftpUsername, string ftpPws)
        {
            try
            {
                ftpUrl = string.Format("ftp://{0}", ftpUrl);
                FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpUrl));

                request.Credentials = new NetworkCredential(ftpUsername, ftpPws);
                request.Method = WebRequestMethods.Ftp.ListDirectory;
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                return true;
            }
            catch (WebException)
            {
                return false;
            }

        }
        #endregion

        #region Save layout, restore layout

        public static string UserLayoutPath = @"Layout\UserLayout";
        public static string GridLayoutPath = @"Layout\GridLayout";

        /// <summary>
        /// Lưu layout của gridcontrol
        /// </summary>
        /// <param name="gctMain"></param>
        /// <param name="FormName"></param>
        public static void SaveLayoutGridControl(string FormName, DevExpress.XtraGrid.GridControl gctMain)
        {
            if (gctMain == null) return;

            if (!Directory.Exists(GridLayoutPath))
                Directory.CreateDirectory(GridLayoutPath);

            string path;
            path = GridLayoutPath + @"\" + FormName + "_" + gctMain.Name + ".xml";
            if (File.Exists(path))
                File.Delete(path);
            gctMain.DefaultView.SaveLayoutToXml(path);

            //DevExpress.XtraGrid.Views.Grid.GridView gvMain = (DevExpress.XtraGrid.Views.Grid.GridView)gctMain.MainView;
            //path = GridLayoutPath + @"\" + FormName + "_" + gvMain.Name + ".xml";
            //if (File.Exists(path))
            //    File.Delete(path);
            //gctMain.DefaultView.SaveLayoutToXml(path);
            //gvMain.SaveLayoutToXml(GridLayoutPath + @"\" + FormName + "_" + gvMain.Name + ".xml");
        }

        public static void SaveLayoutForm(string FormName, DevExpress.XtraLayout.LayoutControl lctMain)
        {
            if (lctMain == null) return;
            if (!Directory.Exists(UserLayoutPath))
                Directory.CreateDirectory(UserLayoutPath);

            string path;
            path = GridLayoutPath + @"\" + FormName + "_" + lctMain.Name + ".xml";
            if (File.Exists(path))
                File.Delete(path);
            lctMain.SaveLayoutToXml(path);
        }

        /// <summary>
        /// Restore layout của gridcontrol
        /// </summary>
        /// <param name="FormName"></param>
        /// <param name="gctMain"></param>
        public static void RestoreLayoutGridControl(string FormName, DevExpress.XtraGrid.GridControl gctMain)
        {
            if (gctMain == null) return;

            string path;
            path = GridLayoutPath + @"\" + FormName + "_" + gctMain.Name + ".xml";
            if (File.Exists(path))
                gctMain.DefaultView.RestoreLayoutFromXml(path);

            //DevExpress.XtraGrid.Views.Grid.GridView gvMain = (DevExpress.XtraGrid.Views.Grid.GridView)gctMain.MainView;
            //path = GridLayoutPath + @"\" + FormName + "_" + gvMain.Name + ".xml";
            //if (File.Exists(path))
            //    gvMain.RestoreLayoutFromXml(path);
        }

        /// <summary>
        /// Restore layout của Layout control
        /// </summary>
        /// <param name="FormName"></param>
        /// <param name="lctMain"></param>
        public static void RestoreLayoutForm(string FormName, DevExpress.XtraLayout.LayoutControl lctMain)
        {
            if (!Directory.Exists(UserLayoutPath))
                Directory.CreateDirectory(UserLayoutPath);
            string path = UserLayoutPath + @"\" + FormName + ".xml";
            if (File.Exists(path))
                lctMain.RestoreLayoutFromXml(path);
        }

        #endregion

        #region Waiting Form
        public static SplashScreenManager ssmWait = null;
        public static void SetCaptionWaitForm(string Caption = "", string Desctiption = "")
        {
            try
            {
                if (ssmWait != null && ssmWait.IsSplashFormVisible)
                {
                    if (!string.IsNullOrEmpty(Caption))
                        ssmWait.SetWaitFormCaption(Caption);
                    if (!string.IsNullOrEmpty(Desctiption))
                        ssmWait.SetWaitFormDescription(Desctiption);
                }
            }
            catch { }
        }

        public static void CallWaitForm(XtraForm frmParent)
        {
            try
            {
                if (ssmWait == null || !ssmWait.IsSplashFormVisible)
                {
                    ssmWait = new SplashScreenManager(frmParent, typeof(GUI.Common.frmWaiting), true, true);
                    ssmWait.ShowWaitForm();
                    SetCaptionWaitForm("Vui lòng chờ!", "Đang xử lý...");
                }
            }
            catch { }
        }

        public static void CloseWaitForm()
        {
            try
            {
                ssmWait.CloseWaitForm();
                ssmWait = null;
            }
            catch { }
        }
        #endregion

        #region XMl
        public static XmlDocument StringToXml(this string oSource)
        {
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(oSource);
            return xml;
        }
        public static string SerializeXML<T>(this List<T> oSource)
        {
            if (oSource == null) return string.Empty;

            XmlSerializer xmlSerial = new XmlSerializer(typeof(List<T>));
            using (StringWriter strWriter = new StringWriter())
            {
                xmlSerial.Serialize(strWriter, oSource);
                return strWriter.ToString();
            }

        }
        public static List<T> DeserializeXML<T>(this string strXML) where T : class, new()
        {
            if (string.IsNullOrEmpty(strXML)) return new List<T>();

            XmlSerializer xmlSerial = new XmlSerializer(typeof(List<T>));
            using (StringReader strReader = new StringReader(strXML))
            {
                return (List<T>)xmlSerial.Deserialize(strReader);
            }
        }
        #endregion
    }
}
