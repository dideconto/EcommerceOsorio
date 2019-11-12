using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace EcommerceOsorio.Utils
{
    public class UtilsSession
    {
        private readonly IHttpContextAccessor _http;
        private readonly string carrinhoId = "CARRINHO_ID";
        public UtilsSession(IHttpContextAccessor http)
        {
            _http = http;
        }
        public string RetornarCarrinhoId()
        {
            if(_http.HttpContext.Session.GetString(carrinhoId) == null)
            {
                _http.HttpContext.Session.SetString(carrinhoId, Guid.NewGuid().ToString());
            }
            return _http.HttpContext.Session.GetString(carrinhoId);
        }
    }
}
