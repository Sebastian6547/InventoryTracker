using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace InvertoryTracker
{
    public class Inventory
    {
        private List<Item> items;
        /// <summary>
        /// Default constructor for this class that just initializes the Item list
        /// </summary>
        public Inventory()
        {
            items = new List<Item>();
        }
        /// <summary>
        /// Meothod that takes in an item and adds it to the Item list
        /// </summary>
        /// <param name="item"></param>
        public void AddItem(Item item)
        {
            items.Add(item);
        }
        /// <summary>
        /// Method that takes in an Item and removes it from the Item list
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool RemoveItem(Item item)
        {
            //for(int i=0;i<items.Count;i++)
            //{
            //    if (item == items[i])
            //    {
            //        items.RemoveAt(i);
            //        return true;
            //    }
            //}
            if(items.Remove(item))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Method that returns a report of the item list including the name, available quantity and minimum quantity of every item
        /// </summary>
        /// <returns>string</returns>
        public string GeneralReport()
        {
            StringBuilder report = new StringBuilder();
            foreach(Item item in items)
            {
                string info = string.Format("Item: {0}, Available Quantity: {1}, Minimum Quantity {2}",item.ItemName,item.AvailableQuantity,item.AvailableQuantity);
                report.Append(info);
            }
            return report.ToString();
        }
        /// <summary>
        /// Mehtod that returns an Inventory of every item that has a available quantity lower that the minimum quantity
        /// </summary>
        /// <returns>Inventory</returns>
        public Inventory ShoppingList()
        {
            Inventory shoppingList = new Inventory();
            foreach(Item item in items)
            {
                if(item.AvailableQuantity<item.MinimumQuantity)
                {
                    shoppingList.AddItem(item);
                }
            }

            return shoppingList;
        }
        /// <summary>
        /// Method that takes data from a csv file and adds it to the Item list
        /// </summary>
        /// <param name="saveLocation"></param>
        public void LoadItems(string saveLocation)
        {
            string[] allValues = File.ReadAllLines(saveLocation);
            foreach (string itemInfo in allValues)
            {
                Item temp = new Item();
                temp.CSVData = itemInfo;
                items.Add(temp);
            }
        }
        /// <summary>
        /// Method that saves the Item list to a file
        /// </summary>
        /// <param name="saveLocation"></param>
        public void SaveItems(string saveLocation)
        {
            StringBuilder itemCSV = new StringBuilder();
            foreach (Item item in items)
                itemCSV.AppendLine(item.CSVData);

            File.WriteAllText(saveLocation, itemCSV.ToString());
        }
        /// <summary>
        /// Method to clear the Item list
        /// </summary>
        public void ClearItems()
        {
            items.Clear();
        }
        /// <summary>
        /// Get method that returns the Item list
        /// </summary>
        /// <returns>List<Item></returns>
        public List<Item> GetItemsList()
        {
            return items;
        }
    }
}
