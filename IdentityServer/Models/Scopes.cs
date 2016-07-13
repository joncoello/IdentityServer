using IdentityServer3.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IdentityServer.Models {
    static class Scopes {
        public static List<Scope> Get() {
            return new List<Scope>
            {
            new Scope
            {
                Name = "api1"
            }
        };
        }
    }
}