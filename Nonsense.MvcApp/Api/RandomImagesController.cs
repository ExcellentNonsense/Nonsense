using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Nonsense.Application.RandomImages;
using Nonsense.Common.Utilities;
using Nonsense.MvcApp.Infrastructure;
using System;
using System.Threading.Tasks;

namespace Nonsense.MvcApp.Api {

    public class RandomImagesController : BaseApiController {

        private readonly IRandomImagesService _randomImagesService;

        public RandomImagesController(IRandomImagesService randomImagesService) {
            Guard.NotNull(randomImagesService, nameof(randomImagesService));

            _randomImagesService = randomImagesService;
        }

        [HttpGet]
        public async Task<JsonResult> GetFlickrImages(int count) {
            var response = await _randomImagesService.GetFlickrImages(count);

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
                        transformedInfo = Json.TransformJson(originalInfo, template);
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
