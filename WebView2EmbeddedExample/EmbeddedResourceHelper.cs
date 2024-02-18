using System.IO;
using System.Reflection;

namespace WebView2EmbeddedExample;

public class EmbeddedResourceHelper(Assembly assembly, string rootDirectory)
{
    private readonly string prefix = assembly.GetName().Name!;

    public Stream GetFile(string relativePath)
    {
        var path = Path
            .Join(prefix, rootDirectory, relativePath)
            .Replace('/', '.')
            .Replace('\\', '.');

        var stream = assembly.GetManifestResourceStream(path);

        if (stream == null)
            throw new FileNotFoundException($"File \"{path}\" not found in embedded resources.");

        return stream;
    }
}
