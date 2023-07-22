using System.Runtime.Serialization;
using static GameEngine.OpenGL.GL;


namespace GameEngine.Rendering;

public struct Vertex : ISerializable
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
    public Vertex(SerializationInfo info, StreamingContext context)
    {
        X = info.GetSingle("x");
        Y = info.GetSingle("y");
        Z = info.GetSingle("z");

        X_normal = 0;
        Y_normal = 0;
        Z_normal = 0;
    }
    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        info.AddValue(nameof(X), X);
        info.AddValue(nameof(Y), Y);
        info.AddValue(nameof(Z), Z);
    }
}
[Serializable]
public struct Mesh : ISerializable
{
    public Vertex[] _verticies { get; private set; }
    public uint[] _indices { get; private set; }

    public string Location;

    public uint _vao { get; private set; }
    public uint _vbo { get; private set; }
    public uint _ibo { get; private set; }
    
    public unsafe Mesh(float[] verts, uint[] indicies)
    {
        Location = "";
        // Initilize
        _verticies = ToVertexArray(verts);
        _indices = indicies;
        CalculateFlatNormals();
        FlipNormals();
    }
    public Mesh(float[] verts, uint[] indicies, string source)
    {
        // Initilize
        Location = source;
        _verticies = ToVertexArray(verts);
        _indices = indicies;
        CalculateFlatNormals();
        FlipNormals();
    }
    public unsafe void _Load()
    {
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
        glVertexAttribPointer(1, 3, GL_FLOAT, false, 6 * sizeof(float), 3 * sizeof(float));

        // Enable both
        glEnableVertexAttribArray(0);
        glEnableVertexAttribArray(1);

        // Create the _ibo
        _ibo = glGenBuffer();
        glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, _ibo);

        // Buffer the indicies in the _ibo
        fixed (uint* i = &_indices[0])
        {
            glBufferData(GL_ELEMENT_ARRAY_BUFFER, _indices.Length, i, GL_STATIC_DRAW);
        }

        // Unbind everything
        glBindBuffer(GL_ARRAY_BUFFER, 0);
        glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, 0);
        glBindVertexArray(0);
    }

    public void CalculateFlatNormals()
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
        }
    }

    public void CalculateSmoothNormals()
    {
        // Calculate the normal of each triangle and add it to each vertex that is part of the triangle
        for (int i = 0; i < _indices.Length; i += 3)
        {
            int i1 = GetVertexPosFromIndex(_indices[i]);
            int i2 = GetVertexPosFromIndex(_indices[i + 1]);
            int i3 = GetVertexPosFromIndex(_indices[i + 2]);

            Vector3 v1 = new(_verticies[i1].X, _verticies[i1].Y, _verticies[i1].Z);
            Vector3 v2 = new(_verticies[i2].X, _verticies[i2].Y, _verticies[i2].Z);
            Vector3 v3 = new(_verticies[i3].X, _verticies[i3].Y, _verticies[i3].Z);

            Vector3 normal = Vector3.Cross(v3 - v1, v2 - v1);

            _verticies[i1].X_normal += normal.X;
            _verticies[i1].Y_normal += normal.Y;
            _verticies[i1].Z_normal += normal.Z;

            _verticies[i2].X_normal += normal.X;
            _verticies[i2].Y_normal += normal.Y;
            _verticies[i2].Z_normal += normal.Z;

            _verticies[i3].X_normal += normal.X;
            _verticies[i3].Y_normal += normal.Y;
            _verticies[i3].Z_normal += normal.Z;
        }

        // Normalize all vertex normals
        for (int i = 0; i < _verticies.Length; i++)
        {
            Vector3 normal = _verticies[i].GetNormal().Normalized();
            _verticies[i].X_normal = normal.X;
            _verticies[i].Y_normal = normal.Y;
            _verticies[i].Z_normal = normal.Z;
        }

        // Calculate the weighted average of the normals of all the faces that share each vertex
        for (int i = 0; i < _verticies.Length; i++)
        {
            Vector3 normal = new();
            float totalWeight = 0;

            for (int j = 0; j < _indices.Length; j += 3)
            {
                int i1 = GetVertexPosFromIndex(_indices[j]);
                int i2 = GetVertexPosFromIndex(_indices[j + 1]);
                int i3 = GetVertexPosFromIndex(_indices[j + 2]);

                if (i1 == i || i2 == i || i3 == i)
                {
                    Vector3 faceNormal = new();
                    float weight = 0;

                    if (i1 == i)
                    {
                        Vector3 v1 = new(_verticies[i1].X, _verticies[i1].Y, _verticies[i1].Z);
                        Vector3 v2 = new(_verticies[i2].X, _verticies[i2].Y, _verticies[i2].Z);
                        Vector3 v3 = new(_verticies[i3].X, _verticies[i3].Y, _verticies[i3].Z);

                        faceNormal = Vector3.Cross(v3 - v1, v2 - v1);
                        weight = Vector3.Dot(faceNormal.Normalized(), new Vector3(_verticies[i1].X_normal, _verticies[i1].Y_normal, _verticies[i1].Z_normal).Normalized());
                    }
                    else if (i2 == i)
                    {
                        Vector3 v1 = new(_verticies[i2].X, _verticies[i2].Y, _verticies[i2].Z);
                        Vector3 v2 = new(_verticies[i3].X, _verticies[i3].Y, _verticies[i3].Z);
                        Vector3 v3 = new(_verticies[i1].X, _verticies[i1].Y, _verticies[i1].Z);

                        faceNormal = Vector3.Cross(v3 - v1, v2 - v1);
                        weight = Vector3.Dot(faceNormal.Normalized(), new Vector3(_verticies[i2].X_normal, _verticies[i2].Y_normal, _verticies[i2].Z_normal).Normalized());
                    }
                    else if (i3 == i)
                    {
                        Vector3 v1 = new(_verticies[i3].X, _verticies[i3].Y, _verticies[i3].Z);
                        Vector3 v2 = new(_verticies[i1].X, _verticies[i1].Y, _verticies[i1].Z);
                        Vector3 v3 = new(_verticies[i2].X, _verticies[i2].Y, _verticies[i2].Z);

                        faceNormal = Vector3.Cross(v3 - v1, v2 - v1);
                        weight = Vector3.Dot(faceNormal.Normalized(), new Vector3(_verticies[i3].X_normal, _verticies[i3].Y_normal, _verticies[i3].Z_normal).Normalized());
                    }

                    normal += faceNormal * weight;
                    totalWeight += weight;
                }
            }

            if (totalWeight > 0)
            {
                normal /= totalWeight;
            }

            normal = normal.Normalized();

            _verticies[i].X_normal = normal.X;
            _verticies[i].Y_normal = normal.Y;
            _verticies[i].Z_normal = normal.Z;
        }
    }

    public void FlipNormals()
    {
        for (int i = 0; i < _verticies.Length; i++)
        {
            Vector3 flipedNormal = -_verticies[i].GetNormal();
            _verticies[i].X_normal = flipedNormal.X;
            _verticies[i].Y_normal = flipedNormal.Y;
            _verticies[i].Z_normal = flipedNormal.Z;
        }
    }

    #region Helper methods
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

    private static Vertex[] ToVertexArray(float[] verts)
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

    private static int GetVertexPosFromIndex(uint index)
    {
        return Convert.ToInt32(index);
    }

    public Mesh(SerializationInfo info, StreamingContext context)
    {
        Location = info.GetString(nameof(Location))!;
        this = MeshLoader.Load(Location);
    }

    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        info.AddValue(nameof(Location), Location);
    }
    #endregion
}