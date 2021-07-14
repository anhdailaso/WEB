define(['_MasterPageController', '_MenuPageController', '_DetailsModalController', 'datatables-bootstrap', 'bootstrap-select'], function (module, menu, modal) {
    var UserRequest = (function () {
        var app = angular.module("UserRequestPage", [])
        app.controller('UserRequestController', function ($scope) {
            var Main = module.Main
            var Languages = module.Languages
            var InfoDetails = modal
            var Loading = module.Loading
            var Alert = module.Alert
            var MainMenu = menu
            var currentPageName = 'UserRequestWeb'
            var menuID = 'mnuListUserRequest'
            var buttonFloat = [
                {
                    "id": "btnMain",
                    "url": "#",
                    "icon": "<i class='fa fa-angle-double-up'></i>"
                },
                {
                    "id": "btnThem",
                    "url": urlAddRequest,
                    "icon": "<i class='fa fa-plus'></i>",
                    "lang": "btnThem"
                    
                },
                //them bo phan
                //{
                //    "id": "btnAdd",
                //    "url": "#",
                //    "icon": "<i class='fa fa-plus-circle'></i>",
                //    "lang": "btnThem",
                //    "func": "fn.thembophan"
                //},
                {
                    "id": "btnSua",
                    "url": "#",
                    "icon": "<i class='fa fa-pencil'></i>",
                    "lang": "btnSua",
                    "func": "fn.Sua"
                },
                //{
                //    "id": "btnRemove",
                //    "url": "#",
                //    "icon": "<i class='fa fa-trash-o'></i>",
                //    "lang": "btnXoa",
                //    "func": 'fn.Removebp'
                //},
                {
                    "id": "btnRemove",
                    "url": "#",
                    "icon": "<i class='fa fa-trash-o'></i>",
                    "lang": "btnXoa",
                    "func": 'fn.Removeyeucau'
                }
            ]
            var vars = {};
            var bindVariables = function () {
                return {
                    $tbRequest: $('#tbRequest'),
                    $tbRequestBody: $('#tbRequest tbody'),
                    $tabRequestDetail: $('a[href="#tabRequestDetail"]'),
                    $tbRequestDetailBody: $("#tbRequestInfo tbody"),
                };
            }

            //var tableBody = $('#tbDocument tbody');
            //$.post(urlGetDocument, { id: requestID, detailID: detailID, deviceID: deviceID }, function (data) {
            //    var i;
            //    tableBody.empty();
            //    if (data.length == 0) { tableBody.append('<tr><td colspan="2"></td></tr>'); }
            //    else {
            //        for (i = 0; i < data.length; i++) {
            //            tableBody.append('<tr data-id =' + data[i].ID + '><td>' + data[i].Path + '</td><td >' + data[i].Note + '</td></tr>');
            //        }
            //    }
            //});
            //danh sach yeu cau

            var fnPrivate = {
                FilterData: function () {
                    $.post(urlFilter, { fromDate: $('#fromDate').val(), toDate: $('#toDate').val(), createdBy: $('#cboCreatedBy').val(), MS_N_XUONG: $('#cboDiaDiem').val() }, function (data) {
                        $('#tbRequest').dataTable().fnClearTable();
                        $('#tbRequest').dataTable().fnDestroy();
                        $('#tbRequest').DataTable({
                            data: data,
                            columns: [
                                { data: 'UserRequestID' },
                                { data: 'DeviceID' },
                                { data: 'DateCreated' },
                                { data: 'HourCreated' },
                                { data: 'RequestedBy' },
                                { data: 'DateCompleted' },
                                { data: 'Email' },
                                { data: 'WorkSiteID' }
                            ]
                            , "columnDefs": [
                                { "width": "180px", "targets": [0] },
                                { "width": "80px", "text-align": "right", "targets": [1] },
                                { "width": "80px", "text-align": "right", "targets": [2] },
                                { "width": "80px", "text-align": "right", "targets": [3] },
                                { "width": "170px", "text-align": "right", "targets": [4] },
                                { "width": "100px", "text-align": "right", "targets": [5] },
                                { "width": "125px", "text-align": "right", "targets": [6] },
                                { "width": "130px", "text-align": "right", "targets": [7] }
                            ],
                            "language": {
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
                            "scrollY": 350,
                            "scrollX": true,
                            "processing": true,
                            'createdRow': function (row, data, dataIndex) {
                                if (data.hasOwnProperty("ID")) {
                                    $(row).attr('data-id', data.ID);
                                }
                            },
                            "order": [[0, 'desc']]
                        });
                        //$('#tbRequest').dataTable().row(':eq(0)', { page: 'current' }).select();
                        //$('#tbRequest').dataTable({
                        //    "order": [[0, 'asc'], [1, 'asc']]
                        //});
                        $($.fn.dataTable.tables(true)).DataTable().columns.adjust().draw(false);
                        Loading.fn.Hide();
                    });

                },
                TableYCChiTiet_RowChanged: function () {
                    var requestID = vars.$tbRequest.find('tr[class$="selected"]').attr('data-id');
                    var detailID = vars.$tbRequestDetailBody.find('tr[class$="selected"]').attr('data-detailid');
                    var deviceID = vars.$tbRequestDetailBody.find('tr[class$="selected"] td').attr('data-deviceid');
                    method.GetComponent(requestID, detailID, deviceID)
                    method.GetDocument(requestID, detailID, deviceID)
                    return false;
                },
                GetComponent: function (requestID, detailID, deviceID) {
                    var tableBody = $('#tbComponent tbody');
                    $.post(urlGetComponent, { id: requestID, detailID: detailID, deviceID: deviceID }, function (data) {
                        var i;
                        tableBody.empty();
                        if (data.length == 0) { tableBody.append('<tr><td colspan="5"></td></tr>'); }
                        else {
                            for (i = 0; i < data.length; i++) {
                                tableBody.append('<tr data-id=' + data[i].ID + '><td>' + data[i].ComponentID + '</td><td>' + data[i].ComponentName + '</td><td>' + data[i].SparePartID + '</td><td>' + data[i].SparePartName + '</td><td>' + data[i].LocationID + '</td></tr>');
                            }
                        }
                    });
                },
                GetDocument: function (requestID, detailID, deviceID) {
                    var tableBody = $('#tbDocument tbody');
                    $.post(urlGetDocument, { id: requestID, detailID: detailID, deviceID: deviceID }, function (data) {
                        var i;
                        tableBody.empty();
                        if (data.length == 0) { tableBody.append('<tr><td colspan="2"></td></tr>'); }
                        else {
                            for (i = 0; i < data.length; i++) {
                                tableBody.append('<tr data-id =' + data[i].ID + '><td>' + data[i].Path + '</td><td >' + data[i].Note + '</td></tr>');
                            }
                        }
                    });
                },
                TableYCChiTiet_ValueChanged: function () {
                    var id = vars.$tbRequest.find('tr[class$="selected"]').attr('data-id');
                    window.setTimeout(function () { method.GetRequestInfomation(id) }, 500);
                },
                TableYeuCau_RowChanged: function () {
                    var id = $(this).attr('data-id');
                    method.GetRequestInfomation(id)
                    window.setTimeout(function () { vars.$tabRequestDetail.tab('show') }, 500);
                },
                ShowThongTinYCChiTiet: function (e) {
                    $(this).each(function () {
                        if (!this.rowIndex) return;
                        InfoDetails.fn.Show($('#tbRequestInfo thead').find('th'), this.cells);
                    });
                },
                GetRequestInfomation: function (id) {
                    var tableBody = $('#tbRequestInfo tbody');
                    $.post(urlGetRequestInfomation, { id: id }, function (data) {
                        var i;
                        tableBody.empty();
                        if (data.length == 0) { tableBody.append('<tr><td colspan="8"><td></tr>'); }
                        else {
                            for (i = 0; i < data.length; i++) {
                                tableBody.append('<tr data-detailid=' + data[i].UserRequestDetailID + '><td data-deviceid = '
                                    + data[i].DeviceID + '>'
                                    + data[i].DeviceID + '</td><td >'
                                    + data[i].DeviceName + '</td><td >'
                                    + data[i].TEN_LOAI_YEU_CAU_BT + '</td><td >'
                                    + data[i].Description + '</td><td >'
                                    + data[i].TEN_NGUYEN_NHAN + '</td><td >'
                                    + data[i].Request + '</td><td class="hidden" >'
                                    + data[i].CauseID + '</td><td class="hidden" >'
                                    + data[i].TypeOfMaintenanceRequestID + '</td><td class="hidden">'
                                    + data[i].PriorityID + '</td><td>'
                                    + data[i].DateOccurred + '</td><td>'
                                    + data[i].HourOccurred + '</td><td>'
                                    + data[i].TEN_UU_TIEN + '</td></tr>'
                                );
                            }
                        }
                        vars.$tbRequestDetailBody.find('tr:first-child').addClass('selected')
                        method.TableYCChiTiet_RowChanged()
                    });
                },
            }
            var method
            $scope.fn = {
                Init: function () {
                    global.CurrentNamePage = currentPageName
                    MainMenu.fn.SetActive(menuID);
                    Languages.fn.AutoChangeLanguage()
                    //init variables
                    vars = bindVariables();
                    method = fnPrivate
                    //init event
                    vars.$tbRequest.DataTable({
                        "language":
                        {
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
                    $('.select2-container--classic').select2({ theme: "classic" });
                    vars.$tbRequestBody.on('dblclick', 'tr', method.TableYeuCau_RowChanged);
                    vars.$tbRequestBody.on('doubletap', 'tr', method.TableYeuCau_RowChanged);
                    vars.$tabRequestDetail.click(method.TableYCChiTiet_ValueChanged);
                    vars.$tbRequestDetailBody.on('click', 'tr', method.TableYCChiTiet_RowChanged);
                    vars.$tbRequestDetailBody.on('dblclick', 'tr', method.ShowThongTinYCChiTiet);
                    vars.$tbRequestDetailBody.on('doubletap', 'tr', method.ShowThongTinYCChiTiet);
                    $("#cboCreatedBy").change(function () {
                        Loading.fn.Show()
                        method.FilterData();
                    });
                    $("#cboDiaDiem").change(function () {
                        Loading.fn.Show()
                        method.FilterData();
                    });
                    Main.fn.InitButtonFloat(buttonFloat)
                    Main.fn.InitDateTimePickerChanged([$('#fromDate'), $('#toDate')], method.FilterData)
                    method.FilterData()
                },

                //Removebp
                Removebp: function () {
                    var id = $('#tbComponent tr[class$="selected"]').attr('data-id')
                    if (typeof id == 'undefined') {
                        Alert.fn.Show("Bạn cần chọn phụ tùng, bộ phận trước khi xóa!", Alert.Type.warning);
                        return false;
                    }
                    Alert.fn.ShowConfirm("Bạn có muốn xóa bộ phận, phụ tùng này không ? ", Alert.Type.info, "Xóa", function (result) {
                        if (result === true) {
                            $.post(urlRemovebp, { id: id }, function (data) {
                                if (data == "success") {
                                    $('#tbComponent tr[class$="selected"]').remove();
                                }
                                else {
                                    Alert.fn.Show(data, Alert.Type.error);
                                }
                            });
                            return false;
                        }
                    });
                },
                //removehinh
                Removehinh: function () {
                    var id = $('#tbDocument tr[class$="selected"]').attr('data-id');
                    if (typeof id == 'undefined') {
                        Alert.fn.Show("Bạn cần chọn hình trước khi xóa!", Alert.Type.warning);
                        return false;
                    }
                    Alert.fn.ShowConfirm("Bạn có muốn xóa bộ phận, phụ tùng này không ? ", Alert.Type.info, "Xóa", function (result) {
                        if (result === true) {
                            $.post(urlRemovehinh, { id: id }, function (data) {
                                if (data == "success") {
                                    $('#tbDocument tr[class$="selected"]').remove();
                                }
                                else {
                                    Alert.fn.Show(data, Alert.Type.error);
                                }
                            });
                            return false;
                        }
                    });
                },
                Removeyeucau: function () {
                    var id = $('#tbRequest tr[class$="selected"]').attr('data-id');
                    if (typeof id == 'undefined') {
                        Alert.fn.Show("Bạn cần chọn yêu cầu người sử dụng trước khi xóa!", Alert.Type.warning);
                        return false;
                    }
                    Alert.fn.ShowConfirm("Bạn có muốn xóa  yêu cầu người sử dụng này không ? ", Alert.Type.info, "Xóa", function (result) {
                        if (result === true) {
                            $.post(urlRemoveyeucau, { id: id }, function (data) {
                                if (data == "success") {
                                    $('#tbRequest tr[class$="selected"]').remove();
                                }
                                else {
                                    Alert.fn.Show(data, Alert.Type.error);
                                }
                            });
                            return false;
                        }
                    });
                },
                Sua: function () {
                    var id = $('#tbRequest tr[class$="selected"]').attr('data-id')
                    if (typeof id == 'undefined') {
                        Alert.fn.Show(Messenger.msgChonYCCanSua, Alert.Type.warning);
                        return false;
                    }
                    window.location.href = urlEditRequest + '-' + id;
                }
            }
        })
        app.init = function () {
            angular.bootstrap(document, ['UserRequestPage']);
        }
        return app;
    })();
    $(function () {
        UserRequest.init()
    })
})


