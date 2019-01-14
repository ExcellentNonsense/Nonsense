using Nonsense.Application.Gateways.WebServices;
using Nonsense.Application.Interfaces;
using Nonsense.Application.RandomImages.Responses;
using Nonsense.Common.Utilities;
using System;
using System.Threading.Tasks;

namespace Nonsense.Application.RandomImages.Interactors {

    public class GetFlickrImagesInteractor : IGetFlickrImagesInteractor {

        private readonly IFlickrService _flickrService;

        public GetFlickrImagesInteractor(IFlickrService flickrService) {
            Guard.NotNull(flickrService, nameof(flickrService));

            _flickrService = flickrService;
        }

        public async Task<bool> Execute(IOutputPort<GetFlickrImagesResponse> outputPort) {
            Guard.NotNull(outputPort, nameof(outputPort));

            var response = await _flickrService.GetRandomPhotos(tags: "nonsense", photosCount: 10);

            outputPort.Handle(
                new GetFlickrImagesResponse(response.Success, response.Messages, response.Data));

            return response.Success;
        }
    }
}
