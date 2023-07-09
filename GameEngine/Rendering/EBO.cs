using static GameEngine.OpenGL.GL;

namespace GameEngine.Rendering;

public sealed class EBO
{
    public uint ID { get; init; }
    public unsafe EBO(float[] indicies)
    {
        ID = glGenBuffer();
        glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, ID);
        fixed (float* v = &indicies[0])
        {
            glBufferData(GL_ELEMENT_ARRAY_BUFFER, sizeof(float) * indicies.Length, v, GL_STATIC_DRAW);
        }

    }
    public void Bind()
    {
        glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, ID);
    }
    public void Unbind()
    {
        glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, 0);
    }
    public void Delete()
    {
        glDeleteBuffer(ID);
    }
}