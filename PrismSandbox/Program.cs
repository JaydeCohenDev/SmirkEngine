// See https://aka.ms/new-console-template for more information

using Prism.Compiler;
using Prism.Generators;
using PrismSandbox;
using PrismSandbox.res.shaders;

PrismCompiler.CompileShader<TestShader>(
    new OpenGLShaderGenerator()    
);