namespace GameEngine.ECS;

public sealed class Transform : Component
{
    public Vector3 position {  get; set; }
    public Quaternion rotation { get; set; }
    public Vector3 scale { get; set; }

    public Vector3 forward;
    public Vector3 right;
    public Vector3 up;

    public Transform(GameObject gameObject) : base(gameObject)
    {
        position = Vector3.Zero;
        rotation = Quaternion.Identity;
        scale = Vector3.One;

        up = new(0, 1, 0);
        forward = new(0, 0, 1);
        right = new(1, 0, 0);
    }
    public override void Update()
    {
        Quaternion q = rotation;
        var yaw = MathF.Atan2(2.0f * (q.Y * q.Z + q.W * q.X), q.W * q.W - q.X * q.X - q.Y * q.Y + q.Z * q.Z);
        var pitch = MathF.Asin(-2.0f * (q.X * q.Z - q.W * q.Y));
        var roll = MathF.Atan2(2.0f * (q.X * q.Y + q.W * q.Z), q.W * q.W + q.X * q.X - q.Y * q.Y - q.Z * q.Z);

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
    public Matrix4x4 GetTransformation()
    {
        Matrix4x4 translation = Matrix4x4.CreateTranslation(position);
        Matrix4x4 scale = Matrix4x4.CreateScale(this.scale);
        Matrix4x4 rotation = Matrix4x4.CreateFromQuaternion(this.rotation);

        if(Quaternion.Zero == Quaternion.CreateFromRotationMatrix(rotation))
        {
            rotation = Matrix4x4.Identity;
        }

        return translation * rotation * scale;
    }
}
