define(['_MasterPageController', '_MenuPageController', '_DetailsModalController', 'datatables-bootstrap', 'bootstrap-select'], function (module, menu, modal) {
    var ReceiptRequest = (function () {
        var app = angular.module("ReceiptRequestPage", [])
        app.controller('ReceiptRequestController', function ($scope) {
            var Main = module.Main
            var Languages = module.Languages
            var InfoDetails = modal
            var Loading = module.Loading
            var Alert = module.Alert
            var MainMenu = menu
            var currentPageName = 'ReceiptRequestWeb'
            var menuID = 'mnuReceiptRequest'
            //var buttonFloat = [
            //    {
            //        "id": "btnMain",
            //        "url": "#",
            //        "icon": "<i class='fa fa-angle-double-up'></i>"
            //    }
            //]
            var vars = {};
            var bindVariables = function () {
                return {
                    //$tbRequest: $('#tbRequest'),
                    
                };
            }
            var fnPrivate = {
            }

            var method
            $scope.fn = {
                Init: function () {
                    alert("ReceiptRequestController");
                    global.CurrentNamePage = currentPageName
                    MainMenu.fn.SetActive(menuID);
                    Languages.fn.AutoChangeLanguage()
                    //init variables
                    vars = bindVariables();
                    method = fnPrivate
                    //init event
                }
            }
        })
        app.init = function () {
            angular.bootstrap(document, ['ReceiptRequestPage']);
        }
        return app;
    })();
    $(function () {
        ReceiptRequest.init()
    })
})


