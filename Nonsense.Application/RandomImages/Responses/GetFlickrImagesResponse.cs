using Nonsense.Common;
using System.Collections.Generic;

namespace Nonsense.Application.RandomImages.Responses {

    public sealed class GetFlickrImagesResponse : OperationResult {

        public string Data { get; }

        public GetFlickrImagesResponse(bool success, IEnumerable<string> errors, string data)
            : base(success, errors) {

            Data = data;
        }
    }
}
