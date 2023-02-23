using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Trax.Framework.Generic.Logger;
using Trax.Infrastructure.Web.Core;
using Trax.Models.Generic.Api.Response;

namespace Trax.Frontend.Web.Controllers
{
    public class TiendaOnlineController : Controller
    {
        private Logger _Logger;
        public TiendaOnlineController()
        {
            this._Logger = new Logger(System.Web.Hosting.HostingEnvironment.MapPath("~/" + Properties.Settings.Default.PathLog));
        }
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        //[UserInRole(Application = "MAC.SASE")]
        public JsonResult GetListClientes()
        {
            ClientesListResponseDTO _Response = new ClientesListResponseDTO();
            var Result = new object();
            string _CurrentUserName = System.Web.HttpContext.Current.User.Identity.Name;
            CoreApi _Core = new CoreApi(this._Logger);
            try
            {
                if (!(_Response = _Core.GetListClientes(Properties.Settings.Default.ApiServicesEndPoint, (ClaimsIdentity)System.Web.HttpContext.Current.User.Identity)).Result.IsOK())
                    throw new Exception(string.Join(", ", _Response.Result.Errors.Select(x => x.Message)));
            }
            catch (Exception ex)
            {
                this._Logger.LogText("Error : Usuario : " + _CurrentUserName);
                this._Logger.LogError(ex);
            }
            return Json(new { data = _Response.List }, JsonRequestBehavior.AllowGet);
        }
    }
}