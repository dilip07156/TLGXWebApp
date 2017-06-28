using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TLGX_Consumer.Models;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Data;
using TLGX_Consumer.App_Code;
using System.Configuration;


namespace TLGX_Consumer.hotels
{
    public partial class manage : System.Web.UI.Page
    {
        public static TLGX_Consumer.controls.hotel.searchHotelascx prePage = new controls.hotel.searchHotelascx();
        MDMSVC.DC_Accomodation_Search_RQ RQParams = new MDMSVC.DC_Accomodation_Search_RQ();
        Controller.AccomodationSVC AccSvc = new Controller.AccomodationSVC();
        lookupAttributeDAL LookupAtrributes = new lookupAttributeDAL();
        MasterDataDAL MasterData = new MasterDataDAL();
        protected void Page_Init(object sender, EventArgs e)
        {
            Guid AccoId;
            if(!Guid.TryParse(Request.QueryString["Hotel_Id"],out AccoId))
            {
                Response.Redirect(Convert.ToString(ConfigurationManager.AppSettings["UnauthorizedUrl"]));
            }
            else
            {
                //For page authroization
                Authorize _obj = new Authorize();
                if (_obj.IsRoleAuthorizedForUrl()) { }
                else
                    Response.Redirect(Convert.ToString(ConfigurationManager.AppSettings["UnauthorizedUrl"]));
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
           // prePage = (controls.hotel.searchHotelascx)PreviousPage.FindControl("searchHotelascx");
        }
    }
}