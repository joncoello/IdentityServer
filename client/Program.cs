using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityModel.Client;
using System.Net.Http;

namespace client {

    class Program {

        static void Main(string[] args) {

            // client
            var t = GetClientToken();
            CallApi(t);

            //user
            t = GetUserToken();
            CallApi(t);

            Console.ReadKey();

        }

        static TokenResponse GetClientToken() {
            var client = new TokenClient(
                "http://wkidentityserver.azurewebsites.net/connect/token",
                "silicon",
                "F621F470-9731-4A25-80EF-67A6F7C5F4B8");

            return client.RequestClientCredentialsAsync("api1").Result;
        }

        static void CallApi(TokenResponse response) {
            var client = new HttpClient();
            client.SetBearerToken(response.AccessToken);

            Console.WriteLine(client.GetStringAsync("http://wkidentityserver.azurewebsites.net/test").Result);
        }

        static TokenResponse GetUserToken() {
            var client = new TokenClient(
                "http://wkidentityserver.azurewebsites.net/connect/token",
                "carbon",
                "21B5F798-BE55-42BC-8AA8-0025B903DC3B");

            return client.RequestResourceOwnerPasswordAsync("bob", "secret", "api1").Result;
        }

    }

}
