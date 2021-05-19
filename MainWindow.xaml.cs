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
        BackendTest backend = new();
        readonly Backend.Backend backend2 = new();

        public MainWindow()
        {
            InitializeComponent();
        }

        public void SearchUserBtn_Click(object sender, EventArgs e)
        {
            //DummyData();
            GetBackendInfo(SearchUserText.Text);
        }

        public async void GetBackendInfo(string search)
        {
            List<Playlist> playlists = await backend2.GetPlaylists(search);

            playlists.ForEach(playlist => { 
                btn = new(); 
                btn.Content = $"{playlist.name}";
                btn.DataContext += $"{playlist.id}";
                btn.Click += new RoutedEventHandler(songsReturn);
                Playlist_Text_Block.Children.Add(btn); 
            });
        }

        private async void songsReturn(object sender, RoutedEventArgs e)
        {
            Song_Text_Block.Children.Clear();
            object _ = ((Button)sender).DataContext;

            List<Song> songs = await backend2.GetSongs(_.ToString());

            songs.ForEach(song =>
            {
                btn = new();
                btn.Content = $"{song.name}";
                Song_Text_Block.Children.Add(btn);
            });
        }

        public void DummyData()
        {
            List<Playlist> playlists = backend.RetrievePlaylists(5);
            List<Song> songs = backend.RetrievePlaylistSongs(3);

            playlists.ForEach(playlist =>
            {
                TextBlock text = new();
                text.Text += $"Name : {playlist.name} \nDescription : {playlist.description}\n";
                Playlist_Text_Block.Children.Add(text);
            });
            songs.ForEach(song =>
            {
                TextBlock textBlock = new();
                textBlock.Text += $"Name: {song.name}\nDuration: {song.duration}\n";
                Song_Text_Block.Children.Add(textBlock);
            });
        }
    }
}
