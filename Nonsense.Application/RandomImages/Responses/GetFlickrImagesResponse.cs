﻿using Nonsense.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nonsense.Application.RandomImages.Responses {
    public class GetFlickrImagesResponse : BaseInteractorResponse {
        public string Data { get; }

        public GetFlickrImagesResponse(bool success, IEnumerable<string> errors, string data)
            : base(success, errors) {
            Data = data;
        }
    }
}
