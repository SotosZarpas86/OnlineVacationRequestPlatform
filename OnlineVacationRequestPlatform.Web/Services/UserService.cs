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
    public class UserService
    {
        public HttpClient Client { get; }
        public UserService(HttpClient client)
        {
            client.BaseAddress = new Uri("https://localhost:44360/");
            Client = client;
        }

        public async Task<List<UserListViewModel>> GetUserListAsync()
        {
            var user = new List<UserListViewModel>();
            try
            {

                var response = await Client.GetAsync("api/user/GetAll");
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    var result = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<UserListViewModel>>(jsonResponse, options);
                    user = result.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return user;
        }

        public async Task<List<RoleModel>> GetRoleListAsync()
        {
            var roles = new List<RoleModel>();
            try
            {
                var response = await Client.GetAsync("api/role/GetAll");
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    var result = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<RoleModel>>(jsonResponse, options);
                    roles = result.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return roles;
        }

        public async Task<ExtendedUserViewModel> GetUserByIdAsync(Guid userId)
        {
            var user = new ExtendedUserViewModel();
            try
            {
                var response = await Client.GetAsync("api/user/Get?id=" + userId);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    user = System.Text.Json.JsonSerializer.Deserialize<ExtendedUserViewModel>(jsonResponse, options);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return user;
        }

        public async Task<ExtendedUserViewModel> AddUserAsync(ExtendedUserViewModel user)
        {
            try
            {
                var jsonPayload = JsonConvert.SerializeObject(user);
                var body = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
                var response = await Client.PostAsync("api/user/Create", body);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    user = System.Text.Json.JsonSerializer.Deserialize<ExtendedUserViewModel>(jsonResponse, options);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return user;
        }

        public async Task<ExtendedUserViewModel> UpdateUserAsync(ExtendedUserViewModel user)
        {
            try
            {
                var userModel = new UserModel
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Password = user.Password,
                    Email = user.Email,
                    RoleId = user.RoleId,
                    SupervisorId = user.SupervisorId
                };
                var jsonPayload = JsonConvert.SerializeObject(userModel);
                var body = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
                var response = await Client.PutAsync("api/user/Update", body);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    user = System.Text.Json.JsonSerializer.Deserialize<ExtendedUserViewModel>(jsonResponse, options);
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
