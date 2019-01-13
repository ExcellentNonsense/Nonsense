using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Nonsense.Application.RandomImages.Interactors;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Nonsense.MvcApp.Features.Band {
    public class BandController : Controller {
        private readonly IGetFlickrImagesInteractor _getFlickrImagesInteractor;
        private readonly GetFlickrImagesPresenter _getFlickrImagesPresenter;

        public BandController(IGetFlickrImagesInteractor getFlickrImagesInteractor, GetFlickrImagesPresenter presenter) {
            _getFlickrImagesInteractor = getFlickrImagesInteractor ?? throw new ArgumentNullException(nameof(getFlickrImagesInteractor));
            _getFlickrImagesPresenter = presenter ?? throw new ArgumentNullException(nameof(presenter));
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
