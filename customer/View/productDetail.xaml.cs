using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DAO;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;

namespace customer.View
{
    /// <summary>
    /// Logique d'interaction pour productDetail.xaml
    /// </summary>
    public partial class productDetail : Window
    {
        public ProductRepository prorepo = new ProductRepository();
        public SaleRespository salerepo = new SaleRespository();
        public Product pro;
        public Sales sale;
        public User user;
        private int qty = 0; 
        public productDetail(Product pro, User usr)
        {
            InitializeComponent();
            this.pro = pro;
            user = usr;
            prodescriptiontxt.Content = pro.description;
            pronametxt.Content = pro.nom;
            propricetxt.Content = pro.prix;
            proImage.Source = LoadImage(pro.image);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (checkQuantity()) {
                if (pro.quantite > qty)
                {
                    sale = new Sales(0, user.id, pro.id, qty, (qty * pro.prix), DateTime.Now);
                    pro.quantite = pro.quantite - qty;
                    salerepo.Add(sale);
                    prorepo.Set(pro);
                    MessageBox.Show("You Order Was Successfull");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Product in store is less than requested quantity");
                }
            }
            else
            {
                MessageBox.Show("Enter a valid Quantity in Quantity Field");
            }
        }
        private bool checkQuantity()
        {
            return int.TryParse(proDetailtxt.Text, out qty);
        }
        private static BitmapImage LoadImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0) return null;
            var image = new BitmapImage();
            using (var mem = new MemoryStream(imageData))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();
            return image;
        }
    }
}
