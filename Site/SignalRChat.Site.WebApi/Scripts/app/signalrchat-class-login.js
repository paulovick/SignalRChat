(function () {
    SignalRChat.Class.Login = function() {
        var parent = new SignalRChat.Class.Base();
        var _this = SignalRChat.Util.extendObject(parent);

        // JSON Properties
        _this.UsernameInputId = "";
        _this.PasswordInputId = "";
        _this.SubmitButtonId = "";
        // End JSON Properties

        var cookieManager = null;

        var usernameInput = null;
        var passwordInput = null;
        var submitButton = null;

        _this.init = function(json) {
            _this.setSettingsByObject(json);
            _this.setVars();
            _this.addEvents();
        };

        _this.setVars = function () {
            cookieManager = SignalRChat.Class.Singleton.CookieManager.getInstance();

            usernameInput = SignalRChat.Util.getJQueryObjectById(_this.UsernameInputId);
            passwordInput = SignalRChat.Util.getJQueryObjectById(_this.PasswordInputId);
            submitButton = SignalRChat.Util.getJQueryObjectById(_this.SubmitButtonId);
        };

        _this.addEvents = function() {
            submitButton.on('click', onLoginClick);
        };

        var onLoginClick = function() {
            var loginRequest = {
                Username: usernameInput.val(),
                Password: passwordInput.val()
            };
            $.ajax({
                type: 'POST',
                url: 'http://localhost:50276/api/loginManager/login',
                data: loginRequest,
                success: onLoginSuccess,
                error: onLoginError,
                dataType: 'json'
            });
        };

        var onLoginSuccess = function (response) {
            cookieManager.set('authorizationToken', response.Token, response.TokenExpiration);
            cookieManager.set('loggedUser', response.User);

            var uriToRedirect = response.UriToRedirect || '/';
            window.location = uriToRedirect;
        };

        var onLoginError = function() {
            console.log("Login error");
        };

        return _this;
    };
})();