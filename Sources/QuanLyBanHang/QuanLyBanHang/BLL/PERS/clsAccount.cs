﻿using EntityModel.DataModel;
using QuanLyBanHang.BLL.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyBanHang.BLL.PERS
{
    public class clsAccount : clsFunction<xAccount>
    {
        #region Contructor
        protected clsAccount() { }
        public new static clsAccount Instance
        {
            get { return new clsAccount(); }
        }
        #endregion

        public IList<xAccount> SearchAccount(bool IsEnable, int KeyID)
        {
            //db = new aModel();
            //IEnumerable<xAccount> lstTemp = db.xAccount.Where(n => n.IsEnable == IsEnable || n.KeyID == KeyID);
            //List<xAccount> lstResult = lstTemp.ToList();
            //return lstResult;

            return new List<xAccount>();
        }

        public async override Task<bool> AddOrUpdate(xAccount entry)
        {
            //db = new aModel();
            //var tran = db.Database.BeginTransaction();
            //try
            //{
            //    db.xAccount.AddOrUpdate(entry);
            //    db.xPersonnel.Find(entry.KeyID).IsAccount = true;
            //    await db.SaveChangesAsync();
            //    tran.Commit();
            //    return true;
            //}
            //catch (Exception ex)
            //{
            //    tran.Rollback();
            //    clsGeneral.showErrorException(ex, "Lỗi thêm mới");
            //    return false;
            //}

            try
            {
                return await Task.Factory.StartNew(() => { return true; });
            }
            catch
            {
                return false;
            }
        }
    }
}
