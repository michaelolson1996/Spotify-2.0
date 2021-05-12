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
using Spotify_2._0.Classes;

namespace Spotify_2._0.Backend
{
    class Backend
    {
        private string ClientId;
        private string ClientSecret;
        private AccessToken Token { get; set; }



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

        private async void InitializeEnvironment()
        {
            Env.TraversePath().Load();
            ClientId = Environment.GetEnvironmentVariable("CLIENT_ID");
            ClientSecret = Environment.GetEnvironmentVariable("CLIENT_SECRET");

            Task<AccessToken> task_token = RetrieveToken();

            Token = await task_token;

            Trace.WriteLine(Token.access_token);

            /*Task<T> task_playlists = RetrievePlaylists<T>("icedin");*/

           /* _ = await task_playlists;*/
            /*_ = RetrievePlaylists("icedin");*/
        }

        private async Task<AccessToken> RetrieveToken()
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

                return JsonConvert.DeserializeObject<AccessToken>(response);
            }
        }

 /*       private async Task<T> RetrievePlaylists<T>(string username)
        {
            string url = $"https://api.spotify.com/v1/users/{username}/playlists?access_token={Token.access_token}";
            *//*Trace.WriteLine("RetrievePlaylists Method");*//*

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var request = await client.GetAsync(url);
                var response = await request.Content.ReadAsStringAsync();

                T type = default(T);

                type = JsonConvert.DeserializeObject<T>(response);

                Trace.WriteLine(type);

               *//* Newtonsoft.Json.JsonSerializer js = new Newtonsoft.Json.JsonSerializer();
                
                Trace.WriteLine(js.Deserialize(response));*//*

                

                return type;
            }
        }*/
    }
}
