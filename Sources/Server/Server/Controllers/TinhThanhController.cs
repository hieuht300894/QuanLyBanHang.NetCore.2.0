﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EntityModel.DataModel;
using Server.Service;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Server.Controllers
{
    public class TinhThanhController : BaseController<eTinhThanh>
    {
        public TinhThanhController(IRepositoryCollection Collection) : base(Collection)
        {
        }
    }
}
