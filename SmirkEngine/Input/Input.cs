using System.Reflection;
using Silk.NET.Input;
using SmirkEngine.Logging;

namespace SmirkEngine;

public static class Input
{
    public static IInputContext? InputContext { get; private set; }
    
    public static void Initialize(IInputContext inputContext)
    {
        InputContext = inputContext;
    }
    
    public static void BindInputFunctions(object target) =>
        BindInputActionFunctions(target);
    
    public static void UnbindInputFunctions(object target) =>
        InputAction.Unbind(target);

    private static void BindInputActionFunctions(object target)
    {
        var inputActionMethods = target.GetType().GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
            .Where(m => m.GetCustomAttribute<InputActionAttribute>() != null);

        foreach (var method in inputActionMethods)
        {
            var inputAction = InputAction.Get(method.Name);
            if (inputAction is null)
                Output.LogWarning($"InputAction {method.Name} not found");

            var callback = (InputActionCallbackDelegate)method.CreateDelegate(typeof(InputActionCallbackDelegate), target);
            inputAction?.AddCallback(target, callback);
        }
    }
}