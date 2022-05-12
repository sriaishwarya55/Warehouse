using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.Services;
using WpfApp1.Properties;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Deserializing json to .net objects
        static Warehouse productObj = ProductsService.GetProductsObject();
        static Inventory inventoryObj = ProductsPartsService.GetProductPartsObject();

        //Creating Mappings for Articles and products
        Dictionary<String, int> articleMap = ProductsPartsService.CreateProductPartsMapping(inventoryObj);
        Dictionary<String, List<ArticleSet>> productMap = ProductsService.CreateProductsMapping(productObj);

        public MainWindow()
        {
            this.InitializeComponent();
            products.Visibility = Visibility.Collapsed;
            dataGrid.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Displays list of articles
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Get_Inventory_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.AutoGenerateColumns = false;
            dataGrid.ItemsSource = null;
            dataGrid.Visibility = Visibility.Visible;
            products.Visibility = Visibility.Collapsed;

            var inventoryList = new List<ProductParts>();
            foreach (var obj in articleMap)
            {
                inventoryList.Add(new ProductParts() { id = obj.Key, stock = obj.Value });

            }

            dataGrid.ItemsSource = inventoryList;


            //The below code can be used for storing and retrieving article and product details

            //var cs = "Host=localhost;Username=postgres;Password=wip;Database=Ikea";

            //using var con = new NpgsqlConnection(cs);
            //con.Open();

            //var sql = "SELECT version()";

            //using var cmd = new NpgsqlCommand(sql, con);

            //var version = cmd.ExecuteScalar().ToString();
            //MessageBox.Show(version);

            //cmd.CommandText = "CREATE TABLE  if not exists cars(id SERIAL PRIMARY KEY, name VARCHAR(255), stock INT)";
            //cmd.ExecuteNonQuery();

            //foreach (ProductParts obj in inventoryObj.inventory)
            //{
            //    inventoryList.Add(new ProductParts() { id = obj.id, name = obj.name, stock = obj.stock });
            //    cmd.CommandText = "insert into cars(id,name,stock) values('" + obj.id + "','" + obj.name + "','" + obj.stock+"')";
            //    cmd.ExecuteNonQuery();

            //}

        }

        /// <summary>
        /// Displays list of products
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Products_List_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = null;
            dataGrid.CanUserAddRows = false;
            products.AutoGenerateColumns = false;
            dataGrid.Visibility = Visibility.Collapsed;
            products.Visibility = Visibility.Visible;

            var productsList = new List<Products>();
            foreach (Products obj in productObj.products)
            {
                productsList.Add(
                    new Products()
                    {
                        name = obj.name,
                        availableStock = ProductsService.CalculateProductsStock(obj)

                    });
            }
            products.ItemsSource = productsList;

        }

        /// <summary>
        /// Performs Sell operation on products
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Sell(object sender, RoutedEventArgs e)
        {
            int count = 0;
            object nameOfProduct = ((Button)sender).CommandParameter;
            List<ArticleSet> articleList = ProductsService.productsMap[nameOfProduct.ToString()];
            foreach (var article in articleList)
            {
                if (ProductsPartsService.articlesMap[article.id] >= article.amount)
                {
                    count++;

                    // Check if all the articles are available for a product
                    if (count == articleList.Count)
                    {
                        SubtractArticlesFromInventory(articleList);
                        MessageBox.Show("Sold");

                    }
                }
                else
                {
                    MessageBox.Show("Cannot perform Sell operation");
                    break;
                }

            }
        }

        private void SubtractArticlesFromInventory(List<ArticleSet> _articleList)
        {
            int articleStock = 0;
            foreach (var article in _articleList)
            {
                articleStock = ProductsPartsService.articlesMap[article.id] - article.amount;
                articleMap[article.id] = articleStock;
            }
        }

    }
}
