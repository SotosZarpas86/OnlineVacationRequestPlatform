using Newtonsoft.Json;
using OnlineVacationRequestPlatform.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OnlineVacationRequestPlatform.Web.Services
{
    public class LoginService
    {
        public HttpClient Client { get; }
        public LoginService(HttpClient client)
        {
            client.BaseAddress = new Uri("https://localhost:44360/");
            Client = client;
        }

        public async Task<LoggedInUserModel> LoginEmployeeAsync(LoginViewModel login)
        {
            var user = new LoggedInUserModel();
            try
            {
                var jsonPayload = System.Text.Json.JsonSerializer.Serialize(login);
                var body = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
                var response = await Client.PostAsync("api/login/Employee", body);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    user = System.Text.Json.JsonSerializer.Deserialize<LoggedInUserModel>(jsonResponse, options);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return user;
        }

        public async Task<LoggedInUserModel> LoginAdminAsync(LoginViewModel login)
        {
            var user = new LoggedInUserModel();
            try
            {
                var jsonPayload = System.Text.Json.JsonSerializer.Serialize(login);
                var body = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
                var response = await Client.PostAsync("api/login/Admin", body);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    user = System.Text.Json.JsonSerializer.Deserialize<LoggedInUserModel>(jsonResponse, options);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return user;
        }
    }
}
