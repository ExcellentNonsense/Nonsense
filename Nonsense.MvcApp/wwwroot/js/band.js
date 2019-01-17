"use strict";

document.addEventListener("DOMContentLoaded", () => {
  band.startTimer();
  band.bindEventHandlers();
  band.switchOnReminder();
  band.fillBand();
});

let band = {};

(function () {
  this.bindEventHandlers = () => {
    window.addEventListener("scroll", () => {
      if (window.pageYOffset + document.documentElement.clientHeight >= document.documentElement.scrollHeight) {
        this.fillBand();
      }
    });

    document.querySelector(".js-page-overlay__btn-hide-overlay")
      .addEventListener("click", hideOverlay);
  };

  this.startTimer = () => {
    const timerInterval = 121;
    const timerDuration = 3600000;

    const timerId = setInterval(updateTime, timerInterval);

    setTimeout(() => {
      clearInterval(timerId);
    }, timerDuration
    );

    let passedMs = 0;

    function updateTime() {
      let clock = document.querySelector(".js-logged-in-user__spent-time");

      passedMs += timerInterval;
      clock.textContent = convertMsToTime(passedMs);
    }
  };

  function convertMsToTime(ms) {
    if (ms >= 1) {
      let milliseconds = ms;
      let seconds = 0;
      let minutes = 0;

      if (ms > 999) {
        milliseconds = ms % 1000;
        let secCount = (ms - milliseconds) / 1000;

        if (secCount > 59) {
          seconds = secCount % 60;
          minutes = (secCount - seconds) / 60;
        }
        else {
          seconds = secCount;
        }
      }

      milliseconds =
        milliseconds === 0 ? "000"
          : milliseconds < 10 ? "00" + milliseconds
            : milliseconds < 100 ? "0" + milliseconds
              : milliseconds;

      seconds =
        seconds === 0 ? "00"
          : seconds < 10 ? "0" + seconds
            : seconds;

      minutes =
        minutes === 0 ? "00"
          : minutes < 10 ? "0" + minutes
            : minutes;

      return minutes + ":" + seconds + ":" + milliseconds;
    }
    else return "00:00:000";
  }

  this.switchOnReminder = () => {
    setTimeout(showOverlay, 1200000, 0);
    setTimeout(showOverlay, 2400000, 1);
    setTimeout(showOverlay, 3600000, 2);
  };

  function showOverlay(suggestionNumber) {
    let formerSuggestion = document.querySelector(".js-page-overlay__active-suggestion");
    let suggestion = document.querySelectorAll(".js-page-overlay__suggestion")[suggestionNumber];
    let pageOverlay = document.querySelector(".js-page-overlay");

    if (formerSuggestion !== null) {
      formerSuggestion.classList.remove("js-page-overlay__active-suggestion");
    }

    suggestion.classList.add("js-page-overlay__active-suggestion");
    pageOverlay.style.opacity = "1";
    pageOverlay.style.display = "block";

    if (suggestionNumber === 2) {
      let continueBtn = document.querySelector(".js-page-overlay__btn-hide-overlay");
      continueBtn.style.display = "none";
    }
  }

  function hideOverlay() {
    let suggestion_lines = document.querySelectorAll(".js-page-overlay__active-suggestion .js-page-overlay__suggestion-row");
    suggestion_lines[0].classList.add("js-page-overlay__suggestion-row--hide-row1");
    suggestion_lines[1].classList.add("js-page-overlay__suggestion-row--hide-row2");
    suggestion_lines[2].classList.add("js-page-overlay__suggestion-row--hide-row3");

    let pageOverlay = document.querySelector(".js-page-overlay");
    pageOverlay.style.opacity = "0";

    setTimeout(() => {
        pageOverlay.style.display = "none";
      },900
    );
  }

  this.fillBand = () => {
    let bandType = document.querySelector(".js-band-item-template").getAttribute("data-band-type");

    switch (bandType) {
      case "my-band":
        fillMyBand();
        break;
      case "flickr-band":
        fillFlickrBand();
        break;
      default:
        break;
    }
  };

  function fillMyBand() {

  }

  function fillFlickrBand() {
    let flickrImagesUrl = "/api/RandomImages/GetFlickrImages?count=10";

    band.toggleDataLoadingNotification();
    utils.getJSON(flickrImagesUrl, loadFlickrImagesToBand);

    function loadFlickrImagesToBand(response) {
      if (response !== null) {
        let itemsContainer = document.querySelector(".js-band-items-container");

        if (response.stat === "ok") {
          let itemTmpl = document.querySelector(".js-band-item-template");
          response.images.forEach((image) => {
            if (image.url_z !== null) {
              let item = document.importNode(itemTmpl.content, true);

              let itemImgSource = item.querySelector(".js-band-item__image-source-info");
              let imgLink = document.createElement("a");
              imgLink.href = "https://www.flickr.com/photos/" + image.owner + "/" + image.id;
              imgLink.target = "_blank";
              imgLink.textContent = image.title;
              itemImgSource.appendChild(imgLink);

              let itemImg = item.querySelector(".js-band-item__image");
              itemImg.src = image.url_z;

              itemsContainer.appendChild(item);
            }
          });
        }
        else {
          band.showDataLoadingError();
        }
      }
      else {
        band.showDataLoadingError();
      }

      band.toggleDataLoadingNotification();
    }
  }

  this.toggleDataLoadingNotification = () => {
    let notification = document.querySelector(".js-data-loading-notification");

    notification.classList.toggle("js-data-loading-notification--hidden");

    let drops = notification.querySelectorAll(".js-data-loading-notification__drop");

    drops.forEach((drop) => {
      drop.classList.toggle("js-data-loading-notification__drop--animated");
    });

    let messageParts = notification.querySelectorAll(".js-data-loading-notification__message-part");

    messageParts.forEach((part) => {
      part.classList.toggle("js-data-loading-notification__message-part--animated");
    });
  };

  this.showDataLoadingError = () => {
    let itemsContainer = document.querySelector(".js-band-items-container");

    let errElClassName = "js-band-loading-images-error";
    let errElMessage = "Сервис недоступен. Попробуйте обновить страницу.";

    let existingError = itemsContainer.querySelector("." + errElClassName);
    if (existingError !== null) {
      itemsContainer.removeChild(existingError);
    }

    let error = document.createElement("p");
    error.className = errElClassName;
    error.style.textAlign = "center";
    error.textContent = errElMessage;

    let head = document.createElement("pre");
    head.textContent += "_\r\n";
    head.textContent += "_/*\\_\r\n";
    head.textContent += "/_____\\\r\n";
    head.textContent += "| %,% |\r\n";
    head.textContent += "\\ (-) /\r\n";
    head.textContent += "\\_'_/\r\n";

    error.appendChild(head);

    utils.insertAfter(error, itemsContainer);
  };
}).apply(band);
