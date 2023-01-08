using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using customer.Model;
using DAO;

namespace customer.ViewModel
{
    class ProductListVM : Utilities.ViewModelBase
    {
        private readonly PageModel _pageModel;
        private ProductRepository productrepo = new ProductRepository();
        public int category
        {
            get { return _pageModel.cat; }
            set { _pageModel.cat = value; OnPropertyChanged(); }
        }

        public List<Product> products
        {
            get { return _pageModel.products; }
            set { _pageModel.products = value; OnPropertyChanged(); }
        }

        public ProductListVM()
        {
            _pageModel = new PageModel();
            products = productrepo.Get();
        }
    }
}
