using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Ninject;
using Model.Interface;
using Model.Repository;

namespace EcomaintSite
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapMvcAttributeRoutes();
            routes.MapRoute(
            name: "",
            url: "",
            defaults: new { controller = "Account", action = "Login", ReturnURL = UrlParameter.Optional, error = "" });

            routes.MapRoute(
            name: "calendar",
            url: "calendar",
            defaults: new { controller = "Calendar", action = "Index", themeStyle = UrlParameter.Optional, typeLang = UrlParameter.Optional });

            routes.MapRoute(
            name: "change-setting",
            url: "change-setting",
            defaults: new { controller = "Languages", action = "ChangeThemeValue", themeStyle = UrlParameter.Optional, typeLang = UrlParameter.Optional });

            routes.MapRoute(
            name: "tracking-user-online",
            url: "tracking-user-online",
            defaults: new { controller = "User", action = "TrackingUserOnline", id = UrlParameter.Optional });


            /*-- GIÁM SÁT TÌNH TRẠNG --*/
            //routes.MapRoute(
            //name: "get-parameter-info",
            //url: "get-parameter-info",
            //defaults: new { controller = "Monitoring", action = "GetParameterInfo", id = UrlParameter.Optional });

            //routes.MapRoute(
            //name: "monitoring-due-checked",
            //url: "monitoring/due-checked",
            //defaults: new { controller = "Monitoring", action = "CheckTheParametersDue", id = UrlParameter.Optional });

            //routes.MapRoute(
            //name: "list-of-monitoring",
            //url: "list-of-monitoring",
            //defaults: new { controller = "Monitoring", action = "List", id = UrlParameter.Optional });

            //routes.MapRoute(
            //name: "monitoring-save",
            //url: "monitoring/save",
            //defaults: new { controller = "Monitoring", action = "Save", id = UrlParameter.Optional });

            routes.MapRoute(
            name: "monitoring",
            url: "monitoring",
            defaults: new { controller = "Monitoring", action = "Index", id = UrlParameter.Optional });

            /*-- Giam Sat Tinh Trang May --*/
            routes.MapRoute(
            name: "monitoringdevice",
            url: "monitoringdevice",
            defaults: new { controller = "Monitoring", action = "ShowMornitoringDevice", id = UrlParameter.Optional });


            /*-- HazardReport --*/
            routes.MapRoute(
            name: "hazardreport",
            url: "hazardreport",
            defaults: new { controller = "Safety", action = "ShowHazardReport", id = UrlParameter.Optional });

            /*-- stopcard --*/
            routes.MapRoute(
            name: "stopcard",
            url: "stopcard",
            defaults: new { controller = "Safety", action = "ShowStopCard", id = UrlParameter.Optional });


            /*-- DANH MỤC ĐƠN VỊ --*/
            routes.MapRoute(
            name: "master-data-org-unit-ghi",
            url: "master-data/org-unit/ghi",
            defaults: new { controller = "OrgUnit", action = "Save", id = UrlParameter.Optional });

            routes.MapRoute(
            name: "master-data-org-unit-xoa",
            url: "master-data/org-unit/xoa",
            defaults: new { controller = "OrgUnit", action = "Remove", id = UrlParameter.Optional });

            routes.MapRoute(
            name: "master-data-org-unit",
            url: "master-data/org-unit",
            defaults: new { controller = "OrgUnit", action = "Index", id = UrlParameter.Optional });

            /*-- YÊU CẦU --*/
            routes.MapRoute(
                name: "user-request",
                url: "user-request",
                defaults: new { controller = "UserRequest", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                 name: "user-request/request-modified",
                 url: "user-request/request-modified",
                 defaults: new { controller = "UserRequest", action = "InitRequest", id = -1 }
            );

            routes.MapRoute(
                name: "user-request-request-modified-id",
                url: "user-request/request-modified-{id}",
                defaults: new { controller = "UserRequest", action = "InitRequest", id = UrlParameter.Optional }
           );

            routes.MapRoute(
                  name: "user-request-filter-data",
                  url: "user-request/filter-data",
                  defaults: new { controller = "UserRequest", action = "FilterData", id = UrlParameter.Optional }
           );
            routes.MapRoute(
                name: "user-request-get-user-request-detail",
                url: "user-request/get-user-request-detail",
                defaults: new { controller = "UserRequest", action = "GetRequestInfomation", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                 name: "user-request-get-document",
                 url: "user-request/get-document",
                 defaults: new { controller = "UserRequest", action = "GetDocument", id = UrlParameter.Optional }
             );
            routes.MapRoute(
                   name: "user-request-save-request",
                   url: "user-request/save-request",
                   defaults: new { controller = "UserRequest", action = "SaveRequest", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "user-request-edit-request",
                url: "user-request/edit-request",
                defaults: new { controller = "UserRequest", action = "EditRequest", id = UrlParameter.Optional }
         );
            routes.MapRoute(
                name: "user-request-get-component",
                url: "user-request/get-component",
                defaults: new { controller = "UserRequest", action = "GetComponent", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                 name: "approve-request",
                 url: "approve-request",
                 defaults: new { controller = "ApproveRequests", action = "Index", id = UrlParameter.Optional }
          );
            routes.MapRoute(
                name: "approve-request-filter-data",
                url: "approve-request/filter-data",
                defaults: new { controller = "ApproveRequests", action = "FilterData", id = UrlParameter.Optional }
         );
            routes.MapRoute(
            name: "approve-request-approved",
            url: "approve-request/approved",
            defaults: new { controller = "ApproveRequests", action = "Approve", id = UrlParameter.Optional }
     );
            routes.MapRoute(
        name: "receipt-request",
        url: "receipt-request",
        defaults: new { controller = "ReceiptRequest", action = "Index", id = UrlParameter.Optional }
 );

            /*-- NGÔN NGỮ --*/
            routes.MapRoute(
            name: "get-language",
            url: "get-language",
            defaults: new { controller = "Languages", action = "GetLanguages", id = UrlParameter.Optional }
         );
            /*My eco main*/
            routes.MapRoute(
            name: "my-ecomain",
            url: "my-ecomain",
            defaults: new { controller = "MyEcomain", action = "Index", id = UrlParameter.Optional });

            /*My eco main*/
            routes.MapRoute(
            name: "work-worker",
            url: "work-worker",
            defaults: new { controller = "WorkWorker", action = "Index", id = UrlParameter.Optional });
            /*-- PBT --*/
            routes.MapRoute(
                        name: "work-order-filter-data",
                        url: "work-order/filter-data",
                        defaults: new { controller = "WorkOrder", action = "FilterData", id = UrlParameter.Optional }
                    );
            routes.MapRoute(
         name: "work-order",
         url: "work-order",
         defaults: new { controller = "WorkOrder", action = "Index", id = UrlParameter.Optional }
     );

            /*-- KHTT --*/
            routes.MapRoute(
          name: "master-plan",
          url: "master-plan",
          defaults: new { controller = "MasterPlans", action = "Index", id = UrlParameter.Optional }
       );
            routes.MapRoute(
         name: "get-type-of-maintenance-by-device",
         url: "get-type-of-maintenance-by-device",
         defaults: new { controller = "MasterPlans", action = "GetTypeOfMaintenanceByDevice", deviceID = UrlParameter.Optional }
      );
            routes.MapRoute(
        name: "master-plan-filter-data",
        url: "master-plan/filter-data",
        defaults: new { controller = "MasterPlans", action = "FilterData", id = UrlParameter.Optional }
 );

            routes.MapRoute(
            name: "periodic-maintenance-create-plan",
            url: "periodic-maintenance/create-plan",
            defaults: new { controller = "PeriodicMaintenance", action = "SavePlan", id = UrlParameter.Optional }
         ); routes.MapRoute(
             name: "periodic-maintenance-create-wo",
             url: "periodic-maintenance/create-wo",
             defaults: new { controller = "PeriodicMaintenance", action = "SaveWO", id = UrlParameter.Optional }
          );
            routes.MapRoute(
          name: "periodic-maintenance",
          url: "periodic-maintenance",
          defaults: new { controller = "PeriodicMaintenance", action = "Index", id = UrlParameter.Optional }
       );
            routes.MapRoute(
           name: "periodic-maintenance-filter-data",
           url: "periodic-maintenance/filter-data",
           defaults: new { controller = "PeriodicMaintenance", action = "FilterData", id = UrlParameter.Optional }
    );
            /*-- Dashboard --*/

            routes.MapRoute(
                      name: "get-device",
                      url: "chart/get-device",
                      defaults: new { controller = "Dashboard", action = "GetDeviceStatus", id = UrlParameter.Optional }
       );
            routes.MapRoute(
                      name: "device-info",
                      url: "chart/get-device-info",
                      defaults: new { controller = "Dashboard", action = "GetDeviceInfo", id = UrlParameter.Optional }
       );
            routes.MapRoute(
                      name: "the-situation-by-wo",
                      url: "chart/the-situation-by-wo",
                      defaults: new { controller = "Dashboard", action = "GetSituationWO", id = UrlParameter.Optional }
       );
            routes.MapRoute(
                      name: "the-situation-by-wo-column",
                      url: "chart/the-situation-by-wo-column",
                      defaults: new { controller = "Dashboard", action = "GetSituationWOColumn", id = UrlParameter.Optional }
       );

            routes.MapRoute(
                     name: "Dashboard",
                     url: "dashboard",
                     defaults: new { controller = "Dashboard", action = "Index", title = UrlParameter.Optional }
      );

            routes.MapRoute(
            name: "sign-in",
            url: "sign-in",
            defaults: new { controller = "Account", action = "Login", ReturnURL = UrlParameter.Optional, error = "" }
         );
            routes.MapRoute(
            name: "log-out",
            url: "log-out",
            defaults: new { controller = "Account", action = "LogOut", ReturnURL = UrlParameter.Optional, error = "" }
         );
            routes.MapRoute(
     name: "about",
     url: "about",
     defaults: new { controller = "About", action = "Index", ReturnURL = UrlParameter.Optional }
  );

            routes.MapRoute(
            name: "Default",
            url: "{controller}/{action}/{id}",
            defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
        );

        }

    }
}
