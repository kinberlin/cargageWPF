using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace customer
{
    /// <summary>
    /// Logique d'interaction pour home.xaml
    /// </summary>
    public partial class home : Window
    {
        public int indexOfImage { get; set; } = 1;
        public DispatcherTimer timer;
        public home()
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(handleTimer);
            timer.Interval = new TimeSpan(0, 0, 10);
            timer.Start();
            welcometxt.Text = "";
            welcometxt.Text = "Votre voiture n'a que besoin de ressembler a vos rêves. Nous nous en chargeons.";
        }

        private void handleTimer(object sender, EventArgs e)
        {
            var welstr = new string[] { "Votre voiture n'a que besoin de ressembler a vos rêves. Nous nous en chargeons. ", "Nous sommes professionelle et toujours prêt a vous servir. Faites nous confiance.", "Commencer dès maintenant avec nous, s'est entamer la réalisation de ses rêves.", "Vous souhaitez volé ? Nous vous transformons en ange et vous donnons des ailes." };
            indexOfImage++;
            if (indexOfImage > 4)
            {
                indexOfImage = 1;
            }
            picHolder.Source = new BitmapImage(new Uri(@"pics/" + indexOfImage + ".jpg", UriKind.Relative));
            welcometxt.Text = welstr[(indexOfImage - 1)];
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            indexOfImage--;
            if (indexOfImage < 1)
            {
                indexOfImage = 4;
            }

            picHolder.Source = new BitmapImage(new Uri(@"pics/" + indexOfImage + ".jpg", UriKind.Relative));
        }

        private void Next(object sender, RoutedEventArgs e)
        {
        }

        private void connexionBtn_Click(object sender, RoutedEventArgs e)
        {
            connexion Connexion = new connexion();
            Connexion.Show();
            this.Close();
        }
    }
}
