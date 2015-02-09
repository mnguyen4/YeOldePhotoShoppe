var UserAccount = function (firstName, lastName, username, email) {
    this.firstName = firstName;
    this.lastName = lastName;
    this.username = username;
    this.email = email;
};

UserAccount.prototype.doLogout = function (token) {
    var postData = {};
    postData["token"] = token;
    Ajax.doRequest("/yops_ws/api/Logout", postData, function (json) {
        if (json.error) {
            alert(json.error);
        }
        location.replace("/yops/Default.aspx");
    });
};