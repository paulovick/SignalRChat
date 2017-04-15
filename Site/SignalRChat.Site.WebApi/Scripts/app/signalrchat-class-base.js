(function() {
    SignalRChat.Class.Base = function () {
        var thisBase = this;

        thisBase.init = SignalRChat.Class.Base.prototype.init;
        thisBase.setSettingsByObject = SignalRChat.Class.Base.prototype.setSettingsByObject;

        return thisBase;
    };

    SignalRChat.Class.Base.prototype.init = function (json) {
        this.setSettingsByObject(json);
    };

    SignalRChat.Class.Base.prototype.setSettingsByObject = function (json) {
        for (var propName in json) {
            if (json.hasOwnProperty(propName)) {
                if (this[propName] !== undefined) {
                    this[propName] = json[propName];
                }
            }
        }
    };
})();