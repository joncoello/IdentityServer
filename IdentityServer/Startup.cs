using IdentityServer.Models;
using IdentityServer3.AccessTokenValidation;
using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Services.InMemory;
using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

[assembly: OwinStartup(typeof(IdentityServer.Startup))]

namespace IdentityServer {
    public class Startup {

        public void Configuration(IAppBuilder app) {

            // auth
            var options = new IdentityServerOptions {
                Factory = new IdentityServerServiceFactory()
                           .UseInMemoryClients(Clients.Get())
                           .UseInMemoryScopes(Scopes.Get())
                           .UseInMemoryUsers(Users.Get()),

                RequireSsl = false
            };

            app.UseIdentityServer(options);

            // accept access tokens from identityserver and require a scope of 'api1'
            app.UseIdentityServerBearerTokenAuthentication(new IdentityServerBearerTokenAuthenticationOptions {
                Authority = "http://localhost:19586",
                ValidationMode = ValidationMode.ValidationEndpoint,

                RequiredScopes = new[] { "api1" }
            });

            // configure web api
            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();

            // require authentication for all controllers
            config.Filters.Add(new AuthorizeAttribute());

            app.UseWebApi(config);

        }
        
    }
}