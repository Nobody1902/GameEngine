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

    private Shader _shader { get; set; }

    public Application(string title, Vector2 size)
    {
        _title = title;
        _size = size;


        _window = new Window(_title, _size);

        _scene = Scene.Empty;

        _shader = Shader.LoadShader("shaders/default.vert", "shaders/default.frag");
        _renderer = new(_window, _scene, _shader);
    }
    public void SetShader(Shader shader)
    {
        _shader = shader;
        _renderer.SetShader(shader);
    }

    public void SetScene(Scene scene)
    {
        _scene = scene;
        _renderer.SetScene(_scene);
    }
    public float DeltaTime { get; private set; }
    public void Run()
    {
        _window.Show();

        // Set the key callback function
        Glfw.SetKeyCallback(_window._window, Input._keyCallback);
        Glfw.SetMouseButtonCallback(_window._window, Input._mouseButtonCallback);
        Glfw.SetCursorPositionCallback(_window._window, Input._mouseCallback);

        _renderer.OnLoad();
        Input.OnLoad();



        // Reset Time
        Glfw.Time = 0;
        double lastTime = Glfw.Time;

        while (!_window.ShouldClose())
        {
            Time._SetDeltaTime(DeltaTime);

            Update();

            _window.PollEvents();

            Render();

            DeltaTime = (float)(Glfw.Time - lastTime) / 1000000.0f;
            lastTime = Glfw.Time;
        }
        _window.CloseWindow();
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
                _renderer.SetScene(_scene);
            }
            // Only update enabled gameObjects
            if (!obj.enabled) { continue; }

            obj.Update();
        }
        Input.Update();
    }

    private void Render()
    {
        _renderer.Render();
    }
}