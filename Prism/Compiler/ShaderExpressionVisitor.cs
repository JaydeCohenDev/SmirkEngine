using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Prism.Compiler;

public abstract class ShaderExpressionVisitor : CSharpSyntaxVisitor<string>
{
    protected virtual string ConvertFunctionCall(string methodName, string arguments)
    {
        return $"{methodName}({arguments})";
    }

    protected virtual string ConvertOperator(SyntaxToken operatorToken)
    {
        return operatorToken.Kind() switch
        {
            SyntaxKind.PlusToken => "+",
            SyntaxKind.MinusToken => "-",
            SyntaxKind.AsteriskToken => "*",
            SyntaxKind.SlashToken => "/",
            SyntaxKind.PlusEqualsToken => "+=",
            SyntaxKind.MinusEqualsToken => "-=",
            SyntaxKind.AsteriskEqualsToken => "*=",
            SyntaxKind.SlashEqualsToken => "/=",
            SyntaxKind.EqualsToken => "=",
            _ => operatorToken.ToString()
        };
    }

    public override string? VisitBinaryExpression(BinaryExpressionSyntax node)
    {
        bool needsParentheses = node.Left is BinaryExpressionSyntax || node.Right is BinaryExpressionSyntax;
        
        string left = Visit(node.Left);
        string right = Visit(node.Right);
        string op = ConvertOperator(node.OperatorToken);
        return needsParentheses ? $"({left} {op} {right})" : $"{left} {op} {right}";
    }

    public override string? VisitLiteralExpression(LiteralExpressionSyntax node)
    {
        return node.ToString();
    }

    public override string? VisitIdentifierName(IdentifierNameSyntax node)
    {
        return node.Identifier.Text;
    }

    public override string? VisitInvocationExpression(InvocationExpressionSyntax node)
    {
        string methodName = ((IdentifierNameSyntax)node.Expression).Identifier.Text;
        string arguments = string.Join(", ", node.ArgumentList.Arguments.Select(a => Visit(a.Expression)));
        return ConvertFunctionCall(methodName, arguments);
    }

    public override string? VisitAssignmentExpression(AssignmentExpressionSyntax node)
    {
        string left = Visit(node.Left);
        string right = Visit(node.Right);
        string op = ConvertOperator(node.OperatorToken);
        return $"{left} {op} {right}";
    }
}