define(['_MasterPageController', '_MenuPageController', '_DetailsModalController', 'datatables-bootstrap', 'bootstrap-select'], function (module, menu, modal) {
    var ApproveRequests = (function () {
        var app = angular.module('ApproveRequestsPage', [])
        app.controller('ApproveRequestsController', function ($scope) {
            var Main = module.Main
            var Languages = module.Languages
            var Alert = module.Alert
            var Loading = module.Loading
            var InfoDetails = modal
            var MainMenu = menu
            var menuID = 'mnuApproveRequest'
            var currentPageName = 'ApproveRequestWeb'
            var buttonFloat = [{
                "id": "btnMain",
                "url": "#",
                "icon": "<i class='fa fa-angle-double-up'></i>"
            }, {
                "id": "btnApproved",
                "url": "#",
                "icon": "<i class='fa fa-check-square-o'></i>",
                "lang": "btnDuyet",
                "func": 'fn.Approved'
            }, {
                "id": "btnNotApprove",
                "url": "#",
                "icon": "<i class='fa fa-square-o'></i>",
                "lang": "btnKhongDuyet",
                "func": 'fn.NotApprove'
            }, {
                "id": "btnExecute",
                "url": "#",
                "icon": "<i class='fa fa-pencil'></i>",
                "lang": "btnThucHien",
                "hidden": "true",
                "func": 'fn.Execute'
            }, {
                "id": "btnCancel",
                "url": "#",
                "icon": "<i class='fa fa-reply'></i>",
                "lang": "btnHuy",
                "hidden": "true",
                "func": 'fn.Cancel'
            }
            ]
            var vars = {}
            var bindVariables = function () {
                return {
                    $table: 'undefined',
                    $btnSave: $('#btnSave'),
                    $btnApproved: $('#btnApproved'),
                    $btnNotApprove: $('#btnNotApprove'),
                    $btnExecute: $('#btnExecute'),
                    $btnCancel: $('#btnCancel'),
                    $tbApproveBody: $('#tbApproveUserRequest tbody'),
                };
            }
            var method
            var fnPrivate = {
                FilterData: function () {
                    $.post(urlFilterData, { fromDate: $('#fromDate').val(), toDate: $('#toDate').val(), option: $('#cboOption').val() }, function (data) {
                        $('#tbApproveUserRequest').dataTable().fnDestroy();
                        vars.$table = $('#tbApproveUserRequest').DataTable({
                            data: data,
                            columns: [
                                { data: 'Choose' },
                                { data: 'DeviceID' },
                                { data: 'Description' },
                                { data: 'Request' },
                                { data: 'TypeOfMaintenanceName' },
                                { data: 'RequestNO' },
                                { data: 'DateCreated' },
                                { data: 'RequestedBy' },
                                { data: 'PriorityName' },
                                { data: 'IsApprovedRequest' }
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
                            columnDefs: [
                                {
                                    'targets': 0,
                                    'width': '60px',
                                    'searchable': false,
                                    'orderable': false,
                                    'className': 'dt-body-center',
                                    'data-toggle': "buttons",
                                    'render': function (data, type, full, meta) {
                                        return '<input type="checkbox" disabled  />';
                                    }
                                },
                                { 'width': '150px', 'targets': 0 },
                                { 'width': '150px', 'targets': 1 },
                                { 'width': '150px', 'targets': 2 },
                                { 'width': '150px', 'targets': 3 },
                                { 'width': '80px', 'targets': 4 },
                                { 'width': '110px', 'targets': 5 },
                                { 'width': '150px', 'targets': 6 },
                                { 'width': '60px', 'targets': 7 },
                                { 'width': '150px', 'targets': 8 },
                                {'targets': 9,'visible':false }
                            ],
                            scrollY: 350,
                            scrollX: true,
                            createdRow: function (row, data, dataIndex) {
                                if (data.hasOwnProperty("ID")) {
                                    $(row).attr('data-id', data.ID);
                                    $(row).attr('data-requestDetailID', data.UserRequestDetailID);
                                    $(row).attr('data-deviceid', data.DeviceID);
                                }
                            }
                        });

                        $($.fn.dataTable.tables(true)).DataTable().columns.adjust().draw(false);
                        if ($('#cboStatus').val() == 'tabApproved') {
                            $('#tbApproveUserRequest').DataTable().column(9).search('True').draw();
                        }
                        else if ($('#cboStatus').val() == 'tabNotApprove') {
                            $('#tbApproveUserRequest').DataTable().column(9).search('False').draw();
                        }
                        else {
                            $('#tbApproveUserRequest').DataTable().columns(9).search('').draw();
                        }
                        Loading.fn.Hide();
                    });
                },
                Save: function () {
                    var tmp = $('#dtTime').val();
                    if (tmp === '') {
                        Alert.fn.Show(Messenger.msgChonNgayYC, Alert.Type.warning);
                        return false;
                    }
                    var lstAsk = new Array();
                    var j = 0;
                    vars.$table.$('input[type="checkbox"]:checked').each(function () {
                        lstAsk[j] = new Object();
                        lstAsk[j].UserRequestID = $(this).closest('tr').attr('data-id');
                        lstAsk[j].DeviceID = $(this).closest('tr').attr('data-deviceid');
                        lstAsk[j].ID = $(this).closest('tr').attr('data-requestDetailID');
                        lstAsk[j].ApprovedRequestBy = $('#txtUser').val();
                        lstAsk[j].ApprovalNote = $('#txtOpinion').val();
                        lstAsk[j].DateApproveRequested = $('#dtTime').val().split('/')[2] + '/' + $('#dtTime').val().split('/')[1] + '/' + $('#dtTime').val().split('/')[0];
                        lstAsk[j].IsExecuteApproveRequested = $('#ckExecute').is(":checked") ? true : false;
                        lstAsk[j].PriorityID = $('#cboPriority').val();
                        lstAsk[j].EmailByApproveRequested = $('#txtMailTo').val();
                        j++;
                    });
                    var stringData = JSON.stringify(lstAsk);
                    $.post(urlApproveRequests, { data: stringData }, function (data) {
                        if (data == "success") {
                            vars.$table.$('input[type="checkbox"]:checked').each(function () {
                                var row = $(this).closest('tr').get(0);
                                vars.$table.row(row).remove().draw(false)
                            });
                            if (Alert.fn.Show(Messenger.msgDuyetThanhCong, Alert.Type.success, Messenger.msgInfo) === true) {
                                method.DisableControl(false);
                                method.HiddenButton();
                            }
                        }
                        else {
                            Alert.fn.Show(Messenger.msgDuyetKhongThanhCong + ' (' + data + ')', Alert.Type.error, Messenger.msgLoi)
                        }
                    });
                },
                DisableControl: function (values) {
                    $('#cboOption').prop('disabled', values)
                    $('#cboStatus').prop('disabled', values)
                    $('#toDate').prop('disabled', values)
                    $('#fromDate').prop('disabled', values)
                },
                HiddenButton: function () {
                    if (vars.$btnApproved.parent().hasClass('toolbar-action')) { //button for desktop
                        vars.$btnApproved.toggleClass('hidden');
                        vars.$btnNotApprove.toggleClass('hidden');
                        vars.$btnExecute.toggleClass('hidden');
                        vars.$btnCancel.toggleClass('hidden');
                    }
                    else { //button for mobile
                        vars.$btnApproved.closest('div').toggleClass('hidden');
                        vars.$btnNotApprove.closest('div').toggleClass('hidden');
                        vars.$btnExecute.closest('div').toggleClass('hidden');
                        vars.$btnCancel.closest('div').toggleClass('hidden');
                    }
                },
                ToggleCheckboxesOnTable: function () {
                    var $checkbox = $(this).find(':checkbox');
                    if (!$checkbox.attr('disabled')) {
                        $checkbox.prop('checked', !$checkbox.prop('checked'))
                    }
                },
                ShowInfo: function () {
                    $(this).each(function () {
                        if (!this.rowIndex) return;
                        InfoDetails.fn.Show($($('#tbApproveUserRequest thead').find('th')), this.cells);
                        return false;
                    });
                }
            };
            $scope.fn = {
                Init: function () {
                    global.CurrentNamePage = currentPageName
                    MainMenu.fn.SetActive(menuID);
                    Languages.fn.AutoChangeLanguage()
                    Main.fn.InitButtonFloat(buttonFloat)
                    $scope.cboOption = '0';
                    $scope.cboStatus = 'tabApproved';
                    method = fnPrivate
                    vars = bindVariables();
                    vars.$table = $("#tbApproveUserRequest").DataTable({
                        "language": {
                            "processing": "<div class='overlay custom-loader-background'><i class='fa fa-cog fa-spin custom-loader-color'></i></div>",
                            "sSearch": "<span data-lang='lblSearch'>" + (global.TypeLanguage == 0 ? "Tìm " : "Search") + "</span> ",
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
                            "emptyTable": "<span data-lang='lblEmpty'>" + (global.TypeLanguage == 0 ? "Không có dữ liệu" : "No data available in table") + "</span>",
                        }
                    });
                    vars.$btnSave.click(method.Save)
                    vars.$tbApproveBody.on('click', 'tr', method.ToggleCheckboxesOnTable);
                    vars.$tbApproveBody.on('dblclick', 'tr', method.ShowInfo);
                    vars.$tbApproveBody.on('doubletap', 'tr', method.ShowInfo);
                    //$("#cboCreatedBy").change(function () {
                    //    Loading.fn.Show()
                    //    method.FilterData();
                    //});
                    Main.fn.InitDateTimePickerChanged([$('#fromDate'), $('#toDate')],
                        method.FilterData)
                    method.FilterData()
                },
                Approved: function () {
                    var keys = vars.$table.data().count();
                    if (keys == 0) {
                        Alert.fn.Show(Messenger.msgDuLieuRong, Alert.Type.warning);
                        return;
                    }
                    vars.$table.$('input[type="checkbox"]').prop('disabled', false)
                    method.HiddenButton()
                    $('#hfStatus').val('true');
                    method.DisableControl(true);
                },
                NotApprove: function () {
                    var keys = vars.$table.data().count();
                    if (keys == 0) {
                        Alert.fn.Show(Messenger.msgDuLieuRong, Alert.Type.warning);
                        return;
                    }
                    vars.$table.$('input[type="checkbox"]').prop('disabled', false)
                    method.HiddenButton()
                    $('#hfStatus').val('true');
                    method.DisableControl(true);
                },
                Execute: function () {
                    var keys = vars.$table.$('input[type="checkbox"]:checked')
                    if (keys.length == 0) {
                        Alert.fn.Show(Messenger.msgChonYCCanDuyet, Alert.Type.warning)
                        return;
                    }
                    $('#txtUser').val($('#hfUsername').val());
                    $('#txtOpinion').val('');
                    $('#txtMailTo').val('');
                    $("#cboPriority option:first").prop('selected', true)
                    var option = $('#cboOption').val();
                    if (option == '1') { //tabChuaXuLy
                        $('#ckExecute').prop('checked', false);
                    }
                    else if (option == '2') { //tabKhongThucHien
                        $('#ckExecute').prop('checked', true);
                    }
                    $('#dialogSave').appendTo("body").modal('show')
                },
                Cancel: function () {
                    method.HiddenButton()
                    vars.$table.$('input[type="checkbox"]').prop('disabled', true)
                    vars.$table.$('input[type="checkbox"]').removeAttr('checked')
                    method.DisableControl(false);
                },
                DropdownValueChanged: function () {
                    var status = $('#cboStatus').val();
                    var option = $('#cboOption').val();
                    if (status == 'tabApproved') {//được duyệt
                        if (option == '0') { //tabChuaKiemTra
                            vars.$btnApproved.prop("disabled", false);
                            vars.$btnNotApprove.prop("disabled", false);
                        }
                        else if (option == '1') { //tabChuaXuLy
                            vars.$btnApproved.prop("disabled", true);
                            vars.$btnNotApprove.prop("disabled", false);
                        }
                        else if (option == '2') { //tabKhongThucHien
                            vars.$btnApproved.prop("disabled", false);
                            vars.$btnNotApprove.prop("disabled", true);
                        }
                        //Loading.fn.Show()
                        //window.setTimeout(function () { method.FilterData(); }, 1500);
                    }
                    else {
                        vars.$btnApproved.prop("disabled", true);
                        vars.$btnNotApprove.prop("disabled", true);
                    }
                    if (status == 'tabApproved') {
                        $('#tbApproveUserRequest').DataTable().column(9).search('True').draw();
                    }
                    else if (status == 'tabNotApprove') {
                        $('#tbApproveUserRequest').DataTable().column(9).search('False').draw();
                    }
                    else {
                        $('#tbApproveUserRequest').DataTable().column(9).search('').draw();
                    }
                },
                DropdownValueChanged1: function () {
                    var status = $('#cboStatus').val();
                    var option = $('#cboOption').val();
                    if (option == '0') { //tabChuaKiemTra
                        vars.$btnApproved.prop("disabled", false);
                        vars.$btnNotApprove.prop("disabled", false);
                    }
                    else if (option == '1') { //tabChuaXuLy
                        vars.$btnApproved.prop("disabled", true);
                        vars.$btnNotApprove.prop("disabled", false);
                    }
                    else if (option == '2') { //tabKhongThucHien
                        vars.$btnApproved.prop("disabled", false);
                        vars.$btnNotApprove.prop("disabled", true);
                    }
                    Loading.fn.Show()
                    window.setTimeout(function () { method.FilterData(); }, 1500);
                }
            }
        })
        app.init = function () {
            angular.bootstrap(document, ['ApproveRequestsPage']);
        }
        return app;
    })();
    $(function () {
        ApproveRequests.init()
    });
})


