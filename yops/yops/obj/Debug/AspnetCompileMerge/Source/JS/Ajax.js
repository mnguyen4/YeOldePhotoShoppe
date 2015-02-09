var Ajax = Ajax || {};

/*
 * Function to call a web service.
 */
Ajax.doRequest = function (url, postData, callback) {
    $.ajax({
        url: url,
        type: "POST",
        contentType: "application/json; charset=UTF-8",
        data: JSON.stringify(postData),
        success: function (json) {
            // pass the response to the callback function
            callback(json);
        }
    });
};