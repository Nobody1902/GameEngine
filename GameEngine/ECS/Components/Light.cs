using GameEngine;
using GameEngine.Rendering;

namespace GameEngine.ECS.Components;

public sealed class Light : Component
{
    public Color Color { get; set; }
    public float Intensity { get; set; } = 1.0f;

    public Light(GameObject gameObject) : base(gameObject)
    {
    }
}