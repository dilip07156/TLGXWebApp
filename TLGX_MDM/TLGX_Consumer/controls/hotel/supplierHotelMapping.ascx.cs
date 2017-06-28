using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLGX_Consumer.MDMSVC;
using TLGX_Consumer.Models;

namespace TLGX_Consumer.controls.hotel
{
    public partial class supplierHotelMapping : System.Web.UI.UserControl
    {

        public Guid Accomodation_ID;
        Controller.AccomodationSVC AccSvc = new Controller.AccomodationSVC();

        MasterDataDAL MasterData = new MasterDataDAL();

        // used for retrieving drop down list attribute values from masters
        lookupAttributeDAL LookupAtrributes = new lookupAttributeDAL();
        public static string AttributeOptionFor = "ProductSupplierMapping";


        protected void GetProductSupplierMapping()
         
        {

            Accomodation_ID = new Guid(Request.QueryString["Hotel_Id"]);
            grdExistingMaps.DataSource = AccSvc.GetAccomodation_ProductMapping(Accomodation_ID, 0, 10);
            grdExistingMaps.DataBind();
                         }





        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                GetProductSupplierMapping();
            }


        }

        protected void btnSearch_Command(object sender, CommandEventArgs e)
        {


            DC_Mapping_ProductSupplier_Search_RQ mySearch = new DC_Mapping_ProductSupplier_Search_RQ();


            mySearch.CityName = txtHotelCity.Text.Trim();


            var res = AccSvc.SearchAccomodation_ProductMapping(mySearch);

            grdPendingMaps.DataSource = res;
            grdPendingMaps.DataBind();





        }
    }
}