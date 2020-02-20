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
using Engine.EventArgs;
using Engine.ViewModels; // import GameSession.cs

namespace WPFUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Main GameSession object
        /// </summary>
        /// <remarks>
        /// readonly - because we only want MainWindow to be able to declare it here or inside a constructor.
        ///           It is so we can never assign a value to it or accidently override somewhere else.
        /// Note: in C# a Class is a reference type.  
        ///       When an object of the class is created, the variable to which the object is assigned holds only a reference to that memory.
        ///       This will be usefull because we will be passing _gameSession(which is a reference to our GameSession Object) to MerchantScreen.
        ///       Where the Player can sell/buy items and those change will then be reflected in the GamesSession Object.
        /// </remarks>
        private readonly GameSession _gamesSession = new GameSession();
        public MainWindow()
        {
            InitializeComponent();
            Console.WriteLine(_gamesSession.GameIconImageSprite);
            // When our UI(xaml) is constructed/rendered
            // we add the function OnGameMessagedRaised() to _gamesSession.OnMessagedRaised
            // so _gamesSession.OnMessagedRaised has a pointer/reference to OnGameMessagedRaised() function.
            // which is our function to display message to the UI(xaml)
            // inside our GamesSession we will trigger OnGameMessagedRaised() to display the message to our UI(xaml)
            _gamesSession.OnMessagedRaised += OnGameMessagedRaised;
           
            DataContext = _gamesSession;
        }

        // our move functions are private because only MainWindow.xaml will be using it.
        // note: the onlick has 2 parameters sent from the xaml when clicked, we are not going to use them, but need to hold them. 
        // We are just using the click to trigger functions (MoveNorth/MoveWest/MoveEast/MoveSouth) in our _gameSession object
        private void OnClick_MoveNorth(object sender, RoutedEventArgs e) 
        {
            _gamesSession.MoveNorth();
        }
        private void OnClick_MoveWest(object sender, RoutedEventArgs e)
        {
            _gamesSession.MoveWest();
        }
        private void OnClick_MoveEast(object sender, RoutedEventArgs e)
        {
            _gamesSession.MoveEast();
        }
        private void OnClick_MoveSouth(object sender, RoutedEventArgs e)
        {
            _gamesSession.MoveSouth();
        }
        
        // display Merchant Screen
        private void OnClick_DisplayMerchantScreen(object sender, RoutedEventArgs e) 
        {
            MerchantScreen merchantScreen = new MerchantScreen();
            merchantScreen.Owner = this;
            merchantScreen.DataContext = _gamesSession;  // pass by references
            merchantScreen.ShowDialog();
           
        }
        private void OnClick_AttackMonster(object sender, RoutedEventArgs e) 
        {
            _gamesSession.AttackCurrentMonster();
        }

        // Display GameMessageEventArgs to xaml 
        private void OnGameMessagedRaised(object sender, GameMessageEventArgs e) 
        {
           
            GameMessages.Document.Blocks.Add(new Paragraph(new Run(e.Message)));
            GameMessages.ScrollToEnd();
        }


    }
}
