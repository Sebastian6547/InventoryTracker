using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace InvertoryTracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Inventory inventory = new Inventory();

        //Save logic variables
        private string saveLocation = string.Empty;
        private bool saved = false;
        /// <summary>
        /// Constructor to bind the item list to the item list box
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            lbItems.ItemsSource = inventory.GetItemsList();
        }
        /// <summary>
        /// Method for when user double clicks an item in the listbox
        /// </summary>
        private void lbItems_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Item item = lbItems.SelectedItem as Item;
            if (item != null)
                MessageBox.Show(item.GetFullInfo(),"Item Info");
        }
        /// <summary>
        /// Method for when user right clicks an item in the listbox and then selects the option to remove
        /// </summary>
        private void menuRemove_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result =
            MessageBox.Show("Are you sure you want to delete this item?","Item Deletion", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                inventory.RemoveItem(lbItems.SelectedItem as Item);
                lbItems.Items.Refresh();
            }
        }
        /// <summary>
        /// Method for when user clicks the shopping list button
        /// </summary>
        private void btnShopList_Click(object sender, RoutedEventArgs e)
        {
            ShoppingListWindow shoppingListWindow = new ShoppingListWindow(inventory);
            shoppingListWindow.Show();
        }
        /// <summary>
        /// Method for when user right clicks an item in the listbox and then select the option to edit it
        /// </summary>
        private void menuEdit_Click(object sender, RoutedEventArgs e)
        {
            EditItemWindow editItemWindow = new EditItemWindow(lbItems.SelectedItem as Item);
            editItemWindow.ShowDialog();
            lbItems.Items.Refresh();
            saved = false;
        }
        /// <summary>
        /// Method for when user clicks the add item button
        /// </summary>
        private void btnAddItem_Click(object sender, RoutedEventArgs e)
        {
            Item newItem = new Item();
            CreateItemWindow createItemWindow = new CreateItemWindow(newItem);
            createItemWindow.ShowDialog();
            if(newItem.ItemName!="NULL")
            {
                inventory.AddItem(newItem);
                lbItems.Items.Refresh();
                saved = false;
            }
        }
        /// <summary>
        /// Method for when user clicks the save list button
        /// </summary>
        private void btnSaveList_Click(object sender, RoutedEventArgs e)
        {
            SaveLogic();
        }
        /// <summary>
        /// Method for to check if there is already a save location and it not it creates one. It then calls a method that is a continuation to the saving process.
        /// </summary>
        private void SaveLogic()
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
        /// <summary>
        /// Method to check if the list is already saved. It then calls a method in the Inventory class to save the list to the file location.
        /// </summary>
        private void WriteDataToFile()
        {
            if (!saved)
            {
                try
                {
                    inventory.SaveItems(saveLocation);
                    saved = true;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        /// <summary>
        /// Method for when user clicks the load list button
        /// </summary>
        private void btnLoadList_Click(object sender, RoutedEventArgs e)
        {
            if (ChecktoOpen())
            {
                OpenFileDialog open = new OpenFileDialog();
                open.Filter = "CVS Files|*.csv";
                if (open.ShowDialog() == true)
                {
                    //save location
                    saveLocation = open.FileName;
                    //clear current list
                    inventory.ClearItems();
                    //read from file
                    ReadVisitorsFromFile();
                    //update UI
                    lbItems.Items.Refresh();
                    saved = true;
                }
            }
        }
        /// <summary>
        /// Method that checks if the list needs to be saved and if yes, it asks the user wether they want to save it or not.
        /// </summary>
        /// <returns>A bool: True if list is saved</returns>
        private bool ChecktoOpen()
        {
            if (saved)
                return true;

            if (string.IsNullOrEmpty(saveLocation) && inventory.GetItemsList().Count == 0)
                return true;

            //Data is not saved
            MessageBoxResult result =
            MessageBox.Show("Do you want to save changes?", "Unsaved changes", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);


            if (result == MessageBoxResult.No)
                return true;

            if (result == MessageBoxResult.Cancel)
                return false;

            if (result == MessageBoxResult.Yes)
                SaveLogic();

            return saved; //if the user saved it mean it ok to open a file and if the user cancels then saved will be false
        }
        /// <summary>
        /// Method that calls calls a method in the Inventory class to load a list from a file.
        /// </summary>
        private void ReadVisitorsFromFile()
        {
            try
            {
                inventory.LoadItems(saveLocation);
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Method for when user clicks the clear list button
        /// </summary>
        private void btnClearList_Click(object sender, RoutedEventArgs e)
        {
            if(ChecktoOpen())
            {
                inventory.ClearItems();
                lbItems.Items.Refresh();
            }
        }
        /// <summary>
        /// Method for when the window closes that calls a method to check if list is saved.
        /// </summary>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = !ChecktoOpen();
        }
    }
}
