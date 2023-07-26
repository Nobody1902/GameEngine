namespace GameEngine;

public static class Time
{
    public static float DeltaTime { get { return _deltaTime; } }
    public static float FPS { get { return _FPS; } }

    private static float _deltaTime;
    private static float _FPS;

    public static void _SetDeltaTime(float deltaTime)
    {
        _deltaTime = deltaTime;
    }
    public static void _SetFPS(double fps)
    {
        _FPS = (float)fps;
    }
}