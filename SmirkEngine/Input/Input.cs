using System.Reflection;
using Silk.NET.Input;
using SmirkEngine.Logging;

namespace SmirkEngine;

public static class Input
{
    private static IInputContext? _inputContext;
    
    public static void Initialize(IInputContext inputContext)
    {
        _inputContext = inputContext;
        
        InputAction.RegisteredActions.ForEach(action =>
        {
            action.BindTriggers(_inputContext);
        });
    }
    
    public static void BindInputFunctions(object target) =>
        BindInputActionFunctions(target);
    
    public static void UnbindInputFunctions(object target) =>
        InputAction.Unbind(target);

    private static void BindInputActionFunctions(object target)
    {
        var inputActionMethods = target.GetType().GetMethods()
            .Where(m => m.GetCustomAttribute<InputActionAttribute>() != null);

        foreach (var method in inputActionMethods)
        {
            var inputAction = InputAction.Get(method.Name);
            if (inputAction is null)
                Output.LogWarning($"InputAction {method.Name} not found");
                    
            inputAction?.AddCallback(target, args =>
                (InputCallbackConsumeType)method.Invoke(target, [args])!);
        }
    }
}