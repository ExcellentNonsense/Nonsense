using Newtonsoft.Json.Linq;

namespace Nonsense.Common.Utilities {

    public static class Json {

        public static JObject TransformJson(JObject original, JObject template) {
            Guard.NotNull(original, nameof(original));
            Guard.NotNull(template, nameof(template));

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