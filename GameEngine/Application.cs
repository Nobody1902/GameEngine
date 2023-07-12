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


        string vertex = """
            #version 330 core

            layout (location = 0) in vec3 aPos;
            layout (location = 1) in vec3 aNormal;

            void main()
            {
                gl_Position = vec4(aPos, 0.0);
            }
            """;
        string fragment = """
            #version 330 core

            out vec4 FragColor;

            void main()
            {
                FragColor = vec4(255.0f, 0.0f, 0.0f, 1.0f);
            }
            """;

        _shader = new Shader(vertex, fragment, true);
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

        _renderer.OnLoad();


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
    }

    private void Render()
    {
        _renderer.Render();
    }
}