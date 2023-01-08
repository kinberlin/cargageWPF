using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using customer.Model;

namespace customer.ViewModel
{
    class ShipmentVM : Utilities.ViewModelBase
    {
        private readonly PageModel _pageModel;
        public string ShipmentTracking
        {
            get { return _pageModel.ShipmentDelivery; }
            set { _pageModel.ShipmentDelivery = value; OnPropertyChanged(); }
        }

        public ShipmentVM()
        {
            _pageModel = new PageModel();
            string time = DateTime.Now.ToString("H:mm");
            ShipmentTracking = time;
        }
    }
}
