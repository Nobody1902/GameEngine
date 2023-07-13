namespace GameEngine;

internal static class ExtensionMethods
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
}