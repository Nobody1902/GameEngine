using GameEngine.ECS;
using GameEngine.ECS.Components;
using GLFW;
using System.Drawing;
using System.Net.Security;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static GameEngine.OpenGL.GL;

namespace GameEngine.Rendering;

public sealed class Renderer
{
    private Scene _scene;
    private Window _window;

    private Shader _shaderProgram;

    public Renderer(Window window, Scene scene, Shader defaultShader)
    {
        _window = window;
        _scene = scene;
        _shaderProgram = defaultShader;
    }

    public void SetScene(Scene scene)
    {
        _scene = scene;
    }
    public void SetWindow(Window window)
    {
        _window = window;
    }
    public unsafe void SetShader(Shader shader)
    {
        _shaderProgram = shader;
        _shaderProgram.Load();
    }
    public unsafe void OnLoad()
    {
        /*glEnable(GL_CULL_FACE);
        glFrontFace(GL_CW);
        glCullFace(GL_BACK);*/

        // Create shaders
        _shaderProgram.Load();
    }

    public unsafe void Render()
    {
        glClearColor(.8f, .8f, .8f, 1f);
        glClear(GL_COLOR_BUFFER_BIT);

        Matrix4x4 proj = Camera.camera.GetMatrix();

        _shaderProgram.Use();

        _shaderProgram.SetMatrix4x4("u_proj", proj);


        foreach (var gameObject in _scene.gameObjects)
        {
            // Skip object that are diabled and those with no MeshRenderer
            if (!gameObject.enabled || !gameObject.HasComponent<MeshRenderer>()) { continue; }

            MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer>();

            DrawMesh(meshRenderer);
        }

        Glfw.SwapBuffers(_window._window);
    }

    public unsafe void DrawMesh(MeshRenderer meshRend)
    {
        Vector4 color = new(((float)meshRend.Color.R)/255, ((float)meshRend.Color.G)/ 255, ((float)meshRend.Color.B) / 255, meshRend.Color.A);
        Matrix4x4 model = meshRend.gameObject.transform.GetModelMatrix();

        _shaderProgram.SetMatrix4x4("u_model", model);

        _shaderProgram.SetVec4("u_color", color);

        Mesh mesh = meshRend.Mesh;

        glBindVertexArray(mesh.vao);

        fixed (uint* i = &mesh._indices[0])
        {
            glDrawElements(GL_TRIANGLES, mesh._indices.Length, GL_UNSIGNED_INT, i);
        }

        glBindVertexArray(0);
    }
}