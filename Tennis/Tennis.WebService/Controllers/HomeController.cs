using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Tennis.WebService.Controllers
{
    public class HomeController : ApiController
    {
        [HttpGet]
        public void EmptyAction()
        {
        }
    }
}
