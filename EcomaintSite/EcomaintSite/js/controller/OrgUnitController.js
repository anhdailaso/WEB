define(['_MasterPageController', '_MenuPageController', '_DetailsModalController'], function (module, menu, modal) {
    var Unit = (function () {
        var app = angular.module("OrgUnitPage", [])
        app.controller('OrgUnitController', function ($scope) {
            var Main = module.Main
            var Languages = module.Languages
            var InfoDetails = modal
            var Loading = module.Loading
            var Alert = module.Alert
            var MainMenu = menu
            var menuID = 'mnuOrgUnit'
            var currentNamePage = 'OrgUnitWeb'
            var buttonFloat = [
                {
                    "id": "btnMain",
                    "url": "#",
                    "icon": "<i class='fa fa-angle-double-up'></i>"
                },
                {
                    "id": "btnAdd",
                    "url": "#",
                    "icon": "<i class='fa fa-plus'></i>",
                    "lang": "btnThem",
                    "func": 'fn.Add'
                },
                {
                    "id": "btnEdit",
                    "url": "#",
                    "icon": "<i class='fa fa-refresh'></i>",
                    "lang": "btnSua",
                    "func": 'fn.Edit'
                },
                {
                    "id": "btnRemove",
                    "url": "#",
                    "icon": "<i class='fa fa-trash-o'></i>",
                    "lang": "btnXoa",
                    "func": 'fn.Remove'
                }
            ]
            var vars = {}
            var BindVariables = function () {
                return {
                    $tbOrgUnitBody: $('#tbOrgUnit tbody'),
                    $btnSave: $('#btnSave'),
                    $txtID: $('#txtID'),
                    $txtName: $('#txtName'),
                    $txtNameOfEng: $('#txtNameOfEng'),
                    $txtShortName: $('#txtShortName'),
                    $txtAddress: $('#txtAddress'),
                    $txtPhone: $('#txtPhone'),
                    $txtFax: $('#txtFax'),
                    $txtCompactName: $('#txtCompactName'),
                    $ckDefault: $('#ckDefault'),
                    $ckOutsource: $('#ckOutsource')
                };
            }
            var method = 'undefined'
            var fnPrivate = {
                ResetText: function () {
                    vars.$txtID.val('');
                    vars.$txtName.val('');
                    vars.$txtNameOfEng.val('');
                    vars.$txtShortName.val('');
                    vars.$txtAddress.val('');
                    vars.$txtPhone.val('');
                    vars.$txtFax.val('');
                    vars.$txtCompactName.val('');
                    vars.$ckDefault.prop('checked', false)
                    vars.$ckOutsource.prop('checked', false)
                },
                EnableControl: function (bool) {
                    vars.$txtID.prop('disabled', bool)
                    vars.$txtName.prop('disabled', bool)
                    vars.$txtNameOfEng.prop('disabled', bool)
                    vars.$txtShortName.prop('disabled', bool)
                    vars.$txtAddress.prop('disabled', bool)
                    vars.$txtPhone.prop('disabled', bool)
                    vars.$txtFax.prop('disabled', bool)
                    vars.$txtCompactName.prop('disabled', bool)
                    vars.$ckDefault.prop('disabled', bool)
                    vars.$ckOutsource.prop('disabled', bool)
                },
                FillDataToModal: function () {
                    var id = $(this).attr('data-id');
                    $(this).each(function () {
                        if (!this.rowIndex) return;
                        vars.$txtID.val(id);
                        vars.$txtName.val(this.cells[0].innerHTML);
                        vars.$txtShortName.val(this.cells[1].innerHTML);
                        vars.$txtNameOfEng.val(this.cells[2].innerHTML);
                        vars.$txtAddress.val(this.cells[3].innerHTML);
                        vars.$txtPhone.val(this.cells[4].innerHTML);
                        vars.$txtFax.val(this.cells[5].innerHTML);
                        vars.$txtCompactName.val(this.cells[6].innerHTML);
                        vars.$ckDefault.prop('checked', this.cells[7].innerHTML == 'true' ? true : false)
                        vars.$ckOutsource.prop('checked', this.cells[8].innerHTML == 'true' ? true : false)
                        return false;
                    });
                },
                ShowDetailAboutUnit: function () {
                    $(this).each(function () {
                        if (!this.rowIndex) return;
                        InfoDetails.fn.Show($('#tbOrgUnit thead tr th'), this.cells);
                        return false;
                    });
                },
                Save: function () {
                    if (vars.$txtID.val() == '') {
                        Alert.fn.Show(Messenger.msgNhapMaDonVi, Alert.Type.warning);
                        vars.$txtID.focus();
                        return false;
                    }
                    if (vars.$txtName.val() == '') {
                        Alert.fn.Show(Messenger.msgNhapTenDonVi, Alert.Type.warning);
                        vars.$txtName.focus();
                        return false;
                    }
                    if (vars.$txtShortName.val() == '') {
                        Alert.fn.Show(Messenger.msgNhapTenNgan, Alert.Type.warning);
                        vars.$txtShortName.focus();
                        return false;
                    }
                    var lstUnit = new Array();
                    lstUnit[0] = new Object();
                    lstUnit[0].ID = vars.$txtID.val();
                    lstUnit[0].Name = vars.$txtName.val();
                    lstUnit[0].ShortName = vars.$txtShortName.val();
                    lstUnit[0].NameOfEng = vars.$txtNameOfEng.val();
                    lstUnit[0].Address = vars.$txtAddress.val();
                    lstUnit[0].Phone = vars.$txtPhone.val();
                    lstUnit[0].Fax = vars.$txtFax.val();
                    lstUnit[0].CompactName = vars.$txtCompactName.val();
                    lstUnit[0].Default = vars.$ckDefault.prop('checked');
                    lstUnit[0].IsOutsource = vars.$ckOutsource.prop('checked');
                    var action = vars.$btnSave.attr('data-action')
                    var stringData = JSON.stringify(lstUnit);
                    $.post(urlSave, { data: stringData, action: action }, function (data) {
                        if (data == "success") {
                            method.EnableControl(false);
                            Alert.fn.Show(Messenger.msgGhiThanhCong, Alert.Type.success, Messenger.msgInfo)
                            var tableBody = $('#tbOrgUnit tbody');
                            if (action !== 'add') {
                                $('#tbOrgUnit tr[data-id="' + lstUnit[0].ID + '"]').remove()
                            }
                            tableBody.append('<tr data-id="' + lstUnit[0].ID + '"> <td style="width: 150px">' + lstUnit[0].Name + '</td> <td style="width: 150px" class="hidden">' + lstUnit[0].ShortName + '</td> <td style="width: 150px">' + lstUnit[0].NameOfEng + '</td> <td style="width: 150px">' + lstUnit[0].Address + '</td>  <td style="width: 150px">' + lstUnit[0].Phone + '</td>  <td style="width: 150px">' + lstUnit[0].Fax + '</td>  <td style="width: 150px" class="hidden">' + lstUnit[0].CompactName + '</td><td style="width: 150px" class="hidden">' + lstUnit[0].Default + '</td> <td style="width: 150px" class="hidden">' + lstUnit[0].IsOutsource + '</td></tr>')
                            $('#tbOrgUnit tr[data-id="' + lstUnit[0].ID + '"]').click()
                            $('#myModal').appendTo("body").modal('hide')
                            vars.$btnSave.attr('data-action', '')
                        }
                        else if (data.indexOf('failure') != "-1") {
                            Alert.fn.Show(Messenger.msgKhongThanhCong + ' (' + data + ')', Messenger.msgLoi, Alert.Type.error)
                        }
                        else {
                            Alert.fn.Show(data, Messenger.msgLoi, Alert.Type.error)
                        }
                    });

                }
            }
            $scope.fn = {
                Init: function () {
                    global.CurrentNamePage = currentNamePage
                    MainMenu.fn.SetActive(menuID);
                    Languages.fn.AutoChangeLanguage()
                    vars = BindVariables();
                    method = fnPrivate
                    vars.$btnSave.click(method.Save)
                    vars.$tbOrgUnitBody.on('click', 'tr', method.FillDataToModal);
                    vars.$tbOrgUnitBody.on('dblclick doubletap', 'tr', method.ShowDetailAboutUnit);
                    Main.fn.InitButtonFloat(buttonFloat)
                    vars.$tbOrgUnitBody.find('tr:first-child').click();
                },
                Add: function () {
                    method.ResetText()
                    method.EnableControl(false)
                    vars.$btnSave.attr('data-action', 'add')
                    $('#myModal').appendTo("body").modal('show')
                },
                Edit: function () {
                    if (vars.$txtID.val() == '')
                        $('#tbOrgUnit tr[class$="selected"]').click();
                    var id = $('#tbOrgUnit tr[class$="selected"]').attr('data-id')
                    if (typeof id == 'undefined') {
                        Alert.fn.Show(Messenger.msgChonDonViSua, Alert.Type.warning);
                        return false;
                    }
                    vars.$btnSave.attr('data-action', 'edit')
                    method.EnableControl(false)
                    vars.$txtID.prop('disabled', true)
                    $('#myModal').appendTo("body").modal('show')
                },
                Remove: function () {
                    var id = $('#tbOrgUnit tr[class$="selected"]').attr('data-id')
                    if (typeof id == 'undefined') {
                        Alert.fn.Show(Messenger.msgChonDonViXoa, Alert.Type.warning);
                        return false;
                    }
                    Alert.fn.ShowConfirm(Messenger.msgXoaYeuCau, Alert.Type.info, Messenger.msgLoi, function (result) {
                        if (result === true) {
                            $.post(urlRemove, { id: id }, function (data) {
                                if (data == "success") {
                                    $('#tbOrgUnit tr[class$="selected"]').remove();
                                }
                                else {
                                    Alert.fn.Show(data, Alert.Type.error);
                                }
                            });
                            return false;
                        }
                    });
                },
            }
        })
        app.init = function () {
            angular.bootstrap(document, ['OrgUnitPage']);
        }
        return app;
    })();
    $(function () {
        Unit.init()
    });
});
