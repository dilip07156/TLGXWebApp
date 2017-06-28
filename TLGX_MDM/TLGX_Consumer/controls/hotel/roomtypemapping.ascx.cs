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
    public partial class roomtypemapping : System.Web.UI.UserControl
    {


        public Guid Accomodation_ID;
        Controller.AccomodationSVC AccSvc = new Controller.AccomodationSVC();

        MasterDataDAL MasterData = new MasterDataDAL();

        // used for retrieving drop down list attribute values from masters
        lookupAttributeDAL LookupAtrributes = new lookupAttributeDAL();
        public static string AttributeOptionFor = "RoomTypeMapping";

        protected void GetMappedRoomTypes()
        {
            Accomodation_ID = new Guid(Request.QueryString["Hotel_Id"]);


            var myMaps = new List<DC_Accomodation_SupplierRoomTypeMapping>();
            myMaps= AccSvc.GetAccomodation_RoomTypeMapping(0, 10, Accomodation_ID, Guid.Empty);

            // this code is just there to generate UI for design purposes and ideally should be optimised by someone smarter than me
            if (myMaps != null)
            {
                    ddlSelectBaseSupplier.DataSource = (from r in myMaps select new { r.Supplier_Id, r.SupplierName}).Distinct().ToList();
                    ddlSelectBaseSupplier.DataBind();

                    ddlSelectSupplier.DataSource = ddlSelectBaseSupplier.DataSource;
                    ddlSelectSupplier.DataBind();
            }

            
            grdExistingMaps.DataSource = myMaps;
            grdExistingMaps.DataBind();

            grdUnMappedSupplierRooms.DataSource = myMaps;
            grdUnMappedSupplierRooms.DataBind();

        }


        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                GetMappedRoomTypes();
            }


        }





    }
}