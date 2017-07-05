using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLGX_Consumer.Controller;

namespace TLGX_Consumer.controls.hotel
{
    public partial class googlePlacesLookup : System.Web.UI.UserControl
    {
        public Guid Accomodation_ID { get; set; }
        AccomodationSVC _objAcco = new AccomodationSVC();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Accomodation_ID = new Guid(Request.QueryString["Hotel_Id"]);
                if (Accomodation_ID != null)
                {
                    var result = _objAcco.GetHotelDetails(Accomodation_ID);

                    if (result != null && result.Count > 0)
                    {
                        string strLat = Convert.ToString(result[0].Latitude);
                        string strLong = Convert.ToString(result[0].Longitude);
                        string strG_PlaceID = Convert.ToString(result[0].Google_Place_Id);
                        //string strAddress = Convert.ToString(result[0].FullAddress);
                        if (!string.IsNullOrWhiteSpace(strLat))
                            hdnLat.Value = strLat;
                        if (!string.IsNullOrWhiteSpace(strLong))
                            hdnLong.Value = strLong;
                        if (!string.IsNullOrWhiteSpace(strG_PlaceID))
                            hdnG_PlaceID.Value = strG_PlaceID;
                        //if (!string.IsNullOrWhiteSpace(strLat))
                        //    hdnLat.Value = strLat;
                    }
                }
                btnAdd.Attributes.Add("onClick", "return false;");
                BindCategory();
            }
        }

        private void BindCategory()
        {
            ddlPlaceCategory.Items.Add(new ListItem("ATM", "ATM"));
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (Accomodation_ID != null)
            {
                var result = _objAcco.GetHotelDetails(Accomodation_ID);

                if (result != null && result.Count > 0)
                {
                    //float lat = float.Parse(result[0].Latitude);
                    //float lon = float.Parse(result[0].Longitude);
                    //string radius = "400";
                    //string PlaceType = ddlPlaceCategory.SelectedValue;

                    //var res =

                }

            }
        }
    }
}