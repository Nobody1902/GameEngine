using GLFW;
using static GameEngine.OpenGL.GL;

namespace GameEngine.Rendering;

public sealed class Renderer
{
    private Scene _scene;
    private Window _window;

    public Renderer(Window window, Scene scene)
    {
        SetWindow(window);
        SetScene(scene);
    }

    public void SetScene(Scene scene)
    {
        _scene = scene;
    }
    public void SetWindow(Window window)
    {
        _window = window;
    }

    public void Render()
    {
        glClearColor(0, 0, 0, 1);
        glClear(GL_COLOR_BUFFER_BIT);

        Glfw.SwapBuffers(_window._window);
    }
}