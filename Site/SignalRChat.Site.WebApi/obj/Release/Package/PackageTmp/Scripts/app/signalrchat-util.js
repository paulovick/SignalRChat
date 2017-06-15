(function() {
    SignalRChat.Util.extendObject = function (o) {
        var f = function () { };
        f.prototype = o;
        return new f();
    };
    SignalRChat.Util.hasOwnProperty = function (obj, prop) {
        var proto = obj.__proto__ || obj.constructor.prototype;
        var result = (prop in obj) && (!(prop in proto) || proto[prop] !== obj[prop]);
        return result;
    };
    SignalRChat.Util.getJQueryObjectById = function (id) {
        var selector = "#" + id;
        var result = $(selector);
        return result;
    };
})();