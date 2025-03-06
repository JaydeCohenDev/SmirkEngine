using Silk.NET.Maths;
using Silk.NET.Windowing;
using SmirkEngine.GameFramework;
using SmirkEngine.Rendering;

namespace SmirkEngine.Core;

public abstract class Game
{
    public string? Name { get; set; }

    protected IWindow? window;
    private static IRenderApi? RenderAPI;

    public static IRenderApi GetRenderApi()
    {
        if(RenderAPI == null)
            throw new Exception("RenderAPI is null");
        return RenderAPI;
    }
    
    public World MainWorld { get; } = new();
    
    public void Run(int windowWidth, int windowHeight, IRenderApi renderApi)
    {
        RenderAPI = renderApi;
        
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
        RenderAPI?.Initialize(window!);
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
        OnRender((float)deltaTime, RenderAPI!);
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