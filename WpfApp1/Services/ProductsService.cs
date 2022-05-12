using System;
using System.Collections.Generic;
using WpfApp1.Properties;
using Newtonsoft.Json;
using System.IO;

namespace WpfApp1.Services
{
    internal static class ProductsService
    {
        public static Dictionary<String, List<ArticleSet>> productsMap = new Dictionary<string, List<ArticleSet>>();

        /// <summary>
        /// Deserialized products.json file to .Net object
        /// </summary>
        /// <returns></returns>
        public static Warehouse GetProductsObject()
        {
            StreamReader r = new StreamReader("C:\\products.json");
            string json = r.ReadToEnd();
            Warehouse productObj = JsonConvert.DeserializeObject<Warehouse>(json);
            return productObj;
        }

        /// <summary>
        /// Creates a mapping of Name and Article set of available products
        /// </summary>
        /// <param name="warehouseObj"></param>
        /// <returns></returns>
        public static Dictionary<String, List<ArticleSet>> CreateProductsMapping(Warehouse warehouseObj)
        {
            foreach (var item in warehouseObj.products)
            {
                productsMap.Add(item.name, item.articleSet);
            }

            return productsMap;
        }

        /// <summary>
        /// Calculates the number of products available 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int CalculateProductsStock(Products obj)
        {
            List<int> num = new List<int>();
                List<ArticleSet> articleList = productsMap[obj.name];
                foreach(var articleSet in articleList)
                {
                    int i = ProductsPartsService.articlesMap[articleSet.id] / articleSet.amount;
                    num.Add(i);
                }
                num.Sort();
                return num[0];

        }
    }
}
