using GameEngine.ECS;
using GameEngine.Rendering;
using GLFW;
using static GameEngine.OpenGL.GL;

using Window = GameEngine.Rendering.Window;

namespace GameEngine;

public sealed class Application
{
    private string _title;
    private Vector2 _size;

    private Window _window { get; init; }
    private Renderer _renderer { get; init; }
    private Scene _scene { get; set; }

    public Application(string title, Vector2 size)
    {
        _title = title;
        _size = size;


        _window = new Window(_title, _size);

        _scene = Scene.Empty;

        _renderer = new(_window, _scene);
    }

    public void SetScene(Scene scene)
    {
        _scene = scene;
        _renderer.SetScene(_scene);
    }

    public void Run()
    {
        _window.Show();

        _renderer.OnLoad();

        while (!_window.ShouldClose())
        {
            Update();

            _window.PollEvents();

            Render();
        }

        // No need to call CloseWindow as it should happen automatically
    }

    private void Update()
    {
        foreach(GameObject obj in _scene.gameObjects)
        {
            // Remove objects that are destroyed
            if (obj._destroyed)
            {
                _scene.RemoveObject(obj);
            }
            // Only update enabled gameObjects
            if (!obj.enabled) { continue; }

            obj.Update();
        }
    }

    private void Render()
    {
        _renderer.Render();
    }
}