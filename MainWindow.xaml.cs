using Spotify_2._0.Backend;
using Spotify_2._0.Classes;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Spotify_2._0
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        //BackendTest backend = new BackendTest();
        Backend.Backend backend2 = new Backend.Backend();

        public MainWindow()
        {
            InitializeComponent();

            /*List<Playlist> playlists = backend.RetrievePlaylists(5);
            List<Song> songs = backend.RetrievePlaylistSongs(3);

            playlists.ForEach(playlist => {
                Playlist_Text_Block.Text += $"Name : {playlist.name} \nDescription : {playlist.description}\n";
            });
            songs.ForEach(song =>
            {
                Song_Text_Block.Text += $"Name: {song.name}\nDuration: {song.duration}\n";
            });*/
        }

        public void SearchUserBtn_Click(object sender,EventArgs e)
        {
            Playlist_Text_Block.Text = "";
            GetBackendInfo();
        }

        public async void GetBackendInfo()
        {
            List<Playlist> playlists = await backend2.GetPlaylists(SearchUserText.Text);
            playlists.ForEach(playlist => { Playlist_Text_Block.Text += $"{playlist.name}\n"; });
        }
    }
}
