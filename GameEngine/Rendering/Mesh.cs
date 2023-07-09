
namespace GameEngine.Rendering;

public struct Mesh
{
    public float[] _verticies { get; private set; }
    public float[] _indices { get; private set; }
    
    public Mesh(float[] verts, float[] indicies)
    {
        _verticies = verts;
        _indices = indicies;
    }
}