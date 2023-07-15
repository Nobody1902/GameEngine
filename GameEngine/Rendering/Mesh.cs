using System.Runtime.InteropServices;
using static GameEngine.OpenGL.GL;


namespace GameEngine.Rendering;

public struct Vertex
{
    public float X;
    public float Y;
    public float Z;

    public float X_normal;
    public float Y_normal;
    public float Z_normal;

    public Vertex(Vector3 position)
    {
        X = position.X;
        Y = position.Y;
        Z = position.Z;

        X_normal = 0;
        Y_normal = 0;
        Z_normal = 0;
    }
    public Vertex(float x, float y, float z)
    {
        X = x;
        Y = y;
        Z = z;

        X_normal = 0;
        Y_normal = 0;
        Z_normal = 0;
    }
    public Vertex(Vector3 position, Vector3 normal)
    {
        X = position.X;
        Y = position.Y;
        Z = position.Z;

        X_normal = normal.X;
        Y_normal = normal.Y;
        Z_normal = normal.Z;
    }

    public Vector3 GetPosition()
    {
        return new(X, Y, Z);
    }
    public Vector3 GetNormal()
    {
        return new(X_normal, Y_normal, Z_normal);
    }
}

public struct Mesh
{
    public Vertex[] _verticies { get; private set; }
    public uint[] _indices { get; private set; }

    public uint _vao { get; private set; }
    public uint _vbo { get; private set; }
    public uint _ibo { get; private set; }
    
    public unsafe Mesh(float[] verts, uint[] indicies)
    {
        // Initilize
        _verticies = ToVertexArray(verts);
        _indices = indicies;
        CalculateNormals();

//      -----------------------------

        _vao = glGenVertexArray();
        glBindVertexArray(_vao);

        _vbo = glGenBuffer();
        glBindBuffer(GL_ARRAY_BUFFER, _vbo);

        // Buffer the verticies and the Vnormals in the _vbo

        float[] vertsNormals = ToFloatArray(_verticies);

        fixed (float* v = &vertsNormals[0])
        {
            glBufferData(GL_ARRAY_BUFFER, sizeof(float) * vertsNormals.Length, v, GL_STATIC_DRAW);
        }

        // Setup the shader layouts
        // - Vertex positions
        glVertexAttribPointer(0, 3, GL_FLOAT, false, 6 * sizeof(float), (void*)0);
        // - Vertex normals
        glVertexAttribPointer(1, 3, GL_FLOAT, false, 6 * sizeof(float), 3*sizeof(float));

        // Enable both
        glEnableVertexAttribArray(0);
        glEnableVertexAttribArray(1);

        // Create the _ibo
        _ibo = glGenBuffer();
        glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, _ibo);

        // Buffer the indicies in the _ibo
        fixed (uint* i = &indicies[0])
        {
            glBufferData(GL_ELEMENT_ARRAY_BUFFER, indicies.Length, i, GL_STATIC_DRAW);
        }

        // Unbind everything
        glBindBuffer(GL_ARRAY_BUFFER, 0);
        glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, 0);
        glBindVertexArray(0);

    }
    private static float[] ToFloatArray(Vertex[] vertices)
    {
        if(vertices.Length == 0) return Array.Empty<float>();

        List<float> floats = new();

        foreach(Vertex v in vertices)
        {
            floats.Add(v.X);
            floats.Add(v.Y);
            floats.Add(v.Z);

            floats.Add(v.X_normal);
            floats.Add(v.Y_normal);
            floats.Add(v.Z_normal);
        }


        return floats.ToArray();
    }
    private Vertex[] ToVertexArray(float[] verts)
    {
        List<Vertex> vertsList = new();

        for (int i = 0; i < verts.Length; i+=3)
        {
            float v = verts[i];
            float v1 = verts[i + 1];
            float v2 = verts[i + 2];

            Vertex vertex = new(v, v1, v2);
            vertsList.Add(vertex);
        }

        return vertsList.ToArray();
    }
    public void CalculateNormals()
    {
        // https://gamedev.stackexchange.com/questions/152991/how-can-i-calculate-normals-using-a-vertex-and-index-buffer
        
        for (int i = 0; i < _indices.Length; i+=3)
        {
            int v1 = GetVertexPosFromIndex(_indices[i]);
            int v2 = GetVertexPosFromIndex(_indices[i + 1]);
            int v3 = GetVertexPosFromIndex(_indices[i + 2]);

            Vertex vertex1 = _verticies[v1];
            Vertex vertex2 = _verticies[v2];
            Vertex vertex3 = _verticies[v3];

            Vector3 edgeAB = vertex2.GetPosition() - vertex1.GetPosition();
            Vector3 edgeAC = vertex3.GetPosition() - vertex1.GetPosition();

            var areaWeightedNormal = Vector3.Cross(edgeAB, edgeAC);

            vertex1.X_normal += areaWeightedNormal.X;
            vertex1.Y_normal += areaWeightedNormal.Y;
            vertex1.Z_normal += areaWeightedNormal.Z;

            vertex2.X_normal += areaWeightedNormal.X;
            vertex2.Y_normal += areaWeightedNormal.Y;
            vertex2.Z_normal += areaWeightedNormal.Z;

            vertex3.X_normal += areaWeightedNormal.X;
            vertex3.Y_normal += areaWeightedNormal.Y;
            vertex3.Z_normal += areaWeightedNormal.Z;

            // Set the normals
            _verticies[v1] = vertex1;
            _verticies[v2] = vertex2;
            _verticies[v3] = vertex3;
        }
        Console.WriteLine();
        for (int v = 0; v < _verticies.Length; v++)
        {
            Vertex vertex = _verticies[v];
            Vector3 normal = vertex.GetNormal();

            _verticies[v].X_normal = normal.X;
            _verticies[v].Y_normal = normal.Y;
            _verticies[v].Z_normal = normal.Z;

            if (_verticies[v].GetNormal().X == float.NaN)
            {
                _verticies[v].X_normal = 0;
                _verticies[v].Y_normal = 0;
                _verticies[v].Z_normal = 0;
            }
            Console.WriteLine(_verticies[v].GetNormal());
        }
    }
    private static int GetVertexPosFromIndex(uint index)
    {
        return Convert.ToInt32(index);
    }
}