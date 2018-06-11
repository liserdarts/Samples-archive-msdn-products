window.Wingtip = window.Wingtip || {};

Wingtip.ChromeControl = function () {

    render = function () {
        var options = {
            "appIconUrl": "../Images/AppIcon.png",
            "appTitle": "System Alerts",
        };

        var nav = new SP.UI.Controls.Navigation(
                                "chrome_ctrl_placeholder",
                                options
                          );
        nav.setVisible(true);
    };

    return {
        render: render
    }
}();

$(document).ready(function () {
    Wingtip.ChromeControl.render();
});