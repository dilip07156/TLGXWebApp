using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLGX_Consumer.Controller;
using TLGX_Consumer.Models;

namespace TLGX_Consumer.controls.hotel
{
    public partial class googlePlacesLookup : System.Web.UI.UserControl
    {
        public Guid Accomodation_ID { get; set; }
        AccomodationSVC _objAcco = new AccomodationSVC();
        lookupAttributeDAL LookupAtrributes = new lookupAttributeDAL();


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
                        string strAddress = Convert.ToString(result[0].FullAddress);
                        if (!string.IsNullOrWhiteSpace(strLat))
                            hdnLat.Value = strLat;
                        if (!string.IsNullOrWhiteSpace(strLong))
                            hdnLong.Value = strLong;
                        if (!string.IsNullOrWhiteSpace(strG_PlaceID))
                            hdnG_PlaceID.Value = strG_PlaceID;
                        if (!string.IsNullOrWhiteSpace(strAddress))
                            hdnAddress.Value = strAddress;
                        ddlNoOfItem.SelectedValue = "5";
                        ddlRadius.SelectedValue = "2000";
                    }
                }
                btnAdd.Attributes.Add("onClick", "return false;");
                lnkbtnMapSelected.Attributes.Add("onClick", "return false;");
                lnkbtnMapAll.Attributes.Add("onClick", "return false;");
                BindCategory();
            }
        }

        private void BindCategory()
        {
            ddlPlaceCategory.Items.Clear();
            ddlPlaceCategory.DataSource = LookupAtrributes.GetAllAttributeAndValuesByFOR("InAndAround", "PlaceCategoryGoogle").MasterAttributeValues;
            ddlPlaceCategory.DataTextField = "AttributeValue";
            ddlPlaceCategory.DataValueField = "MasterAttributeValue_Id";
            ddlPlaceCategory.DataBind();
            ddlPlaceCategory.Items.Insert(0, new ListItem("select", "0"));
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
        public event EventHandler StatusUpdated;

        private void FunctionThatRaisesEvent()
        {
            //Null check makes sure the main page is attached to the event
            if (this.StatusUpdated != null)
                this.StatusUpdated(this, new EventArgs());
        }

        protected void lnkbtnMapSelected_Click(object sender, EventArgs e)
        {
            FunctionThatRaisesEvent();
        }

        protected void lnkbtnMapAll_Click(object sender, EventArgs e)
        {
            FunctionThatRaisesEvent();
        }
    }
}