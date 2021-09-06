    requirejs.config({
    baseUrl: baseUrl,
    paths: {
        /*****************angularjs*****************/
        //'angular': 'https://cdnjs.cloudflare.com/ajax/libs/angular.js/1.6.7/angular.min',
        'angular': baseUrl + 'plugins/angular/angular.min',

        /*****************select2*****************/
        //'select2': 'https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.6-rc.0/js/select2.min',
        'select2': baseUrl + 'browser/select2.min',
        /******************jquery*******************/
        'jquery': baseUrl + 'plugins/jquery/jquery-1.12.2.min',
        'jquery-cookie': baseUrl + 'plugins/jquery/jquery.cookie',
        'jquery-doubletap': baseUrl + 'plugins/jquery/jquery.doubletap',
        'jquery.treegrid': baseUrl + 'plugins/jquery/jquery.treegrid',
        //'jquery-ui': 'http://ajax.googleapis.com/ajax/libs/jqueryui/1.9.1/jquery-ui.min',
        'jquery-ui': baseUrl + 'plugins/jquery/jquery-ui',
        /*****************boostrap******************/
        'bootstrap': baseUrl + 'plugins/bootstrap/bootstrap.min',
        'bootstrap-datetimepicker': baseUrl + 'plugins/bootstrap/bootstrap-datetimepicker.min',
        'bootstrap-select': baseUrl + 'plugins/bootstrap/bootstrap-select.min',
        //'moment': 'https://cdnjs.cloudflare.com/ajax/libs/moment.js/2.16.0/moment.min',
        'moment': baseUrl + 'plugins/moment.min',
        /*****************datatables****************/
        'datatables-jquery': baseUrl + 'plugins/datatables/jquery.dataTables.min',
        'datatables-bootstrap': baseUrl + 'plugins/datatables/dataTables.bootstrap4.min',
        'datatables-buttons': baseUrl + 'plugins/datatables/dataTables.buttons.min',
        'datatables-responsive': baseUrl + 'plugins/datatables/dataTables.responsive.min',
        'datatables-responsive-bs4': baseUrl + 'plugins/datatables/responsive.bootstrap4.min',
        'datatables-buttons-bs4': baseUrl + 'plugins/datatables/buttons.bootstrap4.min',
        'datatables-colvis': baseUrl + 'plugins/datatables/buttons.colVis.min',
        'datatables-html5': baseUrl + 'plugins/datatables/buttons.html5.min',
        'datatables-print': baseUrl + 'plugins/datatables/buttons.print.min',
        'datatables-jszip': baseUrl + 'plugins/datatables/jszip.min',
        'datatables-pdf': baseUrl + 'plugins/datatables/pdfmake.min',
        'datatables-fonts': baseUrl + 'plugins/datatables/vfs_fonts',
        'datatables-standing-redraw': baseUrl + 'plugins/datatables/fnStandingRedraw',
        /*******************linq********************/
        'linq': baseUrl + 'plugins/linq/linq.min',
        /*****************sweetalert****************/
        //'sweetalert': 'http://cdnjs.cloudflare.com/ajax/libs/limonte-sweetalert2/6.8.0/sweetalert2.min',
        'sweetalert': baseUrl + 'browser/sweetalert2.min',
        /*****************highcharts****************/
        //'highcharts': 'https://code.highcharts.com/js/highcharts',
        'highcharts': baseUrl + 'browser/highcharts',
        /*****************buttonfloat***************/
        'button-float': baseUrl + 'plugins/kc.fab.min',
        /******************treeview*****************/
        'treeview-widget': baseUrl + 'plugins/metro.min',
        //'core': 'http://cdnjs.cloudflare.com/ajax/libs/core-js/2.4.1/core',
        //'html5shiv': 'http://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv',
        //'respond': 'http://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min',
        'core': baseUrl + 'browser/core',
        'html5shiv': baseUrl + 'browser/html5shiv',
        'respond': baseUrl + 'browser/respond.min',
        ///*****************controller***************/
        '_MasterPageController': baseUrl + 'controller/_MasterPageController',
        '_SettingPageController': baseUrl + 'controller/_SettingPageController',
        '_MenuPageController': baseUrl + 'controller/_MenuPageController',
        '_DetailsModalController': baseUrl + 'controller/_DetailsModalController',
        'DashChartsController': baseUrl + 'controller/DashChartsController',
        'PeriodicMaintenanceController': baseUrl + 'controller/PeriodicMaintenanceController',
        'OrgUnitController': baseUrl + 'controller/OrgUnitController',
        'ApproveRequestsController': baseUrl + 'controller/ApproveRequestsController',
        'MonitoringController': baseUrl + 'controller/MonitoringController',
        'InitMonitoringController': baseUrl + 'controller/InitMonitoringController',
        'MasterPlansController': baseUrl + 'controller/MasterPlansController',
        'WorkOrderController': baseUrl + 'controller/WorkOrderController',
        'AddRequestController': baseUrl + 'controller/AddRequestController',

        'HazardReportDetailsController': baseUrl + 'controller/HazardReportDetailsController',

        'UserRequestController': baseUrl + 'controller/UserRequestController',
        'CalendarController': baseUrl + 'controller/CalendarController',
        'ReceiptRequestController': baseUrl + 'controller/ReceiptRequestController',
        'MyEcomainController': baseUrl + 'controller/MyEcomainController',
        'WorkWorkerController': baseUrl + 'controller/WorkWorkerController',
        'MornitoringDeviceController': baseUrl + 'controller/MornitoringDeviceController',
        'StopCardController': baseUrl + 'controller/StopCardController',
        'HazardReportController': baseUrl + 'controller/HazardReportController',
        
    },
    shim: {
        'bootstrap': {
            exports: 'jquery',  
            deps: ["jquery"]
        },
        'button-float': {
            exports: 'jquery',
            deps: ["jquery"]
        },
        'datatables-bootstrap': {
            exports: 'jquery',
            deps: ["jquery"]
        },
        'datatables-jquery': {
            exports: 'jquery',
            deps: ["jquery"]
        },
        'datatables-standing-redraw': {
            exports: 'jquery',
            deps: ["jquery", "datatables-jquery"]
        },
        'jquery-cookie': {
            exports: 'jquery',
            deps: ["jquery"]
        },
        'jquery-doubletap': {
            exports: 'jquery',
            deps: ["jquery"]
        },
        '_SettingPageController': {
            exports: 'jquery',
            deps: ["jquery", "jquery-cookie"]
        },
        'jquery': {
            exports: 'jquery'
        },
        'DashChartsController': {
            exports: 'angular',
            deps: ["angular"]
        },
        'OrgUnitController': {
            exports: 'angular',
            deps: ["angular"]
        },
        'UserRequestController': {
            exports: 'angular',
            deps: ['angular', 'select2']
        },
        'ReceiptRequestController': {
            exports: 'angular',
            deps: ['angular']
        },
        'MyEcomainController': {
            exports: 'angular',
            deps: ['angular','select2']
        },
        'WorkWorkerController': {
            exports: 'angular',
            deps: ['angular', 'select2']
        },
        'AddRequestController': {
            exports: 'angular',
            deps: ['angular', 'select2','jquery','jquery-ui']
        },

        'HazardReportDetailsController': {
            exports: 'angular',
            deps: ['angular', 'select2', 'jquery', 'jquery-ui']
        },

        'ApproveRequestsController': {
            exports: 'angular',
            deps: ['angular']
        },
        'MonitoringController': {
            exports: 'angular',
            deps: ['angular', 'select2']
        },
        'InitMonitoringController': {
            exports: 'angular',
            deps: ['angular', 'select2']
        },
        'MornitoringDeviceController': {
            exports: 'angular',
            deps: ['angular', 'select2']
        },
        'StopCardController': {
            exports: 'angular',
            deps: ['angular', 'select2']
        },
        'HazardReportController': {
            exports: 'angular',
            deps: ['angular', 'select2']
        },
        'PeriodicMaintenanceController': {
            exports: 'angular',
            deps: ['angular']
        },
        'MasterPlansController': {
            exports: 'angular',
            deps: ['angular']
        },
        'WorkOrderController': {
            exports: 'angular',
            deps: ['angular','select2']
        },
        'CalendarController': {
            exports: 'angular',
            deps: ['angular']
        },
        'angular': {
            exports: 'angular'
        },
        'dropzone': {
            exports: 'dropzone'
        },
    },
    map: {
        '*': {
            'datatables.net': 'datatables-jquery'
        }
    }
});
require(['bootstrap', 'jquery-doubletap', "core", "moment", "html5shiv", "respond"])
require(["_MenuPageController", "_DetailsModalController", "_MasterPageController", "_SettingPageController"], function (menu, info, module) { 
    //set global variable
    Theme = module.Theme
    Languages = module.Languages
    //-------------------------------------
    module.Session.fn.Init()//create session
    module.Main.fn.Init() //create main module
    info.fn.Init() //create modal
    menu.fn.Init() //create menu
});


