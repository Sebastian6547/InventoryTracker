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
    /// Interaction logic for EditItemWindow.xaml
    /// </summary>
    public partial class EditItemWindow : Window
    {
        private Item item;
        public EditItemWindow(Item item)
        {
            InitializeComponent();
            this.item = item;
            cmbCategory.ItemsSource = Enum.GetValues(typeof(ItemCategory)).Cast<ItemCategory>().ToList();
            cmbSupplier.ItemsSource = Item.GetListOfSuppliers();

            FillForm();
        }
        private void btnEditItem_Click(object sender, RoutedEventArgs e)
        {
            string check = Item.CheckInput(txtName.Text, txtAvailableQuantity.Text, txtMinimumQuantity.Text, txtLocation.Text, cmbSupplier.SelectedIndex, cmbCategory.SelectedIndex);
            if (string.IsNullOrEmpty(check))
            {
                string supplier;
                if (cmbSupplier.SelectedIndex == -1)
                    supplier = "NULL";
                else
                    supplier = cmbSupplier.SelectedItem.ToString();
                item.UpdateItem(txtName.Text, Convert.ToInt32(txtAvailableQuantity.Text), Convert.ToInt32(txtMinimumQuantity.Text), txtLocation.Text, supplier, item.Category = cmbCategory.SelectedItem.ToString());

                Close();
            }
            else
            {
                MessageBox.Show(check, "ERROR!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void FillForm()
        {
            txtName.Text = item.ItemName;
            txtAvailableQuantity.Text = item.AvailableQuantity.ToString();
            txtMinimumQuantity.Text = item.MinimumQuantity.ToString();
            if (item.Supplier != "NULL")
            {
                for (int i = 0; i < Item.GetListOfSuppliers().Count; i++)
                {
                    if (item.Supplier == Item.GetListOfSuppliers()[i])
                    {
                        cmbSupplier.SelectedIndex = i;
                    }
                }
            }
            if (item.Category != "NULL")
            {
                for (int i = 0; i < Enum.GetValues(typeof(ItemCategory)).Cast<ItemCategory>().ToList().Count; i++)
                {
                    if (item.Category == Enum.GetValues(typeof(ItemCategory)).Cast<ItemCategory>().ToList()[i].ToString())
                    {
                        cmbCategory.SelectedIndex = i;
                    }
                }
            }
        }
    }
}
