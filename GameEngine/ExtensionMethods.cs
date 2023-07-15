using GameEngine.Rendering;
using System.Runtime.CompilerServices;

namespace GameEngine;

public static class ExtensionMethods
{
    public static Vector3 Normalized(this Vector3 v)
    {
        float w = MathF.Sqrt(v.X*v.X + v.Y*v.Y + v.Z*v.Z);
        return new Vector3(v.X/w, v.Y/w, v.Z/w);
    }
    public static Vector2 Normalized(this Vector2 v)
    {
        float w = MathF.Sqrt(v.X * v.X + v.Y * v.Y);
        return new Vector2(v.X / w, v.Y / w);
    }

    public static bool IsEmpty(this string s)
    {
        return string.IsNullOrEmpty(s)||string.IsNullOrWhiteSpace(s);
    }
    public static string Clean(this string s)
    {
        return s.Replace("\r", "").Replace(" ", "").Replace("\t", "");
    }
    public static Vector4 MultiplyMatrix(this Matrix4x4 matrix, Vector4 vector)
    {
        return new Vector4(
            matrix.M11 * vector.X + matrix.M12 * vector.Y + matrix.M13 * vector.Z + matrix.M14 * vector.W,
            matrix.M21 * vector.X + matrix.M22 * vector.Y + matrix.M23 * vector.Z + matrix.M24 * vector.W,
            matrix.M31 * vector.X + matrix.M32 * vector.Y + matrix.M33 * vector.Z + matrix.M34 * vector.W,
            matrix.M41 * vector.X + matrix.M42 * vector.Y + matrix.M43 * vector.Z + matrix.M44 * vector.W
        );
    }
    public static Vector3 MultiplyMatrix(this Matrix4x4 matrix, Vector3 vector)
    {
        return new Vector3(
            matrix.M11 * vector.X + matrix.M12 * vector.Y + matrix.M13 * vector.Z + matrix.M14,
            matrix.M21 * vector.X + matrix.M22 * vector.Y + matrix.M23 * vector.Z + matrix.M24,
            matrix.M31 * vector.X + matrix.M32 * vector.Y + matrix.M33 * vector.Z + matrix.M34
        );
    }
    public static Vector3 Vector3(this Vector3 vector, Vector4 vec4)
    {
        vector = new(vec4.X, vec4.Y, vec4.Z);
        return vector;
    }
    public static Vector4 ToVector4(this Color c)
    {
        return new(c.R, c.G, c.B, c.A);
    }
}