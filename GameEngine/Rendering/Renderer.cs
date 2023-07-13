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

        // Enable the depth
        glEnable(GL_DEPTH_TEST);

        // Create shaders
        _shaderProgram.Load();
    }

    public unsafe void Render()
    {
        glClearColor(0f, 0f, 0f, 1f);

        // Clear the color and depth buffers
        glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);

        // Use the shader program
        _shaderProgram.Use();

        // Set the camera projection uniform
        Matrix4x4 proj = Camera.camera.GetMatrix();
        _shaderProgram.SetMatrix4x4("u_proj", proj);

        // Render scene GameObjects
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
        // Get the color to a 0 to 1 range (instead of 0 to 255)
        Vector4 color = new(((float)meshRend.Color.R)/255, ((float)meshRend.Color.G)/ 255, ((float)meshRend.Color.B) / 255, meshRend.Color.A);
        // Get the GameObjects model matrix
        Matrix4x4 model = meshRend.gameObject.transform.GetModelMatrix();

        // Set the color and model matrix uniforms
        _shaderProgram.SetVec4("u_color", color);
        _shaderProgram.SetMatrix4x4("u_model", model);

        // Get the mesh
        Mesh mesh = meshRend.Mesh;

        #region Render Mesh

        glBindVertexArray(mesh.vao);

        fixed (uint* i = &mesh._indices[0])
        {
            glDrawElements(GL_TRIANGLES, mesh._indices.Length, GL_UNSIGNED_INT, i);
        }

        glBindVertexArray(0);

        #endregion
    }
}