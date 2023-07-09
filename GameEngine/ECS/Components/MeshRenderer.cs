
using GameEngine.Rendering;

namespace GameEngine.ECS;

public enum MeshUsage
{
    STATIC_DRAW,
    DYNAMIC_DRAW
}
public sealed class MeshRenderer : Component
{
    public MeshUsage MeshUsage { get; private set; }
    public Mesh Mesh { get; private set; }
    //public Shader Shader { get; private set; }
    public Material Material { get; private set; }

    public MeshRenderer(GameObject gameObject) : base(gameObject)
    {
        this.Mesh = new Mesh();
        this.MeshUsage = MeshUsage.STATIC_DRAW;
    }
    public void SetMesh(Mesh mesh, Material material,/* Shader shader, */MeshUsage usage)
    {
        Mesh = mesh;
        MeshUsage = usage;
        Material = material;
    }
}