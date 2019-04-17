namespace UiTest.Services
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Formatting;
    using System.Text;
    using System.Threading.Tasks;
    using UiTest.Models;

    public class HttpService
    {
        private static HttpClient client = new HttpClient();

        public HttpService()
        {
            this.BaseUrl = "http://localhost:50729/api";
        }

        public string BaseUrl { get; set; }

        public async Task<T> Post<T>(T item, string path)
        {
            T newItem = default(T);
            HttpResponseMessage response = await client.PostAsJsonAsync(
                path, item);
            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                newItem = await response.Content.ReadAsAsync<T>();
            }

            return newItem;
        }

        public async Task<T> Get<T>(string path)
        {
            T item = default(T);
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                item = await response.Content.ReadAsAsync<T>();
            }

            return item;
        }

        public async Task<T> Update<T>(T item, string id, string path)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync(
                $"{path}{id}", item);
            response.EnsureSuccessStatusCode();

            // Deserialize the updated product from the response body.
            item = await response.Content.ReadAsAsync<T>();
            return item;
        }

        public async Task<HttpStatusCode> Delete<T>(string id, string path)
        {
            HttpResponseMessage response = await client.DeleteAsync(
                $"{path}{id}");
            return response.StatusCode;
        }

        public async Task<User> Validate(UserLogin item, string path)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync($"{path}", item);
            User user1 = new User();
            if (response.IsSuccessStatusCode)
            {
                user1 = await response.Content.ReadAsAsync<User>();
            }

            return user1;
        }
    }
}
