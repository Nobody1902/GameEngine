﻿using static GameEngine.OpenGL.GL;

namespace GameEngine.Rendering;

public class Shader
{
    private string _vertex;
    private string _fragment;

    public uint ProgramID { get; private set; }

    public Shader(string vertex, string fragment)
    {
        _vertex = vertex;
        _fragment = fragment;
    }
    public void Load()
    {
        uint vs, fs;
        vs = glCreateShader(GL_VERTEX_SHADER);
        glShaderSource(vs, _vertex);
        glCompileShader(vs);

        // Check for compilation errors
        int[] status = glGetShaderiv(vs, GL_COMPILE_STATUS, 1);
        if (status[0] == 0)
        {
            throw new Exception(glGetShaderInfoLog(vs));
        }

        fs = glCreateShader(GL_FRAGMENT_SHADER);
        glShaderSource(fs, _fragment);
        glCompileShader(fs);

        // Check for compilation errors
        status = glGetShaderiv(fs, GL_COMPILE_STATUS, 1);
        if (status[0] == 0)
        {
            throw new Exception(glGetShaderInfoLog(fs));
        }

        ProgramID = glCreateProgram();
        glAttachShader(vs, ProgramID);
        glAttachShader(fs, ProgramID);

        glLinkProgram(ProgramID);

        // Delete shaders
        glDetachShader(ProgramID, vs);
        glDetachShader(ProgramID, fs);

        glDeleteShader(vs);
        glDeleteShader(fs);
    }
    public void Use()
    {
        glUseProgram(ProgramID);
    }
    private float[] GetMatrix4x4Values(Matrix4x4 m)
    {
        return new float[]
        {
        m.M11, m.M12, m.M13, m.M14,
        m.M21, m.M22, m.M23, m.M24,
        m.M31, m.M32, m.M33, m.M34,
        m.M41, m.M42, m.M43, m.M44
        };
    }
    public void SetMatrix4x4(string uniformName, Matrix4x4 matrix)
    {
        int location = glGetUniformLocation(ProgramID, uniformName);
        glUniformMatrix4fv(location, 1, false, GetMatrix4x4Values(matrix));
    }
    public void SetVec3(string uniformName, Vector3 v)
    {
        int location = glGetUniformLocation(ProgramID, uniformName);
        glUniform3f(location, v.X, v.Y, v.Z);
    }
    public void SetVec2(string uniformName, Vector2 v)
    {
        int location = glGetUniformLocation(ProgramID, uniformName);
        glUniform2f(location, v.X, v.Y);
    }
    public void SetFloat(string uniformName,float v)
    {
        int location = glGetUniformLocation(ProgramID, uniformName);
        glUniform1f(location, v);
    }
}