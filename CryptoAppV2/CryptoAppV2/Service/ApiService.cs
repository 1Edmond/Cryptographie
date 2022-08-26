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
        public static async Task<ApiResult<Inscription>> GetInscription()
        {
            string Url = $"{UrlApi}inscriptions";
            var result = await Client.GetAsync(new Uri(Url));
            string temp = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ApiResult<Inscription>>(temp);

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
            return new ApiResult<Payement>();
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
             
            return new ApiResult<Inscription>();
        }
    }
}
