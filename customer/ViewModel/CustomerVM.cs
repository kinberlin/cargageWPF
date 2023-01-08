using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using customer.Model;
using DAO;

namespace customer.ViewModel
{
    class CustomerVM : Utilities.ViewModelBase
    {
        private readonly PageModel _pageModel;
        private FileOperation fop = new FileOperation();
        private UserRepository userrepo = new UserRepository();
        public int CustomerID
        {
            get { return _pageModel.CustomerCount; }
            set { _pageModel.CustomerCount = value; OnPropertyChanged(); }
        }
        public string CustomerEmail
        {
            get { return _pageModel.Email; }
            set { _pageModel.Email = value; OnPropertyChanged(); }
        }
        public string CustomerNames
        {
            get { return _pageModel.Names; }
            set { _pageModel.Names = value; OnPropertyChanged(); }
        }

        public CustomerVM()
        {
            User user = new User(100528, "Drystan", "Tchamba", "tdrytan@yahoo.com", "123456");
            user = userrepo.Get(fop.ReadFile());
            _pageModel = new PageModel();
            CustomerID = user.id; // 100528;
            CustomerNames = user.nom + " "+user.prenom;//"Drystan Tchamba";
            CustomerEmail = user.login; //"tdrytan@yahoo.com";
        }
    }
}
