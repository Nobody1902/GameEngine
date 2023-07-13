
using GameEngine.Rendering;

namespace GameEngine.ECS;

public sealed class MeshRenderer : Component
{
    public Mesh Mesh { get; private set; }

    public Color Color { get; private set; }

    public MeshRenderer(GameObject gameObject) : base(gameObject)
    {
        Color = Color.White;
    }

    public void SetMesh(Mesh mesh)
    {
        Mesh = mesh;
    }
    public void SetColor(Color color)
    {
        Color = color;
    }
}