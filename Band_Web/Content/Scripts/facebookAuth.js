window.fbAsyncInit = function () {
    FB.init({
        appId: '748282695748570',
        cookie: true,
        xfbml: true,
        version: 'v8.0'
    });

    FB.AppEvents.logPageView();
};

(function (d, s, id) {
    var js, fjs = d.getElementsByTagName(s)[0];
    if (d.getElementById(id)) { return; }
    js = d.createElement(s); js.id = id;
    js.src = "https://connect.facebook.net/en_US/sdk.js";
    fjs.parentNode.insertBefore(js, fjs);
}(document, 'script', 'facebook-jssdk'));

window.onload = function () {
    FB.getLoginStatus(function (response) {
        statusChangeCallback(response);
    });
}
function statusChangeCallback(response) {
    console.log(response);
    if (response.status === 'connected') {
        console.log('Welcome!  Fetching your information.... ');
        FB.api('/me', function (response) {
            console.log('Successful login for: ' + response.name);
        });
    }
    else {
        console.log("Woops...GG")
    }
}

function fbLogin() {
    FB.login(function (response) {
        if (response.session) {
            if (response.scope) {
                //使用者已登入Facebook成功，且已授權你的應用程式存取
                alert("good");
            } else {
                //使用者已登入Facebook成功，但未授權你的應用程式存取
                alert("bad");
            }
        } else {
            //使用者未登入成功
            alert("falt")
        }
    }, { perms: 'publish_stream,offline_access' }); //設定需要授權的項目
}