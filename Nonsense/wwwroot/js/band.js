"use strict";

document.addEventListener("DOMContentLoaded", function () {
  startTimer();
  bindEventHandlers();
  switchOnReminder();
  fillBand();
});

function bindEventHandlers() {
  window.addEventListener("scroll", function () {
    if (window.pageYOffset + document.documentElement.clientHeight >= document.documentElement.scrollHeight) {
      fillBand();
    }
  });

  document.querySelector(".js-page-overlay__btn-hide-overlay")
    .addEventListener("click", hideOverlay);
}

const timerInterval = 121;
const timerDuration = 3600000;

function startTimer() {
  let timerId = setInterval(updateTime, timerInterval);

  setTimeout(
    function () {
      clearInterval(timerId);
    },
    timerDuration
  );
}

let passedMs = 0;
function updateTime() {
  let clock = document.querySelector(".js-logged-in-user__spent-time");

  passedMs += timerInterval;
  clock.textContent = convertMsToTime(passedMs);
}

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

function switchOnReminder() {
  setTimeout(showOverlay, 1200000, 0);
  setTimeout(showOverlay, 2400000, 1);
  setTimeout(showOverlay, 3600000, 2);
}

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

  setTimeout(
    function () {
      pageOverlay.style.display = "none";
    },
    900
  );
}