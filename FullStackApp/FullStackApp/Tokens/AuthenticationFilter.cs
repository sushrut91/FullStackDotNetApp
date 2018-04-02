using FullStackApp.Controllers;
using FullStackApp.Persistance;
using FullStackApp.Services;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace FullStackApp.Tokens
{
    public class AuthenticationFilter:ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if(actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = new System.Net.Http.
                    HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
            }
            else
            {
                string token = actionContext.Request.Headers.Authorization.Parameter;
                JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                JwtSecurityToken tokenDecrypt = handler.ReadJwtToken(token);
                if(!token.Equals("undefined"))
                {
                    var usrName = tokenDecrypt.Claims.Where(c => c.Type == "userName").FirstOrDefault();
                    var passwd = tokenDecrypt.Claims.Where(c => c.Type == "password").FirstOrDefault();

                    if (usrName == null || passwd == null)
                        actionContext.Response = new System.
                            Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
                    else
                    {
                        JWTTokenController tokenController = new JWTTokenController();
                        bool exists = tokenController.usrService.
                            CheckUserExistanceInDB(usrName.Value, passwd.Value);
                        if (!exists)
                            actionContext.Response = new System.
                          Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
                    }

                }
            }
        }
    }
}