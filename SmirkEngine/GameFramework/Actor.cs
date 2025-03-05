using SmirkEngine.Core;
using SmirkEngine.Rendering;

namespace SmirkEngine.GameFramework;

public partial class World
{
    public class Actor : ITickable, IRenderable
    {
        public Transform Transform { get; init; } = new();
        public World? World { get; private set; }
        protected virtual void BeginPlay() {}
        protected virtual void EndPlay() {}
        public virtual void Tick(float deltaTime) {}
        public virtual void Render(float deltaTime, IRenderApi renderer) {}

        protected readonly List<ActorComponent> _components = [];
        protected bool hasBegunPlay = false;
        
        protected List<IRenderable> _renderableComponents => _components.OfType<IRenderable>().ToList();

        internal void InvokeBeginPlay(World world)
        {
            World = world;
            hasBegunPlay = true;
            BeginPlay();
        }

        internal void InvokeEndPlay()
        {
            EndPlay();
            World = null;
        }

        internal void InvokeTick(float deltaTime)
        {
            _components.ForEach(component => component.Tick(deltaTime));
            Tick(deltaTime);
        }

        internal void InvokeRender(float deltaTime, IRenderApi renderer)
        {
            _renderableComponents.ForEach(component => component.Render(deltaTime, renderer));
            Render(deltaTime, renderer);
        }

        public void Destroy()
        {
            if (World is null)
            {
                InvokeEndPlay();
            }
            else
            {
                World.DestroyActor(this);
            }
        }

        public ActorComponent AddComponent(ActorComponent component)
        {
            _components.Add(component);
            if(hasBegunPlay)
                component.BeginPlay();

            return component;
        }

        public T AddComponent<T>() where T : ActorComponent
        {
            var component = AddComponent(Activator.CreateInstance<T>());
            return (T)component;
        }

        public T? GetComponent<T>() where T : ActorComponent
        {
            return _components.OfType<T>().FirstOrDefault();
        }
    }
}