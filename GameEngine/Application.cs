using GameEngine.ECS;
using GameEngine.Rendering;
using GLFW;

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

        string vertex = "#version 330 core" + "\n" +
            "layout(location = 0) in vec3 aPosition;" + "\n" +
            "layout(location = 1) in vec3 aNormal;" + "\n" +
            "uniform mat4 u_proj;" + "\n" +
            "uniform mat4 u_model;" + "\n" +

            "out vec3 oNormal;" + "\n" +

            "void main()" + "\n" +
            "{" + "\n" +
                "oNormal = aNormal;" + "\n" +
                "gl_Position = u_proj * u_model * vec4(aPosition, 1.0);" + "\n" +
            "}";

        string fragment = "#version 330 core" + "\n" +

            "uniform vec4 u_color;" + "\n" +

            "in vec3 oNormal;" + "\n" +

            "out vec4 FragColor;" + "\n" +

            "void main()" + "\n" +
            "{" + "\n" +
                "FragColor = vec4(abs(oNormal), 1.0);" + "\n" +
            "}";

        File.WriteAllText("shaders/default.vert", vertex);
        File.WriteAllText("shaders/default.frag", fragment);

        _window = new Window(_title, _size, 8);

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

        Logger.Log("Loading...");

        _renderer.OnLoad();
        Input.OnLoad();

        Logger.Log("Starting...");
        // Reset Time
        Glfw.Time = 0;
        double lastTime = Glfw.Time;
        double prevTime = 0.0;
        double currentTime = 0.0;
        double timeDiffrance = 0.0;
        uint counter = 0;

        while (!_window.ShouldClose())
        {
            currentTime = Glfw.Time;

            timeDiffrance = currentTime - prevTime;
            counter++;
            if(timeDiffrance >= 1f / 30f)
            {
                double FPS = (1f / timeDiffrance) * counter;
                float fps = MathF.Round((float)FPS, 2);
                Time._SetFPS(fps);

                prevTime = currentTime;
                counter = 0;
            }
            Time._SetDeltaTime(DeltaTime);

            Update();

            _window.PollEvents();

            Render();

            DeltaTime = (float)(Glfw.Time - lastTime) / 1000000.0f;
            lastTime = Glfw.Time;
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