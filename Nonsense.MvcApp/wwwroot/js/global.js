"use strict";

document.addEventListener("DOMContentLoaded", () => {
  global.bindEventHandlers();
});

let global = {};

(function () {
  this.bindEventHandlers = () => {
    document.querySelector(".js-btn-scroll-to-top")
      .addEventListener("click", () => window.scrollTo(window.pageXOffset, 0));
  };
}).apply(global);