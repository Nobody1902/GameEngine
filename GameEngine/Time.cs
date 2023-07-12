namespace GameEngine;

public static class Time
{
    public static float DeltaTime { get { return _deltaTime; } }

    private static float _deltaTime;

    public static void _SetDeltaTime(float deltaTime)
    {
        _deltaTime = deltaTime;
    }
}