namespace GameEngine.ECS;

public sealed class Transform : Component
{
    public Vector3 position {  get; set; }
    public Quaternion rotation { get; set; }
    public Vector3 scale { get; set; }

    public Transform(GameObject gameObject) : base(gameObject)
    {
        position = Vector3.Zero;
        rotation = Quaternion.Identity;
        scale = Vector3.Zero;
    }

    public Matrix4x4 GetTransformation()
    {
        Matrix4x4 translation = Matrix4x4.CreateTranslation(position);
        Matrix4x4 scale = Matrix4x4.CreateScale(this.scale);
        Matrix4x4 rotation = Matrix4x4.CreateFromQuaternion(this.rotation);

        return translation * rotation * scale;
    }
}
