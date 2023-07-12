using static GameEngine.OpenGL.GL;
using GameEngine.ECS.Components;
using System.Runtime.InteropServices;

namespace GameEngine.Rendering;

public struct Mesh
{
    public float[] _verticies { get; private set; }
    public uint[] _indices { get; private set; }

    public uint vao { get; private set; }
    public uint vbo { get; private set; }
    public uint ibo { get; private set; }
    
    public unsafe Mesh(float[] verts, uint[] indicies)
    {
        _verticies = verts;
        _indices = indicies;

        vao = glGenVertexArray();

        glBindVertexArray(vao);


        vbo = glGenBuffer();
        glBindBuffer(GL_ARRAY_BUFFER, vbo);
        fixed (float* v = &_verticies[0])
        {
            glBufferData(GL_ARRAY_BUFFER, sizeof(float) * _verticies.Length, v, GL_STATIC_DRAW);
        }

        glVertexAttribPointer(0, 3, GL_FLOAT, false, 6 * sizeof(float), (void*)0);
        glVertexAttribPointer(1, 3, GL_FLOAT, false, 6 * sizeof(float), 3*sizeof(float));
        glEnableVertexAttribArray(0);
        glEnableVertexAttribArray(1);

        ibo = glGenBuffer();
        glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, ibo);
        fixed (uint* i = &indicies[0])
        {
            glBufferData(GL_ELEMENT_ARRAY_BUFFER, indicies.Length, i, GL_STATIC_DRAW);
        }


        glBindBuffer(GL_ARRAY_BUFFER, 0);
        glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, 0);
        glBindVertexArray(0);

    }
}