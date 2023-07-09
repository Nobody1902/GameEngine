using GameEngine.ECS;
using GameEngine.ECS.Components;
using GLFW;
using System.Drawing;
using System.Runtime.CompilerServices;
using static GameEngine.OpenGL.GL;

namespace GameEngine.Rendering;

public sealed class Renderer
{
    private Scene _scene;
    private Window _window;

    private Shader _shaderProgram;

    public Renderer(Window window, Scene scene)
    {
        _window = window;
        _scene = scene;
    }

    public void SetScene(Scene scene)
    {
        _scene = scene;
    }
    public void SetWindow(Window window)
    {
        _window = window;
    }
    public void LoadShaders()
    {
        string vertex = """
                        #version 330 core
            layout (location = 0) in vec3 aPos;

            uniform mat4 u_proj;

            void main()
            {
                gl_Position = u_proj * vec4(aPos.x, aPos.y, aPos.z, 0.0f);
            }
            """;
        string fragment = """
                        #version 330 core
            out vec4 FragColor;

            void main()
            {
                FragColor = vec4(255.0f, 255.0f, 255.0f, 1.0f);
            } 
            """;
        _shaderProgram = new Shader(vertex, fragment);
        _shaderProgram.Load();
    }
    VAO vao;
    VBO vbo;
    EBO ebo;
    public void OnLoad()
    {
        LoadShaders();
        Matrix4x4 projection = Camera.camera.GetProjectionMatrix();
        _shaderProgram.SetMatrix4x4("u_proj", projection);

        vao = new();
        vbo = new(new float[]
        {
            .5f, .5f, 1f,
            .5f, -.5f, 1f,
            -.5f, -.5f, 1f
        });
        ebo = new(new float[]{
            0,1,2,3,4,5,6,7,8
        });

        vao.Bind();

        vao.LinkVBO(vbo, 0);
        vao.Unbind();
        vbo.Unbind();
        ebo.Unbind();
    }

    public unsafe void Render()
    {
        glClearColor(0, 0, 0, 1);
        glClear(GL_COLOR_BUFFER_BIT);

        /*foreach (GameObject obj in _scene.gameObjects)
        {
            if (!obj.enabled || !obj.HasComponent<MeshRenderer>()) { continue; }

            DrawMeshRend(obj.GetComponent<MeshRenderer>());
        }*/
        _shaderProgram.Use();
        vao.Bind();

        glDrawArrays(GL_TRIANGLES, 0, 3);


        Glfw.SwapBuffers(_window._window);
    }

    public unsafe void DrawMeshRend(MeshRenderer meshRenderer)
    {   

        _shaderProgram.Use();
        
        
    }
}