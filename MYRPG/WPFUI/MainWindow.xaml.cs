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

using Engine.ViewModels; // import GameSession.cs

namespace WPFUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GameSession _gamesSession;
        public MainWindow()
        {
            InitializeComponent();
            _gamesSession = new GameSession();
            DataContext = _gamesSession;
        }

        // our move functions are private because only MainWindow.xaml will be using it.
        // note: the onlick has 2 parameters sent from the xaml when clicked, we are not going to use them, but need to hold them. 
        private void Onclick_MoveNorth(object sender, RoutedEventArgs e) 
        {
            
        }
        private void Onclick_MoveWest(object sender, RoutedEventArgs e)
        {

        }
        private void Onclick_MoveEast(object sender, RoutedEventArgs e)
        {

        }
        private void Onclick_MoveSouth(object sender, RoutedEventArgs e)
        {

        }

    }
}
