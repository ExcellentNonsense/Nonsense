"use strict";

let bandFlickr = {};

(function () {
  this.fillBand = () => {
    let flickrImagesUrl = "/Band/GetFlickrImages";

    utils.getJSON(flickrImagesUrl, loadFlickrImagesToBand);
  };

  function loadFlickrImagesToBand(response) {
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
      let errElClassName = "js-band-loading-images-error";
      let errElMessage = "Сервис недоступен. Попробуйте обновить страницу.";

      let existingError = itemsContainer.querySelector("." + errElClassName);
      if (existingError !== null) {
        itemsContainer.removeChild(existingError);
      }

      let error = document.createElement("h1");
      error.className = errElClassName;
      error.textContent = errElMessage;

      itemsContainer.appendChild(error);
    }
  }
}).apply(bandFlickr);
