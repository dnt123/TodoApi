using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace ConsoleApp1
{
    class Program
    {
        static System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();

        static void Main(string[] args)
        {

            client.DefaultRequestHeaders.Authorization = System.Net.Http.Headers.AuthenticationHeaderValue.Parse("Bearer");
            string s = GenerateToken();

            client.BaseAddress = new Uri("https://localhost:44352");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            
            Console.WriteLine("Hello World!");
            RunAsync().Wait();
          
           
        }
        static async Task RunAsync()
        {
                // Get the product
                await GetProductAsync();
        }

        static async Task GetProductAsync()
        {
           
         //   System.Net.Http.HttpResponseMessage response = await client.GetAsync("/api/todo");
         //   if (response.IsSuccessStatusCode)

          //  {
                var stringTask = client.GetStringAsync("api/todo");
            Task<System.IO.Stream> t =  client.GetStreamAsync("https://api.github.com/orgs/dotnet/repos");

            

            var msg = await stringTask;
                Console.Write(msg);

               // await response.Content.LoadIntoBufferAsync();
                Console.WriteLine("test!");
            //}
            
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
