using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Nonsense.Infrastructure.WebServices {
    public interface IFlickrService {
        Task<String> GetRandomPhotos(string tags, int photosCount);
    }
}
