"use strict";

let utils = {};

(function () {
  this.getJSON = (url, callback) => {
    let request = new XMLHttpRequest();

    request.open("GET", url);

    request.onreadystatechange = () => {
      if (request.readyState === 4 && request.status === 200) {
        let type = request.getResponseHeader("Content-Type");
        if (type.match(/application\/json/)) {
          callback(JSON.parse(request.responseText));
        }
      }
    };

    request.send();
  };
}).apply(utils);