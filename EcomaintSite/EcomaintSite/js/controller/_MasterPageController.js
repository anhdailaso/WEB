define(["sweetalert", 'jquery-cookie', "datatables-jquery", "button-float", 'bootstrap-datetimepicker', 'treeview-widget', 'datatables-standing-redraw'], function (swal) {
    var instanceDatePicker = $('input[data-type=datepicker]')
    var instanceTimePicker = $('input[data-type=timepicker]')
    var instanceDateTimePicker = $('input[data-type=datetimepicker]')
    var tbody = $("tbody")
    var nvavbarMenu = $('#navbar-menu')
    var menuDropdown = $("#btnMenuDropDown")
    var menuSlideLeft = $("#btnSideMenu")
    var setting = $("#Setting")
    var mainMenu = $("#MainMenu")
    var languageMenuItem = $("#LanguageMenuItem")
    var themeMenuItem = $("#ThemeMenuItem")
    var subMenu = $("#SubMenu")
    var btnBackMenu = $("#btnBack")
    var tabContents = $('.nav-tabs a[role=tab]')
    var Module = {

        /**
          * Initializing main page
          */
        Main: (function () {
            var fnPrivate = {
                Rotate90Button: function () {
                    $(this).attr('disabled', true)
                    //$($.fn.dataTable.tables(true)).css('width', '100%');
                    $($.fn.dataTable.tables(true)).DataTable().columns.adjust().draw(false);
                    // $('.dataTable').dataTable().fnStandingRedraw();
                    $(this).find('i').toggleClass('fa-bars-rotate-90');
                    $(this).removeAttr('disabled')
                },
                SlideMenu: function () {
                    $(this).attr('disabled', true)
                    $($.fn.dataTable.tables(true)).DataTable().columns.adjust().draw(false);
                    if ($("#wrapper").hasClass("side-menu")) {
                        $(this).removeClass('pinned');
                    }
                    else {
                        $(this).addClass('pinned');
                    }
                    $("#wrapper").toggleClass("side-menu");
                    $(this).find('i').toggleClass('fa-bars-rotate-90');
                    $("#navbar-menu").toggleClass("hide-menu");
                    $(this).removeAttr('disabled')
                },
                ShowLanguageMenu: function (e) {
                    e.stopPropagation();
                    languageMenuItem.parent().parent().addClass('hidden')
                    subMenu.removeClass('hidden');
                    subMenu.empty();
                    subMenu.append('<li class="dropdown-title"><button type="button" class="btn btn-flat" id="btnBack"><i class="fa fa-caret-left" aria-hidden="true"></i></button>' + (global.TypeLanguage === 0 ? 'Ngôn ngữ' : 'Language') + '</li ><li><a href="#" onclick="return Languages.fn.ManualChangeLanguage(0);" data-lang="lblTiengViet"> <i class="flag-icon flag-icon-vn flag-icon-squared" style="padding-right: 18px;" ></i> ' + (global.TypeLanguage === 0 ? 'Tiếng việt' : 'Vietnamese') + '</a><a href="#" onclick="return Languages.fn.ManualChangeLanguage(1);" data-lang="lblTiengAnh"> <i class="flag-icon flag-icon-us flag-icon-squared" style="padding-right: 18px;" ></i> ' + (global.TypeLanguage === 0 ? 'Tiếng anh' : 'English') + '</a></li>')
                    if (global.TypeLanguage == 0) {
                        $('a .flag-icon-vn').parent().addClass('active');
                        $('a .flag-icon-us').parent().removeClass('active');
                    }
                    else {
                        $('a .flag-icon-us').parent().addClass('active');
                        $('a .flag-icon-vn').parent().removeClass('active');
                    }
                },
                ShowThemeMenu: function (e) {
                    e.stopPropagation();
                    themeMenuItem.parent().parent().addClass('hidden')
                    subMenu.removeClass('hidden');
                    subMenu.empty();
                    subMenu.append('<li class="dropdown-title"><button type= "button" class="btn btn-flat" id= "btnBack"><i class="fa fa-caret-left" aria-hidden="true"></i></button>' + (global.TypeLanguage === 0 ? 'Giao diện' : 'Theme') + '</li ><li><a href="#" onclick="return Theme.fn.ManualChangeTheme(\'default\');" data-style="default"><i class="glyphicon glyphicon-text-color"></i> <span style="padding-right: 18px;" data-lang="lblBlue">' + (global.TypeLanguage === 0 ? 'Ngôn ngữ' : 'Darkblue') + '</span></a><a href="#" onclick="return Theme.fn.ManualChangeTheme(\'dark\');" data-style="dark"><i class="glyphicon glyphicon-text-color"></i> <span style="padding-right: 18px;" data-lang="lblDark">Dark</span></a><a href="#" onclick="return Theme.fn.ManualChangeTheme(\'light\');" data-style="light"><i class="glyphicon glyphicon-text-color"></i> <span style="padding-right: 18px;" data-lang="lblLight">Light</span></a></li>')

                    $('a[data-style]').removeClass('active');
                    if (global.ThemeStyle == 'default') {
                        $('a[data-style=default]').addClass('active');
                    }
                    else if (global.ThemeStyle == 'dark') {
                        $('a[data-style=dark]').addClass('active');
                    }
                    else if (global.ThemeStyle == 'light') {
                        $('a[data-style=light]').addClass('active');
                    }
                },
                ShowMainMenu: function (e) {
                    $(this).toggleClass('shown')
                    e.stopPropagation();
                },
                ShowSubMenu: function (e) {
                    mainMenu.removeClass('hidden')
                    subMenu.addClass('hidden')
                    subMenu.empty();
                    setting.toggleClass('shown')
                },
                RedirectURL: function (e) {
                    if (!e.ctrlKey) {
                        var link = $(this).attr("href");
                        if (link !== '#' && link !== 'javascript:;') {
                            Module.Loading.fn.Show()
                            window.setTimeout(function () {
                                window.location.href = link;
                            }, 100);
                        }
                    }
                },
                OutSiteMenuClick: function () {
                    setting.removeClass('shown')
                    mainMenu.removeClass('hidden')
                    subMenu.addClass('hidden');
                    subMenu.empty();
                },
                AllowShowMultipleModals: function () {
                    var zIndex = 1040 + (10 * $('.modal:visible').length);
                    $(this).css('z-index', zIndex);
                    setTimeout(function () {
                        $('.modal-backdrop').not('.modal-stack').css('z-index', zIndex - 1).addClass('modal-stack');
                    }, 0);
                },
                RedrawDataTables: function () {
                    $($.fn.dataTable.tables(true)).DataTable().columns.adjust().draw(false);
                },
                InitializingDateTimePicker: function () {
                    $('input[data-type]').each(function () {
                        if ($(this).attr('data-type') == 'datepicker') {
                            $(this).datetimepicker({
                                format: 'DD/MM/YYYY'
                            });
                        }
                        else if ($(this).attr('data-type') == 'timepicker') {
                            $(this).datetimepicker({
                                format: 'HH:mm'
                            });
                        }
                        else if ($(this).attr('data-type') == 'datetimepicker') {
                            $(this).datetimepicker({
                                format: 'DD/MM/YYYY HH:mm',
                            });
                        }
                        if ($(this).attr('data-loading')) {
                            $(this).on('dp.change', function (e) {
                                Module.Loading.fn.Show();
                            });
                        }
                        $(this).focus(function (e) {
                            $(this).attr('readonly', 'readonly');
                        }).focusout(function () {
                            $(this).removeAttr('readonly');
                        });
                    })
                }
            }
            var fn = {
                Init: function () {
                    /*Instance datetimepicker***********************/
                    fnPrivate.InitializingDateTimePicker()
                    /*Selected Highlight****************************/
                    tbody.on('click', 'tr', function () {
                        $(this).addClass("selected").siblings().removeClass("selected");
                    });
                    /*Redirect after processing********************/
                    nvavbarMenu.on("click", "a", fnPrivate.RedirectURL);

                    menuDropdown.click(fnPrivate.Rotate90Button)

                    menuSlideLeft.click(fnPrivate.SlideMenu)

                    languageMenuItem.click(fnPrivate.ShowLanguageMenu)

                    setting.click(fnPrivate.ShowMainMenu)

                    themeMenuItem.click(fnPrivate.ShowThemeMenu)

                    subMenu.on("click", "button", fnPrivate.ShowSubMenu);

                    $(document).click(fnPrivate.OutSiteMenuClick);

                    $(document).on('show.bs.modal', '.modal', fnPrivate.AllowShowMultipleModals);

                    tabContents.on('shown.bs.tab', fnPrivate.RedrawDataTables);
                },
                InitButtonFloat: function (element, lstButton) {
                    $('.kc_fab_wrapper').kc_fab(element);
                    $('.kc_fab_wrapper').on('click', '.sub_fab_btn', function (e) {
                        $('.kc_fab_wrapper').find('.sub_fab_btns_wrapper').removeClass('show');
                        $(".kc_fab_overlay").remove();
                        for (var i in element) {
                            if (element[i].url == "#" && $(this).hasClass(element[i].id)) {
                                var scope = angular.element(document.getElementById(element[i].id)).scope();
                                eval('scope.' + element[i].func + '()');
                            }
                        }
                    })
                    if (global.Device == "mobile") {
                        $('#divButtonDefault').empty();
                    }
                    else {
                        $('#divButtonFloat').empty();
                    }
                },
                InitDateTimePickerChanged: function (lst, method) {
                    var length = lst.length;
                    for (var i = 0; i < length; i++) {
                        $(lst[i]).on('dp.change', method);
                    }
                },
                ScanBarCode: function (buttonscan, importfile, savedata, listdata) {
                    function uploadImgDisplay(curFile) {
                        // img URL
                        var fileURL = window.URL.createObjectURL(curFile);
                        // revoke object
                        $('#fileImg').onload = function () {
                            window.URL.revokeObjectURL(fileURL);
                        };
                        // display image
                        $('#dlgFirst').css('display', 'none');
                        $('#fileImg').attr('src', fileURL);
                        $('#dlgReading').css('display', 'block');
                        // upload image
                        var fd = new FormData();
                        fd.append('image', curFile);
                        fd.append('barcodeFormat', 503317503);
                        $.ajax({
                            type: "POST",
                            data: fd,
                            processData: false,
                            contentType: false,
                            url: urlReadbarcode,
                            success: function (resulst) {
                                if (resulst !== "error") {
                                    var exists = false;
                                    if (listdata !== null) {
                                        $(listdata).each(function () {
                                            //listdata.each(function () {
                                            if (this.value == resulst.trim()) {
                                                exists = true;
                                                return false;
                                            }
                                        });
                                        if (exists == true) {
                                            savedata.val(resulst.trim()).change();
                                        }
                                        else {
                                            Module.Alert.fn.Show('Không tồn tại mã này or bạn không có quyền để dùng mã này!', Module.Alert.Type.warning);
                                            //savedata.append(new Option(resulst.trim(), resulst.trim()));
                                        }
                                    }
                                    else {
                                        savedata.val(resulst.trim()).change();
                                    }
                                }
                                else {
                                    Module.Alert.fn.Show('Quét không thành công,Xin vui lòng thử lại!', Module.Alert.Type.warning);
                                }
                            },
                            error: function (response) {
                                Module.Alert.fn.Show('Không tìm thấy', Module.Alert.Type.warning);
                            }
                        });
                    }
                    buttonscan.click(function () {
                        importfile.click();
                    });

                    importfile.change(function () {
                        // is file choosed 
                        if (!this.files.length) {
                            return;
                        }
                        // is image
                        var file = this.files[0];
                        this.value = '';
                        switch (file.type) {
                            case 'image/bmp':
                            case 'image/jpeg':
                            case 'image/jpg':
                            case 'image/png':
                            case 'image/gif':
                                break;
                            default:
                                {
                                    alert('The uploaded file is not supported.');
                                    return;
                                }
                        }
                        uploadImgDisplay(file);
                    });
                }
            }
            return { fn }
        })(),
        /**
        * Initializing Session
        */
        Session: (function () {
            var sessLastActivity;
            //How frequently to check for session expiration in milliseconds
            var sessPollInterval = 1000;

            var request = 1000;
            //How many minutes the session is valid for
            var sessExpirationMinutes = 5;
            //How many minutes before the warning prompt
            var sessWarningMinutes = 4;
            var sessIntervalID;
            var idleButtonIntervalID;
            var SessSetInterval = function () {
                sessIntervalID = setInterval(SessInterval, sessPollInterval);
            }
            var SessKeyPressed = function (ed, e) {
                sessLastActivity = new Date();
            }
            var DetectIdleButton = function (duration) {
                var start = Date.now(),
                    diff,
                    minutes,
                    seconds;
                function timer() {
                    diff = duration - (((Date.now() - start) / request) | 0);
                    if (diff <= 0 && _IDLE == true) {
                        _IDLE = false;
                        $.removeCookie("_IDLE", '/')
                        $.cookie("_IDLE", _IDLE, {
                            expires: 0.2,
                            path: '/'
                        });
                        window.location.href = URLLogOut;
                    }
                };
                idleButtonIntervalID = setInterval(timer, request);
            }
            var SessInterval = function () {
                    var now = new Date();
                var diff = now - sessLastActivity;
                var diffMins = (diff / request / 60);
                TrackingUserOnline();
                if (diffMins >= sessWarningMinutes) {
                    clearInterval(sessIntervalID);
                    var msgAlert_Eng = 'Your session will expire in ' + Module.Convert.fn.Round((sessExpirationMinutes - sessWarningMinutes), 2) +
                        ' minutes (as of ' + Module.Convert.fn.DateTime(now, 'dd/MM/yyyy HH:mm') + '), press OK to remain logged in ' +
                        'or press Cancel to log off. \nIf you are logged off any changes will be lost.';
                    var msgAlert_Vie = 'Phiên làm việc sẽ hết hiệu lực khoảng ' + Module.Convert.fn.Round((sessExpirationMinutes - sessWarningMinutes), 2) +
                        ' phút (tính đến ' + Module.Convert.fn.DateTime(now, 'dd/MM/yyyy HH:mm') + '), nhấn OK để tạo phiên làm việc mới ' +
                        ' hoặc Cancel để thoát. \n 1 số thay đổi sẽ bị mất nếu bạn đăng xuất.';
                    DetectIdleButton(60)
                    Module.Alert.fn.ShowConfirm(global.TypeLanguage == 0 ? msgAlert_Vie : msgAlert_Eng, Module.Alert.Type.warning, 'Warning', function (result) {
                        if (result === true) {
                            _IDLE = false;
                            $.removeCookie("_IDLE", '/')
                            $.cookie("_IDLE", _IDLE, {
                                expires: 0.2,
                                path: '/'
                            });
                            now = new Date();
                            diff = now - sessLastActivity;
                            diffMins = (diff / request / 60);
                            if (diffMins > sessExpirationMinutes) {
                                _IDLE = false;
                                $.removeCookie("_IDLE", '/')
                                $.cookie("_IDLE", _IDLE, {
                                    expires: 0.2,
                                    path: '/'
                                });
                                window.location.href = URLLogOut;
                            }
                            else {
                                fn.InitSession();
                                sessLastActivity = new Date();
                            }
                            clearInterval(idleButtonIntervalID)
                        }
                        else {
                            _IDLE = false;
                            $.removeCookie("_IDLE", '/')
                            $.cookie("_IDLE", _IDLE, {
                                expires: 0.2,
                                path: '/'
                            });
                            window.location.href = URLLogOut;
                        }
                    });
                }
            }

            var TrackingUserOnline = function () {
                $.post(URLTrackingUserOnline, function (data) {
                    if (data > 0) {
                        $('#userOnline').text(data + '/10');
                    }
                    else {
                        _IDLE = false;
                        $.removeCookie("_IDLE", '/')
                        $.cookie("_IDLE", _IDLE, {
                            expires: 0.2,
                            path: '/'
                        });
                        window.location.href = URLLogOut;
                    }
                });
            }
            return {
                fn: {
                    Init: function () {
                        sessLastActivity = new Date();
                        SessSetInterval();
                        $(document).bind('keypress.session', function (ed, e) {
                            SessKeyPressed(ed, e);
                        });
                    },
                }
            }
        })(),
        /**
        * Make a Alert
        */
        Alert: (function () {
            var Type = {
                success: 'success',
                warning: 'warning',
                info: 'info',
                question: 'question',
                error: 'error',
            }
            var fn = {
                Show: function (message, type = Type.error, title = '') {
                    swal({
                        title: title,
                        text: message,
                        type: type
                    }).catch(swal.noop);
                },
                ShowConfirm: function (message, type = Type.error, title = '', callback) {
                    swal({
                        title: title,
                        text: message,
                        type: type,
                        showCancelButton: true,
                        confirmButtonColor: '#3085d6',
                        cancelButtonColor: '#d33',
                        confirmButtonText: 'OK',
                        allowOutsideClick: false
                    }).then(function () {
                        callback(true);
                    }, function (dismiss) {
                        callback(false);
                    }).catch(swal.noop);
                }
            }
            return { fn, Type }
        })(),
        /**
        * Control process
        */
        Loading: (function () {
            var fn = {
                /**
                 * Show processing in main page
                 */
                Show: function () {
                    $('#block').css('display', 'block');
                },
                /**
                 * Hide processing in main page
                 */
                Hide: function () {
                    $('#block').css('display', 'none');
                }
            }
            return { fn }
        })(),
        /**
         * Convert value type to another value type
         */
        Convert: (function () {
            var fn = {
                /**
                 * Rounding (0.3355, 2) => return 0.34
                 */
                Round: function (number, n) {
                    return parseFloat(Math.round(number * Math.pow(10, n)) / Math.pow(10, n)).toFixed(n)
                },
                /**
                 * Include 0 into time string (5) => return 05
                 */
                Time: function (num) {
                    return (num >= 0 && num < 10) ? ("0" + num) : (num + "");
                },
                /**
                * Convert timestamp to format
                * @returns {
                * time: HH:mm
                * vi-vn: dd/MM/yyyy
                * system: MM/dd/yyyy
                * }
                */
                DateTime: function (date, format) {
                    var today = new Date(date);
                    if (today == 'Invalid Date') {
                        date = date.split('/')[2] + '/' + date.split('/')[1] + '/' + date.split('/')[0]
                        today = new Date(date);
                    }
                    var dd = today.getDate();
                    var mm = today.getMonth() + 1;
                    var yyyy = today.getFullYear();
                    if (dd < 10) {
                        dd = '0' + dd;
                    }
                    if (mm < 10) {
                        mm = '0' + mm;
                    }
                    if (format == 'HH:mm') {
                        var hours = today.getHours();
                        var minutes = today.getMinutes();
                        minutes = minutes < 10 ? '0' + minutes : minutes;
                        var strTime = hours + ':' + minutes;
                        return strTime;
                    }
                    else if (format == 'dd/MM/yyyy') {
                        return dd + '/' + mm + '/' + yyyy;
                    }
                    else if (format == 'MM/dd/yyyy') {
                        return mm + '/' + dd + '/' + yyyy;
                    }
                    else if (format == 'yyyy/MM/dd') {
                        return mm + '/' + dd + '/' + yyyy;
                    }
                    else {
                        hours = today.getHours();
                        minutes = today.getMinutes();
                        minutes = minutes < 10 ? '0' + minutes : minutes;
                        strTime = hours + ':' + minutes;
                        return dd + '/' + mm + '/' + yyyy + ' ' + strTime;
                    }
                },
                /**
                * Remove diacritics (accents) from a string
                * @param str: A á ấ ă ằ Ặ
                * @return => a a a a a a
                */
                RemoveVietnameseRemark: function (str) {
                    str = str.toLowerCase();
                    str = str.replace(/à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ/g, "a");
                    str = str.replace(/è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ/g, "e");
                    str = str.replace(/ì|í|ị|ỉ|ĩ/g, "i");
                    str = str.replace(/ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ/g, "o");
                    str = str.replace(/ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ/g, "u");
                    str = str.replace(/ỳ|ý|ỵ|ỷ|ỹ/g, "y");
                    str = str.replace(/đ/g, "d");
                    return str;
                }
            }
            return { fn }
        })(),
        /**
        * Initializing TreeView
        */
        BuildTreeView: (function () {
            var arrNodes = new Array()
            var enterCount = 0;
            /**
            * Make a TreeView after build data
            */
            var BindingDataToTreeView = function (rootNode, source) {
                var length = source.length;
                for (var i = 0; i < length; i++) {
                    if (typeof source[i].items !== 'undefined') {
                        rootNode.append('<li class="node" data-id="' + source[i].id + '"><span class="leaf">' + source[i].label + '</span><span class="node-toggle"></span></li>')
                    }
                    else {
                        rootNode.append('<li class="node" data-id="' + source[i].id + '"><span class="leaf">' + source[i].label + '</span></li>')
                    }
                    if (typeof source[i].items !== 'undefined') {
                        $(rootNode).find('li[data-id="' + source[i].id + '"]').append('<ul></ul>')
                        BindingDataToTreeView($(rootNode).find('li[data-id="' + source[i].id + '"] ul'), source[i].items)
                    }
                }
            }
            /**
            * Searching nodes in treeview
            */
            var SearchingNodesInTreeview = function (rootNode, input) {
                $(input).on('keydown', function (e) {
                    if (e.keyCode == 13) {
                        if (arrNodes.length == 0) {
                            var inputText = $(this).val()
                            var i = 0;
                            $(rootNode).find('span[class=leaf]').each(function (index, el) {
                                if (Module.Convert.fn.RemoveVietnameseRemark($(el).text()).indexOf(Module.Convert.fn.RemoveVietnameseRemark(inputText)) >= 0) {
                                    arrNodes[i] = new Object();
                                    arrNodes[i].node = $(el).get(0)
                                    i++
                                }
                            })
                        }
                        if (arrNodes.length == 0) {
                            Module.Alert.fn.Show('Không tìm thấy', Module.Alert.Type.warning)
                            return false;
                        }
                        //active node
                        $(arrNodes[enterCount].node).click()

                        //scroll to position node
                        var container = $(rootNode).parent()
                        var scrollTo = $(arrNodes[enterCount].node).closest('li')
                        container.animate({
                            scrollTop: scrollTo.offset().top - container.offset().top + container.scrollTop()
                        }, 500);

                        enterCount++;
                        if (enterCount == arrNodes.length) {
                            enterCount = 0;
                            return false;
                        }
                    }
                    else {
                        arrNodes = new Array()
                        enterCount = 0;
                    }
                });
            }
            var fn = {
                /**
                * Build data for treeview
                *
                * Structs for JSON data - return { id , parentid, text }
                */
                BuildDataSourceForTreeView: function (rootNode, data) {
                    var source = [];
                    var items = [];
                    var j = 0;
                    var length = data.length;
                    for (i = 0; i < length; i++) {
                        var item = data[i];
                        var label = item["text"];
                        var parentid = item["parentid"];
                        var id = item["id"];
                        if (items[parentid]) {
                            var item = { id: id, parentid: parentid, label: label, item: item };
                            if (!items[parentid].items) {
                                items[parentid].items = [];
                            }
                            items[parentid].items[items[parentid].items.length] = item;
                            items[id] = item;
                        }
                        else {
                            items[id] = { id: id, parentid: parentid, label: label, item: item };
                            source[j] = items[id];
                            j++;
                        }
                    }
                    //Create input search nodes
                    var parents = $(rootNode).parent().parent()
                    if (parents.hasClass('form-group')) {
                        var inputSearch = $(rootNode).closest('div').attr('id') + '_Search'
                        var element = document.getElementById(inputSearch);
                        if (typeof (element) == 'undefined' || element == null) {
                            parents.append('<input class="form-control" type="text" id="' + inputSearch + '" placeholder="' + (global.TypeLanguage === 0 ? "Tìm" : "Search") + '">')
                            SearchingNodesInTreeview($(rootNode), $('#' + inputSearch))
                        }
                    }
                    BindingDataToTreeView(rootNode, source);
                }
            }
            return { fn }
        })(),
        /**
        * Initializing Theme
        */
        Theme: (function () {
            var fn = {
                /**
                * Change theme from menu
                * @param style: default - dark - light
                */
                ManualChangeTheme: function (style) {
                    $('a[data-style]').removeClass('active');
                    if (style === 'default') {
                        $('link[data-theme=theme]').attr("href", '');
                        $('a[data-style=default]').addClass('active');
                        global.ThemeStyle = 'default'
                    }
                    else if (style === 'dark') {
                        $('link[data-theme=theme]').attr("href", urlThemeDark);
                        $('a[data-style=dark]').addClass('active');
                        global.ThemeStyle = 'dark'
                    }
                    else if (style === 'light') {
                        $('link[data-theme=theme]').attr("href", urlThemeLight);
                        $('a[data-style=light]').addClass('active');
                        global.ThemeStyle = 'light'
                    }

                    $.post(URLChangeSetting, { themeStyle: global.ThemeStyle, typeLang: global.TypeLanguage }, function (data) {
                    });
                    $.removeCookie("ckThemeStyle", '/')
                    $.cookie("ckThemeStyle", global.ThemeStyle, {
                        expires: 365,
                        path: '/'
                    });
                    //setting.removeClass('shown')
                    mainMenu.removeClass('hidden')
                    subMenu.addClass('hidden');
                    subMenu.empty();
                }
            }
            return { fn }
        })(),
        /**
        * Initializing Languages
        */
        Languages: (function () {
            /**
            * Apply language for page
            * @param obj: array
            */
            var FillLanguagesIntoPage = function (obj) {
                $.post(URLLanguages, { data: obj.data, form: obj.form, type: obj.type }, function (data) {
                    if (data.length > 0) {
                        var length = data.length;
                        for (var i = 0; i < length; i++) {
                            if (data[i].Keyword.indexOf('msg') != -1) {
                                Messenger[data[i].Keyword] = data[i].Text;
                            }
                            else {
                                if ($("[data-lang='" + data[i].Keyword + "']").find('i').length > 0) {
                                    if ($("[data-lang='" + data[i].Keyword + "']").parent().hasClass('toolbar-action')) { //set tooltip for button action
                                        $("[data-lang='" + data[i].Keyword + "']").attr('title', data[i].Text);
                                    }
                                    else {
                                        var iTag = $("[data-lang='" + data[i].Keyword + "']").find('i');
                                        $("[data-lang='" + data[i].Keyword + "']").text('');
                                        if (iTag.length > 1) {
                                            $("[data-lang='" + data[i].Keyword + "']").append(iTag[0]).append(' ' + data[i].Text).append(iTag[1]);
                                        }
                                        else {
                                            $("[data-lang='" + data[i].Keyword + "']").append(iTag).append(' ' + data[i].Text);
                                        }
                                    }
                                }
                                else {
                                    if ($("input[data-lang='" + data[i].Keyword + "']").is("[placeholder]")) {
                                        $("input[data-lang='" + data[i].Keyword + "']").attr("placeholder", data[i].Text);
                                    }
                                    else {
                                        $("[data-lang='" + data[i].Keyword + "']").text(data[i].Text);
                                        $("[data-lang='" + data[i].Keyword + "']:not(option)").val(data[i].Text);
                                    }
                                }
                            }
                        }
                    }
                    if (global.TypeLanguage == 0) {
                        $('button .flag-icon-vn').parent().addClass('active');
                        $('button .flag-icon-us').parent().removeClass('active');
                        $('.footer .flag-icon-us').addClass('hidden');
                        $('.footer .flag-icon-vn').removeClass('hidden');
                    }
                    else {
                        $('button .flag-icon-us').parent().addClass('active');
                        $('button .flag-icon-vn').parent().removeClass('active');
                        $('.footer .flag-icon-us').removeClass('hidden');
                        $('.footer .flag-icon-vn').addClass('hidden');
                    }
                    // setting.removeClass('shown')
                    mainMenu.removeClass('hidden')
                    subMenu.addClass('hidden');
                    subMenu.empty();
                    $("#page-wrapper").removeClass("notshow");
                    $("#block2").addClass("hidden");
                    Module.Loading.fn.Hide();
                });
            }
            var fn = {
                /**
                * Change language from menu
                * @param type: 0 - vn | 1 - us
                */
                ManualChangeLanguage: function (type) {
                    global.TypeLanguage = type;
                    Module.Loading.fn.Show();
                    fn.AutoChangeLanguage();
                    $.post(URLChangeSetting, { themeStyle: global.ThemeStyle, typeLang: global.TypeLanguage }, function (data) {
                    });
                    $.removeCookie("ckTypeLanguage", '/')
                    $.cookie("ckTypeLanguage", type, {
                        expires: 365,
                        path: '/'
                    });
                },
                /**
                * Auto change language after postback
                */
                AutoChangeLanguage: function () {
                    if (global.CurrentNamePage == '') {
                        Module.Loading.fn.Hide();
                        return false;
                    }
                    var lstKeyword = new Array();
                    $('[data-lang]').each(function (index, element) {
                        lstKeyword[index] = new Object();
                        lstKeyword[index].Keyword = $(element).attr('data-lang');
                        lstKeyword[index].Text = "";
                    });
                    var obj = {};
                    obj.data = JSON.stringify(lstKeyword);
                    obj.form = global.CurrentNamePage;
                    obj.type = global.TypeLanguage;
                    window.setTimeout(function () { FillLanguagesIntoPage(obj); }, 1500);
                },
            }
            return { fn }
        })(),
    }
    return Module;
});