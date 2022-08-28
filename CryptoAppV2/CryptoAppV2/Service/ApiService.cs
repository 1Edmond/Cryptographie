using CryptoAppV2.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAppV2.Service
{
    public static class ApiService
    {
        public static string UrlApi {
            get => "https://cryptographieapied.herokuapp.com/";
            }
        public static HttpClient Client { get; set; } = new HttpClient();
        public static  Task<ApiResult<Inscription>> GetInscription()
        {
            string Url = $"{UrlApi}inscriptions/{UserSettings.UserInscriptionId}";
            var result = Client.GetAsync(new Uri(Url));
            if (result.Result.IsSuccessStatusCode)
            {
                string temp = result.Result.Content.ReadAsStringAsync().Result;
                return Task.FromResult(JsonConvert.DeserializeObject<ApiResult<Inscription>>(temp));
            }
            return Task.FromResult(new ApiResult<Inscription>()
            {
                StatusCode = "505",
                Message = "Un problème est survenue, réessayez plus tard."
            });
        }
        public static async Task<ApiResult<Inscription>> ExistInscription(string contact)
        {
            string Url = $"{UrlApi}inscriptions/exist/{contact}";
            var result = await Client.GetAsync(new Uri(Url));
            if (result.IsSuccessStatusCode)
            {
                string temp = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ApiResult<Inscription>>(temp);
            }
            return new ApiResult<Inscription>()
            {
                StatusCode = "505",
                Message = "Un problème est survenue, réessayez plus tard."
            };
        }
        public static async Task<ApiResult<Payement>> AddPayement(Payement payement)
        {
            string Url = $"{UrlApi}payements/add";
            string json = JsonConvert.SerializeObject(payement);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
            response = await Client.PostAsync(Url, content);
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiResult<Payement>>(response.Content.ReadAsStringAsync().Result);
            return new ApiResult<Payement>()
            {
                StatusCode = "505",
                Message = "Un problème est survenue, réessayez plus tard."
            };
        }
        public static async Task<ApiResult<Inscription>> AddInscription(Inscription inscription)
        {
            string Url = $"{UrlApi}inscriptions/add";
            string json = JsonConvert.SerializeObject(inscription);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
                response = await Client.PostAsync(Url, content);
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiResult<Inscription>>(response.Content.ReadAsStringAsync().Result);
            return new ApiResult<Inscription>()
            {
                StatusCode = "505",
                Message = "Un problème est survenue, réessayez plus tard."
            };
        }
    }
}
