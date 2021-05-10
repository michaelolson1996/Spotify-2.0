using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spotify_2._0.Classes;

namespace Spotify_2._0.Backend
{
    class BackendTest
    {
        public List<Playlist> playlists { get; set; }
        public List<Song> songs { get; set; }
        Random rng = new Random();

        public List<Playlist> RetrievePlaylists(int how_many)
        {
            try
            {
                playlists.Clear();
            }
            catch (NullReferenceException e)
            {
                playlists = new List<Playlist>();
            }

            // enter 0 for random number under 20
            if (how_many == 0)
                how_many = rng.Next(20);

            // Duplicating image for simplicity, front end will display image so users can see image
            string image = "https://i.scdn.co/image/ab67616d0000b27371af28545729f60b3eca66f7";

            // When playlist is clicked this url will be sent to the back end and backend will fetch songs via this url
            string track_url = "https://api.spotify.com/v1/playlists/5LqYnijWOIzxDPs9AXEiRt/tracks";

            for (int i = 0; i < how_many; i++)
            {
                // users should see Image, Title, Description, and Number of Songs
                playlists.Add(new Playlist(image, "Study Hour", "the music I listen to while I study", track_url, 3));
            }

            return playlists;
        }

        public List<Song> RetrievePlaylistSongs(int how_many)
        {
            try
            {
                songs.Clear();
            }
            catch (NullReferenceException e)
            {
                songs = new List<Song>();
            }
            
            // enter 0 and get random number of songs under 200
            if (how_many == 0)
                how_many = rng.Next(200);


            string[] artists = { "Michael Olson", "Nick Roberson", "Connor Cisney", "Carlos Duran", "Software Stuff", "The Best Artist", "Super Cool Guy" };
            string[] song_album_names = { "The Second Best", "Close to the Worst", "Listen to Me", "No More time", "I am Bored", "This Album", "That Album" };
            string album_image = "https://i.scdn.co/image/ab67616d00001e0285c5e6c686ced3e43bae2748";
            string[] song_names = { "That Song", "Hello", "Say Goodbye To My Friends", "Say Hello to My New Friends", "Writing Stuff", "Such Great Work", "Positive Balance" };
            string preview_url = "https://p.scdn.co/mp3-preview/b1aec4586cd200a817ed4d28baaaa2b3aa483e26?cid=f4b418bcb6cc4ae9b68d96ea8b72ed54";
            int duration = 126345; // duration provided in ms, will add trello card to determine who is in charge of converting to seconds (Front or Back end)

            for (int i = 0; i < how_many; i++)
            {
                // index will pick item from song_artists, song_album_names, artists length will be random
                int index = rng.Next(7);
                songs.Add(new Song(song_names[index], song_album_names[index], album_image, artists.Take(index).ToArray(), duration, preview_url));
            }

            return songs;
        }
    }
}
