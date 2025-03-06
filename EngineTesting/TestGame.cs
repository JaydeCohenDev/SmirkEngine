using SmirkEngine.AssetHandling;
using SmirkEngine.Core;
using SmirkEngine.GameFramework;
using SmirkEngine.GameFramework.Actors;
using SmirkEngine.Rendering;

namespace EngineTesting;

public class TestGame : Game
{
    protected override void OnLoad()
    {
        var model = Asset.Load<Mesh>("res/Building_Residence.fbx");
        
        var shader = Asset.Load<Shader>("res/shaders/default.glsl");
        var material = new Material(shader);

        var actor = new MeshActor();
        actor.MeshComponent.SetMaterial(material);
        MainWorld.Spawn<MeshActor>(actor);
    }

    protected override void OnTick(float deltaTime)
    {
        
    }

    protected override void OnRender(float deltaTime, IRenderApi renderer)
    {
        
    }
}