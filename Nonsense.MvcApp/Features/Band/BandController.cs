using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Nonsense.Application.RandomImages;
using Nonsense.Common.Utilities;
using Nonsense.MvcApp.Infrastructure;
using System;
using System.Threading.Tasks;

namespace Nonsense.MvcApp.Features.Band {

    public class BandController : Controller {

        private readonly IRandomImagesService _randomImagesService;

        public BandController(IRandomImagesService randomImagesService) {
            Guard.NotNull(randomImagesService, nameof(randomImagesService));

            _randomImagesService = randomImagesService;
        }

        [HttpGet]
        public IActionResult Index() {
            var result = RedirectToAction(nameof(FlickrBand));

            if (User.Identity.IsAuthenticated) {
                result = RedirectToAction(nameof(MyBand));
            }

            return result;
        }

        [HttpGet]
        public IActionResult FlickrBand() => View();

        [HttpGet]
        public IActionResult MyBand() => View();

        [HttpGet]
        public async Task<JsonResult> GetFlickrImages() {
            var response = await _randomImagesService.GetFlickrImages();

            var result = new JObject();

            if (response.Success) {
                var flickrJson = JObject.Parse(response.Data);

                var flickrResponseSuccess = flickrJson.SelectToken("$.stat").ToString()
                    .Equals("ok", StringComparison.OrdinalIgnoreCase);

                if (flickrResponseSuccess) {
                    var receivedImagesInfo = flickrJson.SelectTokens("$.photos.photo[*]");
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
                        transformedInfo = Common.Utilities.Json.TransformJson(originalInfo, template);
                        truncatedImagesInfo.Add(transformedInfo);
                    }

                    result.Add(new JProperty(JsonLiterals.propImages, truncatedImagesInfo));
                    result.Add(new JProperty(JsonLiterals.propStatus, JsonLiterals.statusOk));
                }
                else {
                    result.Add(new JProperty(JsonLiterals.propStatus, JsonLiterals.statusFail));
                    result.Add(new JProperty(JsonLiterals.propErrors, flickrJson.SelectToken("$.message")));
                }
            }
            else {
                result.Add(new JProperty(JsonLiterals.propStatus, JsonLiterals.statusFail));
                result.Add(new JProperty(JsonLiterals.propErrors, response.ErrorsList));
            }

            return new JsonResult(result);
        }
    }
}
