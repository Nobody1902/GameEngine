using GLFW;
using static GameEngine.OpenGL.GL;
using System.Xml.Serialization;
using System.Drawing;

namespace GameEngine.Rendering;

public sealed class Window : IDisposable
{
    public string _title { get; init; }
    public Vector2 _size { get; init; }

    public GLFW.Window _window;

    public static Window window;

    public int samples = 8;

    public Window(string title, Vector2 size, int samples = 8)
    {
        _title = title;
        _size = size;


        Logger.Log("Creating window...");
        if (CreateWindow() == 1)
        {
            Logger.Error("Couldn't create GLFW window.");
        }
        window = this;
        this.samples = samples;
        if(this.samples > 8)
        {
            Logger.Warning("The sample count might effect performance.");
        }
    }

    public void Dispose()
    {
        CloseWindow();
    }

    private int CreateWindow()
    {
        Glfw.Init();

        // Setting up the window
        Glfw.WindowHint(Hint.ContextVersionMajor, 3);
        Glfw.WindowHint(Hint.ContextVersionMinor, 3);
        Glfw.WindowHint(Hint.Samples, samples);
        Glfw.WindowHint(Hint.OpenglProfile, Profile.Core);

        Glfw.WindowHint(Hint.Doublebuffer, true);

        Glfw.WindowHint(Hint.Focused, true);
        Glfw.WindowHint(Hint.Resizable, false);

        _window = Glfw.CreateWindow((int)_size.X, (int)_size.Y, _title, GLFW.Monitor.None, GLFW.Window.None);

        _window.Opacity = 0;


        if (_window == GLFW.Window.None)
        {
            // Something is wrong
            return 1;
        }

        // Center the window

        Rectangle monitorSize = Glfw.PrimaryMonitor.WorkArea;
        int screenCenterX = (monitorSize.Width - (int)_size.X) / 2;
        int screenCenterY = (monitorSize.Height - (int)_size.Y) / 2;

        Glfw.SetWindowPosition(_window, screenCenterX, screenCenterY);


        Glfw.MakeContextCurrent(_window);
        Import(Glfw.GetProcAddress);

        glViewport(0, 0, (int)_size.X, (int)_size.X); // Setup the opengl viewport

        Glfw.SwapInterval(1); // Turn on VSync

        return 0;
    }

    public void CloseWindow()
    {
        Glfw.Terminate();
    }

    public bool ShouldClose()
    {
        return Glfw.WindowShouldClose(_window);
    }

    public void PollEvents()
    {
        Glfw.PollEvents();
    }
    public void SetTitle(string title)
    {
        Glfw.SetWindowTitle(_window, title);
    }

    public void Show()
    {
        _window.Opacity = 1;
    }

}