using JetBrains.Annotations;

namespace SmirkEngine;

[AttributeUsage(AttributeTargets.Method)]
[MeansImplicitUse]
public sealed class InputActionAttribute : Attribute
{
    
}

[AttributeUsage(AttributeTargets.Method)]
[MeansImplicitUse]
public sealed class InputAxisAttribute : Attribute
{
    
}