namespace Prism.Validator;

public interface IValidationMessage {}

public abstract class ValidationMessage(string message) : IValidationMessage
{
    public string Message { get; } = message;
}

public class ValidationError(string message) : ValidationMessage(message) {}
public class ValidationWarning(string message) : ValidationMessage(message) {}
public class ValidationInfo(string message) : ValidationMessage(message) {}

public class ValidationResult(Type shaderType)
{
    public List<IValidationMessage> Messages { get; } = [];  
    
    public void AddMessage(IValidationMessage message) => Messages.Add(message);
    public void Clear() => Messages.Clear();
}