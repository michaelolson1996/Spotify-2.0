using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotify_2._0.Classes
{
    class Playlist
    {
        public string image { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string tracks_url { get; set; }
        public int total_songs { get; set; }

        public Playlist(string image, string name, string description, string tracks_url, int total_songs)
        {
            this.image = image;
            this.name = name;
            this.description = description;
            this.tracks_url = tracks_url;
            this.total_songs = total_songs;
        }
    }
}
