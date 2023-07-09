using System.Runtime.CompilerServices;
using static GameEngine.OpenGL.GL;

namespace GameEngine.Rendering;

public sealed class VAO
{
    public uint ID { get; init; }
    public VAO()
    {
        ID = glGenVertexArray();

    }
    public void LinkVBO(VBO vbo, uint layout)
    {
        vbo.Bind();
        glVertexAttribPointer(layout, 3, GL_FLOAT, false, 0, 0);
        glEnableVertexAttribArray(layout);
        vbo.Unbind();
    }
    public void Bind()
    {
        glBindVertexArray(ID);
    }
    public void Unbind()
    {
        glBindVertexArray(0);
    }
    public void Delete()
    {
        glDeleteVertexArray(ID);
    }

}