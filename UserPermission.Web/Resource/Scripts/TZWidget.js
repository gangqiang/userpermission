Tw = window.Tw || {};
Tw.Win = {
    Open: function (config) {
        $(config.oid).dialog(config);
    },
    OpenWithParam: function (config) {
        var dlg = new $.ui.dialog(config);
        dlg.ShowDialog();
    },
    OpenChildDialog: function (config) {
        var dg = frameElement.dg;
        var dialog = dg.win.$(config.oid, document).dialog(config);
        dialog.opt.regDragWindow.push(window);
    },
    Close: function () {
        var dg = frameElement.dg;
        dg.reload();
    },
    CloseAndRedirect: function (reurl) {
        var dg = frameElement.dg;
        dg.reload("", reurl);
    },
    resize: function (config) {

    }
}

Tw.Prompt = {
    Alert: function (msg) {
        $.prompt("<span style=\"color:red;font-size:14px;\">" + msg + "</span>", { buttons: [{ title: '确定', value: true}] });
    },
    Confirm: function (msg, callback) {
        $.prompt("<span style=\"color:red;font-size:14px;\">" + msg + "</span>", { buttons: [{ title: '确定', value: true }, { title: '取消', value: false}], focus: 0, submit: function (v, m, f) {
            if (v) { callback(); }
        }
        })
    },
    Success: function (msg) {
        $.prompt("<span style=\"color:green;font-size:14px;\">" + msg + "</span>", { buttons: [{ title: '确定', value: true}] });
    }
}

Tw.Menu = {
    Show: function (hander, config) {

        var hander = $(hander);
        var p = hander.position();
        $("#" + config.oid).css({ top: p.top + hander.height() - $(window).scrollTop(), left: p.left - $("#" + config.oid).width() + hander.width() })
    }
}