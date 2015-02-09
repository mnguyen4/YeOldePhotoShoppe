var UserAccount = function (firstName, lastName, username) {
    this.firstName = firstName;
    this.lastName = lastName;
    this.username = username;
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

UserAccount.prototype.retrieveAccount = function (usernameID, firstNameID, lastNameID, emailID, token) {
    var postData = {};
    postData["token"] = token;
    Ajax.doRequest("/yops_ws/api/RetrieveAccount", postData, function (json) {
        if (json.error) {
            alert(json.error);
        }
        else {
            $("#" + usernameID).val(json.username);
            $("#" + firstNameID).val(json.firstName);
            $("#" + lastNameID).val(json.lastName);
            $("#" + emailID).val(json.email);
        }
    });
};