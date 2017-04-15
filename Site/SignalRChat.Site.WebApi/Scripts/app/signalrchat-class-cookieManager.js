(function() {
    SignalRChat.Class.Singleton.CookieManager = function () {
        var _this = this;

        _this.get = function(key) {
            var result = Cookies.get(key);
            return result;
        };

        _this.getJson = function(key) {
            var result = Cookies.getJSON(key);
            return result;
        };

        _this.set = function (key, value, expiration) {
            expiration = expiration || 7;
            Cookies.set(key, value, { expires: expiration });
        };

        _this.remove = function(key) {
            Cookies.remove(key);
        };

        return _this;
    };
    SignalRChat.Class.Singleton.CookieManager.instance = null;
    SignalRChat.Class.Singleton.CookieManager.getInstance = function() {
        if (SignalRChat.Class.Singleton.CookieManager.instance === null) {
            SignalRChat.Class.Singleton.CookieManager.instance = new SignalRChat.Class.Singleton.CookieManager();
        }
        return SignalRChat.Class.Singleton.CookieManager.instance;
    };
})();