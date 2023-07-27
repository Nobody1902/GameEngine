using GLFW;
using static GameEngine.OpenGL.GL;
using System.Drawing;

namespace GameEngine.Rendering;

internal static class Window
{
    public static string _title { get; private set; }
    public static Vector2 _size { get; private set; }

    public static GLFW.Window _window;

    public static int _samples = 8;

    public static void Load(string title, Vector2 size, int samples = 8)
    {
        _title = title;
        _size = size;


        Logger.Log("Creating window...");
        if (CreateWindow() == 1)
        {
            Logger.Error("Couldn't create GLFW window.");
        }
        _samples = samples;
        if (_samples > 8)
        {
            Logger.Warning("The sample count might effect performance.");
        }
    }

    private static int CreateWindow()
    {
        Glfw.Init();

        // Setting up the window
        Glfw.WindowHint(Hint.ContextVersionMajor, 3);
        Glfw.WindowHint(Hint.ContextVersionMinor, 3);
        Glfw.WindowHint(Hint.Samples, _samples);
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

    public static void CloseWindow()
    {
        Glfw.Terminate();
    }

    public static bool ShouldClose()
    {
        return Glfw.WindowShouldClose(_window);
    }

    public static void PollEvents()
    {
        Glfw.PollEvents();
    }
    public static void SetTitle(string title)
    {
        Glfw.SetWindowTitle(_window, title);
    }

    public static void Show()
    {
        _window.Opacity = 1;
    }

}