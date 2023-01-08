using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace customer
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public User user;
        public MainWindow(User user)
        {
            InitializeComponent();
            this.user = user;
        }
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CloseApp_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
