namespace Prism.Validator;

public class PrismValidator(Type shaderType)
{
    protected Type ShaderType { get; } = shaderType;

    protected List<PrismShaderConstraint> Constraints { get; } = [];

    public PrismValidator WithConstraint(PrismShaderConstraint constraint)
    {
        Constraints.Add(constraint);
        return this;
    }
    
    public PrismValidator WithConstraint<T>() where T : PrismShaderConstraint
    {
        WithConstraint(Activator.CreateInstance<T>());
        return this;
    }

    public ValidationResult Validate()
    {
        ValidationResult result = new(ShaderType);

        Constraints.ForEach(c => c.ValidateShader(ShaderType, result));
        
        return result;
    }
}