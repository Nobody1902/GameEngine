using GameEngine.ECS.Components;

namespace GameEngine.ECS;

public sealed class Transform : Component
{
    public Vector3 position {  get; set; }
    public Vector3 rotation { get; set; }
    public Vector3 scale { get; set; }

    public Vector3 forward;
    public Vector3 right;
    public Vector3 up;

    public Transform(GameObject gameObject) : base(gameObject)
    {
        position = Vector3.Zero;
        rotation = Vector3.Zero;
        scale = Vector3.One;

        up = new(0, 1, 0);
        forward = new(0, 0, 1);
        right = new(1, 0, 0);
    }
    public override void Update()
    {
        var yaw = rotation.X;
        var pitch = rotation.Y;
        var roll = rotation.Z;

        forward.X = MathF.Cos(pitch) * MathF.Sin(yaw);
        forward.Y = -MathF.Sin(pitch);
        forward.Z = MathF.Cos(pitch) * MathF.Cos(yaw);

        right.X = MathF.Cos(yaw);
        right.Y = 0;
        right.Z = -MathF.Sin(yaw);

        up = Vector3.Cross(forward, right);

        forward = Round(forward);
        right = Round(right);
        up = Round(up);
    }

    static Vector3 Round(Vector3 v)
    {
        var x = float.Round(v.X, 2);
        var y = float.Round(v.Y, 2);
        var z = float.Round(v.Z, 2);
        return new(x, y, z);
    }
    public Matrix4x4 GetModelMatrix()
    {
        Matrix4x4 translation = Matrix4x4.CreateTranslation(position);

        // Calculate the rotation matrix (with radians)
        float x = EngineMath.Radians(this.rotation.X);
        float y = EngineMath.Radians(this.rotation.Y);
        float z = EngineMath.Radians(this.rotation.Z);

        Matrix4x4 rotationX = Matrix4x4.CreateRotationX(x);
        Matrix4x4 rotationY = Matrix4x4.CreateRotationY(y);
        Matrix4x4 rotationZ = Matrix4x4.CreateRotationZ(z);

        Matrix4x4 rotation = rotationX * rotationY * rotationZ;

        Matrix4x4 scale = Matrix4x4.CreateScale(this.scale, transform.position);

        return translation * rotation * scale;
    }
}
