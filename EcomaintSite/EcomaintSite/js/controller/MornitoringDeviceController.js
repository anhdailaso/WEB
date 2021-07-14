define(['_MasterPageController', '_MenuPageController', '_DetailsModalController', 'datatables-bootstrap', 'bootstrap-select'], function (module, menu, modal) {
    var MornitoringDevice = (function () {
        var app = angular.module('MornitoringDeviceDevicePage', [])
        app.controller('MornitoringDeviceController', function ($scope, $compile) {
            var Main = module.Main
            var Languages = module.Languages
            var Convert = module.Convert
            var InfoDetails = modal
            var Loading = module.Loading
            var Alert = module.Alert
            var MainMenu = menu
            var BuildTreeView = module.BuildTreeView
            var menuID = 'mnuMornitoringDevice'
            var currentNamePage = 'MornitoringDeviceWeb'
            var buttonFloat = [
                {
                    "id": "btnMain",
                    "url": "#",
                    "icon": "<i class='fa fa-angle-double-up'></i>"
                },
                {
                    "id": "btnAdd",
                    "url": "#",
                    "icon": "<i class='fa fa-plus-circle'></i>",
                    "lang": "btnThem",
                    "func": "fn.Add"
                },
                {
                    "id": "btnEdit",
                    "url": "#",
                    "icon": "<i class='fa fa-pencil'></i>",
                    "lang": "btnSua",
                    "func": "fn.Edit"
                },
                {
                    "id": "btnRemove",
                    "url": "#",
                    "icon": "<i class='fa fa-trash'></i>",
                    "lang": "btnXoa",
                    "func": "fn.Remove"
                }
            ]
            var Action = {
                add: 0, edit: 1
            }
            var vars = {}
            var bindVariables = function () {
                return {
                    $action: Action.add,
                }
            }
            var fnPrivate = {FilterData: function () {
                },
            }

            var method
            $scope.fn = {
                Init: function () {
                    global.CurrentNamePage = currentNamePage
                    MainMenu.fn.SetActive(menuID);
                    Languages.fn.AutoChangeLanguage()
                    vars = bindVariables();
                    method = fnPrivate;
                },
            }
        })
        app.init = function () {
            angular.bootstrap(document, ['MornitoringDeviceDevicePage'])
        }
        return app;
    })();
    $(function () {
        MornitoringDevice.init()
    });
})