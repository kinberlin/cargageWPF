using System;
using System.Collections.Generic;
using DAO;
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
using System.Windows.Threading;

namespace customer
{
    /// <summary>
    /// Logique d'interaction pour connexion.xaml
    /// </summary>
    public partial class connexion : Window
    {
        public int indexOfImage { get; set; } = 1;
        public UserRepository userrepository;
        public DispatcherTimer timer;
        public connexion()
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(handleTimer);
            timer.Interval = new TimeSpan(0, 0, 10);
            timer.Start();
            welcometxt.Text = "";
            welcometxt.Text = "Ingenieux.";
            userrepository = new UserRepository();
        }
        private void handleTimer(object sender, EventArgs e)
        {
            var welstr = new string[] { "Ingenieux. ", "Efficace.", "Precis.", "Ponctuel." };
            indexOfImage++;
            if (indexOfImage > 4)
            {
                indexOfImage = 1;
            }
            picHolder.Source = new BitmapImage(new Uri(@"pics/" + indexOfImage + ".jpg", UriKind.Relative));
            welcometxt.Text = welstr[(indexOfImage - 1)];
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            register reg = new register();
            this.Close();
            reg.Show();
        }

        private void connexionbtn_Click(object sender, RoutedEventArgs e)
        {
           
            try
                {
                   var user = userrepository.Get(logintxt.Text, passwordtxt.Text);
                if(user == null)
                {
                MessageBox.Show( "No user mathched these credentials", "Credentials");
                }
                else
                {
                    var file = new FileOperation();
                    file.WriteFile(logintxt.Text);
                    var custmain = new MainWindow();
                    //custmain.ShowDialog();
                    custmain.Show();
                    this.Close();
                }
                }
            catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
        }
    }
}
