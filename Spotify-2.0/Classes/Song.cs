using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotify_2._0.Classes
{
    class Song
    {
        public string name { get; set; }
        public string album_name { get; set; }
        public string album_img { get; set; }
        public string[] album_artists { get; set; }
        public int duration { get; set; }
        public string preview_url { get; set; }

        public Song(string name, string album_name, string album_img, string[] album_artists, int duration, string preview_url)
        {
            this.name = name;
            this.album_name = album_name;
            this.album_img = album_img;
            this.album_artists = album_artists;
            this.duration = duration;
            this.preview_url = preview_url;
        }
    }
}
