using GameEngine.ECS.Components;
using Newtonsoft.Json;

namespace GameEngine.ECS;

public sealed class Transform : Component
{
    public Vector3 position { get; set; }
    public Vector3 rotation { get; set; }
    public Vector3 scale { get; set; }

    [JsonIgnore]
    private Vector3 _forward;
    [JsonIgnore]
    private Vector3 _right;
    [JsonIgnore]
    private Vector3 _up;

    [JsonIgnore]
    public Vector3 forward { get { return _forward; } }
    [JsonIgnore]
    public Vector3 right { get { return _right; } }
    [JsonIgnore]
    public Vector3 up { get { return _up; } }

    public Transform(GameObject gameObject) : base(gameObject)
    {
        position = Vector3.Zero;
        rotation = Vector3.Zero;
        scale = Vector3.One;

        _up = new(0, 1, 0);
        _forward = new(0, 0, 1);
        _right = new(1, 0, 0);
    }
    public override void Update()
    {
        CalculateVectors();
    }

    private void CalculateVectors()
    {
        var yaw = rotation.X;
        var pitch = rotation.Y;
        var roll = rotation.Z;

        _forward.X = MathF.Cos(pitch) * MathF.Sin(yaw);
        _forward.Y = -MathF.Sin(pitch);
        _forward.Z = MathF.Cos(pitch) * MathF.Cos(yaw);

        _right.X = MathF.Cos(yaw);
        _right.Y = 0;
        _right.Z = -MathF.Sin(yaw);

        _up = Vector3.Cross(_forward, _right);

        _forward = Round(_forward);
        _right = Round(_right);
        _up = Round(_up);
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
        var pos = position * new Vector3(-1, 1 ,1);
        Matrix4x4 translation = Matrix4x4.CreateTranslation(pos);

        // Calculate the rotation matrix (with radians)
        float x = EngineMath.Radians(this.rotation.X);
        float y = EngineMath.Radians(this.rotation.Y);
        float z = EngineMath.Radians(this.rotation.Z);

        Matrix4x4 rotationX = Matrix4x4.CreateRotationX(x);
        Matrix4x4 rotationY = Matrix4x4.CreateRotationY(y);
        Matrix4x4 rotationZ = Matrix4x4.CreateRotationZ(z);

        Matrix4x4 rotation = rotationX * rotationY * rotationZ;

        Matrix4x4 scale = Matrix4x4.CreateScale(this.scale, pos);

        return (translation * scale) * rotation;
    }
}
