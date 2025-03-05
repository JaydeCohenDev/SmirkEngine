namespace SmirkEngine.Core;

using System.Numerics;

public class Transform
{
    // Position, Rotation, and Scale
    public Vector3 Position { get; set; } = Vector3.Zero;
    public Quaternion Rotation { get; set; } = Quaternion.Identity;
    public Vector3 Scale { get; set; } = Vector3.One;

    // Parent and Children (for hierarchy support)
    public Transform? Parent { get; private set; }
    private readonly List<Transform> _children = [];

    public Vector3 Forward => Vector3.Normalize(Vector3.Transform(Vector3.UnitZ, Rotation));
    public Vector3 Right   => Vector3.Normalize(Vector3.Transform(Vector3.UnitX, Rotation));
    public Vector3 Up      => Vector3.Normalize(Vector3.Transform(Vector3.UnitY, Rotation));

    public Matrix4x4 GetMatrix()
    {
        var translation = Matrix4x4.CreateTranslation(Position);
        var rotation = Matrix4x4.CreateFromQuaternion(Rotation);
        var scale = Matrix4x4.CreateScale(Scale);

        var localMatrix = scale * rotation * translation;

        return Parent != null ? localMatrix * Parent.GetMatrix() : localMatrix;
    }

    public void SetParent(Transform newParent)
    {
        Parent?._children.Remove(this);

        Parent = newParent;
        newParent._children.Add(this);
    }

    public Vector3 GetWorldPosition()
    {
        if (Parent == null)
            return Position;

        // Apply the parent's rotation to the child's local position **correctly**
        var rotatedPosition = Vector3.Transform(Position, Parent.Rotation);
    
        // Compute final world position by adding the parent's world position
        var finalWorldPosition = Parent.GetWorldPosition() + rotatedPosition;

        return finalWorldPosition;
    }

    public Quaternion GetWorldRotation()
    {
        return Parent != null ? Quaternion.Concatenate(Rotation, Parent.GetWorldRotation()) : Rotation;
    }

    public Vector3 GetWorldScale()
    {
        return Parent != null ? Parent.GetWorldScale() * Scale : Scale;
    }

    public void Translate(Vector3 offset)
    {
        Position += offset;
    }

    public void Rotate(Vector3 axis, float angleDegrees)
    {
        var radians = MathF.PI / 180 * angleDegrees;
        var deltaRotation = Quaternion.CreateFromAxisAngle(Vector3.Normalize(axis), radians);

        Rotation = Quaternion.Concatenate(Rotation, deltaRotation);
    }

    public void Rotate(float yawDegrees, float pitchDegrees, float rollDegrees)
    {
        var yaw   = MathF.PI / 180 * yawDegrees;
        var pitch = MathF.PI / 180 * pitchDegrees;
        var roll  = MathF.PI / 180 * rollDegrees;

        var q = Quaternion.CreateFromYawPitchRoll(yaw, pitch, roll);
        Rotation = Quaternion.Concatenate(Rotation, q);
    }

    public void ScaleBy(Vector3 scaleFactor)
    {
        Scale *= scaleFactor;
    }
}