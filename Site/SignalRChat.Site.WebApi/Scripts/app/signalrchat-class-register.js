(function () {
    SignalRChat.Class.Register = function () {
        var parent = new SignalRChat.Class.Base();
        var _this = SignalRChat.Util.extendObject(parent);

        // JSON Properties
        _this.EmailInputId = "";
        _this.UsernameInputId = "";
        _this.PasswordInputId = "";
        _this.SubmitButtonId = "";
        // End JSON Properties

        var cookieManager = null;

        var emailInput = null;
        var usernameInput = null;
        var passwordInput = null;
        var submitButton = null;

        _this.init = function (json) {
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

        _this.addEvents = function () {
            submitButton.on('click', onRegisterClick);
        };

        var onRegisterClick = function () {
            var registerRequest = {
                Email: emailInput.val(),
                Username: usernameInput.val(),
                Password: passwordInput.val()
            };
            $.ajax({
                type: 'POST',
                url: 'http://localhost:50276/api/loginManager/register',
                data: registerRequest,
                success: onRegisterSuccess,
                error: onRegisterError,
                dataType: 'json'
            });
        };

        var onRegisterSuccess = function (response) {
            cookieManager.set('authorizationToken', response.Token, response.TokenExpiration);
            cookieManager.set('loggedUser', response.User);

            var uriToRedirect = response.UriToRedirect || '/';
            window.location = uriToRedirect;
        };

        var onRegisterError = function () {
            console.log("Register error");
        };

        return _this;
    };
})();