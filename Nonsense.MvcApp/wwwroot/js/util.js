"use strict";

let utils = {};

(function () {
  this.getJSON = (url, callback) => {
    let request = new XMLHttpRequest();

    request.open("GET", url);

    request.onreadystatechange = () => {
      if (request.readyState === 4) {
        let response = null;

        if (request.status === 200) {
          let type = request.getResponseHeader("Content-Type");
          if (type.match(/application\/json/)) {
            response = JSON.parse(request.responseText);
          }
        }

        callback(response);
      }
    };

    request.send();
  };

  this.insertAfter = (newNode, referenceNode) => {
    referenceNode.parentNode.insertBefore(newNode, referenceNode.nextSibling);
  };
}).apply(utils);