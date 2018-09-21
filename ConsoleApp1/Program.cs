using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.Net.Http;

namespace ConsoleApp1
{
    class Program
    {
        static HttpClient client = new HttpClient();

        static void Main(string[] args)
        {
            Anvandare.Builder builder = new Anvandare.Builder("Daniel","Nordkvissdf");
            builder.PersonnummerMethod("1233445556");
            Anvandare n = builder.Build();


            Console.WriteLine(n.Efternamn + n.Fornamn + n.Personnummer);

            Anvandare nutritionFacts = new Anvandare();
            Console.WriteLine(nutritionFacts.Personnummer); 



            /**

            Item todoItem = new Item();
            todoItem.Id = 1;
            todoItem.Name = "Name";
            todoItem.IsComplete = true;
            Console.WriteLine(todoItem.ToString());
           
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", GenerateToken());

            client.BaseAddress = new Uri("https://localhost:44352");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
           
            Console.WriteLine(GenerateToken());
            
            TestingToken().Wait();
            TestingToken2NoAsync();
          **/

        }

        static async Task TestingToken()
        {
                var stringTask = client.GetStringAsync("api/todo/isalive");
            //   Task<System.IO.Stream> t =  client.GetStreamAsync("https://api.github.com/orgs/dotnet/repos");
            Console.WriteLine("1!");
            Console.WriteLine("2!");
            TestingToken2NoAsync();
            Console.WriteLine("3!");
            Console.WriteLine("4!");

            var msg = await stringTask;
            Console.Write(msg);
           // Console.WriteLine("test!");
           
            
        }

    

        static void TestingToken2NoAsync() {
            var stringTask = client.GetStringAsync("api/todo/isalive");
            //   Task<System.IO.Stream> t =  client.GetStreamAsync("https://api.github.com/orgs/dotnet/repos");
            Console.Write(stringTask);
            Console.WriteLine("test2!");
        }

        private static string GenerateToken()
        {
            var handler = new JwtSecurityTokenHandler();

            var claims = new[]
            {
                 new Claim("test", "test")
             };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("test123456789#########"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
             issuer: "yourdomain.com",
             audience: "yourdomain.com",
             claims: claims,
             expires: DateTime.Now.AddMinutes(30),
             signingCredentials: creds);

            return handler.WriteToken(token);
        }

      
    }
}
