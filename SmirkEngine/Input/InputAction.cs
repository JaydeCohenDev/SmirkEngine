using JetBrains.Annotations;
using Silk.NET.Input;

namespace SmirkEngine;

public struct InputActionCallbackArgs
{
}

public class InputActionCallback
{
    public required object Target { get; init; }
    public required InputActionCallbackDelegate Callback { get; init; }

    public InputCallbackConsumeType Invoke(InputActionCallbackArgs args)
        => Callback.Invoke(args);
}

public delegate InputCallbackConsumeType InputActionCallbackDelegate(InputActionCallbackArgs args);

public class InputAction
{
    public static readonly List<InputAction> RegisteredActions = [];
    public string Name { get; private set; } = string.Empty;

    protected List<InputActionCallback> _callbacks = [];
    protected List<InputTrigger> _triggers = [];

    public static InputAction Register(string name)
    {
        var action = new InputAction
        {
            Name = name
        };
        RegisteredActions.Add(action);

        return action;
    }

    public static InputAction? Get(string name) =>
        RegisteredActions.Find(a => a.Name == name);

    public static void Unbind(object target) =>
        RegisteredActions.ForEach(action => action.RemoveCallbacksFor(target));

    private InputAction()
    {
    }

    public InputAction WithTrigger(InputTrigger trigger)
    {
        trigger.Setup(Input.InputContext!);
        _triggers.Add(trigger);
        trigger.OnTriggered += () =>
        {
            Invoke(new InputActionCallbackArgs());
        };
        return this;
    }

    public void AddCallback(object target, InputActionCallbackDelegate callback)
    {
        _callbacks.Add(new InputActionCallback
        {
            Target = target,
            Callback = callback
        });
    }

    public void RemoveCallback(object target, InputActionCallbackDelegate callback) =>
        _callbacks.RemoveAll(c => c.Callback == callback && c.Target == target);

    public void RemoveCallbacksFor(object target) =>
        _callbacks.RemoveAll(c => c.Target == target);

    public void ClearCallbacks() => _callbacks.Clear();

    public void Invoke(InputActionCallbackArgs args)
    {
        foreach (var callback in _callbacks)
        {
            switch (callback.Invoke(args))
            {
                case InputCallbackConsumeType.Consume:
                    return;
                case InputCallbackConsumeType.DoNotConsume:
                default:
                    continue;
            }
        }
    }

    public bool HasTrigger<T>(Key key) where T : InputTrigger
    {
        return _triggers.Any(t => t is T);
    }

    public bool HasTrigger<T>(MouseButton button) where T : InputTrigger
    {
        return _triggers.Any(t => t is T);
    }
}