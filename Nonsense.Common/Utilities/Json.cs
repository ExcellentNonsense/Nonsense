using Newtonsoft.Json.Linq;
using System;

namespace Nonsense.Common.Utilities {
    public static class Json {
        public static JObject TransformJson(JObject original, JObject template) {
            if (original == null) {
                throw new ArgumentNullException(nameof(original));
            }

            if (template == null) {
                throw new ArgumentNullException(nameof(template));
            }

            var result = new JObject();

            foreach (var prop in template.Properties()) {
                var propVal = prop.Value?.ToString();

                var path = propVal?.StartsWith("$") == true
                    ? propVal
                    : prop.Path;

                var selectedVal = original.SelectToken(path);
                result.Add(prop.Name, selectedVal);
            }

            return result;
        }
    }
}