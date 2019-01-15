using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Nonsense.Application.Interfaces;
using Nonsense.Application.RandomImages.Responses;
using Nonsense.Common.Utilities;
using Nonsense.MvcApp.Infrastructure;
using System;

namespace Nonsense.MvcApp.Features.Band {

    public class GetFlickrImagesPresenter : IOutputPort<GetFlickrImagesResponse> {

        public JsonResult FinalData { get; private set; }

        public void Handle(GetFlickrImagesResponse response) {
            Guard.NotNull(response, nameof(response));

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

            FinalData = new JsonResult(result);
        }
    }
}
