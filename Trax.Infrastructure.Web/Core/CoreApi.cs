using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Trax.Framework.Generic.Logger;
using Trax.Infrastructure.Web.Client;
using Trax.Models.Generic.Api.Response;
using Trax.Models.Generic.OperationResult;

namespace Trax.Infrastructure.Web.Core
{
    public class CoreApi
    {
        private Logger _Logger;
        public CoreApi(Logger logger)
        {
            this._Logger = logger;
        }
        public ClientesListResponseDTO GetListClientes(string EndPoint, ClaimsIdentity Claims)
        {
            ClientesListResponseDTO Response = new ClientesListResponseDTO();
            try
            {
                ClientRest _Client = new ClientRest(this._Logger);
                _Client.SetEndPoint(EndPoint);
                if (!(Response.Result = _Client.SetToken(Claims)).IsOK())
                    return Response;
                Response = _Client.GetListClientes();
            }
            catch (Exception ex)
            {
                this._Logger.LogError(ex);
                Response.Result.SetStatusCode(OperationResult.StatusCodesEnum.INTERNAL_SERVER_ERROR);
                Response.Result.AddException(ex);
            }
            return Response;
        }
    }
}
