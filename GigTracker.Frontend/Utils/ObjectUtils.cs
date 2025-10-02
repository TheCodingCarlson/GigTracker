using System.Text.Json;

namespace GigTracker.Frontend.Utils
{
    public static class ObjectUtils
    {
        /// <summary>
        /// Performs a deep clone of the given object using JSON serialization.
        /// </summary>
        public static T DeepClone<T>(T obj)
        {
            if (obj is null) return default!;
            var json = JsonSerializer.Serialize(obj);
            return JsonSerializer.Deserialize<T>(json)!;
        }
    }
}
