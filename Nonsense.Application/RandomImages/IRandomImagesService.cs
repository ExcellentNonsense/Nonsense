using System.Threading.Tasks;

namespace Nonsense.Application.RandomImages {

    public interface IRandomImagesService {

        Task<BoundaryResponse<string>> GetFlickrImages(int imagesCount);
    }
}
