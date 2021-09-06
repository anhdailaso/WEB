define(['_MasterPageController', '_MenuPageController', '_DetailsModalController', 'datatables-bootstrap', 'bootstrap-select'], function (module, menu, modal) {
    var StopCard = (function () {
        var app = angular.module('StopCardPage', [])
        app.controller('StopCardController', function ($scope, $compile) {
            var Main = module.Main
            var Languages = module.Languages
            var Convert = module.Convert
            var InfoDetails = modal
            var Loading = module.Loading
            var Alert = module.Alert
            var MainMenu = menu
            var BuildTreeView = module.BuildTreeView
            var menuID = 'mnuStopCard'
            var currentNamePage = 'StopCardWeb'
            var change = false
            var buttonFloat = [
                {
                    "id": "btnMain",
                    "url": "#",
                    "icon": "<i class='fa fa-angle-double-up'></i>"
                },
                {
                    "id": "btnXoa",
                    "url": "#",
                    "icon": "<i class='fa fa-minus-square'></i>",
                    "lang": "btndelete",
                    "func": "fn.Remove"
                },
                {
                    "id": "btnSave",
                    "url": "#",
                    "icon": "<i class='fa fa-floppy-o'></i>",
                    "lang": "btnGhi",
                    "func": "fn.CheckValidateControl"
                },
                {
                    "id": "btnRefresh",
                    "icon": "<i class='fa fa-refresh'></i>",
                    "lang": "btnRefresh",
                    "url": urlInsertStopCard,
                }

            ]
            var Action = {
                add: 0, edit: 1
            }
            var vars = {}
            var bindVariables = function () {
                return {
                    $action: Action.add,
                    $StopCardDatatables: 'undefined',
                    $tbListStopCardBody: $('#tbListStopCard tbody'),
                    $btnChooseStopCard: $('#btnChooseStopCard'),
                }
            }
            var fnPrivate = {
                DisabledButton: function (flag) {
                    $('#btnRemoveDetails').attr('disabled', flag)
                    $('#btnEditDetails').prop('disabled', flag)
                },
                readOnlyControl: function (flag) {
                    $("#frmMain input,#frmMain textarea ").attr('disabled', flag);
                    $("#frmMain .select2").attr('disabled', flag);
                },
                GetStopCardByID: function () {
                    var keys = vars.$StopCardDatatables.data().count();
                    if (keys == 0) {
                        Alert.fn.Show(Messenger.msgDuLieuRong, Alert.Type.warning);
                        return;
                    }
                    keys = vars.$StopCardDatatables.$('tr[class$=selected]')
                    if (keys.length == 0) {
                        Alert.fn.Show(Messenger.msgChonDevice, Alert.Type.warning)
                        return;
                    }
                    Loading.fn.Show()
                    $('#myModal').appendTo("body").modal('hide')
      
                },
                LoadEdits: function () {
                     var keys = vars.$StopCardDatatables.data().count();
                    if (keys == 0) {
                        Alert.fn.Show(global.TypeLanguage == 0 ? "Không có dữ liệu!" : "No data!", Alert.Type.warning);
                        return;
                    }
                    var keys1 = vars.$StopCardDatatables.$('tr[class$=selected]');
                    if (keys1.length == 0) {
                        Alert.fn.Show(global.TypeLanguage == 0 ? "Bạn chưa chọn thẻ stop!" : "You not select Stopcard!", Alert.Type.warning);

                        return;
                    }
                    Loading.fn.Show()
                    $('#myModal').appendTo("body").modal('hide');
                    window.location.href = urlEditStopCard + '?id=' + $('#tbListStopCard tr[class$=selected]').attr('data-id');
                },
                LoadListStopCard: function () {
                    Loading.fn.Show();
                    $.post(urlGetListStopCard, { tungay: $('#fromDate').val(), denngay: $('#toDate').val() }, function (data) {
                        if ($.fn.DataTable.isDataTable('#tbListStopCard')) {
                            $('#tbListStopCard').dataTable().fnDestroy();
                        }
                        vars.$StopCardDatatables = $("#tbListStopCard").DataTable({
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
                        Loading.fn.Hide();
                    });
               
                },
                KiemTraSave: function () {
                    var ketqua = $("#hfUserDuyet").val();
                    if ($('#hfTTPhieu').val() === "0") {
                        //kiem tra co một chứng từ có trạng thái là hoàng thành
                        if (ketqua === 'success') {
                            //kiểm tra check hiện tại là check thì kiểm tra user duyệt == bằng với user login
                            if ($('#APPROVAL_USER').val() == null || $('#APPROVAL_USER').val().trim() !== "" || $('#APPROVAL_USER').val().length == 0) {
                            }
                            else {
                                if ($('#APPROVAL_USER').val() !== $('#hfUsername').val()) {
                                    Alert.fn.Show(global.TypeLanguage == 0 ? "Bạn không có quyền sửa chứng từ này!" : "You do not have the right to edit this ticket!", Alert.Type.warning);
                                    return false;
                                }
                            }
                        }
                        else {
                            //user không có quyền
                            if ($('#CreatedBy').val() !== $('#hfUsername').val()) {
                                Alert.fn.Show(global.TypeLanguage == 0 ? "Bạn không có quyền sửa chứng từ này!" : "You do not have the right to edit this ticket!", Alert.Type.warning);
                                return false;
                            }
                            else {
                                if ($('#APPROVAL_USER').val() == null || $('#APPROVAL_USER').val().trim() == "" || $('#APPROVAL_USER').val().length == 0) {
                                    ;
                                }
                                else {
                                    Alert.fn.Show(global.TypeLanguage == 0 ? "Bạn không có quyền sửa chứng từ này!" : "You do not have the right to edit this ticket!", Alert.Type.warning);
                                    return false
                                }
                            }
                        }
                    }
                    else {
                        Alert.fn.Show(global.TypeLanguage == 0 ? "chứng từ này đã hoàn thành không thể sửa!" : "This form is Done and cannot be edited!", Alert.Type.warning);
                        return false;
                    }

                    if ($('#REPORT_PARENT').val() == null || $('#REPORT_PARENT').val().trim() == "" || $('#REPORT_PARENT').val().length == 0) {
                        Alert.fn.Show(global.TypeLanguage == 0 ? "User báo cáo được để trống!" : "Report parent not blank!", Alert.Type.warning);
                        return false;
                    }

                    if ($('#Description').val() == null || $('#Description').val().trim() == "" || $('#Description').val().length == 0) {
                        Alert.fn.Show(global.TypeLanguage == 0 ? "Mô tả được để trống!" : "Description not blank!", Alert.Type.warning);
                        return false;
                    }
                },
                ShowSentMail: function () {
                    $('#myModal').appendTo("body").modal('show');
                },

            }

            var method
            $scope.fn = {
                Init: function () {
                    global.CurrentNamePage = currentNamePage
                    MainMenu.fn.SetActive(menuID);
                    Languages.fn.AutoChangeLanguage()
                    Main.fn.InitButtonFloat(buttonFloat)
                    vars = bindVariables();
                    method = fnPrivate;
                    vars.$tbListStopCardBody.on('doubletap', 'tr', method.LoadEdits);
                    vars.$tbListStopCardBody.on('dblclick', 'tr', method.LoadEdits);
                    vars.$btnChooseStopCard.click(method.LoadEdits);
                    Main.fn.InitDateTimePickerChanged([$('#fromDate'), $('#toDate')], method.LoadListStopCard);

                    $('#tbStopCardDetails tbody').on('click', 'tr', function () {
                        if ($(this).is('[new]') || $(this).is('[old]')) {
                            method.DisabledButton(false);
                        }
                        else {
                            method.DisabledButton(true);
                        }
                    });

                    $(".select2").select2(
                        {
                            theme: "classic"
                        });

                    $("#sDocDate").datetimepicker({
                        format: 'DD/MM/YYYY',
                        defaultDate: new Date(),
                        useCurrent: false
                    });

                    $('#IS_APPROVED').change(function () {
                        if (this.checked) {
                            $("#APPROVAL_USER").val($("#hfUsername").val());
                        }
                        else {
                            $('#APPROVAL_USER').val("");

                        }
                    });

                    $("#MUCUUTIEN").change(function () {
                        var ngay = $("#sDocDate").val().split("/");
                        switch ($(this).val()) {
                            case '1':
                                $("#Deadline").val(moment(new Date(ngay[2], ngay[1] - 1, ngay[0])).add(1, 'days').format('DD/MM/YYYY'));
                                break;
                            case '2':
                                $("#Deadline").val(moment(new Date(ngay[2], ngay[1] - 1, ngay[0])).add(7, 'days').format('DD/MM/YYYY'));
                                break;
                            case '3':
                                $("#Deadline").val(moment(new Date(ngay[2], ngay[1] - 1, ngay[0])).add(14, 'days').format('DD/MM/YYYY'));
                                break;
                            case '4':
                                $("#Deadline").val(moment(new Date(ngay[2], ngay[1] - 1, ngay[0])).add(42, 'days').format('DD/MM/YYYY'));
                                break;
                            case '5':
                                $("#Deadline").val(moment(new Date(ngay[2], ngay[1] - 1, ngay[0])).add(99, 'days').format('DD/MM/YYYY'));
                                break;
                            default:
                        }

                    });
                    $("#REPORT_PARENT").change(function () {
                        $.post(urlGetDepartmentbyReportParent, { UserName: $(this).val() }, function (data) {
                            $("#Department").val(parseInt(data.Depament)).change();
                        });
                    });

                    $("#sCreatedtime").datetimepicker({
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
                        $("#CreatedBy").val($("#hfUsername").val());
                        var ketqua = $("#hfUserDuyet").val();
                        if (ketqua === 'success') {
                        }
                        else {
                            $("#IS_APPROVED").attr("disabled", "disabled");
                        }
                    }
                    else {
                        if ($('#hfTTPhieu').val() === "0") {
                            //kiem tra co một chứng từ có trạng thái là hoàng thành
                            var ketqua = $("#hfUserDuyet").val();
                            if (ketqua === 'success') {
                                //kiểm tra check hiện tại là check thì kiểm tra user duyệt == bằng với user login
                                if ($('#APPROVAL_USER').val() == null || $('#APPROVAL_USER').val().trim() == "" || $('#APPROVAL_USER').val().length == 0) {

                                }
                                else {
                                    if ($('#APPROVAL_USER').val() == $('#hfUsername').val()) {
                                    }
                                    else {
                                        method.readOnlyControl(true);
                                    }
                                }
                            }
                            else {
                                if ($('#CreatedBy').val() == $('#hfUsername').val()) {
                                    $("#IS_APPROVED").attr("disabled", "disabled");
                                }
                                else {
                                    method.readOnlyControl(true);
                                }
                            }
                        }
                        else {
                            if ($('#APPROVAL_USER').val() == $('#hfUsername').val()) {
                            }
                            else {
                                method.readOnlyControl(true);
                            }
                        }
                    }
                },
                Remove: function () {

                    if ($('#stt').val() === "-1") {
                        Alert.fn.Show(global.TypeLanguage == 0 ? "Bạn chưa chọn chứng từ cần xóa!" : "You have not selected the voucher to delete!", Alert.Type.warning);
                        return false;
                    }
                    var ketqua = $("#hfUserDuyet").val();

                    if ($('#hfTTPhieu').val() === "0") {

                        if (ketqua === 'success') {
                            //kiểm tra check hiện tại là check thì kiểm tra user duyệt == bằng với user login
                            if ($('#APPROVAL_USER').val() == null || $('#APPROVAL_USER').val().trim() == "" || $('#APPROVAL_USER').val().length == 0) {
                            }
                            else {
                                Alert.fn.Show(global.TypeLanguage == 0 ? "Chứng từ đã được duyệt không thể xóa!" : "Approved documents cannot be deleted!", Alert.Type.warning);
                                return false;
                            }
                        }
                        else {
                            //user không có quyền
                            if ($('#CreatedBy').val() !== $('#hfUsername').val()) {
                                Alert.fn.Show(global.TypeLanguage == 0 ? "Bạn không được phép xóa chứng từ!" : "You are not allowed to delete vouchers!", Alert.Type.warning);
                                return false;
                            }
                            else {
                                if ($('#APPROVAL_USER').val() == null || $('#APPROVAL_USER').val().trim() == "" || $('#APPROVAL_USER').val().length == 0) {
                                }
                                else {
                                    Alert.fn.Show(global.TypeLanguage == 0 ? "Chứng từ đã được duyệt không thể xóa!" : "Approved documents cannot be deleted!", Alert.Type.warning);
                                    return false;
                                }
                            }
                        }
                    }
                    else {
                        Alert.fn.Show(global.TypeLanguage == 0 ? "chứng từ này đã hoàn thành không thể xóa!" : "This form is Done and cannot be delete!", Alert.Type.warning);
                        return false;
                    }
                    Alert.fn.ShowConfirm(global.TypeLanguage == 0 ? "Bạn có chắc muốn xóa chứng từ đang chọn?" : "Are you sure you want to delete the selected voucher?", Alert.Type.warning, 'Warning', function (result) {
                        if (result === true) {
                            $.post(urlDeleteStopCard, { ID: $('#ID').val() }, function (data) {
                                if (data === 'success') {
                                    window.location.href = urlInsertStopCard;
                                }
                                else {
                                    Alert.fn.Show(global.TypeLanguage == 0 ? "Xóa chứng từ không thành công!" : "Failed to delete voucher!", Alert.Type.warning);
                                }
                            });
                        }
                    });
                },
                CheckValidateControl: function () {
                    if (change === false) {
                        if (method.KiemTraSave() == false) {
                            return false;
                        }
                    }
                    method.readOnlyControl(false);
                    //add tabble details vao lisst
                    var lstStopCardDetails = new Array();
                    var i = 0;
                    $('#tbStopCardDetails tr').each(function () {
                        if (!this.rowIndex) return;
                        lstStopCardDetails[i] = new Object();

                        lsStopCardDetails[i].ID = $(this).attr('data-id-vd');
                        lsStopCardDetails[i].PreventiveAction = this.cells[0].innerHTML;
                        lsStopCardDetails[i].PersonIncharge = this.cells[1].innerHTML;
                        lsStopCardDetails[i].TENPersonIncharge = this.cells[2].innerHTML;
                        lsStopCardDetails[i].MUCUUTIEN = this.cells[3].innerHTML;
                        lsStopCardDetails[i].TENMUCUUTIEN = this.cells[4].innerHTML;


                        if (this.cells[5].innerHTML == '') {
                            lstStopCardDetails[i].Deadline = null;
                        }
                        else {
                            lstStopCardDetails[i].Deadline = this.cells[5].innerHTML.split('/')[2] + '/' + this.cells[5].innerHTML.split('/')[1] + '/' + this.cells[5].innerHTML.split('/')[0];
                        }
                        if (this.cells[6].innerHTML == '') {
                            lstStopCardDetails[i].CompletedDate = null;
                        }
                        else {
                            lstStopCardDetails[i].CompletedDate = this.cells[6].innerHTML.split('/')[2] + '/' + this.cells[6].innerHTML.split('/')[1] + '/' + this.cells[6].innerHTML.split('/')[0];
                        }
                        lstStopCardDetails[i].Status = this.cells[7].innerHTML;
                        lstStopCardDetails[i].PersonReport = this.cells[8].innerHTML;
                        lstStopCardDetails[i].Description = this.cells[9].innerHTML;

                        lstStopCardDetails[i].THIETBI = this.cells[10].innerHTML;
                        lstStopCardDetails[i].TENTHIETBI = this.cells[11].innerHTML;
                        lstStopCardDetails[i].LOAIYEUCAU = this.cells[12].innerHTML;
                        lstStopCardDetails[i].TENLOAIYEUCAU = this.cells[13].innerHTML;
                        lstStopCardDetails[i].NGUYENNHAN = this.cells[14].innerHTML;

                        lstStopCardDetails[i].TENNGUYENNHAN = this.cells[15].innerHTML;
                        i = i + 1;
                    });

                    var stringData1 = JSON.stringify(lstStopCardDetails).replace(/[\n\t]/g, " ");
                    $formStopCard = $("#form1");
                    $.ajax({
                        url: urlSaveStopCard,
                        type: "post",
                        data: $formStopCard.serialize() + "&StopCardDetails=" + stringData1,
                        success: function (data) {
                            if (data.Message === 'success') {
                                //window.location.href = urlInsertStopCard;
                                $("#stt").val(data.ID);
                                if (change == false) {
                                    $('#txtGuiToi').val(data.mailget);
                                    $('#txtGuiToi').attr('data-loaimail', data.loaimail);
                                }
                                else {
                                    $('#txtGuiToi').val(data.mailBC);
                                    $('#txtGuiToi').attr('data-loaimail', 2);
                                }
                                $('#txtGuiThem').attr('data-report', data.Report);
                                $('#myModalMail').appendTo("body").modal('show');
                            }
                            else {
                                Alert.fn.Show(global.TypeLanguage == 0 ? "Cập nhật chứng từ không thành công!" : "Failed to Update voucher!", Alert.Type.warning);
                            }
                        }
                    });
                },
                CheckValidateControl1: function () {
                    if ($('#PreventiveAction').val() == null || $('#PreventiveAction').val().trim() == "" || $('#PreventiveAction').val().length == 0) {
                        Alert.fn.Show(global.TypeLanguage == 0 ? "Hành động phòng ngừa không được để trống!" : "Preventive Action not blank!", Alert.Type.warning);
                        return false;
                    }

                    var PersonIncharge = $('#PersonIncharge').val();
                    if (PersonIncharge == null || PersonIncharge.trim() == "" || PersonIncharge.length == 0) {
                        Alert.fn.Show(global.TypeLanguage == 0 ? "Người phụ trách không được để trống!" : "Person In charge not blank!", Alert.Type.warning);
                        return false;
                    }

                    if ($('#MUCUUTIEN').val() == null || $('#MUCUUTIEN').val().trim() == "" || $('#MUCUUTIEN').val().length == 0) {
                        Alert.fn.Show(global.TypeLanguage == 0 ? "Người phụ trách không được để trống!" : "Person In charge not blank!", Alert.Type.warning);
                        return false;
                    }

                    var ngay1 = $("#Deadline").val().split("/");
                    //update lại status
                    if ($('#CompletedDate').val() == null || $('#CompletedDate').val().trim() == "" || $('#CompletedDate').val().length == 0) {
                        //nếu chưa có ngày hoàng thành thì so sánh ngày của hiện tại với ngày của date line
                        var time1 = moment(new Date(ngay1[2], ngay1[1] - 1, ngay1[0]));
                        var time2 = moment(new Date());
                        if (time2 > time1) {
                            $("#Status").val("Overdue-Quá hạn");
                        }
                        else {
                            $("#Status").val("Ongoing-Đang xử lý");
                        }
                    }

                    if ($('#btnSave').attr('data-action') != 'edit') {
                        var tableBody = '';
                        if ($('#btnSave').attr('data-list') !== '2'){
                            tableBody = $('#tbStopCardDetails1 tbody');
                        }
                        else {
                            tableBody = $('#tbStopCardDetails2 tbody');
                        }
                        var ID = - Math.floor((Math.random() * 9000000) + 1);
                        tableBody.append('<tr new data-id-vd='
                            + ID + '><td>'
                            + $('#PreventiveAction').val() + '</td><td class="hidden">'
                            + $('#PersonIncharge').val() + '</td><td>'
                            + $('#PersonIncharge option:selected').text() + '</td><td class="hidden">'
                            + $('#MUCUUTIEN').val() + '</td><td style="white-space: normal">'
                            + $('#MUCUUTIEN option:selected').text() + '</td><td>'

                            + $('#Deadline').val() + '</td><td >'
                            + $('#CompletedDate').val() + '</td><td >'
                            + $('#Status').val() + '</td><td class="hidden">'
                            + $('#PersonReport').val() + '</td><td >'
                            + $('#Description').val() + '</td><td class="hidden">'

                            + $('#THIETBI').val() + '</td><td>'
                            + $('#THIETBI option:selected').text() + '</td><td class="hidden">'
                            + $('#LOAIYEUCAU').val() + '</td><td>'
                            + $('#LOAIYEUCAU option:selected').text() + '</td><td class="hidden">'
                            + $('#NGUYENNHAN').val() + '</td><td>'
                            + $('#NGUYENNHAN option:selected').text() + '</td>');
                    }
                    else {
                        var detailID = $('#btnSave').attr('data-id-vd')
                        var table = '';
                        if ($('#btnSave').attr('data-list') !== '2') {
                            table = $('#tbStopCardDetails1 tr[data-id-vd$="' + detailID + '"]')
                        }
                        else {
                            var table = $('#tbStopCardDetails2 tr[data-id-vd$="' + detailID + '"]')

                        }
                        table.empty();
                        table.append('<td>'
                            + $('#PreventiveAction').val() + '</td><td class="hidden">'
                            + $('#PersonIncharge').val() + '</td><td>'
                            + $('#PersonIncharge option:selected').text() + '</td><td class="hidden">'
                            + $('#MUCUUTIEN').val() + '</td><td style="white-space: normal">'
                            + $('#MUCUUTIEN option:selected').text() + '</td><td>'

                            + $('#Deadline').val() + '</td><td >'
                            + $('#CompletedDate').val() + '</td><td >'
                            + $('#Status').val() + '</td><td class="hidden">'
                            + $('#PersonReport').val() + '</td><td >'
                            + $('#Description').val() + '</td><td class="hidden">'

                            + $('#THIETBI').val() + '</td><td>'
                            + $('#THIETBI option:selected').text() + '</td><td class="hidden">'
                            + $('#LOAIYEUCAU').val() + '</td><td>'
                            + $('#LOAIYEUCAU option:selected').text() + '</td><td class="hidden">'
                            + $('#NGUYENNHAN').val() + '</td><td >'

                            + $('#NGUYENNHAN option:selected').text() + '</td>');
                        $('#btnSave').removeAttr("data-id-vd");
                    }
                    $('#myModal1').appendTo("body").modal('hide');
                    $('#btnSave').attr('data-action', 'add');
                },
                ShowListStopCard: function () {
                    method.LoadListStopCard();
                    $('#myModal').appendTo("body").modal('show')
                },
                ShowImage: function () {
                    $('#img01').attr('src', $('#Image_1').attr('src'));
                    $('#myModalHinh').appendTo("body").modal('show');
                },
                AddDetails1: function () {
                    debugger;
                    if ($('#APPROVAL_USER').val() == null || $('#APPROVAL_USER').val().trim() == "" || $('#APPROVAL_USER').val().length == 0) {
                    }
                    else {
                        Alert.fn.Show(global.TypeLanguage == 0 ? "Chứng từ đã duyệt không thể thêm!" : "The browser has not be insert!", Alert.Type.warning);
                        return false;
                    }
                    if (method.KiemTraSave() == false) {
                        return false;
                    }
                    var ngay = $("#sDocDate").val().split("/");
                    $("#Deadline").val(moment(new Date(ngay[2], ngay[1] - 1, ngay[0])).add(1, 'days').format('DD/MM/YYYY'));
                    $("#PreventiveAction").val('')
                    $("#MUCUUTIEN option:first").prop('selected', true).change();
                    $("#THIETBI option:first").prop('selected', true).change();
                    $("#LOAIYEUCAU option:first").prop('selected', true).change();
                    $("#MUCUUTIEN option:first").prop('selected', true).change();
                    $("#NGUYENNHAN option:first").prop('selected', true).change();

                    $('#btnSave').attr('data-action', 'add');
                    $('#btnSave').attr('data-list', '1');
                    $('#myModal1').appendTo("body").modal('show');
                },
                EditDetails1: function () {
                    if ($('#APPROVAL_USER').val() == null || $('#APPROVAL_USER').val().trim() == "" || $('#APPROVAL_USER').val().length == 0) {
                    }
                    else {
                        Alert.fn.Show(global.TypeLanguage == 0 ? "Chứng từ đã duyệt không thể sửa!" : "The browser has not be edit!", Alert.Type.warning);
                        return false;
                    }
                    if (method.KiemTraSave() == false) {
                        return false;
                    }
                    if ($('#tbStopCardDetails tr[class$=selected]').length === 0) {
                        Alert.fn.Show(global.TypeLanguage == 0 ? "Chưa chọn dữ liệu!" : "No data selected!", Alert.Type.warning);
                        return false;
                    }

                    $('#tbStopCardDetails1 tr[class$=selected]').each(function () {
                        if (!this.rowIndex) return;
                        $('#PreventiveAction').val(this.cells[0].innerHTML.trim());
                        $("#PersonIncharge").val(this.cells[1].innerHTML.trim()).change();
                        $("#MUCUUTIEN").val(this.cells[3].innerHTML.trim()).change();
                        $("#Deadline").val(this.cells[5].innerHTML.trim());
                        $("#THIETBI").val(this.cells[10].innerHTML.trim()).change();
                        $("#LOAIYEUCAU").val(this.cells[12].innerHTML.trim()).change();
                        $("#NGUYENNHAN").val(this.cells[14].innerHTML.trim()).change();

                        $("#CompletedDate").val(this.cells[6].innerHTML.trim()).change();
                        $("#Status").val(this.cells[7].innerHTML.trim()).change();
                        $("#PersonReport").val(this.cells[8].innerHTML.trim()).change();
                        $("#Description").val(this.cells[9].innerHTML.trim()).change();

                        $('#myModal1').appendTo("body").modal('show')
                        $('#btnSave').attr('data-action', 'edit')
                        $('#btnSave').attr('data-list', '1');
                        $('#btnSave').attr('data-id-vd', $(this).attr('data-id-vd'))
                        return false;    
                    });
                },
                RemoveDetails1: function () {
                    if ($('#APPROVAL_USER').val() == null || $('#APPROVAL_USER').val().trim() == "" || $('#APPROVAL_USER').val().length == 0) {
                    }
                    else {
                        Alert.fn.Show(global.TypeLanguage == 0 ? "Chứng từ đã duyệt không thể xóa!" : "The Browser from could not remove!", Alert.Type.warning);
                        return false;
                    }

                    if (method.KiemTraSave() == false && $('#stt').val() !== "-1") {
                        return false;
                    }
                    if ($('#tbStopCardDetails tr[class$=selected]').length === 0) {
                        Alert.fn.Show(global.TypeLanguage == 0 ? "Chưa chọn dữ liệu!" : "No data selected!", Alert.Type.warning);
                        return false;
                    }

                    Alert.fn.ShowConfirm(global.TypeLanguage == 0 ? "Bạn có muốn xóa dữ liệu đang chọn!" : "Delete data selected!", Alert.Type.warning, '', function (result) {
                        if (result == true) {
                            $('#tbStopCardDetails tr[class$=selected]').remove();
                            fn.DisabledButton(false);
                        }
                    });
                },
                Report1: function () {
                    if ($('#tbStopCardDetails tr[class$=selected]').find("td").eq(1).html() !== $('#hfIDStafety').val()) {
                        Alert.fn.Show(global.TypeLanguage == 0 ? "Bạn không phải là người báo cáo công việc này!" : "You are not a user report this job!", Alert.Type.warning);
                        return false;
                    }
                    $("#txtNguoiBC").val($('#hfUsername').val());
                    $("#dtNgayHT").val(new Date().toLocaleString('en-GB').substr(0, 10));
                    $("#txtGhiChu").val('');
                    $('#ModaReport').appendTo("body").modal('show');
                },
                ThucHien: function () {
                    $('#tbStopCardDetails tr[class$=selected]').find("td").eq(6).html($("#dtNgayHT").val());
                    $('#tbStopCardDetails tr[class$=selected]').find("td").eq(7).html("Done-Đã hoàn thành");
                    $('#tbStopCardDetails tr[class$=selected]').find("td").eq(8).html($("#txtNguoiBC").val());
                    $('#tbStopCardDetails tr[class$=selected]').find("td").eq(9).html($("#txtGhiChu").val());
                    change = true;

                },
                KhongThucHien: function () {
                    $('#tbStopCardDetails tr[class$=selected]').find("td").eq(6).html($("#dtNgayHT").val());
                    $('#tbStopCardDetails tr[class$=selected]').find("td").eq(7).html("Reject-Hủy bỏ");
                    $('#tbStopCardDetails tr[class$=selected]').find("td").eq(8).html($("#txtNguoiBC").val());
                    $('#tbStopCardDetails tr[class$=selected]').find("td").eq(9).html($("#txtGhiChu").val());
                    change = true;
                },
                SentMail: function () {
                    //(string ID, int sentmailType, string CC, string Mailto, string Report)
                    Loading.fn.Show();
                    $.post(urlSentMail, { ID: $("#stt").val(), sentmailType: $('#txtGuiToi').attr('data-loaimail'), CC: $('#txtGuiThem').val(), Mailto: $('#txtGuiToi').val(), Report: $('#txtGuiThem').attr('data-report') }, function (data) {
                        if (data == "success") {
                            Loading.fn.Hide();
                            //Alert.fn.Show("Thêm yêu cầu thành công", Alert.Type.success, Messenger.msgInfo);
                            window.location.href = urlInsertStopCard;
                        }
                        else {
                            Alert.fn.Show(Messenger.msgGhiKhongThanhCong, Alert.Type.error);
                        }
                    });

                },
                NoSentMail: function () {
                    window.location.href = urlInsertStopCard;
                },
            }
        })
        app.init = function () {
            angular.bootstrap(document, ['StopCardPage'])
        }
        return app;
    })();
    $(function () {
        StopCard.init()
    });
})