(function () {
    var DXDemo = {};
    DXDemo.CurrentBaseColorCookieKey = "DXCurrentBaseColor";
    DXDemo.CurrentFontCookieKey = "DXCurrentFont";

    DXDemo.SetCurrentFont = function (font) {
        ColorSelectorPopup.Hide();
        ASPxClientUtils.SetCookie(DXDemo.CurrentFontCookieKey, font);
        forceReloadPage();
    }
    DXDemo.SetCurrentBaseColor = function (color) {
        ColorSelectorPopup.Hide();
        ASPxClientUtils.SetCookie(DXDemo.CurrentBaseColorCookieKey, color);
        forceReloadPage();
    }
    forceReloadPage = function () {
        if (document.forms[0] && (!document.forms[0].onsubmit)) {
            // for export purposes
            var eventTarget = document.getElementById("__EVENTTARGET");
            if (eventTarget)
                eventTarget.value = "";
            var eventArgument = document.getElementById("__EVENTARGUMENT");
            if (eventArgument)
                eventArgument.value = "";

            document.forms[0].submit();
        } else {
            window.location.reload();
        }
    }
    window.DXDemo = DXDemo;
})();