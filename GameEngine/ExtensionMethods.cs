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
}