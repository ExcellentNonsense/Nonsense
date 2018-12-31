using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Nonsense.Infrastructure.WebServices;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Nonsense.Web.Features.Band {
    public class BandController : Controller {
        private readonly IFlickrService _flickrService;

        public BandController(IFlickrService flickrService) {
            _flickrService = flickrService ?? throw new ArgumentNullException(nameof(flickrService));
        }

        public IActionResult Index() {
            var result = RedirectToAction(nameof(FlickrBand));

            if (User.Identity.IsAuthenticated) {
                result = RedirectToAction(nameof(MyBand));
            }

            return result;
        }

        public IActionResult FlickrBand() => View();

        public IActionResult MyBand() => View();

        public async Task<JsonResult> GetFlickrImages() {
            var okStatus = "ok";
            var failStatus = "fail";

            var result = new JObject();

            try {
                var jsonStringResponse = await _flickrService.GetRandomPhotos(tags: "nonsense", photosCount: 10);
                var response = JObject.Parse(jsonStringResponse);

                var responseStatus = response.SelectToken("$.stat").ToString();

                if (responseStatus.Equals(okStatus, StringComparison.OrdinalIgnoreCase)) {
                    var receivedImagesInfo = response.SelectTokens("$.photos.photo[*]");
                    var truncatedImagesInfo = new JArray();

                    JObject originalInfo;
                    JObject transformedInfo;

                    var template = JObject.Parse(@"{
                      ""id"": """",
                      ""owner"": """",
                      ""title"": """",
                      ""url_z"": """"
                    }");

                    foreach (var imagesInfo in receivedImagesInfo) {
                        originalInfo = JObject.Parse(imagesInfo.ToString());
                        transformedInfo = Infrastructure.Utilities.Json.TransformJson(originalInfo, template);

                        truncatedImagesInfo.Add(transformedInfo);
                    }

                    result.Add(new JProperty("images", truncatedImagesInfo));
                    result.Add(new JProperty("stat", okStatus));
                }
                else {
                    result.Add(new JProperty("stat", failStatus));
                }
            }
            catch (Exception ex) {
                if (ex is ArgumentNullException ||
                    ex is ArgumentOutOfRangeException ||
                    ex is HttpRequestException) {
                    result.RemoveAll();
                    result.Add(new JProperty("stat", failStatus));
                }
                else {
                    throw;
                }
            }

            return Json(result);
        }
    }
}
