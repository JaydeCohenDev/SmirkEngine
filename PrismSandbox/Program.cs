// See https://aka.ms/new-console-template for more information

using Prism.Compiler;
using Prism.Generators;using Prism.Validator;
using PrismSandbox;
using PrismSandbox.res.shaders;

var validator = new PrismValidator(typeof(TestShader))
    .WithConstraint<AllowedTypesConstraint>()
    .WithConstraint<AllowedNamespacesConstraint>()
    .WithConstraint<AllowedSyntaxFeaturesConstraint>()
    .WithConstraint<DisallowedMethodCallsConstraint>();

var result = validator.Validate();
foreach (var message in result.Messages)
{
    var color = message switch
    {
        ValidationError => ConsoleColor.Red,
        ValidationWarning => ConsoleColor.Yellow,
        ValidationInfo => ConsoleColor.Green,
        _ => ConsoleColor.White
    };
    Console.ForegroundColor = color;
    Console.WriteLine(message);
}
Console.ResetColor();

PrismCompiler.CompileShader<TestShader>(
    new OpenGLShaderGenerator()    
);