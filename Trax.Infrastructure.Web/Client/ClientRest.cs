using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Trax.Framework.Generic.Api;
using Trax.Framework.Generic.Logger;
using Trax.Models.ApiServicesWeb.Enum;
using Trax.Models.Generic.Api.Response;
using Trax.Models.Generic.OperationResult;

namespace Trax.Infrastructure.Web.Client
{
    public class ClientRest
    {
        private string Token;
        private string UrlEndPoint;
        private Logger _Logger;
        public ClientRest(Logger logger)
        {
            this.Token = string.Empty;
            this.UrlEndPoint = string.Empty;
            this._Logger = logger;
        }
        public bool HasToken()
        {
            return !string.IsNullOrEmpty(this.Token);
        }
        public bool HasEndPoint()
        {
            return !string.IsNullOrEmpty(this.UrlEndPoint);
        }
        public void SetEndPoint(string UrlEndPoint)
        {
            this.UrlEndPoint = UrlEndPoint;
        }
        public OperationResult SetToken(ClaimsIdentity ClaimsIdentity)
        {
            OperationResult _OperationResult = new OperationResult();
            try
            {
                var _Claim = ClaimsIdentity.FindFirst(x => x.Type == EnumClaims.AccessToken.ToString());
                if (_Claim == null)
                {
                    _OperationResult.SetStatusCode(OperationResult.StatusCodesEnum.UNAUTHORIZED);
                    _OperationResult.AddException(new Exception("No se encontro el Token de Autorización."));
                    return _OperationResult;
                }
                this.Token = _Claim.Value;
                if (string.IsNullOrEmpty(this.Token))
                {
                    _OperationResult.SetStatusCode(OperationResult.StatusCodesEnum.UNAUTHORIZED);
                    _OperationResult.AddException(new Exception("No se encontro el Token de Autorización."));
                    return _OperationResult;
                }
            }
            catch (Exception ex)
            {
                _OperationResult.SetStatusCode(OperationResult.StatusCodesEnum.INTERNAL_SERVER_ERROR);
                _OperationResult.AddException(ex);
            }
            return _OperationResult;
        }
        public OperationResult GetToken(string UserName, string Password)
        {
            OperationResult _OperationResult = new OperationResult();
            MessageFactory _MessageFactory = new MessageFactory(this._Logger);
            var _Response = _MessageFactory.GetToken(this.UrlEndPoint, "/Token", UserName, Password, HttpMethod.Post);
            if (_Response == null)
            {
                _OperationResult.SetStatusCode(OperationResult.StatusCodesEnum.SERVICE_UNAVAILABLE);
                _OperationResult.AddException(new Exception("No se obtuvo respuesta de la API Identity."));
                return _OperationResult;
            }
            this.Token = _Response.AccessToken;
            return _OperationResult;
        }
        public ClientesListResponseDTO GetListClientes()
        {
            var Response = new ClientesListResponseDTO();
            if (!this.HasToken())
            {
                Response.Result.SetStatusCode(OperationResult.StatusCodesEnum.UNAUTHORIZED);
                Response.Result.AddException(new Exception("No se encontro el Token de Autorización."));
                return Response;
            }
            else if (!this.HasEndPoint())
            {
                Response.Result.SetStatusCode(OperationResult.StatusCodesEnum.UNAUTHORIZED);
                Response.Result.AddException(new Exception("Es necesario asignar una UrlEndPoint."));
                return Response;
            }
            //string _Payload = Framework.Generic.Serializer.JsonSerializer.Serialize();
            MessageFactory _MessageFactory = new MessageFactory(this._Logger);
            Response = _MessageFactory.SendRequest<ClientesListResponseDTO>(this.UrlEndPoint, this.Token, "/TiendaWeb/GetListClientes", string.Empty, HttpMethod.Post);
            return Response;
        }
    }
}
