
define([], function () {
    var themeStyle = $.cookie("ckThemeStyle");
    if (themeStyle != "" && typeof themeStyle !== 'undefined') {
        global.ThemeStyle = themeStyle
    } else {
        global.ThemeStyle = '@SessionVariable.ThemeName'
        $.cookie("ckThemeStyle", 'default', {
            expires: 365,
            path: '/'
        });
    }
    var typeLanguage = $.cookie("ckTypeLanguage");
    if (typeLanguage != "" && typeof typeLanguage !== 'undefined') {
        global.TypeLanguage = typeLanguage
    } else {
        global.TypeLanguage = '@SessionVariable.TypeLanguage'
        $.cookie("ckTypeLanguage", '0', {
            expires: 365,
            path: '/'
        });
    }
    _IDLE = $.cookie("_IDLE");
    if (!(_IDLE !== "" && typeof _IDLE !== 'undefined')) {
        _IDLE = false
        $.cookie("_IDLE", _IDLE, {
            expires: 0.2,
            path: '/'
        });
    }
    else {
        if (_IDLE == "true") {
            _IDLE = false;
            $.removeCookie("_IDLE", '/')
            $.cookie("_IDLE", _IDLE, {
                expires: 0.2,
                path: '/'
            });
            window.location.href = URLLogOut;
        }
    }

    if (/Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent)) {
        global.Device = 'mobile';
    }
    else {
        global.Device = 'desktop';
    }
    $.post(URLChangeSetting, { themeStyle: global.ThemeStyle, typeLang: global.TypeLanguage, device: global.Device }, function (data) {
    });
});