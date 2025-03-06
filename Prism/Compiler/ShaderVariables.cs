namespace Prism.Compiler;

public class ShaderVariables
{
    protected Dictionary<string, ShaderVariableDefinition> _variables = [];

    public ShaderVariableDefinition Add(string name, ShaderVariableDefinition variable)
    {
        _variables[name] = variable;
        return variable;
    }

    public bool Get(string name, out ShaderVariableDefinition? variable)
    {
        return _variables.TryGetValue(name, out variable);
    }
    
    public bool Has(string name)
    {
        return _variables.ContainsKey(name);
    }
    
    public List<ShaderVariableDefinition> All => _variables.Values.ToList();
    public List<ShaderVariableDefinition> Inputs => _variables.Values.Where(x => x.IsInput).ToList();
    public List<ShaderVariableDefinition> Outputs => _variables.Values.Where(x => x.IsOutput).ToList();
    public List<ShaderVariableDefinition> Uniforms => _variables.Values.Where(x => x.IsUniform).ToList();
}