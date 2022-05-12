using System;
using System.Collections.Generic;
using WpfApp1.Properties;
using Newtonsoft.Json;
using System.IO;

namespace WpfApp1.Services
{
    internal class ProductsPartsService
    {
        public static Dictionary<String, int> articlesMap = new Dictionary<string, int>();

        /// <summary>
        /// Deserializes the Inventory.Json file to .Net object
        /// </summary>
        /// <returns></returns>
        public static Inventory GetProductPartsObject()
        {
            StreamReader r = new StreamReader("C:\\inventory.json");
            string json = r.ReadToEnd();
            Inventory inventoryObj = JsonConvert.DeserializeObject<Inventory>(json);
            return inventoryObj;
        }

        /// <summary>
        /// Creates a mapping of ID and Stock of available articles
        /// </summary>
        /// <param name="inventoryObj"></param>
        /// <returns></returns>
        public static Dictionary<String, int> CreateProductPartsMapping(Inventory inventoryObj)
        {
            foreach(var item in inventoryObj.inventory)
            {
                articlesMap.Add(item.id, item.stock);
            }
            return articlesMap;
        }
    }
}
