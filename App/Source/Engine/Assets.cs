using System.IO;
using System.Reflection;
using SFML.Graphics;
using SFML.Audio;

namespace GameJam;
static class Assets
{
    // Helper to open an embedded resource stream.
    public static Stream Open(string nameInAssembly)
    {
        var asm = Assembly.GetExecutingAssembly();
        var stream = asm.GetManifestResourceStream(nameInAssembly);
        if (stream == null)
        {
            // Helpful when you’re unsure about the final resource name.
            // Console.WriteLine(string.Join("\n", asm.GetManifestResourceNames()));
            throw new FileNotFoundException($"Resource not found: {nameInAssembly}");
        }
        return stream;
    }
}