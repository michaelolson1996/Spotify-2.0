using Spotify_2._0.Backend;
using Spotify_2._0.Classes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
            GetBackendInfo(SearchUserText.Text);
        }

        public async void GetBackendInfo(string search)
        {
            List<Playlist> playlists = await backend2.GetPlaylists(search);

            playlists.ForEach(playlist => { 
                btn = new(); 
                btn.Content = $"{playlist.name}";
                btn.DataContext += $"{playlist.id}";
                btn.Click += new RoutedEventHandler(SongsReturn);
                Playlist_Text_Block.Children.Add(btn); 
            });
        }

        private async void SongsReturn(object sender, RoutedEventArgs e)
        {
            Song_Text_Block.Children.Clear();
            object _ = ((Button)sender).DataContext;

            List<Song> songs = await backend2.GetSongs(_.ToString());

            songs.ForEach(song =>
            {
                btn = new();
                btn.Content = $"{song.name}";
                string retStr = "";
                song.Artists.ForEach(artist =>
                {
                    retStr += artist.Name;
                });
                btn.Tag = song.preview_url;
                btn.DataContext += $"Artist: {retStr}\n";
                btn.DataContext += $"Album: {song.album_name}";
                btn.MouseDoubleClick += Btn_MouseDoubleClick;
                btn.Click += new RoutedEventHandler(ArtistAlbumReturn);
                Song_Text_Block.Children.Add(btn);
            });
        }

        private void Btn_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                mePlayer.Close();
                mePlayer.Source = new Uri(((Button)sender).Tag.ToString());
                mePlayer.Play();
            }
            catch (Exception)
            {
                mePlayer.Play();
            }

        }

        private void ArtistAlbumReturn(object sender, RoutedEventArgs e)
        {
            AlbumArtist_name.Text = "";

            object _ = ((Button)sender).DataContext;
            Trace.WriteLine(_);

            AlbumArtist_name.Text += _;
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
