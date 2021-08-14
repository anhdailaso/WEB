define(['_MasterPageController', '_MenuPageController', '_DetailsModalController', 'datatables-bootstrap', 'bootstrap-select'], function (module, menu, modal) {
    var HazardReport = (function () {
        var app = angular.module('HazardReportPage', [])
        app.controller('HazardReportController', function ($scope, $compile) {
            var Main = module.Main
            var Languages = module.Languages
            var Convert = module.Convert
            var InfoDetails = modal
            var Loading = module.Loading
            var Alert = module.Alert
            var MainMenu = menu
            var BuildTreeView = module.BuildTreeView
            var menuID = 'mnuHazardReport'
            var currentNamePage = 'HazardReportWeb'
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
                    $HazardDatatables: 'undefined',
                    $tbListHazardBody: $('#tbListHazard tbody'),
                    $btnChooseHazard: $('#btnChooseHazard'),
                }
            }
            var fnPrivate = {
                GetHazardByID: function () {
                    var keys = vars.$HazardDatatables.data().count();
                    if (keys == 0) {
                        Alert.fn.Show(Messenger.msgDuLieuRong, Alert.Type.warning);
                        return;
                    }
                    keys = vars.$HazardDatatables.$('tr[class$=selected]')
                    if (keys.length == 0) {
                        Alert.fn.Show(Messenger.msgChonDevice, Alert.Type.warning)
                        return;
                    }
                    Loading.fn.Show()
                    $('#myModal').appendTo("body").modal('hide')

                    //window.setTimeout(function () { method.LoadGrid($('#tbListHazard tr[class$=selected]').attr('data-id'), 'button'); }, 500);
                    //window.setTimeout(function () { method.LoadSoGioChayMay($('#tbListHazard tr[class$=selected]').attr('data-id'), 'button'); }, 500);
                    //vars.$txtDevice.val(vars.$tbListHazard.find('tr[class$=selected]').attr('data-id'))
                },
                LoadEdits: function () {
                    alert("1");
                    var keys = vars.$HazardDatatables.data().count();
                    if (keys == 0) {
                        Alert.fn.Show(Messenger.msgDuLieuRong, Alert.Type.warning);
                        return;
                    }
                    keys = vars.$HazardDatatables.$('tr[class$=selected]');
                    if (keys.length == 0) {
                        Alert.fn.Show(Messenger.msgChonDevice, Alert.Type.warning);
                        return;
                    }
                    Loading.fn.Show()
                    $('#myModal').appendTo("body").modal('hide');
                    window.location.href = urlEditHazardReport + '?id=' + $('#tbListHazard tr[class$=selected]').attr('data-id');
                }
            }

            var method
            $scope.fn = {
                Init: function () {
                    global.CurrentNamePage = currentNamePage
                    MainMenu.fn.SetActive(menuID);
                    Languages.fn.AutoChangeLanguage()
                    vars = bindVariables();
                    method = fnPrivate;
                    vars.$tbListHazardBody.on('doubletap', 'tr', method.LoadEdits);
                    vars.$tbListHazardBody.on('dblclick', 'tr', method.LoadEdits);
                    vars.$btnChooseHazard.click(method.LoadEdits);
                    $(".select2").select2(
                        {
                            theme: "classic"
                        });

                    $("#DocDate").datetimepicker({
                        format: 'DD/MM/YYYY',
                        defaultDate: new Date(),
                        useCurrent: false
                    });



                    $("#Createdtime").datetimepicker({
                        format: 'DD/MM/YYYY',
                        defaultDate: new Date(),
                        useCurrent: false
                    });


                    $("#DocTime").datetimepicker({
                        format: 'HH:mm',
                        defaultDate: new Date(),
                        useCurrent: false
                    });
                    if ($('#stt').val() === "-1") {
                        $("#DocDate").val(new Date().toLocaleString('en-GB').substr(0, 10));
                        $("#Createdtime").val(new Date().toLocaleString('en-GB').substr(0, 10));
                    }

                },
                ShowListHazard: function () {
                    $.post(urlGetListHazardReport, { tungay: $('#fromDate').val(), denngay: $('#toDate').val() }, function (data) {
                      
                            if ($.fn.DataTable.isDataTable('#tbListHazard')) {
                                $('#tbListHazard').dataTable().fnDestroy();
                            }
                            vars.$HazardDatatables = $("#tbListHazard").DataTable({
                                data: data,
                                columns: [
                                    { data: 'ID' },
                                    { data: 'DocNum' },
                                    { data: 'Description' },
                                    { data: 'DocDate' },
                                    { data: 'CreatedBy' },
                                    { data: 'NGUOITH' },
                                    { data: 'Status' },
                                    { data: 'TTTH' },
                                    { data: 'NLQVP' }
                                ],
                                columnDefs: [
                                    {
                                        "targets": [0],
                                        "visible": false,
                                        "searchable": false
                                    },
                                ],
                                "language":
                                {
                                    "processing": "<div class='overlay custom-loader-background'><i class='fa fa-cog fa-spin custom-loader-color'></i></div>",
                                    "sSearch": "<span data-lang='lblSearch'></span> ",
                                    "info": "",
                                    "zeroRecords": "<span data-lang='lblFilterInfo'>" + (global.TypeLanguage == 0 ? "Không tìm thấy" : "No matching records found") + "</span>",
                                    "lengthMenu": "<span data-lang='lblShow'></span> _MENU_ <span data-lang='lblEntries'></span>",
                                    "infoEmpty": "",
                                    "infoFiltered": "",
                                    "paginate": {
                                        "first": "<<",
                                        "last": ">>",
                                        "next": ">",
                                        "previous": "<"
                                    },
                                    "emptyTable": "<span data-lang='lblEmpty'></span>",
                                },
                                "processing": true,
                                "lengthChange": false,
                                "lengthMenu": [5],
                                createdRow: function (row, data, dataIndex) {
                                    if (data.hasOwnProperty("ID")) {
                                        $(row).attr('data-id', data.ID);
                                    }
                                },
                            });
                            $('#myModal').appendTo("body").modal('show')
                    });
                },
            }
        })
        app.init = function () {
            angular.bootstrap(document, ['HazardReportPage'])
        }
        return app;
    })();
    $(function () {
        HazardReport.init()
    });
})