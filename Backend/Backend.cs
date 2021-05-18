﻿using System;
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
using SpotifyAPI.Web;

namespace Spotify_2._0.Backend
{
    class Backend
    {
        private string ClientId;
        private string ClientSecret;
 /*       private List<Playlist> playlists;*/

        private SpotifyClient spotify { get; set; }

        public Backend()
        {
            InitializeEnvironment();
        }

        private async void InitializeEnvironment()
        {
            Env.TraversePath().Load();
            ClientId = Environment.GetEnvironmentVariable("CLIENT_ID");
            ClientSecret = Environment.GetEnvironmentVariable("CLIENT_SECRET");

            await RetrieveToken();

            var test_playlists = await GetSongs("icedin");

            Trace.WriteLine(test_playlists.Count);

            test_playlists.ForEach(playlist =>
            {
                Trace.WriteLine(playlist);
            });
        }

        private async Task RetrieveToken()
        {
            var config = SpotifyClientConfig.CreateDefault();
            var request = new ClientCredentialsRequest(ClientId, ClientSecret);

            var response = await new OAuthClient(config).RequestToken(request);
            spotify = new SpotifyClient(config.WithToken(response.AccessToken));
        }

        public async Task<List<Playlist>> GetPlaylists(string username)
        {
            try
            {
                var playlists = spotify.Playlists.GetUsers(username);

                var playlist_items = await playlists;

                List<Playlist> returning_playlists = new List<Playlist>();

                foreach (var item in playlist_items.Items)
                {
                    returning_playlists.Add(new Playlist(
                        item.Images.Count > 0 ? item.Images[0].Url : "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Fwallpapertag.com%2Fwallpaper%2Ffull%2F6%2F3%2F6%2F182000-light-gray-background-1920x1200-for-android-tablet.jpg&f=1&nofb=1",
                        item.Name,
                        item.Description,
                        item.Tracks.Href,
                        item.Tracks.Total.Value
                    ));
                }

                return returning_playlists;
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
                return new List<Playlist>();
            }
        }

        public async Task<List<Song>> GetSongs(string playlistUrl)
        {
            try
            {

                var playlist = await spotify.Playlists.Get("0dNixyJyJwMzTuWGNsuVz8");

                List<Song> playlist_songs = new List<Song>();

                foreach (PlaylistTrack<IPlayableItem> item in playlist.Tracks.Items)
                {
                    if (item.Track is FullTrack track)
                    {
                        playlist_songs.Add(
                            new Song(
                                track.Name,
                                track.Album.Name,
                                track.Album.Images[0].Url,
                                track.Album.Artists,
                                track.DurationMs,
                                track.PreviewUrl
                            )
                        );
                    }
                }

                return playlist_songs;
            }
            catch(Exception)
            {
                Trace.WriteLine("Error getting songs");
                return new List<Song>();
            }
        }
    }
}