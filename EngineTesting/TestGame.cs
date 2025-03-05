using SmirkEngine.AssetHandling;
using SmirkEngine.Core;
using SmirkEngine.Rendering;

namespace EngineTesting;

public class TestGame : Game
{
    protected override void OnLoad()
    {
        var shader = Asset.Load<IShader>("res/shaders/default.glsl");
    }

    protected override void OnTick(float deltaTime)
    {
        
    }

    protected override void OnRender(float deltaTime, IRenderApi renderer)
    {
        
    }
}