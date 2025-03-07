using Silk.NET.OpenGL;
using StbImageSharp;

namespace SmirkEngine.Rendering.OpenGL;

public class GLTexture(GL gl) : ITexture
{
    public uint Handle { get; private set; }
    protected readonly GL _gl = gl;

    public bool LoadFromFile(string path)
    {
        Handle = _gl.GenTexture();
        _gl.ActiveTexture(TextureUnit.Texture0);
        _gl.BindTexture(TextureTarget.Texture2D, Handle);
    
        unsafe
        {
            var result = ImageResult.FromMemory(File.ReadAllBytes(path), ColorComponents.RedGreenBlueAlpha);
            fixed (byte* ptr = result.Data)
                _gl.TexImage2D(TextureTarget.Texture2D, 0, InternalFormat.Rgba, (uint)result.Width, (uint)result.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, ptr);
        }
        
        _gl.TexParameterI(GLEnum.Texture2D, GLEnum.TextureWrapS, (int)TextureWrapMode.Repeat);
        _gl.TexParameterI(GLEnum.Texture2D, GLEnum.TextureWrapT, (int)TextureWrapMode.Repeat);
        _gl.TexParameterI(GLEnum.Texture2D, GLEnum.TextureMinFilter, (int)TextureMinFilter.Nearest);
        _gl.TexParameterI(GLEnum.Texture2D, GLEnum.TextureMagFilter, (int)TextureMagFilter.Nearest);

        _gl.BindTexture(TextureTarget.Texture2D, 0);

        return true;
    }
}