using Silk.NET.OpenGL;
using StbImageSharp;

namespace SmirkEngine.Rendering.OpenGL;

public class GLTexture(GL gl) : ITexture
{
    public uint Handle { get; private set; }
    protected readonly GL _gl = gl;
    
    protected TextureFilterMode _filterMode = TextureFilterMode.Nearest;
    protected TextureWrapMode _wrapMode = TextureWrapMode.Repeat;

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
        
        SetTextureFilterMode(TextureFilterMode.Nearest);
        SetTextureWrapMode(TextureWrapMode.Repeat);
        
        _gl.BindTexture(TextureTarget.Texture2D, 0);

        return true;
    }

    public TextureFilterMode GetTextureFilterMode() => _filterMode;
    public void SetTextureFilterMode(TextureFilterMode mode)
    {
        _filterMode = mode;
        _gl.BindTexture(TextureTarget.Texture2D, Handle);

        var glFilterMode = (uint)_filterMode;
        _gl.TexParameterI(GLEnum.Texture2D, GLEnum.TextureMinFilter, in glFilterMode);
        _gl.TexParameterI(GLEnum.Texture2D, GLEnum.TextureMagFilter, in glFilterMode);
        _gl.BindTexture(TextureTarget.Texture2D, 0);
    }

    public TextureWrapMode GetTextureWrapMode() => _wrapMode;
    public void SetTextureWrapMode(TextureWrapMode mode)
    {
        _wrapMode = mode;
        _gl.BindTexture(TextureTarget.Texture2D, Handle);
        
        var glWrapMode = (uint)_wrapMode;
        _gl.TexParameterI(GLEnum.Texture2D, GLEnum.TextureWrapS, in glWrapMode);
        _gl.TexParameterI(GLEnum.Texture2D, GLEnum.TextureWrapT, in glWrapMode);
        _gl.BindTexture(TextureTarget.Texture2D, 0);
    }

}