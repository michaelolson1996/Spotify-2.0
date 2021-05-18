using Spotify_2._0.Backend;
using Spotify_2._0.Classes;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Spotify_2._0
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Button btn = new();
        BackendTest backend = new BackendTest();
        Backend.Backend backend2 = new Backend.Backend();

        public MainWindow()
        {
            InitializeComponent();
        }

        public void SearchUserBtn_Click(object sender,EventArgs e)
        {
            //DummyData();
            GetBackendInfo(SearchUserText.Text);
        }

        public async void GetBackendInfo(string search)
        {
            List<Playlist> playlists = await backend2.GetPlaylists(search);
            List<Song> songs = await backend2.GetSongs(search);
            playlists.ForEach(playlist => { btn = new(); btn.Content = $"{playlist.name}"; Playlist_Text_Block.Children.Add(btn); });
            songs.ForEach(song => { btn.Content = $"{song.name}"; Song_Text_Block.Children.Add(btn); });
        }

        /*public void DummyData()
        {
            List<Playlist> playlists = backend.RetrievePlaylists(5);
            List<Song> songs = backend.RetrievePlaylistSongs(3);

            playlists.ForEach(playlist =>
            {
                Playlist_Text_Block.Text += $"Name : {playlist.name} \nDescription : {playlist.description}\n";
            });
            songs.ForEach(song =>
            {
                Song_Text_Block.Text += $"Name: {song.name}\nDuration: {song.duration}\n";
            });
        }*/
    }
}
