using GameEngine.ECS;
using GameEngine.ECS.Components;
using Newtonsoft.Json;

namespace GameEngine;

public sealed class Scene
{
    public static readonly Scene Empty = new();

    public HashSet<GameObject> gameObjects {  get; private set; }

    public Camera Camera { get; private set; }

    public static Scene Current { get; private set; }

    public Scene()
    {
        gameObjects = new();
        Current = this;
    }

    public void RemoveObject(GameObject obj)
    {
        gameObjects.Remove(obj);
    }
    public void AddObject(GameObject obj)
    {
        if (obj.HasComponent<Camera>())
        {
            Camera = obj.GetComponent<Camera>();
        }
        gameObjects.Add(obj);
    }
    public override string ToString()
    {
        return $$"""Scene(){{{gameObjects}}}""";
    }
}