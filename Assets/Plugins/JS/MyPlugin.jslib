var MyPlugin = {

    showAds: function()
    {
        MyUnityTransformer.showAds();
    },
    VibrateJS: function(ms)
    {
        MyUnityTransformer.haptic(ms);
    },
    hapticFeedback: function(type) {
        Telegram.WebApp.HapticFeedback.impactOccurred(Pointer_stringify(type));
    },
    Swipe: function(x0, y0, x1, y1, t0, t1)
    {
        MyUnityTransformer.swipe();
    },
    Press: function(x0, y0, t0)
    {
        MyUnityTransformer.press();
    },
    GetPlatform: function()
    {
        var str = Telegram.WebApp.platform;

        var buffer = _malloc(lengthBytesUTF8(str) + 1);
        writeStringToMemory(str, buffer);
        return buffer;
    },

    upgradeCard: function(str)
    {
        MyUnityTransformer.upgradeCard(Pointer_stringify(str));
    },
    _CopyToClipboard: function(str)
    {
        MyUnityTransformer.copyToClipboard(Pointer_stringify(str));
    },
    SendToFriend: function(str)
    {
        MyUnityTransformer.SendToFriend(Pointer_stringify(str));
    },
    _OpenURL: function(str)
    {
        MyUnityTransformer.openURL(Pointer_stringify(str));
    },
    _GetProfile: function()
    {
        console.log("_GetProfile | Unity Transformer")
        return MyUnityTransformer.getProfile();
    },
    _GetBalance: function()
    {
        return MyUnityTransformer.getBalance();
    },
    _GetRating: function()
    {
        return MyUnityTransformer.getRating();
    },
    _GetEnergy: function()
    {
        return MyUnityTransformer.getEnergy();
    },
    GetUsername: function()
    {

        //var str = Telegram.WebApp.initDataUnsafe.user.username;

        //var buffer = _malloc(lengthBytesUTF8(str) + 1);
        //writeStringToMemory(str, buffer);
        //return buffer;

        console.log("MyUnityTransformer | GetUsername")

        return MyUnityTransformer.getUsername();
    },
    _GetUsername: function()
    {
        //var str = Telegram.WebApp.initDataUnsafe.user.username;

        //var buffer = _malloc(lengthBytesUTF8(str) + 1);
        //writeStringToMemory(str, buffer);
        //return buffer;

        console.log("MyUnityTransformer | _GetUsername")

        return MyUnityTransformer.getUsername();
    },
    _GetPlatform: function()
    {
        var str = Telegram.WebApp.platform;

        var buffer = _malloc(lengthBytesUTF8(str) + 1);
        writeStringToMemory(str, buffer);
        return buffer;
    },
    _HandleClick: function(count)
    {
        MyUnityTransformer._HandleClick(count);
    },
    _HandleSwipe: function(x0, y0, x1, y1, t0, t1)
    {
        MyUnityTransformer.swipe(x0, y0, x1, y1, t0, t1);
    },
    _HandlePress: function(x0, y0, t0)
    {
        MyUnityTransformer.press(x0, y0, t0);
    },
	_Merge: function(str)
    {
		console.log("_Merge data1="+str);
		console.log("_Merge data2="+Pointer_stringify(str));
		let jsonObject = JSON.parse(Pointer_stringify(str));
		let cardIds = jsonObject.card_ids;
		console.log("_Merge cardIds="+cardIds);
		let cardIdsString = "[" + cardIds.toString() + "]";
		console.log("_Merge cardIdsString="+cardIdsString);
		MyUnityTransformer.Merge(cardIdsString);
    },
	_Probability: function(str)
    {
		console.log("_Probability data1="+str);
		console.log("_Probability data2="+Pointer_stringify(str));
		let jsonObject = JSON.parse(Pointer_stringify(str));
		let cardIds = jsonObject.card_ids;
		console.log("_Probability cardIds="+cardIds);
		let cardIdsString = "[" + cardIds.toString() + "]";
		console.log("_Probability cardIdsString="+cardIdsString);
        MyUnityTransformer.Probability(cardIdsString);
    },
    CheckSubscribeOnGroup: function(type) {
        MyUnityTransformer.CheckSubscribeOnGroup(Pointer_stringify(type));
    },
    CollectCoinsFromFriends: function()
    {
        MyUnityTransformer.CollectCoinsFromFriends();
    },
    CollectCoinsAutoMining: function()
    {
        MyUnityTransformer.CollectCoinsAutoMining();
    },
	SelectCard: function(card_id)
    {
		console.log("MyPlugin SelectCard card_id="+card_id);
        MyUnityTransformer.SelectCard(card_id);
    },
	OpenBox: function(box_id)
    {
		console.log("MyPlugin OpenBox box_id="+box_id);
        MyUnityTransformer.OpenBox(box_id);
    },
	_BoostUpgradeCard: function(num1,num2)
    {
		console.log("MyPlugin _BoostUpgradeCard num1="+num1);
        MyUnityTransformer.BoostUpgradeCard(num1,num2);
    },
    JoinToCommunity: function(community_id)
    {
        MyUnityTransformer.JoinToCommunity(community_id);
    },
    LeaveCommunity: function(community_id)
    {
        MyUnityTransformer.LeaveCommunity(community_id);
    },
	GetConfig: function()
    {
        MyUnityTransformer.GetConfig();
    },
    CreateCommunity: function()
    {
        MyUnityTransformer.CreateCommunity();
    },
    updatePage: function ()
    {
        MyUnityTransformer.updatePage();
    },
    connectWallet : function ()
    {
        MyUnityTransformer.connectWallet();
    },
    disconnectWallet : function ()
    {
        MyUnityTransformer.disconnectWallet();
    },
    checkRestoreConnection : function ()
    {
        MyUnityTransformer.checkRestoreConnection();
    }
};

mergeInto(LibraryManager.library, MyPlugin);