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

namespace TLGX_Consumer.controls.hotel
{
    public partial class passengeroccupancy : System.Web.UI.UserControl
    {
        public Guid Accomodation_ID;
        protected static string AttributeOptionFor = "PassengerOccupancy";

        lookupAttributeDAL LookupAtrributes = new lookupAttributeDAL();
        MasterDataDAL MasterData = new MasterDataDAL();
        Controller.AccomodationSVC AccSvc = new Controller.AccomodationSVC();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Accomodation_ID = new Guid(Request.QueryString["Hotel_Id"]);
                fillOccupancy();
            }
        }

        private void fillOccupancy()
        {
            Accomodation_ID = new Guid(Request.QueryString["Hotel_Id"]);
            grdOccupanyDetail.DataSource = AccSvc.GetHotelPaxOccupancyDetails(Accomodation_ID, Guid.Empty);
            grdOccupanyDetail.DataBind();
        }

        private void filltype(DropDownList obj)
        {
            fillAttributeValues(obj, "RoomType");
        }

        private void fillcategories(DropDownList obj)
        {
            var result = AccSvc.GetRoomDetails(Accomodation_ID, Guid.Empty);

            obj.DataSource = (from r in result
                              where r.IsActive == true
                              select r).ToList();

            obj.DataTextField = "RoomCategory";
            obj.DataValueField = "Accommodation_RoomInfo_Id";
            obj.DataBind();
        }

        private void fillAttributeValues(DropDownList obj, string Attribute_Name)
        {
            obj.DataSource = LookupAtrributes.GetAllAttributeAndValuesByFOR(AttributeOptionFor, Attribute_Name).MasterAttributeValues;
            obj.DataTextField = "AttributeValue";
            obj.DataValueField = "MasterAttributeValue_Id";
            obj.DataBind();
        }

        protected void grdOccupanyDetail_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Guid myRow_Id = Guid.Parse(e.CommandArgument.ToString());

            GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            int index = row.RowIndex;

            if (e.CommandName == "Select")
            {
                //int index = Convert.ToInt32(e.CommandArgument);

                //Guid myRow_Id = Guid.Parse(grdOccupanyDetail.DataKeys[index].Value.ToString());
                Accomodation_ID = new Guid(Request.QueryString["Hotel_Id"]);
                List<MDMSVC.DC_Accommodation_PaxOccupancy> obj = new List<MDMSVC.DC_Accommodation_PaxOccupancy>();
                obj.Add(new MDMSVC.DC_Accommodation_PaxOccupancy
                {
                    Accommodation_PaxOccupancy_Id = myRow_Id,
                    Accommodation_Id = Accomodation_ID,
                    Category = grdOccupanyDetail.Rows[index].Cells[0].Text,
                    RoomType = grdOccupanyDetail.Rows[index].Cells[1].Text
                });

                if (!string.IsNullOrEmpty(grdOccupanyDetail.Rows[index].Cells[2].Text))
                {
                    obj[0].MaxAdults = Convert.ToInt32(grdOccupanyDetail.Rows[index].Cells[2].Text);
                }
                if (!string.IsNullOrEmpty(grdOccupanyDetail.Rows[index].Cells[3].Text))
                {
                    obj[0].ToAgeForExtraBed = Convert.ToInt32(grdOccupanyDetail.Rows[index].Cells[3].Text);
                }
                if (!string.IsNullOrEmpty(grdOccupanyDetail.Rows[index].Cells[4].Text))
                {
                    obj[0].FromAgeForExtraBed = Convert.ToInt32(grdOccupanyDetail.Rows[index].Cells[4].Text);
                }
                if (!string.IsNullOrEmpty(grdOccupanyDetail.Rows[index].Cells[5].Text))
                {
                    obj[0].MaxPaxWithExtraBed = Convert.ToInt32(grdOccupanyDetail.Rows[index].Cells[5].Text);
                }
                if (!string.IsNullOrEmpty(grdOccupanyDetail.Rows[index].Cells[6].Text))
                {
                    obj[0].MaxCNB = Convert.ToInt32(grdOccupanyDetail.Rows[index].Cells[6].Text);
                }
                if (!string.IsNullOrEmpty(grdOccupanyDetail.Rows[index].Cells[7].Text))
                {
                    obj[0].FromAgeForCNB = Convert.ToInt32(grdOccupanyDetail.Rows[index].Cells[7].Text);
                }
                if (!string.IsNullOrEmpty(grdOccupanyDetail.Rows[index].Cells[8].Text))
                {
                    obj[0].ToAgeForCNB = Convert.ToInt32(grdOccupanyDetail.Rows[index].Cells[8].Text);
                }
                if (!string.IsNullOrEmpty(grdOccupanyDetail.Rows[index].Cells[9].Text))
                {
                    obj[0].MaxChild = Convert.ToInt32(grdOccupanyDetail.Rows[index].Cells[9].Text);
                }
                if (!string.IsNullOrEmpty(grdOccupanyDetail.Rows[index].Cells[10].Text))
                {
                    obj[0].FromAgeForCIOR = Convert.ToInt32(grdOccupanyDetail.Rows[index].Cells[10].Text);
                }
                if (!string.IsNullOrEmpty(grdOccupanyDetail.Rows[index].Cells[11].Text))
                {
                    obj[0].ToAgeForCIOR = Convert.ToInt32(grdOccupanyDetail.Rows[index].Cells[11].Text);
                }
                if (!string.IsNullOrEmpty(grdOccupanyDetail.Rows[index].Cells[12].Text))
                {
                    obj[0].MaxPax = Convert.ToInt32(grdOccupanyDetail.Rows[index].Cells[12].Text);
                }
                if (!string.IsNullOrEmpty(grdOccupanyDetail.Rows[index].Cells[13].Text))
                {
                    obj[0].Legacy_Htl_Id = Convert.ToInt32(grdOccupanyDetail.Rows[index].Cells[13].Text);
                }
                if (!string.IsNullOrEmpty(grdOccupanyDetail.Rows[index].Cells[13].Text))
                {
                    obj[0].Accommodation_RoomInfo_Id = new Guid(grdOccupanyDetail.Rows[index].Cells[14].Text);
                }
                frmPassengerOCcupancy.ChangeMode(FormViewMode.Edit);
                frmPassengerOCcupancy.DataSource = obj;
                frmPassengerOCcupancy.DataBind();

                TextBox txtMAxAdults = (TextBox)frmPassengerOCcupancy.FindControl("txtMAxAdults");
                TextBox txtPassengers = (TextBox)frmPassengerOCcupancy.FindControl("txtPassengers");
                TextBox txtFromCXBAge = (TextBox)frmPassengerOCcupancy.FindControl("txtFromCXBAge");
                TextBox txtToCXBAge = (TextBox)frmPassengerOCcupancy.FindControl("txtToCXBAge");
                TextBox txtMaxCXB = (TextBox)frmPassengerOCcupancy.FindControl("txtMaxCXB");
                TextBox txtFromCNBAge = (TextBox)frmPassengerOCcupancy.FindControl("txtFromCNBAge");
                TextBox txtToCNBAge = (TextBox)frmPassengerOCcupancy.FindControl("txtToCNBAge");
                TextBox txtMaxCNB = (TextBox)frmPassengerOCcupancy.FindControl("txtMaxCNB");
                TextBox txtFromCIORAge = (TextBox)frmPassengerOCcupancy.FindControl("txtFromCIORAge");
                TextBox txtToCIORAge = (TextBox)frmPassengerOCcupancy.FindControl("txtToCIORAge");
                TextBox txtMaxCIOR = (TextBox)frmPassengerOCcupancy.FindControl("txtMaxCIOR");
                TextBox txtRoomInfoId = (TextBox)frmPassengerOCcupancy.FindControl("txtRoomInfoId");

                DropDownList ddlRoomCategory = (DropDownList)frmPassengerOCcupancy.FindControl("ddlRoomCategory");
                DropDownList ddlRoomType = (DropDownList)frmPassengerOCcupancy.FindControl("ddlRoomType");

                BindCategory(ddlRoomCategory);
                filltype(ddlRoomType);
                ddlRoomCategory.SelectedIndex = ddlRoomCategory.Items.IndexOf(ddlRoomCategory.Items.FindByText(System.Web.HttpUtility.HtmlDecode(grdOccupanyDetail.Rows[index].Cells[0].Text)));
                ddlRoomType.SelectedIndex = ddlRoomType.Items.IndexOf(ddlRoomType.Items.FindByText(System.Web.HttpUtility.HtmlDecode(grdOccupanyDetail.Rows[index].Cells[1].Text)));
                txtMAxAdults.Text = System.Web.HttpUtility.HtmlDecode(grdOccupanyDetail.Rows[index].Cells[2].Text);
                txtToCXBAge.Text = System.Web.HttpUtility.HtmlDecode(grdOccupanyDetail.Rows[index].Cells[3].Text);
                txtFromCXBAge.Text = System.Web.HttpUtility.HtmlDecode(grdOccupanyDetail.Rows[index].Cells[4].Text);
                txtMaxCXB.Text = System.Web.HttpUtility.HtmlDecode(grdOccupanyDetail.Rows[index].Cells[5].Text);
                txtMaxCNB.Text = System.Web.HttpUtility.HtmlDecode(grdOccupanyDetail.Rows[index].Cells[6].Text);
                txtFromCNBAge.Text = System.Web.HttpUtility.HtmlDecode(grdOccupanyDetail.Rows[index].Cells[7].Text);
                txtToCNBAge.Text = System.Web.HttpUtility.HtmlDecode(grdOccupanyDetail.Rows[index].Cells[8].Text);
                txtMaxCIOR.Text = System.Web.HttpUtility.HtmlDecode(grdOccupanyDetail.Rows[index].Cells[9].Text);
                txtFromCIORAge.Text = System.Web.HttpUtility.HtmlDecode(grdOccupanyDetail.Rows[index].Cells[10].Text);
                txtToCIORAge.Text = System.Web.HttpUtility.HtmlDecode(grdOccupanyDetail.Rows[index].Cells[11].Text);
                txtPassengers.Text = System.Web.HttpUtility.HtmlDecode(grdOccupanyDetail.Rows[index].Cells[12].Text);
                txtRoomInfoId.Text = ddlRoomCategory.SelectedItem.Value;
            }

            else if (e.CommandName.ToString() == "SoftDelete")
            {
                TLGX_Consumer.MDMSVC.DC_Accommodation_PaxOccupancy newObj = new MDMSVC.DC_Accommodation_PaxOccupancy
                {
                    Accommodation_PaxOccupancy_Id = myRow_Id,
                    IsActive = false,
                    Edit_Date = DateTime.Now,
                    Edit_User = System.Web.HttpContext.Current.User.Identity.Name
                };

                if (AccSvc.UpdateHotelPaxOccupancy(newObj))
                {
                    fillOccupancy();
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, "Occupancy has been deleted successfully", BootstrapAlertType.Success);
                };
            }

            else if (e.CommandName.ToString() == "UnDelete")
            {
                TLGX_Consumer.MDMSVC.DC_Accommodation_PaxOccupancy newObj = new MDMSVC.DC_Accommodation_PaxOccupancy
                {
                    Accommodation_PaxOccupancy_Id = myRow_Id,
                    IsActive = true,
                    Edit_Date = DateTime.Now,
                    Edit_User = System.Web.HttpContext.Current.User.Identity.Name
                };

                if (AccSvc.UpdateHotelPaxOccupancy(newObj))
                {
                    fillOccupancy();
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, "Occupancy has been retrived successfully", BootstrapAlertType.Success);
                };

            }
        }

        protected void frmPassengerOCcupancy_ItemCommand(object sender, FormViewCommandEventArgs e)
        {
            TextBox txtMAxAdults = (TextBox)frmPassengerOCcupancy.FindControl("txtMAxAdults");
            TextBox txtPassengers = (TextBox)frmPassengerOCcupancy.FindControl("txtPassengers");
            TextBox txtFromCXBAge = (TextBox)frmPassengerOCcupancy.FindControl("txtFromCXBAge");
            TextBox txtToCXBAge = (TextBox)frmPassengerOCcupancy.FindControl("txtToCXBAge");
            TextBox txtMaxCXB = (TextBox)frmPassengerOCcupancy.FindControl("txtMaxCXB");
            TextBox txtFromCNBAge = (TextBox)frmPassengerOCcupancy.FindControl("txtFromCNBAge");
            TextBox txtToCNBAge = (TextBox)frmPassengerOCcupancy.FindControl("txtToCNBAge");
            TextBox txtMaxCNB = (TextBox)frmPassengerOCcupancy.FindControl("txtMaxCNB");
            TextBox txtFromCIORAge = (TextBox)frmPassengerOCcupancy.FindControl("txtFromCIORAge");
            TextBox txtToCIORAge = (TextBox)frmPassengerOCcupancy.FindControl("txtToCIORAge");
            TextBox txtMaxCIOR = (TextBox)frmPassengerOCcupancy.FindControl("txtMaxCIOR");
            TextBox txtRoomInfoId = (TextBox)frmPassengerOCcupancy.FindControl("txtRoomInfoId");
            DropDownList ddlRoomCategory = (DropDownList)frmPassengerOCcupancy.FindControl("ddlRoomCategory");
            DropDownList ddlRoomType = (DropDownList)frmPassengerOCcupancy.FindControl("ddlRoomType");

            Accomodation_ID = new Guid(Request.QueryString["Hotel_Id"]);

            if (e.CommandName == "AddOccupancy")
            {
                MDMSVC.DC_Accommodation_PaxOccupancy newObj = new MDMSVC.DC_Accommodation_PaxOccupancy
                {
                    Accommodation_PaxOccupancy_Id = Guid.NewGuid(),
                    Legacy_Htl_Id = AccSvc.GetLegacyHotelId(Accomodation_ID),
                    Accommodation_Id = Accomodation_ID,
                    Accommodation_RoomInfo_Id = new Guid(ddlRoomCategory.SelectedItem.Value),
                    Category = ddlRoomCategory.SelectedItem.Text,
                    RoomType = ddlRoomType.SelectedItem.Text,
                    MaxAdults = Convert.ToInt32(txtMAxAdults.Text),
                    MaxPax = Convert.ToInt32(txtPassengers.Text),
                    FromAgeForExtraBed = Convert.ToInt32(txtFromCXBAge.Text),
                    ToAgeForExtraBed = Convert.ToInt32(txtToCXBAge.Text),
                    MaxPaxWithExtraBed = Convert.ToInt32(txtMaxCXB.Text),
                    MaxCNB = Convert.ToInt32(txtMaxCNB.Text),
                    FromAgeForCNB = Convert.ToInt32(txtFromCNBAge.Text),
                    ToAgeForCNB = Convert.ToInt32(txtToCNBAge.Text),
                    MaxChild = Convert.ToInt32(txtMaxCIOR.Text),
                    FromAgeForCIOR = Convert.ToInt32(txtFromCIORAge.Text),
                    ToAgeForCIOR = Convert.ToInt32(txtToCIORAge.Text),
                    Create_Date = DateTime.Now,
                    Create_User = System.Web.HttpContext.Current.User.Identity.Name,
                    IsActive = true
                };
                if (AccSvc.AddAccommodationPaxOccupancyDetail(newObj))
                {
                    frmPassengerOCcupancy.DataBind();
                    fillOccupancy();

                    //ddlRoomCategory = (DropDownList)frmPassengerOCcupancy.FindControl("ddlRoomCategory");
                    //fillcategories(ddlRoomCategory);

                    //ddlRoomType = (DropDownList)frmPassengerOCcupancy.FindControl("ddlRoomType");
                    //filltype(ddlRoomType);
                    hdnFlag.Value = "true";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), DateTime.Today.Ticks.ToString(),"alert('Hi');", true);
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, "Occupancy has been added successfully", BootstrapAlertType.Success);
                }
                else
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, "Error Occurred", BootstrapAlertType.Warning);
            }
            else if (e.CommandName == "UpdateOccupancy")
            {
                Accomodation_ID = new Guid(Request.QueryString["Hotel_Id"]);
                Guid myRow_Id = Guid.Parse(grdOccupanyDetail.SelectedDataKey.Value.ToString());
                var result = AccSvc.GetHotelPaxOccupancyDetails(Accomodation_ID, myRow_Id);
                if (result.Count > 0)
                {
                    MDMSVC.DC_Accommodation_PaxOccupancy newObj = new MDMSVC.DC_Accommodation_PaxOccupancy
                    {
                        Accommodation_PaxOccupancy_Id = myRow_Id,
                        Accommodation_Id = Accomodation_ID,
                        Legacy_Htl_Id = AccSvc.GetLegacyHotelId(Accomodation_ID),
                        Accommodation_RoomInfo_Id = new Guid(ddlRoomCategory.SelectedItem.Value),
                        Category = ddlRoomCategory.SelectedItem.Text,
                        RoomType = ddlRoomType.SelectedItem.Text,
                        MaxAdults = Convert.ToInt32(txtMAxAdults.Text),
                        MaxPax = Convert.ToInt32(txtPassengers.Text),
                        FromAgeForExtraBed = Convert.ToInt32(txtFromCXBAge.Text),
                        ToAgeForExtraBed = Convert.ToInt32(txtToCXBAge.Text),
                        MaxPaxWithExtraBed = Convert.ToInt32(txtMaxCXB.Text),
                        MaxCNB = Convert.ToInt32(txtMaxCNB.Text),
                        FromAgeForCNB = Convert.ToInt32(txtFromCNBAge.Text),
                        ToAgeForCNB = Convert.ToInt32(txtToCNBAge.Text),
                        MaxChild = Convert.ToInt32(txtMaxCIOR.Text),
                        FromAgeForCIOR = Convert.ToInt32(txtFromCIORAge.Text),
                        ToAgeForCIOR = Convert.ToInt32(txtToCIORAge.Text),
                        Edit_Date = DateTime.Now,
                        Edit_User = System.Web.HttpContext.Current.User.Identity.Name,
                        IsActive = true
                    };
                    if (AccSvc.UpdateHotelPaxOccupancy(newObj))
                    {
                        Accomodation_ID = new Guid(Request.QueryString["Hotel_Id"]);
                        frmPassengerOCcupancy.ChangeMode(FormViewMode.Insert);
                        frmPassengerOCcupancy.DataBind();

                        fillOccupancy();

                        //ddlRoomCategory = (DropDownList)frmPassengerOCcupancy.FindControl("ddlRoomCategory");
                        //fillcategories(ddlRoomCategory);

                        //ddlRoomType = (DropDownList)frmPassengerOCcupancy.FindControl("ddlRoomType");
                        //filltype(ddlRoomType);
                        hdnFlag.Value = "true";
                        BootstrapAlert.BootstrapAlertMessage(dvMsg, "Occupancy has been updated successfully", BootstrapAlertType.Success);
                    }
                    else
                        BootstrapAlert.BootstrapAlertMessage(dvMsg, "Error Occurred", BootstrapAlertType.Warning);
                }
            }
        }

        // builds the additional header row onto the grid
        protected void grdOccupanyDetail_DataBound(object sender, EventArgs e)
        {
            //var myGridView = (GridView)sender;
            //if (myGridView.Controls.Count > 0)
            //    AddSuperHeader(myGridView);
        }

        private static void AddSuperHeader(GridView gridView)
        {
            var myTable = (Table)gridView.Controls[0];
            var myNewRow = new GridViewRow(0, -1, DataControlRowType.Header, DataControlRowState.Normal);
            myNewRow.Cells.Add(MakeCell("", 3));
            myNewRow.Cells.Add(MakeCell("Extra Bed", 3));
            myNewRow.Cells.Add(MakeCell("Child No Bed", 3));
            myNewRow.Cells.Add(MakeCell("Child In Own Room", 3));
            myNewRow.Cells.Add(MakeCell("", 2));

            myTable.Rows.AddAt(0, myNewRow);
        }

        private static TableHeaderCell MakeCell(string text = null, int span = 1)
        {
            return new TableHeaderCell() { ColumnSpan = span, Text = text ?? string.Empty, CssClass = "table-header" };
        }

        protected void grdOccupanyDetail_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.DataItem != null)
            {
                LinkButton btnDelete = (LinkButton)e.Row.FindControl("btnDelete");
                if (btnDelete.CommandName == "UnDelete")
                {
                    e.Row.Font.Strikeout = true;
                }
            }
        }

        protected void ddlRoomCategory_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnNewCreate_Click(object sender, EventArgs e)
        {
            hdnFlag.Value = "false";
            frmPassengerOCcupancy.ChangeMode(FormViewMode.Insert);
            frmPassengerOCcupancy.DataBind();
            DropDownList ddlRoomCategory = (DropDownList)frmPassengerOCcupancy.FindControl("ddlRoomCategory");
            DropDownList ddlRoomType = (DropDownList)frmPassengerOCcupancy.FindControl("ddlRoomType");
            if (Accomodation_ID == Guid.Empty)
            {
                Accomodation_ID = new Guid(Request.QueryString["Hotel_Id"]);
            }
            BindCategory(ddlRoomCategory);
            filltype(ddlRoomType);
        }
        private void BindCategory(DropDownList obj)
        {
            var result = AccSvc.GetRoomDetails_RoomCategory(Accomodation_ID);
            obj.DataSource = result;
            obj.DataTextField = "RoomCategory";
            obj.DataValueField = "Accommodation_RoomInfo_Id";
            obj.DataBind();
        }
    }
}