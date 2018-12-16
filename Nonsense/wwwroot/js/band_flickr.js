"use strict";

function fillBand() {
  let flickrImagesUrl = "/Band/GetFlickrImages";

  getJSON(flickrImagesUrl, loadFlickrImagesToBand);
}

function loadFlickrImagesToBand(response) {
  let itemsContainer = document.getElementById("items_container");

  if (response.stat === "ok") {
    let itemTmpl = document.getElementById("item_tmpl");
    response.images.forEach(function (image) {
      let item = document.importNode(itemTmpl.content, true);

      let itemImgSource = item.querySelector(".item_image_source");
      let imgLink = document.createElement("a");
      imgLink.href = "https://www.flickr.com/photos/" + image.owner + "/" + image.id;
      imgLink.target = "_blank";
      imgLink.textContent = image.title;
      itemImgSource.appendChild(imgLink);

      let itemImg = item.querySelector("img");
      itemImg.src = image.url_z;

      itemsContainer.appendChild(item);
    });
  }
  else {
    let error = document.createElement("h1");
    error.textContent = "Сервис недоступен. Попробуйте обновить страницу.";
    itemsContainer.innerHTML = "";
    itemsContainer.appendChild(error);
  }
}