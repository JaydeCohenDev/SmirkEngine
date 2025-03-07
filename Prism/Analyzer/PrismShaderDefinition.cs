using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Prism.Attributes;
using Prism.Core;

namespace Prism.Analyzer;

public class PrismShaderProgramDefinition
{
    public string Name { get; set; } = string.Empty;
    public string SourcePath { get; set; } = string.Empty;
    public Type ShaderProgramType { get; set; } = null!;
    public List<PrismSubShaderDefinition> SubShaders { get; } = [];
    public SyntaxNode? RootNode { get; set; } = null;

    public string? LoadSource()
    {
        return File.ReadAllText($"res/shaders/{SourcePath}"); // TODO fix hardcode path
    }
}

public class PrismSubShaderDefinition
{
    public string Name { get; set; } = string.Empty;
    public Type ShaderType { get; set; } = null!;
    public ClassDeclarationSyntax? Syntax { get; set; } = null;
    
    public List<PrismShaderProperty> Properties { get; } = [];
    public List<PrismShaderMethod> Methods { get; } = [];
}

public class PrismShaderProperty
{
    public string Name { get; set; } = string.Empty;
    public Type Type { get; set; } = null!;
    public SyntaxNode? Syntax { get; set; } = null;
}

public class PrismShaderMethod
{
    public string Name { get; set; } = string.Empty;
    public Type Type { get; set; } = null!;
    public MethodInfo  Info { get; set; } = null!;
    
    public SyntaxNode? RootNode { get; set; } = null;
}