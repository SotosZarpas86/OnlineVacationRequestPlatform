using OnlineVacationRequestPlatform.Web.Models;
using OnlineVacationRequestPlatform.Web.Utilities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OnlineVacationRequestPlatform.Web.Services
{
    public class VacationRequestService
    {
        public HttpClient Client { get; }
        public VacationRequestService(HttpClient client)
        {
            client.BaseAddress = new Uri("https://localhost:44360/");
            Client = client;
        }

        public async Task<List<VacationApplicationViewModel>> GetVacationRequestsByUserAsync(Guid userId)
        {
            var vacationRequests = new List<VacationApplicationViewModel>();
            try
            {
                var response = await Client.GetAsync("api/vacationrequest/GetAll?id=" + userId);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    vacationRequests = JsonSerializer.Deserialize<List<VacationApplicationViewModel>>(jsonResponse, options);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return vacationRequests;
        }

        public async Task<VacationApplicationViewModel> GetVacationRequestAsync(Guid vacationRequestId)
        {
            VacationApplicationViewModel vacationRequest = null;
            try
            {
                var response = await Client.GetAsync("api/vacationrequest/Get?id=" + vacationRequestId);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    vacationRequest = JsonSerializer.Deserialize<VacationApplicationViewModel>(jsonResponse, options);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return vacationRequest;
        }

        public async Task<VacationApplicationViewModel> SaveVacationRequestAsync(VacationApplicationViewModel vacationRequest)
        {
            try
            {
                var jsonPayload = JsonSerializer.Serialize(vacationRequest);
                var body = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
                var response = await Client.PostAsync("api/vacationrequest/Create", body);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    vacationRequest = JsonSerializer.Deserialize<VacationApplicationViewModel>(jsonResponse, options);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return vacationRequest;
        }

        public async Task<bool> UpdateVacationRequestStatusAsync(VacationApplicationStatusViewModel vacationApplicationStatus)
        {
            var updateStatus = false;
            try
            {
                var jsonPayload = JsonSerializer.Serialize(vacationApplicationStatus);
                var body = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
                var response = await Client.PutAsync("api/vacationrequest/UpdateRequestStatus", body);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    updateStatus = true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return updateStatus;
        }
    }
}
