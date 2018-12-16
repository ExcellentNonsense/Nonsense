"use strict";

window.addEventListener("scroll", function () {
  console.log(window.pageYOffset);
  console.log(document.documentElement.clientHeight);
  console.log(document.documentElement.scrollHeight);
  if (window.pageYOffset + document.documentElement.clientHeight >= document.documentElement.scrollHeight) {
    fillBand();
  }
});

document.addEventListener("DOMContentLoaded", function () {
  startTimer();
  switchOnReminder();
  fillBand();
});

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
  let clock = document.getElementById("spent_time");

  passedMs += timerInterval;
  clock.innerHTML = convertMsToTime(passedMs);
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
      (milliseconds == 0) ? "000"
        : (milliseconds < 10) ? "00" + milliseconds
          : (milliseconds < 100) ? "0" + milliseconds
            : milliseconds;

    seconds =
      (seconds == 0) ? "00"
        : (seconds < 10) ? "0" + seconds
          : seconds;

    minutes =
      (minutes == 0) ? "00"
        : (minutes < 10) ? "0" + minutes
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
  let formerSuggestion = document.querySelector("#page_overlay .active_suggestion");
  let suggestion = document.querySelectorAll("#page_overlay p")[suggestionNumber];
  let pageOverlay = document.getElementById("page_overlay");

  if (formerSuggestion != null) {
    formerSuggestion.classList.remove("active_suggestion");
  }

  suggestion.classList.add("active_suggestion");
  pageOverlay.style.opacity = "1";
  pageOverlay.style.display = "block";

  if (suggestionNumber == 2) {
    let continueBtn = document.querySelector("#page_overlay input");
    continueBtn.style.display = "none";
  }
}

function hideOverlay() {
  let suggestion_lines = document.querySelectorAll("#page_overlay .active_suggestion span");
  suggestion_lines[0].classList.add("suggestion_row1");
  suggestion_lines[1].classList.add("suggestion_row2");
  suggestion_lines[2].classList.add("suggestion_row3");

  let pageOverlay = document.getElementById("page_overlay");
  pageOverlay.style.opacity = "0";

  setTimeout(
    function () {
      pageOverlay.style.display = "none";
    },
    900
  );
}