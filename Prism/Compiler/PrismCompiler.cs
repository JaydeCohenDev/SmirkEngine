using System.Reflection;
using Prism.Attributes;
using Prism.Core;
using Prism.Types;

namespace Prism.Compiler;

public static partial class PrismCompiler
{
    public static string CompileShader<T>(IShaderCodeGenerator generator) where T : PrismShader
    {
        var shaderType = typeof(T);

        Console.WriteLine($"Starting to compile shader: {shaderType.Name}");
        
        foreach (var subShader in ExtractSubShaders<T>())
        {
            Console.WriteLine($"Found sub shader: {subShader.Name}");
            
            var shaderVars = ExtractShaderVars(subShader);
            var shaderCode = generator.GenerateShaderCode(subShader, shaderVars);
            Console.WriteLine($"Generated Code using generator {generator.GetType().Name}");
            Console.WriteLine(shaderCode);
        }

        return "";


    }

    private static List<Type> ExtractSubShaders<T>() where T : PrismShader
    {
        return typeof(T).GetNestedTypes(BindingFlags.NonPublic | BindingFlags.Public)
            .Where(t => typeof(PrismShader.ISubShader).IsAssignableFrom(t)).ToList();
    }

    private static ShaderVariables ExtractShaderVars(Type shaderType)
    {
        Console.WriteLine($"Extracting shader vars from {shaderType.Name}");
        
        var variables = new ShaderVariables();
        int nextInputBinding = 0;
        int nextOutputLocation = 0;

        foreach (var member in shaderType.GetMembers(BindingFlags.Public | BindingFlags.NonPublic |
                                                     BindingFlags.Instance))
        {
            Type memberType = null;

            if (member is FieldInfo field)
            {
                memberType = field.FieldType;
            }
            else if (member is PropertyInfo property)
            {
                memberType = property.PropertyType;
            }
            else
            {
                continue;
            }

            var inputAttrib = member.GetCustomAttribute<ShaderInput>();
            var outputAttrib = member.GetCustomAttribute<ShaderOutput>();

            if (inputAttrib != null)
            {
                var inputVar = variables.Add(member.Name, new ShaderVariableDefinition
                {
                    Name = member.Name,
                    Type = memberType,
                    IsInput = true,
                    Binding = nextInputBinding++,
                    MemberInfo = member
                });
                Console.WriteLine($"Found input: {inputVar}");
            }
            else if (outputAttrib != null)
            {
                var outputVar = variables.Add(member.Name, new ShaderVariableDefinition
                {
                    Name = member.Name,
                    Type = memberType,
                    IsOutput = true,
                    Location = nextOutputLocation++,
                    MemberInfo = member
                });
                Console.WriteLine($"Found Output: {outputVar}");
            }
        }
        
        return variables;
    }
}