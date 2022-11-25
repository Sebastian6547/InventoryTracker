using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for CreateItemWindow.xaml
    /// </summary>
    public partial class CreateItemWindow : Window
    {
        private Item item;
        /// <summary>
        /// Constructor that takes in the inventory and binds the category combo box with the ItemCategory enum and the supplier combo box with the supplier array
        /// </summary>
        /// <param name="inventory"></param>
        public CreateItemWindow(Item item)
        {
            InitializeComponent();
            this.item = item;
            cmbCategory.ItemsSource = Enum.GetValues(typeof(ItemCategory)).Cast<ItemCategory>().ToList(); //I used the following link to help me with this: https://stackoverflow.com/questions/1167361/how-do-i-convert-an-enum-to-a-list-in-c
            cmbSupplier.ItemsSource = Item.GetListOfSuppliers();
        }
        /// <summary>
        /// Method for when user clicks the create item button
        /// </summary>
        private void btnCreateItem_Click(object sender, RoutedEventArgs e)
        {
            string check= Item.CheckInput(txtName.Text,txtAvailableQuantity.Text,txtMinimumQuantity.Text,txtLocation.Text,cmbSupplier.SelectedIndex,cmbCategory.SelectedIndex);
            if (string.IsNullOrEmpty(check))
            {
                string supplier;
                if (cmbSupplier.SelectedIndex == -1)
                    supplier = "NULL";
                else
                    supplier = cmbSupplier.SelectedItem.ToString();

                item.UpdateItem(
                    txtName.Text, 
                    Convert.ToInt32(txtAvailableQuantity.Text), 
                    Convert.ToInt32(txtMinimumQuantity.Text), 
                    txtLocation.Text, 
                    supplier, 
                    cmbCategory.SelectedItem.ToString()
                );
                Close();
            }
            else
            {
                MessageBox.Show(check, "ERROR!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        /// <summary>
        /// Method for when user click the clear item button
        /// </summary>
        private void btnClearItem_Click(object sender, RoutedEventArgs e)
        {
            foreach (object obj in gridContent.Children)
            {
                if (obj is TextBox)
                    (obj as TextBox).Clear();
                else if (obj is ComboBox)
                    (obj as ComboBox).SelectedIndex = -1;
            }
        }
    }
}
