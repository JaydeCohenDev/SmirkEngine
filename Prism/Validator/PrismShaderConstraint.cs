using System.Reflection;

namespace Prism.Validator;

public abstract class PrismShaderConstraint
{
    public abstract bool ValidateShader(Type shaderType, ValidationResult result);
}

public class AllowedTypesConstraint : PrismShaderConstraint
{
    private static BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
    
    public override bool ValidateShader(Type shaderType, ValidationResult result)
    {
        foreach (var field in shaderType.GetFields(bindingFlags))
        {
            if (!IsTypeAllowed(field.FieldType))
            {
                
            }
        }
    }
    
    protected bool IsTypeAllowed(Type type)
    {
        return true;
    }
}

public class AllowedNamespacesConstraint : PrismShaderConstraint
{
    public override bool ValidateShader(Type shaderType, ValidationResult result)
    {
        throw new NotImplementedException();
    }
}

public class AllowedSyntaxFeaturesConstraint : PrismShaderConstraint
{
    public override bool ValidateShader(Type shaderType, ValidationResult result)
    {
        throw new NotImplementedException();
    }
}

public class DisallowedMethodCallsConstraint : PrismShaderConstraint
{
    public override bool ValidateShader(Type shaderType, ValidationResult result)
    {
        throw new NotImplementedException();
    }
}