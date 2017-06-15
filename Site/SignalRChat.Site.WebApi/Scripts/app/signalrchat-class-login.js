(function () {
    SignalRChat.Class.Login = function() {
        var parent = new SignalRChat.Class.Base();
        var _this = SignalRChat.Util.extendObject(parent);

        // JSON Properties
        _this.UsernameInputId = "";
        _this.PasswordInputId = "";
        _this.SubmitButtonId = "";
        _this.ErrorMessageId = "";
        _this.SiteBaseUrl = "";
        // End JSON Properties

        var cookieManager = null;

        var usernameInput = null;
        var passwordInput = null;
        var submitButton = null;
        var errorMessageContainer = null;

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
            errorMessageContainer = SignalRChat.Util.getJQueryObjectById(_this.ErrorMessageId);
        };

        _this.addEvents = function() {
            submitButton.on('click', onLoginClick);
        };

        var onLoginClick = function() {
            var loginRequest = {
                Username: usernameInput.val(),
                Password: passwordInput.val()
            };
            var loginEndpoint = _this.SiteBaseUrl + "api/loginManager/login";
            $.ajax({
                type: 'POST',
                url: loginEndpoint,
                data: loginRequest,
                success: onLoginSuccess,
                error: onLoginError,
                dataType: 'json'
            });

            submitButton.html('Logging... ');
            submitButton.append($('<i class="fa fa-spinner fa-pulse fa-fw"></i>'));
        };

        var onLoginSuccess = function (response) {
            cookieManager.set('authorizationToken', response.Token, response.TokenExpiration);
            cookieManager.set('loggedUser', response.User);

            var uriToRedirect = response.UriToRedirect || '/';
            window.location = uriToRedirect;
        };

        var onLoginError = function() {
            submitButton.html("Sign in");
            errorMessageContainer.toggleClass("signalr-hide");
            setTimeout(function() {
                errorMessageContainer.toggleClass("signalr-hide");
            }, 3000);
        };

        return _this;
    };
})();