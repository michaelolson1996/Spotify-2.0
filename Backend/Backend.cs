using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using DotNetEnv;
using System.Net.Http;
using System.Text.Json;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace Spotify_2._0.Backend
{
    class Backend
    {
        private string ClientId;
        private string ClientSecret;

        class AccessToken
        {
            public string access_token { get; set; }
            public string token_type { get; set; }
            public long expires_in { get; set; }
        }

        public Backend()
        {
            InitializeEnvironment();
            
        }

        private void InitializeEnvironment()
        {
            Env.TraversePath().Load();
            ClientId = Environment.GetEnvironmentVariable("CLIENT_ID");
            ClientSecret = Environment.GetEnvironmentVariable("CLIENT_SECRET");

            _ = RetrieveToken();
        }

        private async Task RetrieveToken()
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add( new MediaTypeWithQualityHeaderValue("application/json"));

                // add client id and client secret to the authorization header
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                        "Basic",
                        Convert.ToBase64String(Encoding.UTF8.GetBytes($"{ClientId}:{ClientSecret}")));

                // append the grant type to the request body
                List<KeyValuePair<string, string>> requestData = new List<KeyValuePair<string, string>>();
                requestData.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));

                FormUrlEncodedContent requestBody = new FormUrlEncodedContent(requestData);

                // send request and await response
                var request = await client.PostAsync("https://accounts.spotify.com/api/token", requestBody);
                var response = await request.Content.ReadAsStringAsync();

                JsonSerializer.

                return JsonConvert.DeserializeObject<AccessToken>(response);
            }
        }
    }
}
