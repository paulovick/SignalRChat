(function () {
    SignalRChat.Class.ChatHub = function() {
        var parent = new SignalRChat.Class.Base();
        var _this = SignalRChat.Util.extendObject(parent);

        // JSON Properties
        _this.DiscussionContainerId = "";
        _this.InputContainerId = "";
        _this.SendButtonId = "";
        _this.Sender = {};
        _this.Receiver = {};
        _this.Chat = {};
        _this.Messages = [];
        // End JSON Properties

        _this.Hub = null;
        _this.DiscussionContainer = null;
        _this.InputContainer = null;
        _this.SendButton = null;

        _this.init = function(json) {
            _this.setSettingsByObject(json);
            _this.setVars();
        };

        _this.setVars = function() {
            _this.Hub = $.connection.chatHub;
            _this.DiscussionContainer = SignalRChat.Util.getJQueryObjectById(_this.DiscussionContainerId);
            _this.InputContainer = SignalRChat.Util.getJQueryObjectById(_this.InputContainerId);
            _this.SendButton = SignalRChat.Util.getJQueryObjectById(_this.SendButtonId);

            _this.Hub.client.addChatMessage = addChatMessage;

            $.connection.hub.start().done(initHub);
        };

        var initHub = function () {
            _this.Hub.server.initGroup(_this.Chat.Id);
            initSendButton();
        };

        var renderMessage = function (message) {
            var sender = getSender(message);
            _this.DiscussionContainer.append('<li><strong>' +
                htmlEncode(sender.Username) +
                '</strong>: ' +
                htmlEncode(message.Message) +
                '</li>');
        };

        var getSender = function(message) {
            if (message.SenderUserId === _this.Sender.Id) {
                return _this.Sender;
            }
            if (message.SenderUserId === _this.Receiver.Id) {
                return _this.Receiver;
            }

            return null;
        };

        var addChatMessage = function (message) {
            renderMessage(message);
        };

        var initSendButton = function() {
            _this.SendButton.click(handleSendClick);
            _this.InputContainer.keypress(function(e) {
                var key = e.which;
                var enterKeyValue = 13;
                if (key === enterKeyValue) {
                    _this.SendButton.click();
                    return false;
                }
            });

            enableSend();
        };

        var handleSendClick = function() {
            var messageContent = _this.InputContainer.val();
            var message = {
                ChatId: _this.Chat.Id,
                SenderUserId: _this.Sender.Id,
                SentDate: Date.now(),
                Message: messageContent
        };

            _this.Hub.server.sendMessage(_this.Chat.Id, _this.Sender.Id, messageContent);
            renderMessage(message);
            _this.InputContainer.val('');
            _this.InputContainer.focus();
        };

        var enableSend = function() {
            _this.SendButton.prop('disabled', false);
        };

        var htmlEncode = function(value) {
            var encodedValue = $('<div />').text(value).html();
            return encodedValue;
        };

        return _this;
    };
})();