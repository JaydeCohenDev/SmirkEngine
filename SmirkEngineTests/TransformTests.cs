using System.Numerics;
using SmirkEngine.Core;
using Xunit.Abstractions;

namespace SmirkEngineTests;

public class TransformTests(ITestOutputHelper testOutputHelper)
{
    private const float Tolerance = 0.0001f; // Small margin for floating-point errors

    // ✅ Test Default Transform Values
    [Fact]
    public void Transform_DefaultValues_Correct()
    {
        var transform = new Transform();

        Assert.Equal(Vector3.Zero, transform.Position);
        Assert.Equal(Quaternion.Identity, transform.Rotation);
        Assert.Equal(Vector3.One, transform.Scale);
        Assert.Null(transform.Parent);
        Assert.Equal(Vector3.UnitZ, transform.Forward);
        Assert.Equal(Vector3.UnitX, transform.Right);
        Assert.Equal(Vector3.UnitY, transform.Up);
    }

    // ✅ Test Translation
    [Fact]
    public void Transform_Translate_PositionUpdated()
    {
        var transform = new Transform();
        transform.Translate(new Vector3(3, 5, -2));

        Assert.Equal(new Vector3(3, 5, -2), transform.Position);
    }

    // ✅ Test Rotation (90 degrees around Y-axis)
    [Fact]
    public void Transform_RotateY_ForwardVectorCorrect()
    {
        var transform = new Transform();
        transform.Rotate(Vector3.UnitY, 90);

        var expectedForward = new Vector3(1, 0, 0); // Forward should be along +X axis
        Assert.True(Vector3.Distance(transform.Forward, expectedForward) < Tolerance);
    }

    // ✅ Test Scaling
    [Fact]
    public void Transform_ScaleBy_ScaleUpdated()
    {
        var transform = new Transform();
        transform.ScaleBy(new Vector3(2, 0.5f, 1));

        Assert.Equal(new Vector3(2, 0.5f, 1), transform.Scale);
    }

    // ✅ Test Parent-Child Relationship
    [Fact]
    public void Transform_SetParent_ChildInheritsTransform()
    {
        var parent = new Transform { Position = new Vector3(5, 0, 0) };
        var child = new Transform { Position = new Vector3(2, 0, 0) };
        
        child.SetParent(parent);

        Assert.Equal(parent, child.Parent);
        Assert.Equal(new Vector3(7, 0, 0), child.GetWorldPosition()); // Child should be offset by parent's position
    }

    // ✅ Test World Position with Rotation
    [Fact]
    public void Transform_SetParent_WithRotation_CorrectWorldPosition()
    {
        var parent = new Transform();
        parent.Rotate(Vector3.UnitY, -90);
        parent.Translate(new Vector3(5, 0, 0));

        var child = new Transform { Position = new Vector3(2, 0, 0) };
        child.SetParent(parent);

        var worldPosition = child.GetWorldPosition();
        var expectedPosition = new Vector3(5, 0, 2); 

        testOutputHelper.WriteLine($"Expected: {expectedPosition}, Actual: {worldPosition}");

        Assert.True(Vector3.Distance(worldPosition, expectedPosition) < Tolerance, 
            $"Expected: {expectedPosition}, Actual: {worldPosition}");
    }

    // ✅ Test Forward Vector after Rotation
    [Fact]
    public void Transform_RotateX_UpVectorCorrect()
    {
        var transform = new Transform();
        transform.Rotate(Vector3.UnitX, 90);

        var expectedUp = new Vector3(0, 0, 1); // Up should now be along +Z axis
        Assert.True(Vector3.Distance(transform.Up, expectedUp) < Tolerance);
    }

    // ✅ Test Rotation Composition (Yaw, Pitch, Roll)
    [Fact]
    public void Transform_RotateYawPitchRoll_CorrectOrientation()
    {
        var transform = new Transform();
    
        // Apply rotations
        transform.Rotate(90, 45, 0);

        // Correct expected forward vector after Yaw (90°) and Pitch (45°)
        var expectedForward = Vector3.Normalize(new Vector3(0.707f, -0.707f, 0f));

        var actualForward = transform.Forward;

        Assert.True(Vector3.Distance(actualForward, expectedForward) < Tolerance, 
            $"Expected: {expectedForward}, Actual: {actualForward}");
    }

    // ✅ Test GetMatrix() Produces Correct Transform Matrix
    [Fact]
    public void Transform_GetMatrix_ValidTransformation()
    {
        var transform = new Transform();
        transform.Translate(new Vector3(1, 2, 3));
        transform.Rotate(Vector3.UnitY, 90);
        transform.ScaleBy(new Vector3(2, 2, 2));

        var matrix = transform.GetMatrix();

        var transformedPoint = Vector3.Transform(Vector3.UnitZ, matrix);
        var expectedPoint = new Vector3(3, 2, 3); // Scaling affects translation too

        Assert.True(Vector3.Distance(transformedPoint, expectedPoint) < Tolerance);
    }

    // ✅ Test Parent-Child Scale Inheritance
    [Fact]
    public void Transform_SetParent_WithScaling_CorrectWorldScale()
    {
        var parent = new Transform();
        parent.ScaleBy(new Vector3(2, 2, 2));

        var child = new Transform();
        child.ScaleBy(new Vector3(1, 0.5f, 1));
        child.SetParent(parent);

        Assert.Equal(new Vector3(2, 1, 2), child.GetWorldScale()); // Child should inherit scale from parent
    }

    // ✅ Test World Rotation with Parent
    [Fact]
    public void Transform_SetParent_WithRotation_CorrectWorldRotation()
    {
        var parent = new Transform();
        parent.Rotate(Vector3.UnitY, 90);

        var child = new Transform();
        child.Rotate(Vector3.UnitY, 45);
        child.SetParent(parent);

        var expectedRotation = Quaternion.CreateFromAxisAngle(Vector3.UnitY, MathF.PI / 4 + MathF.PI / 2);
        Assert.True(Quaternion.Dot(child.GetWorldRotation(), expectedRotation) > 0.99f);
    }
}