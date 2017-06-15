(function () {
    SignalRChat.Class.Menu = function() {
        var parent = new SignalRChat.Class.Base();
        var _this = SignalRChat.Util.extendObject(parent);

        // JSON Properties
        _this.LogoutButtonId = "";
        // End JSON Properties

        var cookieManager = null;

        var logoutButton = null;

        _this.init = function(json) {
            _this.setSettingsByObject(json);
            _this.setVars();
            _this.addEvents();
        };

        _this.setVars = function () {
            cookieManager = SignalRChat.Class.Singleton.CookieManager.getInstance();

            logoutButton = SignalRChat.Util.getJQueryObjectById(_this.LogoutButtonId);
        };

        _this.addEvents = function() {
            logoutButton.on('click', onLogoutClick);
        };

        var onLogoutClick = function () {
            cookieManager.remove('authorizationToken');
            cookieManager.remove('loggedUser');
            location.reload();
        };

        return _this;
    };
})();