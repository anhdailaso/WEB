define(["linq"], function () {
    var menuActive = ''
    return {
        fn: {
            Init: function () {
                $('#navbar-menu').on("click", "a", function () {
                    menuActive = $(this).parent().parent().find('li.active > a').attr('id')
                    $(this).next().toggleClass('in');
                    if ($(this).next().hasClass('in')) {
                        $(this).find('i.fa-caret-down').css('transform', 'rotate(360deg)')
                    } else {
                        $(this).find('i.fa-caret-down').css('transform', 'rotate(270deg)')
                    }
                });
            },
            GetChildMenu: function (main, ID, menuID) {
                $.post(urlGetChildMenu, { ID: ID }, function (data) {
                    $(menuID).empty();
                    for (var i = 0; i < data.length; i++) {
                        if (data[i].MetaTitle === '#') {
                            $(menuID).append(
                                '<li><a data-lang="' + data[i].MenuID + '" href="javascript:;" data-toggle="collapsed" data-target="#' + data[i].MenuID + '" onclick="MainMenu.fn.GetChildMenu(this,' + data[i].ID + ', ' + data[i].MenuID + ')"><i class="' + data[i].Icons + '"></i> ' + (global.TypeLanguage == 0 ? data[i].Name : data[i].NameEng) + '<i class="fa fa-fw fa-caret-down" style="float: right;margin-top: 3px;"></i></a><ul id="' + data[i].MenuID + '" class="collapsed"></ul></li>'
                            )
                        }
                        else {
                            $(menuID).append(
                                "<li><a  data-lang='" + data[i].MenuID + "'  id='" + data[i].MenuID + "' href='" + data[i].MetaTitle + "'><i class='" + data[i].Icons + "'></i> " + (global.TypeLanguage == 0 ? data[i].Name : data[i].NameEng) + "</a></li>"
                            )
                        }
                    }
                    $('#' + menuActive).parent().addClass('active')
                });
            },
            SetActive: function (menuID) {
                if ($('#' + menuID).length > 0) {
                    $('#' + menuID).parent().addClass('active')
                }
                else {
                    $.post(urlAllChildMenu, { menuID: menuID }, function (data) {
                        var mnuRoot = Enumerable.From(data).Where(function (x) { return x.Root === 0 }).SingleOrDefault();
                        for (var i = 0; i < data.length; i++) {
                            if (data[i].Root === 0) continue;
                            if (data[i].MetaTitle === '#') {
                                $('#' + mnuRoot).append(
                                    '<li><a href="javascript:;" data-lang="' + data[i].MenuID + '" data-toggle="collapsed" data-target="#' + data[i].MenuID + '" id="' + data[i].MenuID + '" onclick="MainMenu.fn.GetChildMenu(this,' + data[i].ID + ', ' + data[i].MenuID + ')"><i class="' + data[i].Icons + '"></i> ' + (global.TypeLanguage == 0 ? data[i].Name : data[i].NameEng) + '<i class="fa fa-fw fa-caret-down" style="float: right;margin-top: 3px;"></i></a><ul id="' + data[i].MenuID + '" class="collapsed"></ul></li>'
                                )
                            }
                            else {
                                $('#' + mnuRoot).append(
                                    "<li><a id='" + data[i].MenuID + "' href='" + data[i].MetaTitle + "' data-lang='" + data[i].MenuID + "' ><i class='" + data[i].Icons + "'></i> " + (global.TypeLanguage == 0 ? data[i].Name : data[i].NameEng) + "</a></li>"
                                )
                            }
                        }
                        $('#' + menuID).parent().addClass('active')
                        $('#' + mnuRoot).toggleClass('in');
                    });
                }
            }
        }
    }
});