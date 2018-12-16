using Newtonsoft.Json.Linq;

namespace Common {
    public static class JsonUtilities {
        public static JObject TransformJson(JObject original, JObject template) {
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
