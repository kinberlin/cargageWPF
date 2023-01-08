using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using DAO;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace customer.View
{
    /// <summary>
    /// Logique d'interaction pour ProductList.xaml
    /// </summary>
    public partial class ProductList : UserControl
    {

        ProductRepository prorepo;
        List<Product> prolist = new List<Product>();
        public ProductList()
        {
            InitializeComponent();
            prorepo = new ProductRepository();
            prolist = prorepo.Get();
            proDataGrid.ItemsSource = prolist;
            
        }
        public void setDataGrid()
        {
            /*proDataGrid.Columns[0].Header = "Product ID";
            proDataGrid.Columns[1].Header = "Product Image";
            proDataGrid.Columns[2].Header = "Product Price";
            proDataGrid.Columns[3].Header = "Product Quantity";
            proDataGrid.Columns[5].Header = "Product Description";*/
            
            
        }
    }
}
