using Microsoft.AspNetCore.Mvc;
using Nonsense.Application.RandomImages.Interactors;
using Nonsense.Common.Utilities;
using System.Threading.Tasks;

namespace Nonsense.MvcApp.Features.Band {

    public class BandController : Controller {

        private readonly IGetFlickrImagesInteractor _getFlickrImagesInteractor;
        private readonly GetFlickrImagesPresenter _getFlickrImagesPresenter;

        public BandController(IGetFlickrImagesInteractor getFlickrImagesInteractor, GetFlickrImagesPresenter presenter) {
            Guard.NotNull(getFlickrImagesInteractor, nameof(getFlickrImagesInteractor));
            Guard.NotNull(presenter, nameof(presenter));

            _getFlickrImagesInteractor = getFlickrImagesInteractor;
            _getFlickrImagesPresenter = presenter;
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
            await _getFlickrImagesInteractor.Execute(_getFlickrImagesPresenter);
            return _getFlickrImagesPresenter.FinalData;
        }
    }
}
