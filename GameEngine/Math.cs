namespace GameEngine;

public static class EngineMath
{
    // Constants
    public const double PI = 3.14159265358979323846;
    public const double E = 2.71828182845904523536;

    // EngineMath functions
    public static float Radians(float degrees)
    {
        return (float)(degrees * PI / 180.0);
    }

    public static float Degrees(float radians)
    {
        return (float)(radians * 180.0 / PI);
    }
}