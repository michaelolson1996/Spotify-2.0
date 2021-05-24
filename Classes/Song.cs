using SpotifyAPI.Web;
using System.Collections.Generic;

namespace Spotify_2._0.Classes
{
    internal class Song
    {
        public string name { get; set; }
        public string album_name { get; set; }
        public string album_img { get; set; }
        public string[] album_artists { get; set; }
        public int duration { get; set; }
        public string preview_url { get; set; }
        public string name1 { get; }
        public string name2 { get; }
        public string url { get; }
        public List<SimpleArtist> Artists { get; }
        public int DurationMs { get; }
        public string PreviewUrl { get; }
        public string id { get; set; }

        public Song(string name, string album_name, string album_img, string[] album_artists, string id, int duration, string preview_url)
        {
            this.name = name;
            this.album_name = album_name;
            this.album_img = album_img;
            this.id = id;
            this.album_artists = album_artists;
            this.duration = duration;
            this.preview_url = preview_url;
        }

        public Song(string name1, string name2, string url, List<SimpleArtist> artists, string id, int durationMs, string previewUrl)
        {
            this.name = name1;
            this.album_name = name2;
            this.album_img = url;
            this.Artists = artists;
            this.id = id;
            this.duration = durationMs;
            this.preview_url = previewUrl;
        }

        public override string ToString()
        {
            return $"{name}\n{duration}\n{preview_url}";
        }
    }
}