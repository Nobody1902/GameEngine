﻿using GLFW;
using static GameEngine.OpenGL.GL;
using System.Xml.Serialization;
using System.Drawing;

namespace GameEngine.Rendering;

public sealed class Window : IDisposable
{
    private string _title;
    private Vector2 _size;

    public GLFW.Window _window;

    public Window(string title, Vector2 size)
    {

        _title = title;
        _size = size;

        if (CreateWindow() == 1)
        {
            throw new System.Exception("Couldn't create GLFW window.");
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
        Glfw.WindowHint(Hint.OpenglProfile, Profile.Core);

        Glfw.WindowHint(Hint.Doublebuffer, true);

        Glfw.WindowHint(Hint.Focused, true);
        Glfw.WindowHint(Hint.Resizable, false);

        // Hide the window at startup
        Glfw.WindowHint(Hint.Visible, true);

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

        Glfw.SwapInterval(1); // Turn off VSync

        return 0;
    }

    private void CloseWindow()
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

    public void Show()
    {
        Glfw.WindowHint(Hint.Visible, true);
        _window.Opacity = 1;
    }

}