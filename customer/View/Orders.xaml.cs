using DAO;
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

namespace customer.View
{
    /// <summary>
    /// Interaction logic for Orders.xaml
    /// </summary>
    public partial class Orders : UserControl
    {
        private FileOperation fop = new FileOperation();
        private UserRepository userrepo = new UserRepository();
        private SaleRespository salerepo = new SaleRespository();
        private ProductRepository prorepo = new ProductRepository();
        private User user;
        List<Sales> saleList;
        Product pro;
        int index;
        public Orders()
        {
            InitializeComponent();
            user = userrepo.Get(fop.ReadFile());
            saleList = salerepo.GetByUser(user.id);
            index = 0;
            navigate();
        }

        private void nextBtn_Click(object sender, RoutedEventArgs e)
        {
            if((index+1) < saleList.Count())
            {
                index++;
                navigate();
            }
        }

        public void navigate()
        {
            if (saleList.Count > 0)
            {
                var sl = saleList[index];
                pro = prorepo.Get(sl.product);
                prodescriptiontxt.Content = pro.description;
                pronomtxt.Content = pro.nom;
                proImage.Source = LoadImage(pro.image);
                propricetxt.Content = pro.prix.ToString() + " XAF";
                saleqtytxt.Content = sl.quantity;
                saleIdtxt.Text = "0000" + sl.id.ToString();
                saleDatetxt.Text = sl.sale_date.ToString();
                salecosttxt.Content = sl.total.ToString() + " XAF";
            }
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

        private void backtBtn_Click(object sender, RoutedEventArgs e)
        {
            if ( (index-1) >=0)
            {
                index--;
                navigate();
            }
        }
    }
}
