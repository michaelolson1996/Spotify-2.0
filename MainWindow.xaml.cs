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
        /// <summary>
        /// this is the physical button that sends the username to backend
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SearchUserBtn_Click(object sender, EventArgs e)
        {
            GetBackendInfo(SearchUserText.Text);
        }
        /// <summary>
        /// This is where we search for users based on their username and return data that Spotify's API gives us
        /// </summary>
        /// <param name="search"></param>
        public async void GetBackendInfo(string search)
        {
            Playlist_Text_Block.Children.Clear();
            List<Playlist> playlists = await backend2.GetPlaylists(search);

            playlists.ForEach(playlist => {
                btn = new();
                btn.Content = $"{playlist.name}";
                btn.DataContext += $"{playlist.id}";
                btn.Click += new RoutedEventHandler(SongsReturn);
                Playlist_Text_Block.Children.Add(btn);
            });
        }
        /// <summary>
        /// returns the song information and playlist data from the backend
        /// </summary>
        /// <param name="sender">WPF handling how buttons send actions</param>
        /// <param name="e">arguments that some buttons and actions require</param>
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
                    retStr += artist.Name + '\n';
                });
                btn.Tag = song.preview_url;
                btn.DataContext += $"Artist(s): {retStr}\n";
                btn.DataContext += $"Album: {song.album_name}";
                btn.MouseDoubleClick += Btn_MouseDoubleClick;
                btn.Click += new RoutedEventHandler(ArtistAlbumReturn);
                Song_Text_Block.Children.Add(btn);
            });
        }
        /// <summary>
        /// this plays the preview for the songs returned in each playlist
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <summary>
        /// this returns the album and artist that the song clicked is in
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ArtistAlbumReturn(object sender, RoutedEventArgs e)
        {
            AlbumArtist_name.Text = "";

            object _ = ((Button)sender).DataContext;
            Trace.WriteLine(_);

            AlbumArtist_name.Text += _;
        }
        /// <summary>
        /// this was dummy data for testing purposes in sprint 1
        /// </summary>
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
