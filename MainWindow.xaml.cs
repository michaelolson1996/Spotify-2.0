using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Spotify_2._0.Backend;
using Spotify_2._0.Classes;
using System.IO;

namespace Spotify_2._0
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        BackendTest backend = new BackendTest();
//        Backend.Backend backend2 = new Backend.Backend();

        public MainWindow()
        {
            InitializeComponent();

            List<Playlist> playlists = backend.RetrievePlaylists(50);
            List<Song> songs = backend.RetrievePlaylistSongs(30);

            playlists.ForEach(playlist => {
                Playlist_Text_Block.Text += $"Name : {playlist.name} \nDescription : {playlist.description}\n";
            });
        }
    }
}
