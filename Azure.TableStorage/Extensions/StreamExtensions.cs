using System.IO;
using System.Threading.Tasks;

namespace Azure.TableStorage.Extensions
{
    internal static class StreamExtensions
    {
        internal static async Task<string> AsString(this Stream stream)
        {
            if (stream == null || !stream.CanRead) return string.Empty;

            using (var reader = new StreamReader(stream))
            {
                return await reader.ReadToEndAsync();
            }
        }
    }
}
