using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace InvertoryTracker
{
    public class Item
    {
        private string _itemName;
        private int _availableQuantity;
        private int _minimumQuantity;
        private string _location;
        private string _supplier;
        private string _category;

        private static List<string> listOfSuppliers = new List<string>{ "Costco", "IGA", "Metro", "Adonis","Walmart"};

        /// <summary>
        /// Default contructor that sets all the values to either null or 0
        /// </summary>
        public Item()
        {
            this._itemName = null;
            this._availableQuantity = 0;
            this._minimumQuantity = 0;
            this._location = null;
            this._supplier = null;
            this._category = null;
        }
        /// <summary>
        /// Constructor that takes in all the parameters of an item and sets the properties of the fields with them
        /// </summary>
        /// <param name="itemName"></param>
        /// <param name="availableQuantity"></param>
        /// <param name="minimumQuantity"></param>
        /// <param name="location"></param>
        /// <param name="supplier"></param>
        /// <param name="category"></param>
        public Item(string itemName, int availableQuantity, int minimumQuantity, string location, string supplier, string category)
        {
            this.ItemName = itemName;
            this.AvailableQuantity = availableQuantity;
            this.MinimumQuantity = minimumQuantity;
            this.Location = location;
            this.Supplier = supplier;
            this.Category = category;
        }
        #region Properties
        //Properties for all non static fields
        public string ItemName
        {
            get
            {
                if (_itemName == null)
                    return "NULL";

                return _itemName;
            }
            set
            {
                _itemName = value;
            }
        }

        public int AvailableQuantity
        {
            get { return _availableQuantity; }
            set
            {
                if (value < 0)
                    throw new Exception();
                _availableQuantity = value;
            }
        }

        public int MinimumQuantity
        {
            get { return _minimumQuantity; }
            set
            {
                if (value < 0)
                    throw new Exception();

                _minimumQuantity = value;
            }
        }

        public string Location
        {
            get
            {
                if (_location == null||_location=="")
                    return "NULL";

                return _location;
            }
            set { _location = value; }
        }

        public string Supplier
        {
            get
            {
                if (_supplier == null)
                    return "NULL";

                return _supplier;
            }
            set { _supplier = value; }
        }

        public string Category
        {
            get { return _category; }
            set { _category = value; }
        }
        #endregion
        /// <summary>
        /// Method that returns all the information about an item
        /// </summary>
        /// <returns>string</returns>
        public string GetFullInfo()
        {
            return string.Format(
                "Item Name:\t{0}\n" +
                "Available Quantity:\t{1}\n" +
                "Minimum Quantity\t{2}\n" +
                "Location:\t{3}\n" +
                "Supplier:\t{4}\n" +
                "Category:\t{5}\n",
                ItemName, AvailableQuantity, MinimumQuantity, Location, Supplier, Category);
        }
        /// <summary>
        /// Method that takes in all parameters for an item and changes the properties with them
        /// </summary>
        /// <param name="itemName"></param>
        /// <param name="availableQuantity"></param>
        /// <param name="minimumQuantity"></param>
        /// <param name="location"></param>
        /// <param name="supplier"></param>
        /// <param name="category"></param>
        public void UpdateItem(string itemName, int availableQuantity, int minimumQuantity, string location, string supplier, string category)
        {
            ItemName = itemName;
            AvailableQuantity = availableQuantity;
            MinimumQuantity = minimumQuantity;
            Location = location;
            Supplier = supplier;
            Category = category;
        }
        /// <summary>
        /// Calculated property to get the non static fields of an item and to set the non static fields with comma seperated values
        /// </summary>
        public string CSVData
        {
            get
            {
                return string.Format("{0},{1},{2},{3},{4},{5}",ItemName,AvailableQuantity,MinimumQuantity,Location,Supplier,Category);
            }
            set
            {
                //string comma separated and set the fields of the visitor
                string[] allData = value.Split(',');
                try
                {
                    UpdateItem(allData[0], Convert.ToInt32(allData[1]), Convert.ToInt32(allData[2]), allData[3], allData[4], allData[5]);
                }
                catch (Exception ex)
                {
                    throw new Exception("All Data Property value not valid " + ex.Message);
                }
            }
        }
        /// <summary>
        /// Method that overrides the return of ToString 
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            return string.Format("Name: {0}\tAvailable Quantity: {1}\tMinimum Quantity: {2}\tLocation: {3}\tSupplier: {4}\tCategory: {5}",ItemName,AvailableQuantity,MinimumQuantity,Location,Supplier,Category);
        }
        public static string CheckInput(string itemName, string availableQuantity, string minimumQuantity, string location, int supplier, int category) //takes in location and supplier in case they need to be checked in the future
        {
            StringBuilder builder = new StringBuilder();
            if (string.IsNullOrEmpty(itemName))
                builder.AppendLine("Item Name is a required field");
            if (string.IsNullOrEmpty(availableQuantity))
                builder.AppendLine("Available Quantity is a required field");
            if (string.IsNullOrEmpty(minimumQuantity))
                builder.AppendLine("Minimum Quantity is a required field");
            if (category == -1)
                builder.AppendLine("Category is a required field");
            if (!int.TryParse(availableQuantity, out _)||Convert.ToInt32(availableQuantity) < 0)
            {
                builder.AppendLine("Available Quantity needs to be a number above 0");
            }
            if (!int.TryParse(minimumQuantity, out _)|| Convert.ToInt32(minimumQuantity) < 1)
            {
                builder.AppendLine("Minimum Quantity needs to be a number above 1");
            }
            return builder.ToString();
        }
        /// <summary>
        /// Static method to get the list of suppliers
        /// </summary>
        /// <returns>List<string></returns>
        public static List<string> GetListOfSuppliers()
        {
            return listOfSuppliers;
        }
        /// <summary>
        /// Static method to add a supplier to the list of suppliers
        /// </summary>
        /// <param name="supplier"></param>
        /// <returns>List<string></returns>
        public static List<string> AddSupplier(string supplier)
        {
            listOfSuppliers.Add(supplier);
            return listOfSuppliers;
        }
        /// <summary>
        /// Static method to remove a supplier from the supplier list
        /// </summary>
        /// <param name="supplier"></param>
        /// <returns>bool: wether removing was successful of not</returns>
        public static bool RemoveSupplier(string supplier)
        {
            for (int i = 0; i < listOfSuppliers.Count; i++)
            {
                if (listOfSuppliers[i] == supplier)
                {
                    listOfSuppliers.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }
    }

    public enum ItemCategory
    {
        Pantry, Diary, Drinks, FrozenFood, FruitsAndVegetables, Bakery, CleaningSupplies //More categories can be added
    }
}
