using Nonsense.Application.RandomImages.Responses;
using System.Threading.Tasks;

namespace Nonsense.Application.RandomImages {

    public interface IRandomImagesService {

        Task<GetFlickrImagesResponse> GetFlickrImages();
    }
}
