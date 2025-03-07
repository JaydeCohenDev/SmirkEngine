using Silk.NET.Input;

namespace SmirkEngine;

public abstract class InputTrigger
{
    public Action? OnTriggered;

    public abstract void Setup(IInputContext inputContext);
}

public class KeyPress(Key key) : InputTrigger
{
    public Key Key { get; } = key;
    
    public override void Setup(IInputContext inputContext)
    {
        foreach (var keyboard in inputContext.Keyboards)
        {
            keyboard.KeyDown += (kb, key, code) =>
            {
                if (key == Key)
                    OnTriggered?.Invoke();
            };
        }
    }
}

public class KeyRelease(Key key) : InputTrigger
{
    public Key Key { get; } = key;
    
    public override void Setup(IInputContext inputContext)
    {
        foreach (var keyboard in inputContext.Keyboards)
        {
            keyboard.KeyUp += (kb, key, code) =>
            {
                if (key == Key)
                    OnTriggered?.Invoke();
            };
        }
    }
}

public class MouseButtonPress(MouseButton button) : InputTrigger
{
    public MouseButton Button { get; } = button;
    
    public override void Setup(IInputContext inputContext)
    {
        foreach (var mouse in inputContext.Mice)
        {
            mouse.MouseDown += (m, button) =>
            {
                if (button == Button)
                    OnTriggered?.Invoke();
            };
        }
    }
}

public class MouseButtonRelease(MouseButton button) : InputTrigger
{
    public MouseButton Button { get; } = button;
    
    public override void Setup(IInputContext inputContext)
    {
        foreach (var mouse in inputContext.Mice)
        {
            mouse.MouseUp += (m, button) =>
            {
                if (button == Button)
                    OnTriggered?.Invoke();
            };
        }
    }
}