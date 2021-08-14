define(['_MasterPageController', '_MenuPageController', '_DetailsModalController', 'datatables-bootstrap'], function (module, menu) {
    var InitMonitoring = (function () {
        var app = angular.module('InitMonitoringPage', [])
        app.controller('InitMonitoringController', function ($scope) {
            var Main = module.Main
            var Languages = module.Languages
            var Alert = module.Alert
            var Loading = module.Loading
            var MainMenu = menu
            var menuID = 'mnuMornitoring'
            var stt = 1
            var Ckeckload = 1
            var currentPageName = 'MornitoringWeb'
            var buttonFloat = [
                {
                    "id": "btnMain",
                    "url": "#",
                    "icon": "<i class='fa fa-angle-double-up'></i>"
                },
                {
                    "id": "btnbochon",
                    "url": '#',
                    "icon": "<i class='fa fa fa-square-o'></i>",
                    "lang": "btnbochon",
                    "func": "fn.bochon()"
                },
                {
                    "id": "btnchondat",
                    "url": '#',
                    "icon": "<i class='fa fa-check-square-o'></i>",
                    "lang": "btnchondat",
                    "func": "fn.chondat()"
                },
                {
                    "id": "btnchonkhongdat",
                    "url": '#',
                    "icon": "<i class='fa  fa-check-square'></i>",
                    "lang": "btnchonkhongdat",
                    "func": "fn.chonkhongdat()"
                },
                {
                    "id": "btnSave",
                    "url": '#',
                    "icon": "<i class='fa fa-floppy-o'></i>",
                    "lang": "btnGhi",
                    "func": "fn.Ghi()"
                },
                {
                    "id": "btnBack",
                    "url": urlGetGiamSatTinhTrang,
                    "icon": "<i class='fa fa-reply'></i>",
                    "lang": "btnTroVe"
                }



            ]
            var vars = {}
            var bindVariables = function () {
                return {
                    $busy: false,
                    $equipDatatables: 'undefined',
                    $equipDatatablesTmp: 'undefined',
                    $qualityParamsDatatables: 'undefined',
                    $btnChooseEquip: $('#btnChooseEquip'),
                    $btnChoose: $('#btnChoose'),
                    $tbEquip: $('#tbEquip'),
                    $tbEquipBody: $('#tbEquip tbody'),
                    $txtDevice: $('#txtDevice'),
                    $tbQualityParameterBody: $('#tbQualityParameter tbody'),
                    $tbQualityParameter: $('#tbQualityParameter'),
                    $tbGiamSatTinhTrang: $('#tbGiamSatTinhTrang'),
                    $tbGiamSatTinhTrangBody: $('#tbGiamSatTinhTrang tbody'),
                    $tableGiamSatTinhTrang: 'undefined',
                    $tablethongso: 'undefined',
                    $tabGiamSat: $('a[href="#tabMonitoring"]'),
                    $tabDinhTinh: $('a[href="#tabThongSoDT"]'),
                    $tabDinhLuong: $('a[href="#tabThongSoDL"]')
                };
            }
            var fnPrivate = {
                LoadGrid: function (id, action) {
                    if (Ckeckload == 0) return;
                    if (method.KiemTraChonMay() !== false) {
                        Loading.fn.Show()
                        $.post(urlConditionMonitoringParameterGet, { id: id, due: $('input[name = "optradio"]:checked').val(), todate: $('#fromDate').val(), mslcv: $('#cbbLoaiCV').val(), stt: $('#stt').val() }, function (data) {
                            if ($.fn.DataTable.isDataTable('#tbQualityParameter')) {
                                $('#tbQualityParameter').dataTable().fnDestroy();
                            }
                            vars.$tbQualityParameterBody.empty()
                            if (data.length > 0) {
                                var i = 0;
                                for (i = 0; i < data.length; i++) {
                                    var ghichu = '';
                                    if (data[i].Note === null) {
                                        ghichu = "";
                                    }
                                    else {
                                        ghichu = data[i].Note;
                                    }

                                    if (data[i].TypeOfParam === true) {
                                        if (data[i].Measurement == 1) {
                                            vars.$tbQualityParameterBody.append('<tr><td >(' + data[i].ComponentID + ') ' + data[i].ComponentName + '- (' + data[i].MonitoringParamsName + ')</td><td class="dt-body-center" data-toggle="buttons" style="width:120px" data-pass="' + data[i].Pass + '"><label class="btn"><input class="form-control" data-id="' + data[i].ValueParamID + '" data-msbophan="' + data[i].ComponentID + '" data-msthongso="' + data[i].MonitoringParamsID + '" type="checkbox" checked/><i class="fa fa-square-o"></i><i class="fa fa-check-square-o"></i></label><td>' + data[i].ValueParamName + '</td><td><textarea class="form-control" id="txtGhiChu">' + ghichu + '</textarea><td><img id="' + i + '"    style="width: 100px;height: 50px;"  ' + (data[i].ImageGS === null ? 'src=""' : 'src="../Monitoring/RetrieveImage?moningtoringID=' + data[i].MonitoringParamsID + '&compunetID=' + data[i].ComponentID + ' "') + 'data-mor ="' + data[i].MonitoringParamsID + '" data-com ="' + data[i].ComponentID + '" onclick ="LoadHinh(this)" class="rounded mx-auto d-block" alt=""></td> </tr></tr>')
                                        }
                                        else {
                                            vars.$tbQualityParameterBody.append('<tr><td >(' + data[i].ComponentID + ') ' + data[i].ComponentName + '- (' + data[i].MonitoringParamsName + ')</td><td class="dt-body-center" data-toggle="buttons" style="width:120px" data-pass="' + data[i].Pass + '"><label class="btn"><input class="form-control" data-id="' + data[i].ValueParamID + '" data-msbophan="' + data[i].ComponentID + '" data-msthongso="' + data[i].MonitoringParamsID + '" type="checkbox"/><i class="fa fa-square-o"></i><i class="fa fa-check-square-o"></i></label><td>' + data[i].ValueParamName + '</td><td><textarea class="form-control" id="txtGhiChu">' + ghichu + '</textarea><td><img id="' + i + '"    style="width: 100px;height: 50px;"  ' + (data[i].ImageGS === null ? 'src=""' : 'src="../Monitoring/RetrieveImage?moningtoringID=' + data[i].MonitoringParamsID + '&compunetID=' + data[i].ComponentID + ' "') + 'data-mor ="' + data[i].MonitoringParamsID + '" data-com ="' + data[i].ComponentID + '" onclick ="LoadHinh(this)" class="rounded mx-auto d-block" alt=""></td> </tr></tr>')
                                        }
                                    }
                                    else {
                                        vars.$tbQualityParameterBody.append('<tr><td >(' + data[i].ComponentID + ') ' + data[i].ComponentName + '-' + data[i].MonitoringParamsName + ' (' + data[i].TEN_DV_DO + ')</td><td style="padding-left: 20px;">' + data[i].ValueParamName.split[0] + '</td><td style="width:110px" ><input  value="' + (data[i].Measurement == null ? '' : data[i].Measurement) + '" class="form-control" type="text"   data-range=\'' + data[i].ValueParamName.split('!')[1] + '\' style="width: 100%" data-msbophan="' + data[i].ComponentID + '" data-msthongso="' + data[i].MonitoringParamsID + '" /></td><td><textarea class="form-control" id="txtGhiChu">' + ghichu + '</textarea></td><td><img id="' + i + '"    style="width: 100px;height: 50px;"  ' + (data[i].ImageGS === null ? 'src=""' : 'src="../Monitoring/RetrieveImage?moningtoringID=' + data[i].MonitoringParamsID + '&compunetID=' + data[i].ComponentID + ' "') + 'data-mor ="' + data[i].MonitoringParamsID + '" data-com ="' + data[i].ComponentID + '" onclick ="LoadHinh(this)" class="rounded mx-auto d-block" alt=""></td> </tr></tr>')
                                    }
                                }
                                vars.$qualityParamsDatatables = $("#tbQualityParameter").DataTable({
                                    "columnDefs": [
                                        { "visible": false, "targets": 0 },
                                        { "width": '10%', "targets": 1 },
                                        { "width": "40%", "targets": 2 },
                                        { "width": "40%", "targets": 3 },
                                        { "width": "10%", "targets": 4 }
                                    ],
                                    "language":
                                    {
                                        "processing": "<div class='overlay custom-loader-background'><i class='fa fa-cog fa-spin custom-loader-color'></i></div>",
                                        "sSearch": "<span data-lang='lblSearch'>" + (global.TypeLanguage == 0 ? "Tìm " : "Search") + "</span> ",
                                        "info": "",
                                        "zeroRecords": "<span data-lang='lblFilterInfo'>" + (global.TypeLanguage == 0 ? "Không tìm thấy" : "No matching records found") + "</span>",
                                        "lengthMenu": "<span data-lang='lblShow'>" + (global.TypeLanguage == 0 ? "Xem " : "Show") + "</span> _MENU_ <span data-lang='lblEntries'>" + (global.TypeLanguage == 0 ? "dòng " : "entries") + "</span>",
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
                                    "lengthChange": false,
                                    "lengthMenu": [20],
                                    "scrollY": '50vh',
                                    "scrollX": true,
                                    "processing": true,
                                    "drawCallback": function (settings) {
                                        var api = this.api();
                                        var rows = api.rows({ page: 'current' }).nodes();
                                        var last = null;
                                        var groupadmin = [];
                                        api.column(0, { page: 'current' }).data().each(function (group, i) {
                                            if (last !== group) {
                                                $(rows).eq(i).before(
                                                    '<tr data-root"' + i + '" class="group" id="' + i + '"><td colspan="4">' + group + '</td></tr>'
                                                );
                                                groupadmin.push(i);
                                                last = group;
                                            }
                                        });
                                        for (var k = 0; k < groupadmin.length; k++) {
                                            $("#" + groupadmin[k]).nextUntil("#" + groupadmin[k + 1]).addClass(' group-' + groupadmin[k]);
                                            $("#" + groupadmin[k]).nextUntil("#" + groupadmin[k + 1]).attr('data-root', groupadmin[k]);
                                            $("#" + groupadmin[k]).click(function () {
                                                var gid = $(this).attr("id");
                                                $(".group-" + gid).slideToggle(300);
                                            });
                                        }
                                    }
                                });
                                $($.fn.dataTable.tables(true)).css('width', '100%');
                                $($.fn.dataTable.tables(true)).DataTable().columns.adjust().draw(false);
                                Loading.fn.Hide();
                            }
                            else {
                                Loading.fn.Hide();
                                //if (action == 'button') {
                                //    $('#myModal').appendTo("body").modal('show')
                                //}
                                Alert.fn.Show((global.TypeLanguage == 0 ? "Không có dữ liệu!" : "No data to print!"), Alert.Type.warning);
                            }
                        });
                    }
                },

                LoadSoGioChayMay: function (id, action) {
                    if (method.KiemTraChonMay() !== false) {
                        $.post(urlGetThoiGianChayMay, { msmay: id }, function (data) {
                            $('#lblGioChayMay').text(data.chuoi);
                            $('#txtSoGioLuyKe').val(data.sglk);
                            $('#lblGioChayMay').attr('data-giochay', data.sgs);
                        });
                    }
                },


                LoadGridTMP: function (id, action) {
                    if (method.KiemTraChonMay() !== false) {
                        Loading.fn.Show()
                        $.post(urlConditionMonitoringParameterGetTMP, function (data) {
                            if ($.fn.DataTable.isDataTable('#tbQualityParameter')) {
                                $('#tbQualityParameter').dataTable().fnDestroy();
                            }
                            vars.$tbQualityParameterBody.empty()
                            if (data.length > 0) {
                                var i = 0;
                                for (i = 0; i < data.length; i++) {
                                    var ghichu = '';
                                    if (data[i].Note === null) {
                                        ghichu = "";
                                    }
                                    else {
                                        ghichu = data[i].Note;
                                    }

                                    if (data[i].TypeOfParam === true) {
                                        if (data[i].Measurement == 1) {
                                            vars.$tbQualityParameterBody.append('<tr><td >(' + data[i].ComponentID + ') ' + data[i].ComponentName + '- (' + data[i].MonitoringParamsName + ')</td><td class="dt-body-center" data-toggle="buttons" style="width:120px" data-pass="' + data[i].Pass + '"><label class="btn"><input class="form-control" data-id="' + data[i].ValueParamID + '" data-msbophan="' + data[i].ComponentID + '" data-msthongso="' + data[i].MonitoringParamsID + '" type="checkbox" checked/><i class="fa fa-square-o"></i><i class="fa fa-check-square-o"></i></label><td>' + data[i].ValueParamName + '</td><td><textarea class="form-control" id="txtGhiChu">' + ghichu + '</textarea></tr>')
                                        }
                                        else {
                                            vars.$tbQualityParameterBody.append('<tr><td >(' + data[i].ComponentID + ') ' + data[i].ComponentName + '- (' + data[i].MonitoringParamsName + ')</td><td class="dt-body-center" data-toggle="buttons" style="width:120px" data-pass="' + data[i].Pass + '"><label class="btn"><input class="form-control" data-id="' + data[i].ValueParamID + '" data-msbophan="' + data[i].ComponentID + '" data-msthongso="' + data[i].MonitoringParamsID + '" type="checkbox"/><i class="fa fa-square-o"></i><i class="fa fa-check-square-o"></i></label><td>' + data[i].ValueParamName + '</td><td><textarea class="form-control" id="txtGhiChu">' + ghichu + '</textarea></tr>')
                                        }
                                    }
                                }
                                vars.$qualityParamsDatatables = $("#tbQualityParameter").DataTable({
                                    "columnDefs": [
                                        { "visible": false, "targets": 0 },
                                        { "width": '10%', "targets": 1 },
                                        { "width": "40%", "targets": 2 },
                                        { "width": "50%", "targets": 3 }
                                    ],
                                    "language":
                                    {
                                        "processing": "<div class='overlay custom-loader-background'><i class='fa fa-cog fa-spin custom-loader-color'></i></div>",
                                        "sSearch": "<span data-lang='lblSearch'>" + (global.TypeLanguage == 0 ? "Tìm " : "Search") + "</span> ",
                                        "info": "",
                                        "zeroRecords": "<span data-lang='lblFilterInfo'>" + (global.TypeLanguage == 0 ? "Không tìm thấy" : "No matching records found") + "</span>",
                                        "lengthMenu": "<span data-lang='lblShow'>" + (global.TypeLanguage == 0 ? "Xem " : "Show") + "</span> _MENU_ <span data-lang='lblEntries'>" + (global.TypeLanguage == 0 ? "dòng " : "entries") + "</span>",
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
                                    "lengthChange": false,
                                    "lengthMenu": [20],
                                    "scrollY": '50vh',
                                    "scrollX": true,
                                    "processing": true,
                                    "drawCallback": function (settings) {
                                        var api = this.api();
                                        var rows = api.rows({ page: 'current' }).nodes();
                                        var last = null;
                                        var groupadmin = [];
                                        api.column(0, { page: 'current' }).data().each(function (group, i) {
                                            if (last !== group) {
                                                $(rows).eq(i).before(
                                                    '<tr data-root"' + i + '" class="group" id="' + i + '"><td colspan="3">' + group + '</td></tr>'
                                                );
                                                groupadmin.push(i);
                                                last = group;
                                            }
                                        });
                                        for (var k = 0; k < groupadmin.length; k++) {
                                            $("#" + groupadmin[k]).nextUntil("#" + groupadmin[k + 1]).addClass(' group-' + groupadmin[k]);
                                            $("#" + groupadmin[k]).nextUntil("#" + groupadmin[k + 1]).attr('data-root', groupadmin[k]);
                                            $("#" + groupadmin[k]).click(function () {
                                                var gid = $(this).attr("id");
                                                $(".group-" + gid).slideToggle(300);
                                            });
                                        }
                                    }
                                });
                                $($.fn.dataTable.tables(true)).css('width', '100%');
                                $($.fn.dataTable.tables(true)).DataTable().columns.adjust().draw(false);
                                Loading.fn.Hide();
                            }
                            else {
                                Loading.fn.Hide();
                                Alert.fn.Show(Messenger.msgKhongCoDuLieu, Alert.Type.warning);
                            }
                        });
                    }
                },
                GetConditionMonitoringByDevice: function () {
                    var keys = vars.$equipDatatables.data().count();
                    if (keys == 0) {
                        Alert.fn.Show(Messenger.msgDuLieuRong, Alert.Type.warning);
                        return;
                    }
                    keys = vars.$equipDatatables.$('tr[class$=selected]')
                    if (keys.length == 0) {
                        Alert.fn.Show(Messenger.msgChonDevice, Alert.Type.warning)
                        return;
                    }
                    Loading.fn.Show()
                    $('#myModal').appendTo("body").modal('hide')
                    window.setTimeout(function () { method.LoadGrid($('#tbEquip tr[class$=selected]').attr('data-id'), 'button'); }, 500);
                    window.setTimeout(function () { method.LoadSoGioChayMay($('#tbEquip tr[class$=selected]').attr('data-id'), 'button'); }, 500);
                    vars.$txtDevice.val(vars.$tbEquip.find('tr[class$=selected]').attr('data-id'))
                },
                ToggleCheckboxesOnTable: function () {
                    var $checkbox = $(this).find(':checkbox');
                    $checkbox.prop('checked', !$checkbox.prop('checked'))
                    if ($checkbox.is(':checked')) {
                        $checkbox.closest('td').addClass("checked");
                        var root = $(this).parent().attr('data-root');
                        var dat = $(this).attr('data-pass');//dat
                        if (dat == 1) {
                            $('#tbQualityParameter tr[data-root=' + root + '] td[data-pass ="0"] input[type=checkbox]').prop('checked', false);
                        }
                        else {
                            $('#tbQualityParameter tr[data-root=' + root + '] td[data-pass ="1"] input[type=checkbox]').prop('checked', false);
                        }
                    }
                    else {
                        $(this).parent().removeClass("checked");
                    }
                },
                DetectInputValueNumber: function () {
                    if (method.KiemTraChonMay() == false) { $(this).val(''); return false; }
                    if ($(this).val() == '') return false;
                    if (!$.isNumeric($(this).val())) {
                        $(this).val('');
                        $(this).focus();
                        Alert.fn.Show((global.TypeLanguage == 0 ? "Chỉ được nhập số!" : "Please enter a valid number!"), Alert.Type.warning);
                        return false;
                    }
                    else {
                        var gtd = $('#lblGioChayMay').data('giochay');
                        var gtht = $('#txtSoGioLuyKe').val();
                        if (parseFloat(gtht) <= parseFloat(gtd) && parseFloat(gtht) != 0) {
                            Alert.fn.Show((global.TypeLanguage == 0 ? "Số giờ lũy kế nhỏ hơn trước đó!" : "The accumulated hours are smaller than before!"), Alert.Type.warning);
                            $(this).val('');
                            $(this).focus();
                        }
                    }
                },
                DetectInputValueNumberGrid: function () {
                    if (vars.$busy == true) return false;
                    if ($(this).val() == '') return false;
                    if (!$.isNumeric($(this).val())) {
                        vars.$busy = true;
                        $(this).val('');
                        $(this).focus();
                        Alert.fn.Show((global.TypeLanguage == 0 ? "Chỉ được nhập số!" : "Please enter a valid number!"), Alert.Type.warning);
                        vars.$busy = false;
                        return false;
                    }
                    var data = JSON.parse($(this).attr('data-range'));
                    var flag = 0;
                    for (var i = 0; i < data.length; i++) {
                        if (parseFloat(data[i].GiaTriTren) >= parseFloat($(this).val()) && parseFloat($(this).val()) >= parseFloat(data[i].GiaTriDuoi)) {
                            flag = flag + 1;
                            continue;
                        }
                    }
                    if (flag == 0) {
                        vars.$busy = true; //out endless loop (recusrive)
                        $(this).focus();
                        Alert.fn.Show(global.TypeLanguage == 0 ? "Giá trị nhập không phù hợp!" : "Not suitable for input value!", Alert.Type.warning);
                        vars.$busy = false;
                        return false;
                    }
                },

                KiemTraChonMay: function () {
                    var tmp;
                    if ($('#stt').val() != "-1") {
                        tmp = $('#cbbThietBi').val();
                    }
                    else {
                        tmp = $('#txtDevice').val();
                    }
                    if (tmp === '') {
                        Alert.fn.Show("máy chưa được chọn", Alert.Type.warning);
                        return false;
                    }
                    else {
                        return true;
                    }
                },
                insertChat: function (who, text, time) {
                    var me = {};
                    me.avatar = "https://lh6.googleusercontent.com/-lr2nyjhhjXw/AAAAAAAAAAI/AAAAAAAARmE/MdtfUmC0M4s/photo.jpg?sz=48";
                    var you = {};
                    you.avatar = "https://a11.t26.net/taringa/avatares/9/1/2/F/7/8/Demon_King1/48x48_5C5.jpg";

                    if (time === undefined) {
                        time = 0;
                    }
                    var control = "";
                    var date = method.formatAMPM(new Date());
                    if (who == "me") {
                        control = '<li style="width:100%">' +
                            '<div class="msj macro">' +
                            '<div class="avatar"><img class="img-circle" style="width:50%;" src="' + me.avatar + '" /></div>' +
                            '<div class="text text-l">' +
                            '<p>' + text + '</p>' +
                            '<p><small>' + date + '</small></p>' +
                            '</div>' +
                            '</div>' +
                            '</li>';
                    } else {
                        control = '<li style="width:100%;">' +
                            '<div class="msj-rta macro">' +
                            '<div class="text text-r">' +
                            '<p>' + text + '</p>' +
                            '<p><small>' + date + '</small></p>' +
                            '</div>' +
                            '<div class="avatar" style="padding:0px 0px 0px 10px !important"><img class="img-circle" style="width:50%;" src="' + you.avatar + '" /></div>' +
                            '</li>';
                    }
                    setTimeout(
                        function () {
                            $(".frame ul").append(control).scrollTop($(" ul").prop('scrollHeight'));
                        }, time);

                },
                resetChat: function () {
                    $(".frame ul").empty();
                },
                formatAMPM: function (date) {
                    var hours = date.getHours();
                    var minutes = date.getMinutes();
                    var ampm = hours >= 12 ? 'PM' : 'AM';
                    hours = hours % 12;
                    hours = hours ? hours : 12; // the hour '0' should be '12'
                    minutes = minutes < 10 ? '0' + minutes : minutes;
                    var strTime = hours + ':' + minutes + ' ' + ampm;
                    return strTime;
                },
                SpeackGSTT: function (input) {
                    var msmay = $('#txtDevice').val();
                    $.ajax({
                        url: urlSpeakVoice,
                        type: "Post",
                        dataType: "json",
                        data: { a: input, stt: stt, msmay: msmay },
                        success: function (resulst) {
                            if (resulst.stt != 0) {
                                stt = stt + 1;
                            }
                            var audio = new Audio('../Resource/' + resulst.name + '.mp3');
                            var audio1 = new Audio('../EcomaintSite/Resource/' + resulst.name + '.mp3');
                            method.insertChat("you", resulst.tengt, 1000);
                            audio.load();
                            audio.play();
                            audio1.load();
                            audio1.play();
                        },
                        error: function (response) {
                            alert(response);
                        }
                    });
                }
            }
            var method
            $scope.fn = {
                Init: function () {
                    global.CurrentNamePage = currentPageName
                    MainMenu.fn.SetActive(menuID);
                    Languages.fn.AutoChangeLanguage()
                    Main.fn.InitButtonFloat(buttonFloat)
                    method = fnPrivate
                    vars = bindVariables();
                    $('#fromDate').attr("disabled", true);
                    $(".select2").select2(
                        {
                            theme: "classic"
                        });
                    $('input[type=radio][name=optradio]').change(function () {
                        if ($('#stt').val() != "-1") {
                            method.LoadGrid($('#cbbThietBi').val(), 'keypress');
                        }
                        else {
                            method.LoadGrid(vars.$txtDevice.val(), 'keypress');
                        }
                        if (this.value == 0) {
                            $('#fromDate').attr("disabled", true);
                        }
                        else {
                            $('#fromDate').attr("disabled", false);
                        }
                    });
                    if ($('#stt').val() != "-1") {
                        Ckeckload = 0;
                        $('#cbbDiaDiem').attr("disabled", true);
                        $("#cbbLoaiCV").change(function () {
                            method.LoadGrid($('#cbbThietBi').val(), 'keypress');
                        });
                        Main.fn.InitDateTimePickerChanged([$('#fromDate')], method.LoadGrid($('#cbbThietBi').val(), 'keypress'));
                        $('#fromDate').on('dp.change', function () {
                            method.LoadGrid($('#cbbThietBi').val(), 'keypress');
                        });
                        $("#cbbThietBi").change(function () {
                            method.LoadGrid($('#cbbThietBi').val(), 'keypress');
                        });
                        Ckeckload = 1;
                        method.LoadGrid($('#cbbThietBi').val(), 'keypress');
                        method.LoadSoGioChayMay($('#cbbThietBi').val(), 'keypress');

                    }
                    else {
                        $("#cbbLoaiCV").change(function () {
                            method.LoadGrid(vars.$txtDevice.val(), 'keypress');
                        });
                        $('#fromDate').on('dp.change', function () {
                            method.LoadGrid(vars.$txtDevice.val(), 'keypress');
                        });
                        if ($("#txtDevice").val() !== "") {
                            method.LoadGrid(vars.$txtDevice.val(), 'keypress');
                        }
                    }
                    vars.$equipDatatables = $("#tbEquip").DataTable();
                    vars.$equipDatatablesTmp = vars.$equipDatatables.rows().data();
                    vars.$tbEquipBody.on('doubletap', 'tr', method.GetConditionMonitoringByDevice)
                    vars.$tbEquipBody.on('dblclick', 'tr', method.GetConditionMonitoringByDevice)
                    vars.$btnChooseEquip.click(method.GetConditionMonitoringByDevice)
                    vars.$qualityParamsDatatables = $("#tbQualityParameter").DataTable({
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
                        responsive: true,
                        "lengthChange": false,
                        "lengthMenu": [10]
                    });
                    vars.$tbQualityParameterBody.on('click', 'td:first-child', method.ToggleCheckboxesOnTable);
                    $('#tbthongsodinhtinh').on('click', 'tr', method.TableThongSo_RowChanged);
                    Main.fn.ScanBarCode($('#ReadBtn1'), $('#fileToUpload'), $('#cbbThietBi'), $('#cbbThietBi option'));
                    Main.fn.ScanBarCode($('#ReadBtn'), $('#fileToUpload'), $('#txtDevice'), null);
                    vars.$txtDevice.on('keypress', function (e) {
                        if (e.which === 13) {
                            Loading.fn.Show()
                            window.setTimeout(function () { method.LoadGrid(vars.$txtDevice.val(), 'keypress'); }, 500);
                        }
                    });
                    vars.$txtDevice.on('change', function (e) {
                        method.LoadGrid(vars.$txtDevice.val(), 'change');
                        method.LoadSoGioChayMay(vars.$txtDevice.val(), 'change');
                    });

                    $('#txtSoGioLuyKe').on('change', method.DetectInputValueNumber);



                    //su kien chat
                    $(".mytext").on("keydown", function (e) {
                        if (e.which == 13) {
                            var text = $(this).val();
                            if (text == '')
                                text = 'ok';
                            if (text !== "") {
                                method.insertChat("me", text);
                                method.SpeackGSTT(text);
                                $(this).val('');
                            }
                        }
                    });
                    $('body > div > div > div:nth-child(2) > span').click(function () {
                        $(".mytext").trigger({ type: 'keydown', which: 13, keyCode: 13 });
                    }),
                        method.resetChat();
                    //-- Print Messages
                    //insertChat("me", "Spaceman: Computer! Computer! Do we bring battery?!", 9500);
                    //insertChat("you", "LOL", 12000);

                    vars.$tbQualityParameterBody.on("focusout", "input[type=text]", method.DetectInputValueNumberGrid);},
                Ghi: function () {
                    if (vars.$qualityParamsDatatables.data().count() == 0) {
                        Alert.fn.Show(Messenger.msgDuLieuRong, Alert.Type.warning);
                        return;
                    }
                    var count = 0;
                    if (vars.$qualityParamsDatatables.$('input[type=checkbox]:checked').length == 0) {
                        count = count + 1;
                    }
                    if (vars.$qualityParamsDatatables.$('input[type=text]:not([value=""])').length == 0) {
                        count = count + 1;
                    }
                    if (count == 2) {
                        Alert.fn.Show(Messenger.msgChuaChonDuLieu, Alert.Type.warning);
                        return
                    }
                    Loading.fn.Show();
                    var lstParameter = new Array();
                    if (vars.$qualityParamsDatatables.$('input[type=text]:not([value=""])').length > 0) {
                        vars.$qualityParamsDatatables.$('input[type=text]:not([value=""])').each(function (i, obj) {
                            lstParameter[i] = new Object();
                            if ($('#stt').val() === '-1') {
                                lstParameter[i].DeviceID = vars.$txtDevice.val();
                            }
                            else {
                                lstParameter[i].DeviceID = $('#cbbThietBi').val();
                            }
                            lstParameter[i].MonitoringParamsID = $(obj).attr('data-msthongso');
                            lstParameter[i].ComponentID = $(obj).attr('data-msbophan');
                            lstParameter[i].TypeOfParam = 0;
                            lstParameter[i].ValueParamID = -1;
                            var data = JSON.parse($(obj).attr('data-range'));
                            var j = 0;
                            for (j = 0; j < data.length; j++) {
                                if (parseFloat(data[j].GiaTriTren) >= parseFloat($(obj).val()) && parseFloat($(obj).val()) >= parseFloat(data[j].GiaTriDuoi)) {
                                    lstParameter[i].ID = data[j].ID
                                    break;
                                }
                            }
                            lstParameter[i].Measurement = $(obj).val();
                            lstParameter[i].Note = $(obj).closest('tr').find('textarea').val();
                        });
                    }
                    var cur_length = lstParameter.length;
                    if (vars.$qualityParamsDatatables.$('input[type=checkbox]:checked').length > 0) {
                        vars.$qualityParamsDatatables.$('input[type=checkbox]:checked').each(function (i, obj) {//lay nhung dong duoc check
                            var j = i + cur_length;
                            lstParameter[j] = new Object();
                            if ($('#stt').val() === '-1') {
                                lstParameter[j].DeviceID = vars.$txtDevice.val();
                            }
                            else {
                                lstParameter[j].DeviceID = $('#cbbThietBi').val();
                            }
                            lstParameter[j].MonitoringParamsID = $(obj).attr('data-msthongso');
                            lstParameter[j].ComponentID = $(obj).attr('data-msbophan');
                            lstParameter[j].TypeOfParam = 1;
                            lstParameter[j].ID = 1;
                            lstParameter[j].ValueParamID = $(obj).attr('data-id');
                            lstParameter[j].Measurement = 1;
                            lstParameter[j].Note = $(obj).closest('tr').find('textarea').val();
                        });
                    }
                    $.post(urlConditionMonitoringParameterSave, { data: JSON.stringify(lstParameter), mscn: $("#cbbNhanVien").val(), stt: $('#stt').val(), ngaykt: $('#ngaykt').val(), giokt: $('#giokt').val(), chisotruoc: $('#lblGioChayMay').data('giochay'), luyke: $('#txtSoGioLuyKe').val() }, function (data) {
                        if (data == "success") {
                            $('#tbQualityParameter').dataTable().fnDestroy();
                            $('#tbQualityParameter tbody').empty();
                            Loading.fn.Hide();
                            window.location.href = urlGetGiamSatTinhTrang;
                        }
                        else {
                            
                            Loading.fn.Hide();
                            Alert.fn.Show(Messenger.msgGhiKhongThanhCong, Alert.Type.error);
                        }
                    });
                },
                chonkhongdat: function () {
                    if (vars.$qualityParamsDatatables.data().count() == 0) {
                        Alert.fn.Show(Messenger.msgDuLieuRong, Alert.Type.warning);
                        return;
                    }
                    var rows = $('#tbQualityParameter').DataTable().rows().nodes();
                    $("input[type=checkbox]", rows).prop('checked', false);
                    $("td[data-pass=0] input[type=checkbox]", rows).prop('checked', true);
                },
                chondat: function () {
                    if (vars.$qualityParamsDatatables.data().count() == 0) {
                        Alert.fn.Show(Messenger.msgDuLieuRong, Alert.Type.warning);
                        return;
                    }
                    var rows = $('#tbQualityParameter').DataTable().rows().nodes();
                    $("input[type=checkbox]", rows).prop('checked', false);
                    $("td[data-pass=1] input[type=checkbox]", rows).prop('checked', true);
                },
                bochon: function () {
                    if (vars.$qualityParamsDatatables.data().count() == 0) {
                        Alert.fn.Show(Messenger.msgDuLieuRong, Alert.Type.warning);
                        return;
                    }

                    var rows = $('#tbQualityParameter').DataTable().rows().nodes();
                    $("input[type=checkbox]", rows).prop('checked', false);
                },
                ShowEquipForm: function () {
                    $.post(urlCheckTheParametersDue, { msnx: $("#cbbDiaDiem").val() }, function (data) {
                        if (data.length > 0) {
                            if ($.fn.DataTable.isDataTable('#tbEquip')) {
                                $('#tbEquip').dataTable().fnDestroy();
                            }
                            vars.$equipDatatables = $("#tbEquip").DataTable({
                                data: data,
                                columns: [
                                    { data: 'ID' },
                                    { data: 'Name' }
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
                                    if (data.hasOwnProperty("ID")) {
                                        $(row).attr('data-id', data.ID);
                                    }
                                },
                            });
                            $('#myModal').appendTo("body").modal('show')
                        }
                        else {
                            Alert.fn.Show(Messenger.msgKhongCoThietBi, Alert.Type.warning);
                            return false;
                        }
                    });
                },
                ShowRuntime: function () {
                    var tmp;
                    if ($('#stt').val() != "-1") {
                        tmp = $('#cbbThietBi').val();
                    }
                    else {
                        tmp = $('#txtDevice').val();
                    }
                    if (tmp === '') {
                        Alert.fn.Show("máy chưa được chọn", Alert.Type.warning);
                    }
                    $.post(urlGetRuntimeInYear, { msmay: tmp }, function (data) {
                        if (data.length > 0) {
                            if ($.fn.DataTable.isDataTable('#tbRuntime')) {
                                $('#tbRuntime').dataTable().fnDestroy();
                            }
                            vars.$equipDatatables = $("#tbRuntime").DataTable({
                                data: data,
                                columns: [
                                    { data: 'NGAY' },
                                    { data: 'CHI_SO_DONG_HO' },
                                    { data: 'SO_GIO_LUY_KE' }
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
                                    if (data.hasOwnProperty("NGAY")) {
                                        $(row).attr('data-id', data.NGAY);
                                    }
                                },
                            });
                            $('#myModalRuntime').appendTo("body").modal('show')
                        }
                        else {
                            Alert.fn.Show(Messenger.msgKhongCoThietBi, Alert.Type.warning);
                            return false;
                        }
                    });
                },
                ShowSpeckVoice: function () {
                    if (vars.$qualityParamsDatatables.data().count() == 0) {
                        Alert.fn.Show(Messenger.msgDuLieuRong, Alert.Type.warning);
                        return;
                    }
                    $('#myModalSpeak').appendTo("body").modal('show');
                    var msmay = $('#txtDevice').val();

                    $.ajax({
                        url: urlStarVoice,
                        type: "Post",
                        dataType: "json",
                        data: { stt: stt, msmay: msmay },
                        success: function (resulst) {
                            stt = 1;
                            //var audio = new Audio('../Resource/' + resulst.name + '.mp3');
                            method.insertChat("you", resulst.tengt, 1000);
                            //audio.load();
                            //audio.play();
                            //var audio1 = new Audio('../GSTT/Resource/' + resulst.name + '.mp3');
                            //audio1.load();
                            //audio1.play();
                            method.SpeackGSTT('OK');
                            //await sleep(5000);

                        },
                        error: function (response) {
                            alert(response);
                        }
                    });

                },

            }
        })
        app.init = function () {
            angular.bootstrap(document, ['InitMonitoringPage'])
        }
        return app;
    })();
    $(function () {
        InitMonitoring.init()
    });
})