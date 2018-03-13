using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FullStackApp.Tokens;
using Microsoft.IdentityModel.Tokens;
using System.Web.Http.Cors;
using FullStackApp.Models;
using FullStackApp.Persistance;
using FullStackApp.Services;

namespace FullStackApp.Controllers
{
    [EnableCors("http://localhost:4200", "*", "*")]
    public class JWTTokenController : ApiController
    {
        private UnitOfWork workUnit = null;
        public UserService usrService { get; set; }

        public JWTTokenController()
        {
            workUnit = new UnitOfWork(new ApplicationDbContext());
            usrService = new UserService(workUnit);
        }
        [AllowAnonymous]
        [Route("api/tokenUtils/createnewtoken")]
        [HttpPost]
        public IHttpActionResult CreateNewToken(AppUser userDetails)
        {
            //Consider right now that authentication is successful.
            JwtTokenBuilder tokenBuilder = new JwtTokenBuilder();
            SymmetricSecurityKey key = JwtSecurityKey.Create("abcdefghijklmnopqrstuvwxyz");
            tokenBuilder.AddExpiry(60);
            tokenBuilder.AddSubject("FullStackApp");
            tokenBuilder.AddIssuer("Veracity");
            tokenBuilder.AddAudience("FullStackAppUsers");
            tokenBuilder.AddClaim("userName", userDetails.UserName);
            tokenBuilder.AddClaim("password", userDetails.Password);
            tokenBuilder.AddSecurityKey(key);

            JwtToken newToken = tokenBuilder.Build();
            return Ok(newToken);
        }

        [Route("api/tokenUtils/authenticate")]
        [HttpGet]
        [AuthenticationFilter]
        public IHttpActionResult GetAuthenticateResponse()
        {
            string status = "Successfully Authenticated!";
            return Ok(status);
        }
    }
}
