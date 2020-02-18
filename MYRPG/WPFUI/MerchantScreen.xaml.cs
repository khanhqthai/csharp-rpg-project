using Engine.Models;
using Engine.ViewModels;
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
using System.Windows.Shapes;

namespace WPFUI
{
    /// <summary>
    /// Interaction logic for MerchantScreen.xaml
    /// </summary>
    public partial class MerchantScreen : Window
    {
        public GameSession Session => DataContext as GameSession;
        public MerchantScreen()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Closes Merchant Shop Screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnClick_ExitShop(Object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Buys an item from Merchant's inventory
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnClick_Buy(Object sender, RoutedEventArgs e)
        {
            Item item = ((FrameworkElement)sender).DataContext as Item; // get item from row clicked
            if (item != null) 
            {   // check if Player has enough gold
                if (Session.CurrentPlayer.Gold >= item.Price) 
                {
                    // subtract gold from player, add item to Player's inventory, remove item from Merchant's inventory
                    Session.CurrentPlayer.Gold -= item.Price;
                    Session.CurrentPlayer.AddItemToInventory(item);
                    Session.CurrentMerchant.RemoveItemFromInventory(item);
                    
                }
            }
        }
        /// <summary>
        /// Sells item in player's inventory
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnClick_Sell(Object sender, RoutedEventArgs e)
        {
            Item item = ((FrameworkElement)sender).DataContext as Item; // get item from row clicked
            if (item != null)
            {
                // give player gold, remove item from Player's inventory, add item to Merchant's inventory
                Session.CurrentPlayer.Gold += item.Price;
                Session.CurrentPlayer.RemoveItemFromInventory(item);
                Session.CurrentMerchant.AddItemToInventory(item);

            }
            else 
            {
                MessageBox.Show("You do not have enough Gold to purchase item.");
            }
        }
    }

}
