using EntityModel.DataModel;
using Microsoft.AspNetCore.Mvc;
using Server.Service;
using System.Collections.Generic;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Server.Controllers
{
    public class PersonnelController : BaseController<xPersonnel>
    {
        public PersonnelController(IRepositoryCollection Collection) : base(Collection)
        {
        }

        [Route("Test")]
        public IEnumerable<xPersonnel> Test()
        {
            Instance.Context = new Model.zModel();
            return new List<xPersonnel>();
        }
    }
}
