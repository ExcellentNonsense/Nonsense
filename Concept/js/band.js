window.onload = function () {
  startTimer();
  switchOnReminder();
  fillBand();
}

const timerInterval = 121;
const timerDuration = 3600000;
function startTimer() {
  var timerId = setInterval(updateTime, timerInterval);
  setTimeout(function () { clearInterval(timerId) }, timerDuration);
}

var passedMs = 0;
function updateTime() {
  passedMs += timerInterval;
  var clock = document.getElementById("spent_time");
  clock.innerHTML = convertMsToTime(passedMs);
}

function convertMsToTime(ms) {
  if (ms >= 1) {
    var milliseconds = ms, seconds = 0, minutes = 0;
    if (ms > 999) {
      milliseconds = ms % 1000;
      var secCount = (ms - milliseconds) / 1000;
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
  var formerSuggestion = document.querySelector("#page_overlay .active_suggestion");
    suggestion = document.querySelectorAll("#page_overlay p")[suggestionNumber],
    pageOverlay = document.getElementById("page_overlay");

  if (formerSuggestion != null) {
    formerSuggestion.classList.remove("active_suggestion");
  }
  suggestion.classList.add("active_suggestion");
  pageOverlay.style.opacity = "1";
  pageOverlay.style.display = "block";

  if (suggestionNumber == 2) {
    var continueBtn = document.querySelector("#page_overlay input");
    continueBtn.style.display = "none";
  }
}

function hideOverlay() {
  var suggestion_lines = document.querySelectorAll("#page_overlay .active_suggestion span");
  suggestion_lines[0].classList.add("suggestion_row1");
  suggestion_lines[1].classList.add("suggestion_row2");
  suggestion_lines[2].classList.add("suggestion_row3");

  var pageOverlay = document.getElementById("page_overlay");
  pageOverlay.style.opacity = "0";
  setTimeout(function () {
    pageOverlay.style.display = "none";
  }, 900);
  
}

function fillBand() {
  var imagesCount = 4;
  for (var i = 1; i <= imagesCount; i++) {
    var messageTmpl = document.getElementById("message_tmpl"),
      img = messageTmpl.content.querySelector("img");
    img.src = "../content/images/" + i + ".jpg";
    var message = document.importNode(messageTmpl.content, true),
      messagesContainer = document.getElementById("messages_container");
    messagesContainer.appendChild(message);
  }
}