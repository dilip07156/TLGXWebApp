using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Data;
using TLGX_Consumer.Models;
using TLGX_Consumer.MDMSVC;

namespace TLGX_Consumer.Controller
{

    public class AccomodationSVC
    {

        #region Hotel Search 
        public List<DC_Accomodation_Search_RS> SearchHotels(MDMSVC.DC_Accomodation_Search_RQ RQParams)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Accomodation_SearchURI"], RQParams, typeof(MDMSVC.DC_Accomodation_Search_RQ), typeof(List<MDMSVC.DC_Accomodation_Search_RS>), out result);
            return result as List<DC_Accomodation_Search_RS>;
        }
        public List<DC_Accomodation_AutoComplete_RS> SearchHotelsAutoComplete(MDMSVC.DC_Accomodation_AutoComplete_RQ RQParams)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Accomodation_SearchAutoComplete"], RQParams, typeof(MDMSVC.DC_Accomodation_AutoComplete_RQ), typeof(List<MDMSVC.DC_Accomodation_AutoComplete_RS>), out result);
            return result as List<DC_Accomodation_AutoComplete_RS>;
        }

        public List<string> GetAccomodationNames(MDMSVC.DC_Accomodation_Search_RQ RQParams)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Accomodation_SearchNamesURI"], RQParams, typeof(MDMSVC.DC_Accomodation_Search_RQ), typeof(List<string>), out result);
            return result as List<string>;
        }

        public List<string> GetRoomCategoryMaster(MDMSVC.DC_RoomCategoryMaster_RQ RQParams)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Accomodation_RoomCategoryMaster"], RQParams, typeof(MDMSVC.DC_RoomCategoryMaster_RQ), typeof(List<string>), out result);
            return result as List<string>;
        }

        public List<DC_Accomodation> GetHotelDetails(Guid AccommodationId)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.GetData(string.Format(ConfigurationManager.AppSettings["Accomodation_DetailURI"], AccommodationId), typeof(List<DC_Accomodation>), out result);
            return result as List<DC_Accomodation>;
        }
        public List<DC_AccomodationBasic> GetAccomodationBasicInfo(Guid AccommodationId)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.GetData(string.Format(ConfigurationManager.AppSettings["Accomodation_DetailBasic"], AccommodationId), typeof(List<DC_AccomodationBasic>), out result);
            return result as List<DC_AccomodationBasic>;
        }

        // Add Hotel with Basic Data
        public bool AddHotelDetail(MDMSVC.DC_Accomodation HotelData)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Accomodation_AddInfoURI"], HotelData, typeof(MDMSVC.DC_Accomodation), typeof(bool), out result);
            return (bool)result;
        }

        // Update Hotel Overview Info
        public bool UpdateHotelDetail(MDMSVC.DC_Accomodation HotelData)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Accomodation_UpdateURI"], HotelData, typeof(MDMSVC.DC_Accomodation), typeof(bool), out result);
            return (bool)result;
        }

        // Update Hotel Geocode Info
        public bool UpdateHotelGeoDetail(MDMSVC.DC_Accomodation HotelData)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Accomodation_UpdateGeoURI"], HotelData, typeof(MDMSVC.DC_Accomodation), typeof(bool), out result);
            return (bool)result;
        }

        // Update Hotel Status Info
        public bool UpdateHotelDetailStatus(MDMSVC.DC_Accomodation_UpdateStatus_RQ HotelData)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Accomodation_UpdateDetailStatusURI"], HotelData, typeof(MDMSVC.DC_Accomodation_UpdateStatus_RQ), typeof(bool), out result);
            return (bool)result;
        }
        #endregion

        #region Acco Facilities
        // gets Facility details for hotel 
        public List<DC_Accommodation_Facility> GetHotelFacilityDetails(Guid Accommodation_Id, Guid DataKey_Id)
        {

            object result = null;
            ServiceConnection.MDMSvcProxy.GetData(string.Format(ConfigurationManager.AppSettings["Accomodation_FacilitiesURI"], Accommodation_Id, DataKey_Id), typeof(List<DC_Accommodation_Facility>), out result);
            return result as List<DC_Accommodation_Facility>;
        }

        public bool AddHotelFacilityDetails(MDMSVC.DC_Accommodation_Facility AF)
        {

            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Accomodation_AddFacilitiesURI"], AF, typeof(DC_Accommodation_Facility), typeof(bool), out result);
            return (bool)result;
        }

        public bool UpdateHotelFacilityDetails(MDMSVC.DC_Accommodation_Facility AF)
        {

            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Accomodation_UpdateFacilitiesURI"], AF, typeof(DC_Accommodation_Facility), typeof(bool), out result);
            return (bool)result;
        }

        #endregion

        #region Accommodation Descriptions
        // gets Accommodation Descriptions for hotel
        public List<DC_Accommodation_Descriptions> GetHotelDescriptionDetails(Guid Accommodation_Id, Guid Accommodation_Descrtiptions_Id)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.GetData(string.Format(ConfigurationManager.AppSettings["Accomodation_DescriptionsURI"], Accommodation_Id, Accommodation_Descrtiptions_Id), typeof(List<DC_Accommodation_Descriptions>), out result);
            return result as List<DC_Accommodation_Descriptions>;
        }

        // inserts new Accommodation Description
        public bool AddAccommodationDescriptionDetail(MDMSVC.DC_Accommodation_Descriptions AF)
        {

            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Accomodation_AddDescriptionURI"], AF, typeof(DC_Accommodation_Descriptions), typeof(bool), out result);
            return (bool)result;
        }

        public bool UpdateHotelDescriptions(MDMSVC.DC_Accommodation_Descriptions AF)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Accomodation_UpdateDescriptionsURI"], AF, typeof(DC_Accommodation_Descriptions), typeof(bool), out result);
            return (bool)result;
        }
        #endregion

        #region Accommodation Pax Occupancy
        // gets Accommodation Pax Occupancy for hotel
        public List<DC_Accommodation_PaxOccupancy> GetHotelPaxOccupancyDetails(Guid Accommodation_Id, Guid Accommodation_Occupancy_Id)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.GetData(string.Format(ConfigurationManager.AppSettings["Accomodation_GetOccupancyURI"], Accommodation_Id, Accommodation_Occupancy_Id), typeof(List<DC_Accommodation_PaxOccupancy>), out result);
            return result as List<DC_Accommodation_PaxOccupancy>;
        }

        // inserts new Accommodation Pax Occupancy
        public bool AddAccommodationPaxOccupancyDetail(MDMSVC.DC_Accommodation_PaxOccupancy PO)
        {

            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Accomodation_AddOccupancURI"], PO, typeof(DC_Accommodation_PaxOccupancy), typeof(bool), out result);
            return (bool)result;
        }

        // updates existing Accommodation Pax Occupancy
        public bool UpdateHotelPaxOccupancy(MDMSVC.DC_Accommodation_PaxOccupancy PO)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Accomodation_UpdateOccupancURI"], PO, typeof(DC_Accommodation_PaxOccupancy), typeof(bool), out result);
            return (bool)result;
        }
        #endregion

        #region Accomodation Contacts


        // gets Accommodation Contact details for hotel
        public List<DC_Accommodation_Contact> GetHotelContactDetails(Guid Accommodation_Id, Guid DataKey_Id)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.GetData(string.Format(ConfigurationManager.AppSettings["Accomodation_ContactsURI"], Accommodation_Id, DataKey_Id), typeof(List<DC_Accommodation_Contact>), out result);
            return result as List<DC_Accommodation_Contact>;
        }
        // Adds Accommodation Contact details for hotel
        public bool AddHotelContactsDetails(MDMSVC.DC_Accommodation_Contact AC)
        {

            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Accomodation_AddContactsURI"], AC, typeof(DC_Accommodation_Contact), typeof(bool), out result);
            return (bool)result;
        }
        // Updates Accommodation Contact details for hotel
        public bool UpdateHotelContactDetails(MDMSVC.DC_Accommodation_Contact AC)
        {

            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Accomodation_UpdateContactsURI"], AC, typeof(DC_Accommodation_Contact), typeof(bool), out result);
            return (bool)result;
        }

        public int? GetLegacyHotelId(Guid Accomodation_Id)
        {
            int? ret = 0;
            using (TLGX_MAPPEREntities1 context = new TLGX_MAPPEREntities1())
            {
                var acco = (from ac in context.Accommodations
                            where ac.Accommodation_Id == Accomodation_Id
                            select new { ac.Legacy_HTL_ID }).Single();

                ret = acco.Legacy_HTL_ID;

            }

            return ret;
        }

        #endregion

        #region Accomodation Status
        // gets Accommodation Status details for hotel
        public List<DC_Accommodation_Status> GetHotelStatusDetails(Guid Accommodation_Id, Guid Accomodation_Status_Id)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.GetData(string.Format(ConfigurationManager.AppSettings["Accomodation_StatusURI"], Accommodation_Id, Accomodation_Status_Id), typeof(List<DC_Accommodation_Status>), out result);
            return result as List<DC_Accommodation_Status>;
        }

        public bool AddHotelStatus(MDMSVC.DC_Accommodation_Status AF)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Accomodation_AddStatusURI"], AF, typeof(DC_Accommodation_Status), typeof(bool), out result);
            return (bool)result;
        }

        public bool UpdateHotelStatus(MDMSVC.DC_Accommodation_Status AF)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Accomodation_UpdateStatusURI"], AF, typeof(DC_Accommodation_Status), typeof(bool), out result);
            return (bool)result;
        }



        #endregion

        #region Dynamic Attributes


        public List<DC_DynamicAttributes> GetHotelDynamicAttributes(Guid Accommodation_Id, Guid Sub_Object_Id, Guid DynamicAttributes_Id)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.GetData(string.Format(ConfigurationManager.AppSettings["Accomodation_DynamicAttributesURI"], Accommodation_Id, Sub_Object_Id, DynamicAttributes_Id), typeof(List<DC_DynamicAttributes>), out result);
            return result as List<DC_DynamicAttributes>;
        }

        public bool AddHotelDynamicAttributes(MDMSVC.DC_DynamicAttributes AF)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Accomodation_AddDynamicAttributesURI"], AF, typeof(DC_DynamicAttributes), typeof(bool), out result);
            return (bool)result;
        }

        public bool UpdateHotelDynamicAttributes(MDMSVC.DC_DynamicAttributes AF)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Accomodation_UpdateDynamicAttributesURI"], AF, typeof(DC_DynamicAttributes), typeof(bool), out result);
            return (bool)result;
        }

        #endregion

        #region RouteInformation    

        // gets Accommodation Descriptions for hotel
        public List<DC_Accommodation_RouteInfo> GetHowToReachDetails(Guid Accommodation_Id, Guid Accommodation_Route_Id)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.GetData(string.Format(ConfigurationManager.AppSettings["Accomodation_HowToReachURI"], Accommodation_Id, Accommodation_Route_Id), typeof(List<DC_Accommodation_RouteInfo>), out result);
            return result as List<DC_Accommodation_RouteInfo>;
        }

        // inserts new Accommodation Description
        public bool AddHowToReach(MDMSVC.DC_Accommodation_RouteInfo AF)
        {

            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Accomodation_AddHowToReachURI"], AF, typeof(DC_Accommodation_RouteInfo), typeof(bool), out result);
            return (bool)result;
        }

        public bool UpdateHowToReach(MDMSVC.DC_Accommodation_RouteInfo AF)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Accomodation_UpdateHowToReachURI"], AF, typeof(DC_Accommodation_RouteInfo), typeof(bool), out result);
            return (bool)result;
        }


        #endregion

        #region InAndAround


        public List<DC_Accommodation_NearbyPlaces> GetNearbyPlacesDetails(Guid Accommodation_Id, Guid Accommodation_NearbyPlace_Id)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.GetData(string.Format(ConfigurationManager.AppSettings["Accomodation_NearbyPlacesURI"], Accommodation_Id, Accommodation_NearbyPlace_Id), typeof(List<DC_Accommodation_NearbyPlaces>), out result);
            return result as List<DC_Accommodation_NearbyPlaces>;
        }
        public List<DC_Accommodation_NearbyPlaces> GetNearbyPlacesDetailsWithPaging(Guid Accommodation_Id, Guid Accommodation_NearbyPlace_Id, string pageSize, string pageindex)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.GetData(string.Format(ConfigurationManager.AppSettings["Accomodation_NearbyPlacesURIWithPaging"], Accommodation_Id, Accommodation_NearbyPlace_Id, pageSize, pageindex), typeof(List<DC_Accommodation_NearbyPlaces>), out result);
            return result as List<DC_Accommodation_NearbyPlaces>;
        }

        public bool AddNearbyPlaces(MDMSVC.DC_Accommodation_NearbyPlaces AF)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Accomodation_AddNearbyPlacesURI"], AF, typeof(DC_Accommodation_NearbyPlaces), typeof(bool), out result);
            return (bool)result;
        }

        public bool UpdateNearbyPlaces(MDMSVC.DC_Accommodation_NearbyPlaces AF)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Accomodation_UpdateNearbyPlacesURI"], AF, typeof(DC_Accommodation_NearbyPlaces), typeof(bool), out result);
            return (bool)result;
        }
        public DC_Message AddUpdatePlaces(MDMSVC.DC_GooglePlaceNearByWithAccoID _lst)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["AddUpldate_Places"], _lst, typeof(MDMSVC.DC_GooglePlaceNearByWithAccoID), typeof(DC_Message), out result);
            return result as DC_Message;
        }
        #endregion

        #region Hotel Rules


        public List<DC_Accommodation_RuleInfo> GetHotelRuleDetails(Guid Accommodation_Id, Guid Accommodation_NearbyPlace_Id)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.GetData(string.Format(ConfigurationManager.AppSettings["Accomodation_HotelRulesURI"], Accommodation_Id, Accommodation_NearbyPlace_Id), typeof(List<DC_Accommodation_RuleInfo>), out result);
            return result as List<DC_Accommodation_RuleInfo>;
        }


        public bool AddHotelRule(MDMSVC.DC_Accommodation_RuleInfo AF)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Accomodation_AddHotelRulesURI"], AF, typeof(DC_Accommodation_RuleInfo), typeof(bool), out result);
            return (bool)result;
        }

        public bool UpdateHotelRule(MDMSVC.DC_Accommodation_RuleInfo AF)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Accomodation_UpdateHotelRulesURI"], AF, typeof(DC_Accommodation_RuleInfo), typeof(bool), out result);
            return (bool)result;


        }
        #endregion

        #region     HotelUpdates


        public List<DC_Accommodation_HotelUpdates> GetHotelUpdateDetails(Guid Accommodation_Id, Guid Accommodation_HotelUpdates_Id)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.GetData(string.Format(ConfigurationManager.AppSettings["Accomodation_HotelUpdatesURI"], Accommodation_Id, Accommodation_HotelUpdates_Id), typeof(List<DC_Accommodation_HotelUpdates>), out result);
            return result as List<DC_Accommodation_HotelUpdates>;
        }


        public bool AddHotelUpdate(MDMSVC.DC_Accommodation_HotelUpdates AF)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Accomodation_AddHotelUpdatesURI"], AF, typeof(DC_Accommodation_HotelUpdates), typeof(bool), out result);
            return (bool)result;
        }

        public bool UpdateHotelUpdate(MDMSVC.DC_Accommodation_HotelUpdates AF)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Accomodation_UpdateHotelUpdatesURI"], AF, typeof(DC_Accommodation_HotelUpdates), typeof(bool), out result);
            return (bool)result;


        }
        #endregion

        #region HealthAndSafety

        public List<DC_Accommodation_HealthAndSafety> GetHealthAndSafetyDetails(Guid Accommodation_Id, Guid Accommodation_HealthAndSafety_Id)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.GetData(string.Format(ConfigurationManager.AppSettings["Accomodation_HealthAndSafetyURI"], Accommodation_Id, Accommodation_HealthAndSafety_Id), typeof(List<DC_Accommodation_HealthAndSafety>), out result);
            return result as List<DC_Accommodation_HealthAndSafety>;
        }


        public bool AddHealthAndSafety(MDMSVC.DC_Accommodation_HealthAndSafety AF)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Accomodation_AddHealthAndSafetyURI"], AF, typeof(DC_Accommodation_HealthAndSafety), typeof(bool), out result);
            return (bool)result;
        }

        public bool UpdateHealthAndSafety(MDMSVC.DC_Accommodation_HealthAndSafety AF)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Accomodation_UpdateHealthAndSafetyURI"], AF, typeof(DC_Accommodation_HealthAndSafety), typeof(bool), out result);
            return (bool)result;

        }



        #endregion

        #region Accomodation Rooms

        public List<DC_Accommodation_RoomInfo> GetRoomDetails(Guid Accommodation_Id, Guid Accommodation_RoomInfo_Id)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.GetData(string.Format(ConfigurationManager.AppSettings["Accomodation_RoomURI"], Accommodation_Id, Accommodation_RoomInfo_Id), typeof(List<DC_Accommodation_RoomInfo>), out result);
            return result as List<DC_Accommodation_RoomInfo>;
        }
        public List<DC_Accommodation_RoomInfo> GetRoomDetailsByWithPagging(DC_Accommodation_RoomInfo_RQ RQ)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Accomodation_RoomInfor_URI"], RQ, typeof(DC_Accommodation_RoomInfo_RQ), typeof(List<DC_Accommodation_RoomInfo>), out result);
            return result as List<DC_Accommodation_RoomInfo>;
        }
        public List<DC_Accomodation_Category_DDL> GetRoomDetails_RoomCategory(Guid Accommodation_Id)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.GetData(string.Format(ConfigurationManager.AppSettings["Accomodation_RoomCategory"], Accommodation_Id), typeof(List<DC_Accomodation_Category_DDL>), out result);
            return result as List<DC_Accomodation_Category_DDL>;
        }
        public bool AddRoom(MDMSVC.DC_Accommodation_RoomInfo AF)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Accomodation_AddRoomURI"], AF, typeof(DC_Accommodation_RoomInfo), typeof(bool), out result);
            return (bool)result;
        }

        public bool UpdateRoom(MDMSVC.DC_Accommodation_RoomInfo AF)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Accomodation_UpdateRoomURI"], AF, typeof(DC_Accommodation_RoomInfo), typeof(bool), out result);
            return (bool)result;

        }
        public DC_Message CopyAccomodationInfo(MDMSVC.DC_Accomodation_CopyRoomDef AF)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Accomodation_CopyAccoRoomInfo"], AF, typeof(DC_Accomodation_CopyRoomDef), typeof(DC_Message), out result);
            return result as DC_Message;

        }

        #endregion

        #region Accomodation Rooms Facilities

        public List<DC_Accomodation_RoomFacilities> GetRoomFacilitiesDetails(Guid Accommodation_Id, Guid Accommodation_RoomInfo_Id, Guid Accommodation_RoomFacility_Id)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.GetData(string.Format(ConfigurationManager.AppSettings["Accomodation_RoomFacilitiesURI"], Accommodation_Id, Accommodation_RoomInfo_Id, Accommodation_RoomFacility_Id), typeof(List<DC_Accomodation_RoomFacilities>), out result);
            return result as List<DC_Accomodation_RoomFacilities>;
        }


        public bool AddRoomFacilities(MDMSVC.DC_Accomodation_RoomFacilities AF)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Accomodation_AddRoomFacilitiesURI"], AF, typeof(DC_Accomodation_RoomFacilities), typeof(bool), out result);
            return (bool)result;
        }

        public bool UpdateRoomFacilities(MDMSVC.DC_Accomodation_RoomFacilities AF)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Accomodation_UpdateRoomFacilitiesURI"], AF, typeof(DC_Accomodation_RoomFacilities), typeof(bool), out result);
            return (bool)result;

        }

        #endregion

        #region Geo Location
        public DC_GeoLocation GetGeoLocationByAddress(MDMSVC.DC_Address_Physical AP)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["GeoLocation_ByAddress"], AP, typeof(DC_Address_Physical), typeof(DC_GeoLocation), out result);
            return result as DC_GeoLocation;
        }

        public DC_GeoLocation GetGeoLocationByLatLng(MDMSVC.DC_Address_GeoCode GC)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["GeoLocation_ByLatLng"], GC, typeof(DC_Address_GeoCode), typeof(DC_GeoLocation), out result);
            return result as DC_GeoLocation;
        }
        #endregion

        #region Address Heirarchy Update
        public bool UpdateAdressHeirarchy(MDMSVC.DC_Country_State_City_Area_Location obj)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["UpdateAddressMasterHierarchy"], obj, typeof(DC_Country_State_City_Area_Location), typeof(bool), out result);
            return (bool)result;
        }
        #endregion

        #region Classification Attrbiutes
        public List<DC_Accomodation_ClassificationAttributes> GetClassificationAttributes(Guid Accommodation_Id, Guid Accommodation_ClassificationAttribute_Id)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.GetData(string.Format(ConfigurationManager.AppSettings["Accomodation_ClassificationAttributesURI"], Accommodation_Id, Accommodation_ClassificationAttribute_Id), typeof(List<DC_Accomodation_ClassificationAttributes>), out result);
            return result as List<DC_Accomodation_ClassificationAttributes>;
        }

        public bool AddClassificationAttributes(MDMSVC.DC_Accomodation_ClassificationAttributes AF)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Accomodation_AddClassificationAttributesURI"], AF, typeof(DC_Accomodation_ClassificationAttributes), typeof(bool), out result);
            return (bool)result;
        }

        public bool UpdateClassificationAttributes(MDMSVC.DC_Accomodation_ClassificationAttributes AF)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Accomodation_UpdateClassificationAttributesURI"], AF, typeof(DC_Accomodation_ClassificationAttributes), typeof(bool), out result);
            return (bool)result;

        }
        #endregion

        #region Site Map
        public List<DC_SiteMap> GetSiteMapMaster(int ID, string applicationID = "0")
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.GetData(string.Format(ConfigurationManager.AppSettings["SiteMap_Get"], ID, applicationID), typeof(List<DC_SiteMap>), out result);
            return result as List<DC_SiteMap>;
        }

        public bool UpdateSiteMapNode(MDMSVC.DC_SiteMap AF)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["SiteMap_Update"], AF, typeof(DC_SiteMap), typeof(bool), out result);
            return (bool)result;
        }

        public bool AddSiteMapNode(MDMSVC.DC_SiteMap AF)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["SiteMap_Add"], AF, typeof(DC_SiteMap), typeof(bool), out result);
            return (bool)result;
        }

        #endregion

        #region Accommodation ProductMapping

        public List<DC_Accomodation_ProductMapping> GetAccomodation_ProductMapping(Guid Accommodation_Id, int PageNumber, int PageSize)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.GetData(string.Format(ConfigurationManager.AppSettings["Accomodation_ProductMappingURI"], PageNumber, PageSize, Accommodation_Id), typeof(List<DC_Accomodation_ProductMapping>), out result);
            return result as List<DC_Accomodation_ProductMapping>;
        }


        public List<DC_Accomodation_ProductMapping> SearchAccomodation_ProductMapping(MDMSVC.DC_Mapping_ProductSupplier_Search_RQ RQParams)
        {

            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Accomodation_SearchProductMappingURI"], RQParams, typeof(MDMSVC.DC_Mapping_ProductSupplier_Search_RQ), typeof(List<DC_Accomodation_ProductMapping>), out result);
            return result as List<DC_Accomodation_ProductMapping>;

        }

        public bool UpdateAccomodation_ProductMapping(MDMSVC.DC_Accomodation_ProductMapping AF)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Accomodation_UpdateProductMappingURI"], AF, typeof(DC_Accomodation_ProductMapping), typeof(bool), out result);
            return (bool)result;

        }

        #endregion

        #region Admin_Roles_Get
        public List<DC_Roles> GetAllRoles()
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.GetData(ConfigurationManager.AppSettings["Admin_Roles_Get"], typeof(List<DC_Roles>), out result);
            return result as List<DC_Roles>;
        }

        public List<DC_SiteMap> GetSiteMapByUserRole(string UserName)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.GetData(string.Format(ConfigurationManager.AppSettings["Admin_SiteMap_GetByUserRole"], UserName), typeof(List<DC_SiteMap>), out result);
            return result as List<DC_SiteMap>;
        }

        #endregion

        #region Accommodation RoomTypeMapping

        public List<DC_Accomodation_SupplierRoomTypeMapping> GetAccomodation_RoomTypeMapping(int PageNumber, int PageSize, Guid Accommodation_Id, Guid Supplier_Id)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.GetData(string.Format(ConfigurationManager.AppSettings["Accomodation_RoomTypeMappingURI"], PageNumber, PageSize, Accommodation_Id, Supplier_Id), typeof(List<DC_Accomodation_SupplierRoomTypeMapping>), out result);
            return result as List<DC_Accomodation_SupplierRoomTypeMapping>;
        }


        public bool UpdateAccomodation_RoomTypeMapping(MDMSVC.DC_Accomodation_SupplierRoomTypeMapping AF)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Accomodation_UpdateRoomTypeMappingURI"], AF, typeof(DC_Accomodation_SupplierRoomTypeMapping), typeof(bool), out result);
            return (bool)result;
        }




        #endregion

        #region Accomodation Media
        public List<DC_Accommodation_Media> GetAccomodation_MediaDetails(Guid Accommodation_Id, Guid DataKey_Id)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.GetData(string.Format(ConfigurationManager.AppSettings["Accomodation_GetMediaURI"], Accommodation_Id, DataKey_Id), typeof(List<DC_Accommodation_Media>), out result);
            return result as List<DC_Accommodation_Media>;
        }

        public bool AddAccomodation_MediaDetails(MDMSVC.DC_Accommodation_Media AM)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Accomodation_AddMediaURI"], AM, typeof(DC_Accommodation_Media), typeof(bool), out result);
            return (bool)result;
        }

        public bool UpdateAccomodation_MediaDetails(MDMSVC.DC_Accommodation_Media AM)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Accomodation_UpdateMediaURI"], AM, typeof(DC_Accommodation_Media), typeof(bool), out result);
            return (bool)result;
        }
        public bool CheckMediaPositionDuplicate(string Accommodation_Id, string MediaPosition)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.GetData(string.Format(ConfigurationManager.AppSettings["CheckMediaPositionDuplicateforAccommodation"], Accommodation_Id, MediaPosition), typeof(bool), out result);
            return (bool)result;
        }
        #endregion

        #region Accomodation Media Attributes
        public List<DC_Accomodation_Media_Attributes> GetAccomodation_MediaAttributesDetails(Guid Accommodation_Media_Id, Guid DataKey_Id, int PageNo, int PageSize)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.GetData(string.Format(ConfigurationManager.AppSettings["Accomodation_GetMediaAttributesURI"], Accommodation_Media_Id, DataKey_Id, PageNo, PageSize), typeof(List<DC_Accomodation_Media_Attributes>), out result);
            return result as List<DC_Accomodation_Media_Attributes>;
        }

        public bool AddAccomodation_MediaAttributesDetails(MDMSVC.DC_Accomodation_Media_Attributes AM)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Accomodation_AddMediaAttributesURI"], AM, typeof(DC_Accomodation_Media_Attributes), typeof(bool), out result);
            return (bool)result;
        }

        public bool UpdateAccomodation_MediaAttributesDetails(MDMSVC.DC_Accomodation_Media_Attributes AM)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Accomodation_UpdateMediaAttributesURI"], AM, typeof(DC_Accomodation_Media_Attributes), typeof(bool), out result);
            return (bool)result;
        }
        #endregion

        public List<DC_Accomodation> GetAccomodationMissingAttributeDetails(MDMSVC.DC_Accomodation_Search_RQ RQParams)
        {
            object result = null;
            ServiceConnection.MDMSvcProxy.PostData(ConfigurationManager.AppSettings["Get_AccomodationDetails"], RQParams, typeof(MDMSVC.DC_Accomodation_Search_RQ), typeof(List<DC_Accomodation>), out result);
            return result as List<DC_Accomodation>;
        }
    }
}