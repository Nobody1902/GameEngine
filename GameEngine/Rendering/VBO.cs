using static GameEngine.OpenGL.GL;

namespace GameEngine.Rendering;

public sealed class VBO
{
    public uint ID { get; init; }
    public unsafe VBO(float[] verticies)
    {
        ID = glGenBuffer();
        glBindBuffer(GL_ARRAY_BUFFER, ID);
        fixed (float* v = &verticies[0])
        {
            glBufferData(GL_ARRAY_BUFFER, sizeof(float) * verticies.Length, v, GL_STATIC_DRAW);
        }

    }
    public void Bind()
    {
        glBindBuffer(GL_ARRAY_BUFFER, ID);
    }
    public void Unbind()
    {
        glBindBuffer(GL_ARRAY_BUFFER, 0);
    }
    public void Delete()
    {
        glDeleteBuffer(ID);
    }
}