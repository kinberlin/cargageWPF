using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DAO;
using customer;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace cargageWPF
{
    /// <summary>
    /// Logique d'interaction pour register.xaml
    /// </summary>
    public partial class register : Window
    {
        public int indexOfImage { get; set; } = 3;
        public DispatcherTimer timer;
        public UserRepository userrepo;
        public register()
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(handleTimer);
            timer.Interval = new TimeSpan(0, 0, 10);
            timer.Start();
            welcometxt.Text = "";
            welcometxt.Text = "Prendre vie. ";
            userrepo = new UserRepository();
        }
        private void handleTimer(object sender, EventArgs e)
        {
            var welstr = new string[] { "Prendre vie. ", "Stupefaction.", "Famille et Surprise.", "Nous vous attendons." };
            indexOfImage++;
            if (indexOfImage > 6)
            {
                indexOfImage =3;
            }
            picHolder.Source = new BitmapImage(new Uri(@"pics/" + indexOfImage + ".jpg", UriKind.Relative));
            welcometxt.Text = welstr[(indexOfImage - 3)];
        }

        private void signupbtn_Click(object sender, RoutedEventArgs e)
        {
            var user = new User(0, nomtxt.Text, prenomtxt.Text, logintxt.Text, mtptxt.Text);
            
            try {
                var usr = userrepo.Add(user);
                if(usr != null)
                {
                    var mainw = new customer.MainWindow(usr);
                    mainw.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("An error Occured. Email support at andersontchamba@gmail.com ", "Error");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void SignInBtn_Click(object sender, RoutedEventArgs e)
        {
            connexion co = new connexion();
            this.Close();
            co.Show();
        }
    }
}
