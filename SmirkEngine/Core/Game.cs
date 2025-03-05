using Silk.NET.Maths;
using Silk.NET.Windowing;
using SmirkEngine.Rendering;

namespace SmirkEngine.Core;

public abstract class Game
{
    public string? Name { get; set; }

    protected IWindow? window;
    protected IRenderApi? renderApi;
    
    public void Run(int windowWidth, int windowHeight, IRenderApi renderApi)
    {
        var options = WindowOptions.Default with
        {
            Size = new Vector2D<int>(windowWidth, windowHeight),
            Title = Name ?? "SmirkEngine"
        };

        window = Window.Create(options);
        window.Load += OnLoad_Internal;
        window.Render += OnRender_Internal;
        window.Update += OnUpdate_Internal;
        window.Resize += OnResize_Internal;
        window.Run();
    }
    
    private void OnLoad_Internal()
    {
        renderApi?.Initialize(window!);
        OnLoad();
    }

    private void OnResize_Internal(Vector2D<int> obj)
    {
        
    }

    private void OnUpdate_Internal(double deltaTime)
    {
        OnTick((float)deltaTime);
    }

    private void OnRender_Internal(double deltaTime)
    {
        OnRender((float)deltaTime, renderApi!);
    }

    protected virtual void OnLoad()
    {
        
    }

    protected virtual void OnTick(float deltaTime)
    {
        
    }

    protected virtual void OnRender(float deltaTime, IRenderApi renderer)
    {
        
    }
}