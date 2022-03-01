﻿using KFKWS3_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace KFKWS3_HFT_2021221.GUI
{
    public class RestService
    {
        HttpClient client;

        public RestService(string baseurl = null)
        {
            if(baseurl != null)
            Init(baseurl);
        }

        private void Init(string baseurl)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(baseurl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            try
            {
                client.GetAsync("").GetAwaiter().GetResult();
            }
            catch (HttpRequestException)
            {
                throw new ArgumentException("Endpoint is not available!");
            }
        }

        public List<T> Get<T>(string endpoint)
        {
            List<T> items = new List<T>();
            HttpResponseMessage response = client.GetAsync(endpoint).GetAwaiter().GetResult();
            if (response.IsSuccessStatusCode)
            {
                items = response.Content.ReadAsAsync<List<T>>().GetAwaiter().GetResult();
            }
            return items;
        }

        
        public T GetSingle<T>(string endpoint)
        {
            T item = default(T);
            HttpResponseMessage response = client.GetAsync(endpoint).GetAwaiter().GetResult();
            if (response.IsSuccessStatusCode)
            {
                item = response.Content.ReadAsAsync<T>().GetAwaiter().GetResult();
            }
            return item;
        }

        public T Get<T>(int id, string endpoint)
        {
            T item = default(T);
            HttpResponseMessage response = client.GetAsync($"{endpoint}/{id}").GetAwaiter().GetResult();
            if (response.IsSuccessStatusCode)
            {
                item = response.Content.ReadAsAsync<T>().GetAwaiter().GetResult();
            }
            return item;
        }

        public List<T> GetQuery<T>(string query)
        {
            return this.Get<T>($"query/{query}");            
        }

        public void Post<T>(T item, string endpoint)
        {
            HttpResponseMessage response = client.PostAsJsonAsync(endpoint, item).GetAwaiter().GetResult();
            response.EnsureSuccessStatusCode();
        }
        
        public void Delete(int id, string endpoint)
        {
            HttpResponseMessage response = client.DeleteAsync($"{endpoint}/{id}").GetAwaiter().GetResult();
            response.EnsureSuccessStatusCode();
        }

        public void Put<T>(T item, string endpoint)
        {
            HttpResponseMessage response = client.PutAsJsonAsync(endpoint, item).GetAwaiter().GetResult();
            response.EnsureSuccessStatusCode();
        }
    }
}