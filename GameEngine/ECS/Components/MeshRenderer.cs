
using GameEngine.Rendering;

namespace GameEngine.ECS;

public sealed class MeshRenderer : Component
{
    public Mesh Mesh { get; set; }
    public Color Color { get; set; }
    public Shader Shader { get; set; }

    public MeshRenderer(GameObject gameObject) : base(gameObject)
    {
        Color = Color.White;
        Shader = null;
    }

    public void SetMesh(Mesh mesh)
    {
        mesh._Load();
        Mesh = mesh;
    }
    public void SetColor(Color color)
    {
        Color = color;
    }
    public void SetShader(Shader shader)
    {
        Shader = shader;
    }
}