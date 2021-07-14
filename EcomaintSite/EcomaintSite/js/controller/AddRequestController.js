define(['_MasterPageController', '_MenuPageController', 'bootstrap-select'], function (module, menu) {
    var AddRequest = (function () {
        var app = angular.module('AddRequestPage', [])
        app.controller('AddRequestController', function ($scope) {
            //Tạo các biến để sử dụng các module trong file hiện tại
            var Convert = module.Convert
            var Main = module.Main
            var Languages = module.Languages
            var Alert = module.Alert
            var MainMenu = menu
            var menuID = 'mnuListUserRequest'
            var currentNamePage = 'UserRequestWeb'
            var request = "";

            // struct of Buttons floating for mobile
            var buttonFloat = [
                {
                    "id": "btnMain",
                    "url": "#",
                    "icon": "<i class='fa fa-angle-double-up'></i>"
                },
                {
                    "id": "btnAdd",
                    "url": '#',
                    "icon": "<i class='fa fa-plus'></i>",
                    "lang": "btnThem",
                    "func": "fn.Add" //set tên hàm tương ứng
                },
                {
                    "id": "btnEdit",
                    "url": '#',
                    "icon": "<i class='fa fa-pencil'></i>",
                    "lang": "btnSua",
                    "func": "fn.Edit"
                },
                {
                    "id": "btnRemove",
                    "url": '#',
                    "icon": "<i class='fa fa-trash'></i>",
                    "lang": "btnXoa",
                    "func": "fn.Remove"
                },
                {
                    "id": "btnSave",
                    "url": '#',
                    "icon": "<i class='fa fa-floppy-o'></i>",
                    "lang": "btnGhi",
                    "func": "fn.CheckValidateControl"
                },
                {
                    "id": "btnBack",
                    "url": urlRequest,
                    "icon": "<i class='fa fa-reply'></i>",
                    "lang": "btnTroVe"
                }
            ]
            var fnPrivate = {
                DisabledButton: function (flag) {
                    $('#btnRemove').attr('disabled', flag)
                    $('#btnEdit').prop('disabled', flag)
                }
            }
            var method = 'undefined'
            function xlchuoi(resulst) {
                var dsmail = $("#txtEmail").val();
                if (dsmail.split(";").length == 1) {
                    return resulst;
                }
                else {
                    return dsmail.substring(0, dsmail.lastIndexOf(";")) + ';' + resulst;
                }
            }
            $scope.fn = {
                Init: function () {
                    global.CurrentNamePage = currentNamePage // Set CurrentNamePage = 'UserRequestWeb' to change to languages for Page
                    MainMenu.fn.SetActive(menuID) //Set highlight for menuitem
                    Languages.fn.AutoChangeLanguage()
                    Main.fn.InitButtonFloat(buttonFloat)//Init button float for mobile
                    method = fnPrivate
                    if ($('#hfID').val() == "-1") {
                        $('#cboWorkSite').val('-1');
                    }
                    else {
                        $('#cboWorkSite').val($('#workSiteID').val());
                    }
                    Main.fn.ScanBarCode($('#ReadBtn'), $('#fileToUpload'), $('#cboDevice'), '#cboDevice option');
                    $('#tbNguoiYeuCau tbody').on('dblclick', 'tr', function () {
                        var tennyc = $('#tbNguoiYeuCau tbody').find('tr[class$=selected] td:nth-child(2)').text();
                        $('#ModalNCY').appendTo("body").modal('hide');
                        $('#cboCreatedBy').val(tennyc);
                    });
                    $('#tbRequestInfo tbody').on('click', 'tr', function () {
                        if ($(this).is('[new]')) {
                            method.DisabledButton(false);
                        }
                        else {
                            method.DisabledButton(true);
                        }
                    });
                    $('#btnChooseNYC').click(function () {
                        var tennyc = $('#tbNguoiYeuCau tbody').find('tr[class$=selected] td:nth-child(2)').text();
                        $('#ModalNCY').appendTo("body").modal('hide');
                        $('#cboCreatedBy').val(tennyc);
                    });
                    var rowCount = $('#tbRequestInfo >tbody >tr').length;
                    if (rowCount == 0) {
                        $('#cboWorkSite').attr('disabled', false);
                    }
                    else {
                        $('#cboWorkSite').attr('disabled', true);
                    }
                    $('.select2-container--classic').select2({ theme: "classic" });
                },
                CheckValidateControl: function () {
                    if (request == $('#txtRequestNum').val()) return;
                    if ($('#hfID').val() == -1) {
                        var countRow = $('#hfCount').val();
                        if (countRow == 0) {
                            Alert.fn.Show(Messenger.msgDSMayRong, Alert.Type.warning);
                            $('.glyphicon-plus').addClass('info')
                            return false;
                        }
                        var tmp = $('#dateOfRequest').val()
                        if (tmp == '') {
                            Alert.fn.Show(Messenger.msgChonNgayYC, Alert.Type.warning);
                            return false;
                        }
                        tmp = $('#timeOfRequest').val()
                        if (tmp == '') {
                            Alert.fn.Show(Messenger.msgChonGioYC, Alert.Type.warning);
                            return false;
                        }
                        if ($('#completedDate').val() != '') {
                            var ngayYC = Convert.fn.DateTime($('#dateOfRequest').val(), 'MM/dd/yyyy')
                            var ngayHT = Convert.fn.DateTime($('#completedDate').val(), 'MM/dd/yyyy')

                            if (ngayHT < ngayYC) {
                                Alert.fn.Show(Messenger.msgNgayHTLonHonNgayYC, Alert.Type.warning);
                                return false;
                            }
                        }
                        var nguoiYC = $('#cboCreatedBy').val();
                        if (nguoiYC == null || nguoiYC.trim() == "" || nguoiYC.length == 0) {
                            $('#btnSave').prop("disabled", false);
                            Alert.fn.Show(Messenger.msgChonNguoiYC, Alert.Type.warning);
                            return false;
                        }
                    }
                    request = $('#txtRequestNum').val();
                    var lstRequest = new Array();
                    lstRequest[0] = new Object();
                    lstRequest[0].ID = $('#hfID').val();
                    lstRequest[0].Email = $('#txtEmail').val();
                    lstRequest[0].UserRequestID = $('#txtRequestNum').val();
                    lstRequest[0].RequestNO = $('#txtRequestNum').val();
                    lstRequest[0].RequestedBy = $('#cboCreatedBy').val();
                    lstRequest[0].WorkSiteID = $('#cboWorkSite').val();
                    lstRequest[0].DateCreated = $('#dateOfRequest').val().split('/')[2] + '/' + $('#dateOfRequest').val().split('/')[1] + '/' + $('#dateOfRequest').val().split('/')[0];
                    lstRequest[0].HourCreated = '1900/01/01 ' + $('#timeOfRequest').val().split(' ')[0];
                    if ($('#completedDate').val() == '') {
                        lstRequest[0].DateCompleted = null;
                    }
                    else {
                        lstRequest[0].DateCompleted = $('#completedDate').val().split('/')[2] + '/' + $('#completedDate').val().split('/')[1] + '/' + $('#completedDate').val().split('/')[0];
                    }

                    //var completedDate;
                    //try {
                    //    completedDate = new Date($('#completedDate').val());
                    //    if (isNaN(completedDate)) {
                    //        lstRequest[0].DateCompleted = null
                    //    }
                    //    else {
                    //        lstRequest[0].DateCompleted = $('#completedDate').val().split('/')[2] + '/' + $('#completedDate').val().split('/')[1] + '/' + $('#completedDate').val().split('/')[0];
                    //    }
                    //}
                    //catch (e) {
                    //    lstRequest[0].DateCompleted = null
                    //}
                    var stringData = JSON.stringify(lstRequest);
                    var lstRequestDetails = new Array();
                    var i = 0;
                    var now = new Date();
                    $('#tbRequestInfo tr[new]').each(function () {
                        if (!this.rowIndex) return;
                        lstRequestDetails[i] = new Object();
                        lstRequestDetails[i].UserRequestDetailID = $(this).attr('data-id-vd');
                        lstRequestDetails[i].MS_MAY_OLD = "-1";
                        lstRequestDetails[i].DeviceID = this.cells[0].innerHTML;
                        lstRequestDetails[i].Description = this.cells[1].innerHTML;
                        lstRequestDetails[i].Request = this.cells[2].innerHTML;
                        lstRequestDetails[i].CauseID = this.cells[10].innerHTML;
                        lstRequestDetails[i].TypeOfMaintenanceRequestID = this.cells[6].innerHTML;
                        lstRequestDetails[i].PriorityID = this.cells[7].innerHTML;
                        if (this.cells[12].innerHTML == '') {
                            lstRequestDetails[i].HourOccurred = null;
                        }
                        else {
                            lstRequestDetails[i].HourOccurred = '1900/01/01 ' + this.cells[12].innerHTML.split(' ')[0];
                        }
                        if (this.cells[11].innerHTML == '') {
                            lstRequestDetails[i].DateOccurred = null;
                        }
                        else {
                            lstRequestDetails[i].DateOccurred = this.cells[11].innerHTML.split('/')[2] + '/' + this.cells[11].innerHTML.split('/')[1] + '/' + this.cells[11].innerHTML.split('/')[0];
                        }
                        //lstRequest[0].HourCreated = '1900/01/01 ' + $('#timeOfRequest').val().split(' ')[0];
                        //lstRequest[0].DateCompleted = this.cells[12].innerHTML.split('/')[2] + '/' + this.cells[12].innerHTML.split('/')[1] + '/' + this.cells[12].innerHTML.split('/')[0];

                        //lstRequestDetails[i].DateOccurred = Convert.fn.Time(now.getMonth() + 1) + '/' + Convert.fn.Time(now.getDate()) + '/' + Convert.fn.Time(now.getFullYear());
                        //lstRequestDetails[i].HourOccurred = '1900/01/01 ' + Convert.fn.Time(now.getHours()) + ':' + Convert.fn.Time(now.getMinutes());
                        //lstRequestDetails[i].CauseID = null;
                        //lstRequestDetails[i].TypeOfMaintenanceRequestID = -1;
                        //lstRequestDetails[i].PriorityID = null;
                        i = i + 1;
                    });
                    var stringData1 = JSON.stringify(lstRequestDetails);
                    if ($('#hfID').val() == "-1") {
                        $.post(urlSaveRequest, { request: stringData, requestInfo: stringData1, diadiem: $('#cboWorkSite').val() }, function (data) {
                            if (data == "success") {
                                //Alert.fn.Show("Thêm yêu cầu thành công", Alert.Type.success, Messenger.msgInfo);
                                window.location.href = urlRequest;
                            }
                            else {
                                Alert.fn.Show(Messenger.msgGhiKhongThanhCong, Alert.Type.error);
                            }
                        });
                    }
                    else {
                        $.post(urlEditRequest, { request: stringData, requestInfo: stringData1, diadiem: $('#cboWorkSite').val() }, function (data) {
                            if (data == "success") {
                                    window.location.href = urlRequest;
                            }
                            else {
                                Alert.fn.Show(Messenger.msgGhiKhongThanhCong, Alert.Type.error);
                            }
                        });
                    }
                },
                CheckValidateControl1: function () {
                    var Device = $('#cboDevice').val();
                    if (Device == null || Device.trim() == "" || Device.length == 0) {
                        Alert.fn.Show(Messenger.msgChonDevice, Alert.Type.warning);
                        return false;
                    }
                    if ($('#txtDescription').val() == null || $('#txtDescription').val().trim() == "" || $('#txtDescription').val().length == 0) {
                        Alert.fn.Show(Messenger.msgNhapMoTa, Alert.Type.warning);
                        return false;
                    }
                    if ($('#txtRequest').val() == null || $('#txtRequest').val().trim() == "" || $('#txtRequest').val().length == 0) {
                        Alert.fn.Show("Vui lòng nhập nội dung!", Alert.Type.warning);
                        return false;
                    }
                    if ($('#btnSave').attr('data-action') != 'edit') {
                        var tableBody = $('#tbRequestInfo tbody');
                        var ID = Math.floor((Math.random() * 9000000) + 1);
                        tableBody.append('<tr new data-id-vd='
                            + ID + '><td style="width: 150px">'
                            + $('#cboDevice').val() + '</td><td style="white-space: normal">'
                            + $('#txtDescription').val() + '</td><td style="white-space: normal">'
                            + $('#txtRequest').val() + '</td><td style="white-space: normal">'
                            + $('#cbonguyennhan option:selected').text() + '</td><td style="white-space: normal">'
                            + $('#cbouutien option:selected').text() + '</td><td style="white-space: normal">'
                            + $('#cboyeucaubaotri option:selected').text() + '</td><td class="hidden">'
                            + $('#cboyeucaubaotri').val() + '</td><td class="hidden">'
                            + $('#cbouutien').val() + '</td><td class="hidden">'
                            + $('#cboyeucaubaotri').val() + '</td><td class="hidden">'
                            + $('#cboyeucaubaotri').val() + '</td><td class="hidden">'
                            + $('#cbonguyennhan').val() + '</td><td >'
                            + $('#dateOccurred').val() + '</td><td >'
                            + $('#hourOccurred').val() + '</td>');
                        $('#hfCount').val(parseInt($('#hfCount').val()) + 1)
                        $('#cboWorkSite').attr('disabled', true);
                    }
                    else {
                        var detailID = $('#btnSave').attr('data-id-vd')
                        var table = $('#tbRequestInfo tr[data-id-vd$="' + detailID + '"]')
                        table.empty();
                        table.append('<td style="width: 150px">'
                            + $('#cboDevice').val() + '</td><td style="white-space: normal">'
                            + $('#txtDescription').val() + '</td><td style="white-space: normal">'
                            + $('#txtRequest').val() + '</td><td style="white-space: normal">'
                            + $('#cbonguyennhan option:selected').text() + '</td><td style="white-space: normal">'
                            + $('#cbouutien option:selected').text() + '</td><td style="white-space: normal">'
                            + $('#cboyeucaubaotri option:selected').text() + '</td><td class="hidden">'
                            + $('#cboyeucaubaotri').val() + '</td><td class="hidden">'
                            + $('#cbouutien').val() + '</td><td class="hidden">'
                            + $('#cboyeucaubaotri').val() + '</td><td class="hidden">'
                            + $('#cboyeucaubaotri').val() + '</td><td class="hidden">'
                            + $('#cbonguyennhan').val() + '</td><td >'
                            + $('#dateOccurred').val() + '</td><td >'
                            + $('#hourOccurred').val() + '</td>');
                        $('#btnSave').removeAttr("data-id-vd");
                    }
                    $('#myModal').appendTo("body").modal('hide');
                    $('#btnSave').attr('data-action', 'add');
                },
                AutoCompleteMail: function () {
                    $("#txtEmail").autocomplete({
                        source: function (request, response) {
                            $.ajax({
                                url: urlAutocomplete,
                                type: "POST",
                                dataType: "json",
                                data: {
                                    keyword: $("#txtEmail").val(),
                                },
                                success: function (data) {
                                    response($.map(data, function (item) {
                                        return {
                                            label: item,
                                            value: xlchuoi(item)  //nếu là lần đàu thì cleate sạch xong cộng, lần 2 clea sau dấu ;
                                        };
                                    }))
                                },
                                error: function () {
                                    alert('something went wrong !');
                                }
                            })
                        }
                    });
                },
                Add: function () {
                    //load combobox de
                    var $wordsite = $('#cboWorkSite').val();
                    if ($wordsite == "-1") {
                        Alert.fn.Show("Bạn cần phải chọn địa điểm!", Alert.Type.error);
                        return;
                    }
                    $('#cboDevice option').removeData();
                    $.ajax({
                        type: "POST",
                        url: urlDevices,
                        data: { WorkSiteID: $('#cboWorkSite').val() },
                        success: function (data) {
                            var s;
                            for (var i = 0; i < data.length; i++) {
                                s += '<option value="' + data[i].ID + '">' + data[i].Name + '</option>';
                            }
                            $("#cboDevice").html(s);
                        }
                    });
                    $("#cboDevice option:first").prop('selected', true)
                    $("#txtDescription").val('')
                    $("#txtRequest").val('')
                    $('#btnSave').attr('data-action', 'add')
                    $('#myModal').appendTo("body").modal('show')
                },
                Edit: function () {
                    $('#tbRequestInfo tr[class$=selected]').each(function () {
                        if (!this.rowIndex) return;
                        $('#cboDevice').val(this.cells[0].innerHTML.trim()); 
                        $("#txtDescription").val(this.cells[1].innerHTML)
                        $("#txtRequest").val(this.cells[2].innerHTML)
                        $('#myModal').appendTo("body").modal('show')
                        $('#btnSave').attr('data-action', 'edit')
                        $('#btnSave').attr('data-id-vd', $(this).attr('data-id-vd'))
                        return false;
                    });
                },
                Remove: function () {
                    Alert.fn.ShowConfirm(Messenger.msgXoaYeuCau, Alert.Type.warning, '', function (result) {
                        if (result == true) {
                            $('#tbRequestInfo tr[new][class$=selected]').remove();
                            $('#hfCount').val(parseInt($('#hfCount').val()) - 1)
                            var rowCount = $('#tbRequestInfo >tbody >tr').length;
                            if (rowCount == 0) {
                                $('#cboWorkSite').attr('disabled', false);
                            }
                        }
                    });
                },
                NguoiYeuCau: function () {
                    $.post(urlShowNguoiYeuCau, function (data) {
                        if (data.length > 0) {
                            if ($.fn.DataTable.isDataTable('#tbNguoiYeuCau')) {
                                $('#tbNguoiYeuCau').dataTable().fnDestroy();
                            }
                            $("#tbNguoiYeuCau").DataTable({
                                data: data,
                                columns: [
                                    { data: 'Value' },
                                    { data: 'Text' }
                                ],
                                columnDefs: [
                                    {
                                        'targets': 0,
                                        'max-width': '150px',
                                    }
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
                                    if (data.hasOwnProperty("Value")) {
                                        $(row).attr('data-id', data.ID);
                                    }
                                },
                            });
                            $('#ModalNCY').appendTo("body").modal('show')
                        }
                        else {
                            Alert.fn.Show(Messenger.msgKhongCoThietBi, Alert.Type.warning);
                            return false;
                        }
                    });
                }

            }
        })
        app.init = function () {
            angular.bootstrap(document, ['AddRequestPage']);
        }
        return app;
    })();
    $(function () {
        AddRequest.init() //khởi tạo module angular
    })
})