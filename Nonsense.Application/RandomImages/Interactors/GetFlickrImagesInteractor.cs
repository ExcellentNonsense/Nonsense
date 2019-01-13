using Nonsense.Application.Gateways.WebServices;
using Nonsense.Application.Interfaces;
using Nonsense.Application.RandomImages.Responses;
using System;
using System.Threading.Tasks;

namespace Nonsense.Application.RandomImages.Interactors {

    public class GetFlickrImagesInteractor : IGetFlickrImagesInteractor {

        private readonly IFlickrService _flickrService;

        public GetFlickrImagesInteractor(IFlickrService flickrService) {
            _flickrService = flickrService ?? throw new ArgumentNullException(nameof(flickrService));
        }

        public async Task<bool> Execute(IOutputPort<GetFlickrImagesResponse> outputPort) {
            if (outputPort == null) {
                throw new ArgumentNullException(nameof(outputPort));
            }

            var response = await _flickrService.GetRandomPhotos(tags: "nonsense", photosCount: 10);

            outputPort.Handle(
                new GetFlickrImagesResponse(response.Success, response.Messages, response.Data));

            return response.Success;
        }
    }
}
