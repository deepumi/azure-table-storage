using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Azure.Storage.Table.Extensions
{
    internal static class StreamExtensions
    {
        internal static async Task<string> StringAsync(this Stream stream)
        {
            if (stream == null || !stream.CanRead) return string.Empty;

            using (var reader = new StreamReader(stream, Encoding.UTF8))
            {
                return await reader.ReadToEndAsync();
            }
        }

        internal static async Task<Stream> CopyAsync(this Stream stream)
        {
            var ms = new MemoryStream();

            await stream.CopyToAsync(ms);

            ms.Seek(0, SeekOrigin.Begin);

            return ms;
        }
    }
}