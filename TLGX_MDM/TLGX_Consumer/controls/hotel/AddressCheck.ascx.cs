using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TLGX_Consumer.controls.hotel
{
    public partial class AddressCheck : System.Web.UI.UserControl
    {
        string _street;
        string _street2;
        string _street3;
        string _street4;
        string _street5;
        string _suburbs;
        string _postcode;
        string _country;
        string _state;
        string _city;
        string _area;
        string _location;

        public string Street
        {
            set
            {
                _street = value;
            }
        }

        public string Street2
        {
            set
            {
                _street2 = value;
            }
        }

        public string Street3
        {
            set
            {
                _street3 = value;
            }
        }

        public string Street4
        {
            set
            {
                _street4 = value;
            }
        }

        public string Street5
        {
            set
            {
                _street5 = value;
            }
        }

        public string Suburbs
        {
            set
            {
                _suburbs = value;
            }
        }

        public string Postcode
        {
            set
            {
                _postcode = value;
            }
        }

        public string Country
        {
            set
            {
                _country = value;
            }
        }

        public string State
        {
            set
            {
                _state = value;
            }
        }

        public string City
        {
            set
            {
                _city = value;
            }
        }

        public string Area
        {
            set
            {
                _area = value;
            }
        }

        public string Location
        {
            set
            {
                _location = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                txtStreet.Text = _street;
                txtStreet2.Text = _street2;
                txtStreet3.Text = _street3;
                txtStreet4.Text = _street4;
                txtStreet5.Text = _street5;
                txtSuburbs.Text = _suburbs;
                txtState.Text = _state;
                txtPostalCode.Text = _postcode;
                txtLocation.Text = _location;
                txtCountry.Text = _country;
                txtCity.Text = _city;
                txtArea.Text = _area;
            }
        }
    }
}
