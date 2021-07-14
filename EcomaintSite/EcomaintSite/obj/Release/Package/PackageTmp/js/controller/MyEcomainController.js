define(['_MasterPageController', '_MenuPageController', '_DetailsModalController', 'datatables-bootstrap', 'bootstrap-select'], function (module, menu, modal) {
    var MyEcomain = (function () {
        var app = angular.module("MyEcomainPage", [])
        app.controller('MyEcomainController', function ($scope, $compile) {
            var Main = module.Main
            var Languages = module.Languages
            var InfoDetails = modal
            var Loading = module.Loading
            var Alert = module.Alert
            var MainMenu = menu
            var currentPageName = 'MyEcomainWeb'
            var menuID = 'mnuMyEcomain'
            var buttonFloat = [
                //{
                //    "id": "btnbt",
                //    "url": "#",
                //    "func": "fn.ShowPBT"
                //}
            ]
            var vars = {};
            var bindVariables = function () {
                return {
                    //$tbPhieuBaoTri: $('#tbphieubaotri'),
                };
            }
            var fnPrivate = {
                FilterData: function () {
                    $.post(urlMyecomain, {
                        fromDate: $('#fromDate').val(), toDate: $('#toDate').val(), ms_nx: $("#cbbDiaDiem").val(), may: $("#cbbThietBi").val(), giadoan: $('input[name = "optradio"]:checked').val()
                    }, function (data) {
                        $('#tbmyecomain').dataTable().fnClearTable();
                        $('#tbmyecomain').dataTable().fnDestroy();
                        $('#tbmyecomain').DataTable({
                            data: data,
                            columns: [
                                { data: 'MS_MAY' },
                                { data: 'TEN_MAY' },
                                {
                                    data: 'PBT',
                                    render: function (data, type, row) {
                                        if (type === 'display') {
                                            //return '<input type="checkbox" disabled="disabled"  class="editor-pbt">';
                                            return '<a type="button" class="btn btn-default disabled" name = "btnbt" href="MyEcomain/ShowPBT/' + row['MS_MAY'] + '?nhaxuong=' + $("#cbbDiaDiem").val() + '&fromday=' + $('#fromDate').val() + '&today=' + $('#toDate').val() + '"> <span class="badge">No</span></a>';
                                        }
                                        return data;
                                    },
                                    className: "dt-body-right"
                                },
                                {
                                    data: 'GSTT',
                                    render: function (data, type, row) {
                                        if (type === 'display') {
                                            //return '<input type="checkbox" disabled="disabled"  class="editor-gs">';
                                            return '<a type="button" class="btn btn-default disabled" name = "btngs" href="MyEcomain/ShowGSTT/' + row['MS_MAY'] + '?nhaxuong=' + $("#cbbDiaDiem").val() + '&fromday=' + $('#fromDate').val() + '&today=' + $('#toDate').val() + '"> <span class="badge">No</span></a>';
                                        }
                                        return data;
                                    },
                                    className: "dt-body-right"
                                }
                            ],
                            "columnDefs": [
                                { "width": "30%", "targets": [0] },
                                { "width": "100%", "text-align": "right", "targets": [1] },
                                { "width": "20%", "text-align": "right", "targets": [2] },
                                { "width": "20%", "text-align": "right", "targets": [3] }
                            ],
                            rowCallback: function (row, data) {
                                //// Set the checked state of the checkbox in the table
                                //$('input.editor-pbt', row).prop('checked', data.PBT == 1);
                                //$('input.editor-gs', row).prop('checked', data.GSTT == 1);
                                if (data.PBT === 1) {
                                    $("[name=btnbt]", row).addClass("btn").removeClass("btn-default disabled");
                                    $("[name=btnbt] .badge", row).empty();
                                    $("[name=btnbt] .badge", row).append("Yes");
                                }
                                if (data.GSTT === 1) {
                                    $("[name=btngs]", row).addClass("btn").removeClass("btn-default disabled");
                                    $("[name=btngs] .badge", row).empty();
                                    $("[name=btngs] .badge", row).append("Yes");
                                }
                            },
                            "language": {
                                "processing": "<div class='overlay custom-loader-background'><i class='fa fa-cog fa-spin custom-loader-color'></i></div>",
                                "sSearch": "<span data-lang='lblSearch'></span> ",
                                "info": "",
                                "zeroRecords": "<span data-lang='lblFilterInfo'>" + (global.TypeLanguage === 0 ? "Không tìm thấy" : "No matching records found") + "</span>",
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
                            "scrollY": 350,
                            "bLengthChange": false,
                            "scrollX": true,
                            "processing": true,
                            'createdRow': function (row, data, dataIndex) {
                                if (data.hasOwnProperty("ID")) {
                                    $(row).attr('data-id', data.MS_MAY);
                                }
                            },
                            "order": [[0, 'desc']]
                        });
                        $($.fn.dataTable.tables(true)).DataTable().columns.adjust().draw(false);
                        Loading.fn.Hide();
                    });
                },
                EvenRadio: function () {
                    $('input[type=radio][name=optradio]').change(function () {
                        if (this.value === 0) {
                            $('#toDate').hide();
                            $('[data-lang=lblDenngay]').hide();
                            $("#fromDate").val(new Date().toLocaleString('en-GB').substr(0, 10));
                        }
                        else if (this.value === 1) {
                            $('#toDate').toggle();
                            $('[data-lang=lblDenngay]').toggle();

                            var CurrentDate = new Date();
                            CurrentDate.setMonth(CurrentDate.getMonth() - 1);
                            $("#fromDate").val(CurrentDate.toLocaleString('en-GB').substr(0, 10));
                            $("#toDate").val(new Date().toLocaleString('en-GB').substr(0, 10));
                        }
                        method.FilterData();
                    });
                },
                LoadMay: function () {
                    $('#cbbThietBi option').remove();
                    $.ajax({
                        type: "POST",
                        url: urlDevices,
                        data: { WorkSiteID: $('#cbbDiaDiem').val()},
                        success: function (data) {
                            var s = '<option value=-1> < ALL > </option>';
                            for (var i = 0; i < data.length; i++) {
                                s += '<option value="' + data[i].ID + '">' + data[i].Name + '</option>';
                            }
                            $("#cbbThietBi").html(s);
                        }
                    });
                }
            }
            var method;
            $scope.fn = {
                Init: function () {
                    global.CurrentNamePage = currentPageName
                    MainMenu.fn.SetActive(menuID);
                    Languages.fn.AutoChangeLanguage()
                    //init variables
                    vars = bindVariables();
                    method = fnPrivate;
                    method.EvenRadio();
                    method.LoadMay();
                    method.FilterData();
                    $("#cbbDiaDiem").change(function () {
                        method.FilterData();
                        method.LoadMay();
                    });
                    Main.fn.InitDateTimePickerChanged([$('#fromDate'), $('#toDate')], method.FilterData);
                    Main.fn.ScanBarCode($('#ReadBtn'), $('#fileToUpload'), $('#cbbThietBi'), '#cbbThietBi option');
                    Main.fn.InitButtonFloat(buttonFloat);
                    $('.select2-container--classic').select2({ theme: "classic" });
                }
            }
        })
        app.init = function () {
            angular.bootstrap(document, ['MyEcomainPage']);
        }
        return app;
    })();
    $(function () {
        MyEcomain.init()
    })
})


