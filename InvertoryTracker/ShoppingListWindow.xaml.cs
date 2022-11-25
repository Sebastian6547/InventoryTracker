using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace InvertoryTracker
{
    /// <summary>
    /// Interaction logic for ShoppingListWindow.xaml
    /// </summary>
    public partial class ShoppingListWindow : Window
    {
        private Inventory shoppingInventory;

        private string saveLocation = string.Empty;
        public ShoppingListWindow(Inventory inventory)
        {
            InitializeComponent();
            this.shoppingInventory = inventory.ShoppingList();
            dgShopList.ItemsSource = shoppingInventory.GetItemsList();
        }

        private void btnSaveShopList_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(saveLocation))
            {
                
                SaveFileDialog save = new SaveFileDialog();
                save.Filter = "CVS Files|*.csv";
                if (save.ShowDialog() == true)
                {
                    saveLocation = save.FileName;
                }
                else
                    return;
            }
            WriteDataToFile();
        }
        private void WriteDataToFile()
        {
            try
            {
                shoppingInventory.SaveItems(saveLocation);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
