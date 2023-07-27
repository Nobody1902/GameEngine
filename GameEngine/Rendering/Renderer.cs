using GameEngine.ECS;
using GameEngine.ECS.Components;
using GLFW;
using static GameEngine.OpenGL.GL;

namespace GameEngine.Rendering;

internal sealed class Renderer
{
    private Scene _scene;

    private Shader _shaderProgram;

    public Renderer(Scene scene, Shader defaultShader)
    {
        _scene = scene;
        _shaderProgram = defaultShader;
    }

    public void SetScene(Scene scene)
    {
        _scene = scene;
    }
    public unsafe void SetShader(Shader shader)
    {
        _shaderProgram = shader;
        _shaderProgram.Load();
    }
    public unsafe void OnLoad()
    {
        // Enable back face culling
        glEnable(GL_CULL_FACE);
        glFrontFace(GL_CCW);
        glCullFace(GL_BACK);

        // Enable the depth
        glEnable(GL_DEPTH_TEST);

        // Enable anti-aliasing
        glEnable(GL_MULTISAMPLE);

        // Create shaders
        _shaderProgram.Load();
    }
    public Color BackgroundColor = Color.Gray.Normalize();
    public unsafe void Render()
    {
        glClearColor(BackgroundColor.R, BackgroundColor.G, BackgroundColor.B, BackgroundColor.A);

        // Clear the color and depth buffers
        glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);


        // Render scene GameObjects
        foreach (var gameObject in _scene.gameObjects)
        {
            // Skip object that are diabled and those with no MeshRenderer
            if (!gameObject.enabled || (!gameObject.HasComponent<MeshRenderer>())) { continue; }

            MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer>();

            // Get the shader program
            Shader _shader = _shaderProgram;
            if(meshRenderer?.Shader != null)
            {
                _shader = meshRenderer.Shader;
            }

            // Use the shader program
            _shader.Use();
            LoadCamera(_shader);
            LoadLights(_shader);

            // Draw
            DrawMesh(meshRenderer!, _shader);

        }

        Glfw.SwapBuffers(Window._window);
    }

    private static unsafe void LoadCamera(Shader _shader)
    {
        // Set the camera projection uniform
        Matrix4x4 proj = Camera.camera.GetMatrix();
        Vector3 camForward = Camera.camera.transform.forward;
        _shader.SetMatrix4x4("u_proj", proj);
        _shader.SetVec3("u_viewDir", camForward);
    }

    private unsafe void LoadLights(Shader _shader)
    {
        // Load in the lights
        int lightIndex = 0;
        foreach (var gameObject in _scene.gameObjects)
        {
            // Skip object, that are diabled and those with no Light
            if (!gameObject.enabled || !gameObject.HasComponent<Light>()) { continue; }

            Light light = gameObject.GetComponent<Light>();

            // Set all
            _shader.SetVec3($"u_light[{lightIndex}].position", light.transform.GetModelMatrix().MultiplyMatrix(light.transform.position * -Vector3.One));
            _shader.SetVec4($"u_light[{lightIndex}].color", light.Color.ToVector4());
            _shader.SetFloat($"u_light[{lightIndex}].intensity", light.Intensity);

            lightIndex++;
        }

        _shader.SetFloat("u_LightSize", lightIndex);
    }

    public unsafe void DrawMesh(MeshRenderer meshRend, Shader _shader)
    {
        // Get the color to a 0 to 1 range (instead of 0 to 255)
        Vector4 color = meshRend.Color.Normalize().ToVector4();
        // Get the GameObjects model matrix
        Matrix4x4 model = meshRend.gameObject.transform.GetModelMatrix();

        // Set the color and model matrix uniforms
        _shader.SetVec4("u_color", color);
        _shader.SetMatrix4x4("u_model", model);

        // Get the mesh
        Mesh mesh = meshRend.Mesh;

        #region Render Mesh

        glBindVertexArray(mesh._vao);

        fixed (uint* i = &mesh._indices[0])
        {
            glDrawElements(GL_TRIANGLES, mesh._indices.Length, GL_UNSIGNED_INT, i);
        }

        glBindVertexArray(0);

        #endregion
    }
}