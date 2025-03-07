using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Prism.Core;
using Prism.Types;

namespace Prism.Validator;

public abstract class PrismShaderConstraint
{
    public abstract void ValidateShader(Type shaderType, ValidationResult result);
}

public class AllowedTypesConstraint : PrismShaderConstraint
{
    private static BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly;
    
    public override void ValidateShader(Type shaderType, ValidationResult result)
    {
        var subShaders = shaderType.GetNestedTypes(BindingFlags.NonPublic | BindingFlags.Public)
            .Where(t => typeof(PrismShader.ISubShader).IsAssignableFrom(t)).ToList();

        foreach (var subShader in subShaders)
        {
            ValidateSubShader(subShader, result);
        }
    }

    private void ValidateSubShader(Type shaderType, ValidationResult result)
    {
        foreach (var field in shaderType.GetFields(bindingFlags))
        {
            if (!IsTypeAllowed(field.FieldType))
            {
                result.AddMessage(new ValidationError($"Field '{field.Name}' has a disallowed type '{field.FieldType.Name}'."));
            }
        }

        foreach (var property in shaderType.GetProperties(bindingFlags))
        {
            if (!IsTypeAllowed(property.PropertyType))
            {
                result.AddMessage(new ValidationError($"Property '{property.Name}' has a disallowed type '{property.PropertyType.Name}'."));
            }
        }

        var methods = shaderType.GetMethods(bindingFlags);
        foreach (var method in methods)
        {
            if (method.DeclaringType != shaderType)
                continue;
            
            if (!IsTypeAllowed(method.ReturnType))
            {
                result.AddMessage(new ValidationError($"Method '{method.Name}' has a disallowed return type '{method.ReturnType.Name}'."));
            }
            
            foreach (var parameter in method.GetParameters())
            {
                if (!IsTypeAllowed(parameter.ParameterType))
                {
                    result.AddMessage(new ValidationError($"Method '{method.Name}' parameter '{parameter.Name}' uses disallowed type '{parameter.ParameterType.Name}'"));
                }
            }
        }
    }

    protected bool IsTypeAllowed(Type type)
    {
        if (type.IsAssignableTo(typeof(IShaderVarType)) && !type.IsAbstract)
            return true;

        return type switch
        {
            not null when type == typeof(void) => true,
            _ => false
        };
    }
}

public class AllowedNamespacesConstraint : PrismShaderConstraint
{
    public override void ValidateShader(Type shaderType, ValidationResult result)
    {
        
    }

    protected bool IsNamespaceAllowed(string namespaceName)
    {
        return namespaceName switch
        {
            _ => true
        };
    }
}

public class AllowedSyntaxFeaturesConstraint : PrismShaderConstraint
{
    public override void ValidateShader(Type shaderType, ValidationResult result)
    {
        
    }

    protected bool IsSyntaxFeatureAllowed(SyntaxNode node)
    {
        return node switch
        {
            ForEachStatementSyntax => true,
            _ => false
        };
    }
}

public class DisallowedMethodCallsConstraint : PrismShaderConstraint
{
    public override void ValidateShader(Type shaderType, ValidationResult result)
    {
        
    }
}