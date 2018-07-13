using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TLGX_Consumer.App_Code;

namespace TLGX_Consumer.admin
{
    public partial class manageDistributionLayer : System.Web.UI.Page
    {
        Controller.MasterDataSVCs MasterSvc = new Controller.MasterDataSVCs();


        protected void Page_Load(object sender, EventArgs e)
        {
            dvMsg.Style.Add("display", "none");
            //For page authroization 
            Authorize _obj = new Authorize();
            if (_obj.IsRoleAuthorizedForUrl()) { }
            else
                Response.Redirect(Convert.ToString(ConfigurationManager.AppSettings["UnauthorizedUrl"]));

            if (!IsPostBack)
            {
                GetStaticHotelData();
                GetUpdatedDistributionLog();
                GetMLDataTransferStatus();

            }
        }



        protected void GetStaticHotelData()
        {
            dvMsg.Style.Add("display", "none");
            MDMSVC.DC_SupplierEntity RQ = new MDMSVC.DC_SupplierEntity();
            var result = MasterSvc.GetStaticHotel(RQ);

            if (result != null)
            {
                grdSupplierEntity.DataSource = result;
                grdSupplierEntity.DataBind();

            }
            else
            {
                grdSupplierEntity.DataSource = null;
                grdSupplierEntity.DataBind();

            }


            foreach (GridViewRow rowitem in grdSupplierEntity.Rows)
            {
                if (rowitem.Cells[4].Text == "Running" || rowitem.Cells[4].Text == "Scheduled")
                {
                    foreach (GridViewRow row in grdSupplierEntity.Rows)
                    {
                        LinkButton refreshButton = (LinkButton)(row.FindControl("btnUpdate"));
                        refreshButton.Enabled = false;
                        refreshButton.BackColor = System.Drawing.Color.Red;
                    }
                    break;
                }
                else
                {
                    foreach (GridViewRow row in grdSupplierEntity.Rows)
                    {
                        LinkButton refreshButton = (LinkButton)(row.FindControl("btnUpdate"));
                        refreshButton.Enabled = true;
                        refreshButton.BackColor = System.Drawing.Color.Green;
                    }

                }

            }
        }

        //For refreshing distribution log 
        protected void GetUpdatedDistributionLog()
        {

            MDMSVC.DC_RefreshDistributionDataLog RQ = new MDMSVC.DC_RefreshDistributionDataLog();
            var res = MasterSvc.GetRefreshDistributionLog(RQ);

            LastUpdatedCountryMaster.Text = (res.Where(x => x.Element == "Country" && x.Type == "Master").Select(y => y.Create_Date).FirstOrDefault()).ToString();
            LastUpdatedCountryMapping.Text = (res.Where(x => x.Element == "Country" && x.Type == "Mapping").Select(y => y.Create_Date).FirstOrDefault()).ToString();

            LastUpdatedCityMapping.Text = (res.Where(x => x.Element == "City" && x.Type == "Mapping").Select(y => y.Create_Date).FirstOrDefault()).ToString();
            LastUpdatedCityMaster.Text = (res.Where(x => x.Element == "City" && x.Type == "Master").Select(y => y.Create_Date).FirstOrDefault()).ToString();

            LastUpdatedHotelMapping.Text = (res.Where(x => x.Element == "Hotel" && x.Type == "Mapping").Select(y => y.Create_Date).FirstOrDefault()).ToString();
            LastUpdatedHotelMappingLite.Text = (res.Where(x => x.Element == "Hotel" && x.Type == "MappingLite").Select(y => y.Create_Date).FirstOrDefault()).ToString();

            LastUpdatedActivityMapping.Text = (res.Where(x => x.Element == "Activities" && x.Type == "Mapping").Select(y => y.Create_Date).FirstOrDefault()).ToString();

            LastUpdatedSupplierMapping.Text = (res.Where(x => x.Element == "Supplier" && x.Type == "Master").Select(y => y.Create_Date).FirstOrDefault()).ToString();

            LastUpdatedPortMaster.Text = (res.Where(x => x.Element == "Port" && x.Type == "Master").Select(y => y.Create_Date).FirstOrDefault()).ToString();

            LastUpdatedStateMaster.Text = (res.Where(x => x.Element == "State" && x.Type == "Master").Select(y => y.Create_Date).FirstOrDefault()).ToString();

            LastUpdatedZoneMaster.Text = (res.Where(x => x.Element == "Zone" && x.Type == "Master").Select(y => y.Create_Date).FirstOrDefault()).ToString();

            LastUpdatedZoneTypeMaster.Text = (res.Where(x => x.Element == "ZoneType" && x.Type == "Master").Select(y => y.Create_Date).FirstOrDefault()).ToString();


        }
        #region ==Geography Masters&Mapping
        #region == Country Master&Mapping
        protected void btnRefreshCountryMaster_Click(object sender, EventArgs e)
        {
            var res = MasterSvc.RefreshCountryMaster(Guid.Empty);
            if (res != null)
            {
                BootstrapAlert.BootstrapAlertMessage(dvMsg, res.StatusMessage, (BootstrapAlertType)res.StatusCode);
                GetUpdatedDistributionLog();
            }
            else
            {
                BootstrapAlert.BootstrapAlertMessage(dvMsg, "Country Master Sync failed.", BootstrapAlertType.Danger);
            }

        }
        protected void btnRefreshCountryMapping_Click(object sender, EventArgs e)
        {
            var res = MasterSvc.RefreshCountryMapping(Guid.Empty);
            if (res != null)
            {
                BootstrapAlert.BootstrapAlertMessage(dvMsg, res.StatusMessage, (BootstrapAlertType)res.StatusCode);
                GetUpdatedDistributionLog();
            }
            else
            {
                BootstrapAlert.BootstrapAlertMessage(dvMsg, "Country Mapping Sync failed.", BootstrapAlertType.Danger);
            }
        }
        #endregion

        #region == City Master&Mapping
        protected void btnRefreshCityMaster_Click(object sender, EventArgs e)
        {
            var res = MasterSvc.RefreshCityMaster(Guid.Empty);
            if (res != null)
            {
                BootstrapAlert.BootstrapAlertMessage(dvMsg, res.StatusMessage, (BootstrapAlertType)res.StatusCode);
                GetUpdatedDistributionLog();
            }
            else
            {
                BootstrapAlert.BootstrapAlertMessage(dvMsg, "City Mapping Sync failed.", BootstrapAlertType.Danger);
            }
        }
        protected void btnRefreshCityMapping_Click(object sender, EventArgs e)
        {
            var res = MasterSvc.RefreshCityMapping(Guid.Empty);
            if (res != null)
            {
                BootstrapAlert.BootstrapAlertMessage(dvMsg, res.StatusMessage, (BootstrapAlertType)res.StatusCode);
                GetUpdatedDistributionLog();
            }
            else
            {
                BootstrapAlert.BootstrapAlertMessage(dvMsg, "City Mapping Sync failed.", BootstrapAlertType.Danger);
            }
        }
        #endregion

        #region== Port & state Master
        protected void btnRefreshPortMaster_Click(object sender, EventArgs e)
        {
            var res = MasterSvc.RefreshPortMaster(Guid.Empty);
            if (res != null)
            {
                BootstrapAlert.BootstrapAlertMessage(dvMsg, res.StatusMessage, (BootstrapAlertType)res.StatusCode);
                GetUpdatedDistributionLog();
            }
            else
            {
                BootstrapAlert.BootstrapAlertMessage(dvMsg, "Port Master Sync failed.", BootstrapAlertType.Danger);
            }

        }

        protected void btnRefreshStateMaster_Click(object sender, EventArgs e)
        {
            var res = MasterSvc.RefreshStateMaster(Guid.Empty);
            if (res != null)
            {
                BootstrapAlert.BootstrapAlertMessage(dvMsg, res.StatusMessage, (BootstrapAlertType)res.StatusCode);
                GetUpdatedDistributionLog();
            }
            else
            {
                BootstrapAlert.BootstrapAlertMessage(dvMsg, "Port Master Sync failed.", BootstrapAlertType.Danger);
            }
        }
        #endregion

        #region == Zone &zoneType
        protected void btnRefreshZoneMaster_Click(object sender, EventArgs e)
        {
            LoadGeographyData(new MDMSVC.DC_MongoDbSyncRQ { Element = "Zone", Type = "Master" });
        }

        protected void btnRefreshZoneTypeMaster_Click(object sender, EventArgs e)
        {
            LoadGeographyData(new MDMSVC.DC_MongoDbSyncRQ { Element = "ZoneType", Type = "Master" });
        }
        protected void LoadGeographyData(MDMSVC.DC_MongoDbSyncRQ RQ)
        {
            var res = MasterSvc.SyncGeographyData(RQ);
            if (res != null)
            {
                BootstrapAlert.BootstrapAlertMessage(dvMsg, res.StatusMessage, (BootstrapAlertType)res.StatusCode);
            }
            else
            {
                BootstrapAlert.BootstrapAlertMessage(dvMsg, "Data Sync failed.", BootstrapAlertType.Danger);
            }
        }
        #endregion

        protected void btnRefreshSupplyMaster_Click(object sender, EventArgs e)
        {
            var res = MasterSvc.RefreshSupplyMaster(Guid.Empty);
            if (res != null)
            {
                BootstrapAlert.BootstrapAlertMessage(dvMsg, res.StatusMessage, (BootstrapAlertType)res.StatusCode);
                GetUpdatedDistributionLog();
            }
            else
            {
                BootstrapAlert.BootstrapAlertMessage(dvMsg, "Supply Master Sync failed.", BootstrapAlertType.Danger);
            }
        }
        #endregion

        #region ProductMaster & mapping

        #region == Hotel Master & mappingLite
        protected void btnRefreshHotelMapping_Click(object sender, EventArgs e)
        {
            var res = MasterSvc.RefreshHotelMapping(Guid.Empty);
            if (res != null)
            {
                BootstrapAlert.BootstrapAlertMessage(dvMsg, res.StatusMessage, (BootstrapAlertType)res.StatusCode);
                GetUpdatedDistributionLog();
            }
            else
            {
                BootstrapAlert.BootstrapAlertMessage(dvMsg, "Hotel Mapping Sync failed.", BootstrapAlertType.Danger);
            }
        }
        protected void btnRefreshHotelMappingLite_Click(object sender, EventArgs e)
        {
            var res = MasterSvc.RefreshHotelMappingLite(Guid.Empty);
            if (res != null)
            {
                BootstrapAlert.BootstrapAlertMessage(dvMsg, res.StatusMessage, (BootstrapAlertType)res.StatusCode);
                GetUpdatedDistributionLog();
            }
            else
            {
                BootstrapAlert.BootstrapAlertMessage(dvMsg, "Hotel MappingLite Sync failed.", BootstrapAlertType.Danger);
            }
        }
        #endregion

        #region = Acitvity Mapping
        protected void btnRefreshActivityMapping_Click(object sender, EventArgs e)
        {
            var res = MasterSvc.RefreshActivityMapping(Guid.Empty);
            if (res != null)
            {
                BootstrapAlert.BootstrapAlertMessage(dvMsg, res.StatusMessage, (BootstrapAlertType)res.StatusCode);
                GetUpdatedDistributionLog();
            }
            else
            {
                BootstrapAlert.BootstrapAlertMessage(dvMsg, "Activity Mapping Sync failed.", BootstrapAlertType.Danger);
            }
        }
        #endregion

        #endregion
        
        #region == Supplier Static Data
        protected void grdSupplierEntity_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "refresh")
            {
                dvMsg.Style.Add("display", "none");
                Guid myRowId = Guid.Parse(e.CommandArgument.ToString());

                var res = MasterSvc.RefreshStaticHotel(Guid.Empty, myRowId);

                if (res != null)
                {
                    GetUpdatedDistributionLog();
                    GetStaticHotelData();
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, res.StatusMessage, (BootstrapAlertType)res.StatusCode);
                }
                else
                {
                    BootstrapAlert.BootstrapAlertMessage(dvMsg, "Supplier Staic Hotel Sync failed.", BootstrapAlertType.Danger);
                }

            }
        }

        protected void grdSupplierEntity_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.DataItem != null)

            {
                //coding for progress bar
                int intTotalCount = ((TLGX_Consumer.MDMSVC.DC_SupplierEntity)e.Row.DataItem).TotalCount ?? 0;
                int intMongoPushCount = ((TLGX_Consumer.MDMSVC.DC_SupplierEntity)e.Row.DataItem).MongoPushCount ?? 0;

                double progressWidth = intTotalCount != 0 ? Math.Round((Convert.ToDouble(intMongoPushCount) / Convert.ToDouble(intTotalCount)) * 100.0, 2) : 0.0;

                //find div in row
                HtmlControl divCompleted = (HtmlControl)e.Row.FindControl("divCompleted");
                Label lblcompleted = (Label)e.Row.FindControl("lblcompleted");
                if (divCompleted != null && progressWidth > 0)
                {
                    divCompleted.Attributes.Remove("aria-valuenow");
                    divCompleted.Attributes.Add("aria-valuenow", progressWidth.ToString());

                    divCompleted.Attributes.Remove("style");
                    divCompleted.Attributes.Add("style", "width: " + progressWidth.ToString() + "%");

                    if (lblcompleted != null)
                        lblcompleted.Text = Convert.ToString(progressWidth) + "%";

                }

            }
        }


        protected void TimerStaticData_Tick(object sender, EventArgs e)
        {

            foreach (GridViewRow rowitem in grdSupplierEntity.Rows)
            {
                if (rowitem.Cells[4].Text == "Running" || rowitem.Cells[4].Text == "Scheduled")
                {
                    //foreach (GridViewRow row in grdSupplierEntity.Rows)
                    //{
                    GetStaticHotelData();
                    //}
                    // break;
                }
            }


        }
        #endregion

        #region == DATA trans to ML  Button Event click
        private void GetMLDataTransferStatus()
        {
            try
            {
                //Get All 
                var res = MasterSvc.GetMLDataApiTransferStatus();
                foreach (var item in res)
                {
                    if (item.EntityType == "MLDATAMASTERACCO")
                    {
                        lblAccoMasterDataPushLastRun.InnerText = Convert.ToString(item.LastUpdate);
                        lblAccoMasterDataPushLastRunStatus.InnerText = Convert.ToString(item.Status);

                        if (item.Per > 0)
                        {
                            divAccoMasterDataPushLastRun.Attributes.Remove("aria-valuenow");
                            divAccoMasterDataPushLastRun.Attributes.Add("aria-valuenow", item.Per.ToString());

                            divAccoMasterDataPushLastRun.Attributes.Remove("style");
                            divAccoMasterDataPushLastRun.Attributes.Add("style", "width: " + item.Per.ToString() + "%");

                            AccoMasterDataPushLastRuncompleted.Text = Convert.ToString(item.Per) + "%";

                        }
                    }
                    else if (item.EntityType == "MLDATAMASTERACCORMFACILITY")
                    {
                        lblAccoMasterRoomFacilityDataPushLastRun.InnerText = Convert.ToString(item.LastUpdate);
                        lblAccoMasterRoomFacilityDataPushLastRunStatus.InnerText = Convert.ToString(item.Status);
                        if (item.Per > 0)
                        {
                            divAccoMasterRoomFacilityDataPushLastRunCompleted.Attributes.Remove("aria-valuenow");
                            divAccoMasterRoomFacilityDataPushLastRunCompleted.Attributes.Add("aria-valuenow", item.Per.ToString());
                            divAccoMasterRoomFacilityDataPushLastRunCompleted.Attributes.Remove("style");
                            divAccoMasterRoomFacilityDataPushLastRunCompleted.Attributes.Add("style", "width: " + item.Per.ToString() + "%");

                            AccoMasterRoomFacilityDataPushLastRunCompleted.Text = Convert.ToString(item.Per) + "%";

                        }
                    }
                    else if (item.EntityType == "MLDATAMASTERACCORMINFO")
                    {
                        lblAccoMasterRoomInfoDataPushLastRun.InnerText = Convert.ToString(item.LastUpdate);
                        lblAccoMasterRoomInfoDataPushLastRunStatus.InnerText = Convert.ToString(item.Status);
                        if (item.Per > 0)
                        {
                            divAccoMasterRoomInfoDataPushLastRunCompleted.Attributes.Remove("aria-valuenow");
                            divAccoMasterRoomInfoDataPushLastRunCompleted.Attributes.Add("aria-valuenow", item.Per.ToString());
                            divAccoMasterRoomInfoDataPushLastRunCompleted.Attributes.Remove("style");
                            divAccoMasterRoomInfoDataPushLastRunCompleted.Attributes.Add("style", "width: " + item.Per.ToString() + "%");

                            AccoMasterRoomInfoDataPushLastRunCompleted.Text = Convert.ToString(item.Per) + "%";

                        }
                    }
                    else if (item.EntityType == "MLDATAROOMTYPEMATCHING")
                    {
                        lblRoomTypeMatchingDataPushLastRun.InnerText = Convert.ToString(item.LastUpdate);
                        lblRoomTypeMatchingDataPushLastRunStatus.InnerText = Convert.ToString(item.Status);
                        if (item.Per > 0)
                        {
                            divRoomTypeMatchingDataPushLastRunCompleted.Attributes.Remove("aria-valuenow");
                            divRoomTypeMatchingDataPushLastRunCompleted.Attributes.Add("aria-valuenow", item.Per.ToString());
                            divRoomTypeMatchingDataPushLastRunCompleted.Attributes.Remove("style");
                            divRoomTypeMatchingDataPushLastRunCompleted.Attributes.Add("style", "width: " + item.Per.ToString() + "%");

                            RoomTypeMatchingDataPushLastRunCompleted.Text = Convert.ToString(item.Per) + "%";

                        }
                    }
                    else if (item.EntityType == "MLDATASUPPLIERACCO")
                    {
                        lblSupplierAccoDataPushLastRun.InnerText = Convert.ToString(item.LastUpdate);
                        lblSupplierAccoDataPushLastRunStatus.InnerText = Convert.ToString(item.Status);
                        if (item.Per > 0)
                        {
                            divSupplierAccoDataPushLastRunCompleted.Attributes.Remove("aria-valuenow");
                            divSupplierAccoDataPushLastRunCompleted.Attributes.Add("aria-valuenow", item.Per.ToString());
                            divSupplierAccoDataPushLastRunCompleted.Attributes.Remove("style");
                            divSupplierAccoDataPushLastRunCompleted.Attributes.Add("style", "width: " + item.Per.ToString() + "%");

                            SupplierAccoDataPushLastRunCompleted.Text = Convert.ToString(item.Per) + "%";

                        }

                    }
                    else if (item.EntityType == "MLDATASUPPLIERACCORM")
                    {
                        lblSupplierAccoRoomDataPushLastRun.InnerText = Convert.ToString(item.LastUpdate);
                        lblSupplierAccoRoomDataPushLastRunStatus.InnerText = Convert.ToString(item.Status);
                        if (item.Per > 0)
                        {
                            divSupplierAccoRoomDataPushLastRunCompleted.Attributes.Remove("aria-valuenow");
                            divSupplierAccoRoomDataPushLastRunCompleted.Attributes.Add("aria-valuenow", item.Per.ToString());
                            divSupplierAccoRoomDataPushLastRunCompleted.Attributes.Remove("style");
                            divSupplierAccoRoomDataPushLastRunCompleted.Attributes.Add("style", "width: " + item.Per.ToString() + "%");

                            SupplierAccoRoomDataPushLastRunCompleted.Text = Convert.ToString(item.Per) + "%";

                        }

                    }
                    else if (item.EntityType == "MLDATASUPPLIERACCORMEXTATTR")
                    {
                        lblSupplierAccoRoomExtendedAttrDataPushLastRun.InnerText = Convert.ToString(item.LastUpdate);
                        lblSupplierAccoRoomExtendedAttrDataPushLastRunStatus.InnerText = Convert.ToString(item.Status);
                        if (item.Per > 0)
                        {
                            divSupplierAccoRoomExtendedAttrDataPushLastRun.Attributes.Remove("aria-valuenow");
                            divSupplierAccoRoomExtendedAttrDataPushLastRun.Attributes.Add("aria-valuenow", item.Per.ToString());
                            divSupplierAccoRoomExtendedAttrDataPushLastRun.Attributes.Remove("style");
                            divSupplierAccoRoomExtendedAttrDataPushLastRun.Attributes.Add("style", "width: " + item.Per.ToString() + "%");

                            SupplierAccoRoomExtendedAttrDataPushLastRunCompleted.Text = Convert.ToString(item.Per) + "%";

                        }

                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        protected void btnAccoMasterDataPush_Click(object sender, EventArgs e)
        {
            RunMLDATAAPI(new MDMSVC.DC_Distribution_MLDataRQ { Type = "Master", Element = "MLDATAMASTERACCO" });
        }
        protected void btnAccoMasterRoomFacility_Click(object sender, EventArgs e)
        {
            RunMLDATAAPI(new MDMSVC.DC_Distribution_MLDataRQ { Type = "Master", Element = "MLDATAMASTERACCORMFACILITY" });

        }
        protected void btnAccoMasterRoomInfo_Click(object sender, EventArgs e)
        {
            RunMLDATAAPI(new MDMSVC.DC_Distribution_MLDataRQ { Type = "Master", Element = "MLDATAMASTERACCORMINFO" });

        }
        protected void btnRoomTypeMatching_Click(object sender, EventArgs e)
        {
            RunMLDATAAPI(new MDMSVC.DC_Distribution_MLDataRQ { Type = "MAPPING", Element = "MLDATAROOMTYPEMATCHING" });

        }
        protected void btnSupplierAcco_Click(object sender, EventArgs e)
        {
            RunMLDATAAPI(new MDMSVC.DC_Distribution_MLDataRQ { Type = "MAPPING", Element = "MLDATASUPPLIERACCO" });

        }
        protected void btnSupplierAccoRoom_Click(object sender, EventArgs e)
        {
            RunMLDATAAPI(new MDMSVC.DC_Distribution_MLDataRQ { Type = "MAPPING", Element = "MLDATASUPPLIERACCORM" });

        }

        protected void btnSupplierAccoRoomExtedAttr_Click(object sender, EventArgs e)
        {
            RunMLDATAAPI(new MDMSVC.DC_Distribution_MLDataRQ { Type = "MAPPING", Element = "MLDATASUPPLIERACCORMEXTATTR" });
        }
        public void RunMLDATAAPI(MDMSVC.DC_Distribution_MLDataRQ _obj)
        {
            var res = MasterSvc.PushSyncMLAPIData(_obj);
            if (res != null)
            {
                BootstrapAlert.BootstrapAlertMessage(dvMsg, res.StatusMessage, (BootstrapAlertType)res.StatusCode);
            }
            else
            {
                BootstrapAlert.BootstrapAlertMessage(dvMsg, "Hotel MappingLite Sync failed.", BootstrapAlertType.Danger);
            }
        }

        #endregion

    }


}