using GameEngine.Rendering;
using System;

namespace GameEngine.ECS.Components;

public sealed class Camera : Component
{
    public float FOV;
    public float nearClip;
    public float farClip;

    public static Camera camera;

    public Camera(GameObject gameObject) : base(gameObject)
    {
        if (camera != null)
        {
            camera = this;
            throw new Exception("Multiple cameras in scene!");
        }
        // Setup default camera settings
        FOV = 45;
        nearClip = .01f;
        farClip = 100;

        // Set the static camera refrence
        camera = this;
    }
    public const float speed = 1_000_000f;
    public override void Update()
    {
        if (Input.KeyPressed(GLFW.Keys.W))
        {
            gameObject.transform.position += new Vector3(0, 0, Time.DeltaTime * speed) * gameObject.transform.forward.Normalized();
        }
        if(Input.KeyPressed(GLFW.Keys.S))
        {
            gameObject.transform.position += new Vector3(0, 0, -Time.DeltaTime * speed) * gameObject.transform.forward.Normalized();
        }
        if (Input.KeyPressed(GLFW.Keys.D))
        {
            gameObject.transform.position += new Vector3(-Time.DeltaTime * speed, 0, 0) * gameObject.transform.right.Normalized();
        }
        if (Input.KeyPressed(GLFW.Keys.A))
        {
            gameObject.transform.position += new Vector3(Time.DeltaTime * speed, 0, 0) * gameObject.transform.right.Normalized();
        }
    }

    public Matrix4x4 GetProjectionMatrix()
    {
        float FOV_radians = ((float)EngineMath.PI / 180) * FOV;

        Matrix4x4 proj = Matrix4x4.CreatePerspectiveFieldOfView(FOV_radians, Window.window._size.X / Window.window._size.Y, nearClip, farClip);

        return proj;
    }
    public Matrix4x4 GetViewMatrix()
    {
        Matrix4x4 view = Matrix4x4.CreateLookAt(gameObject.transform.position, gameObject.transform.position+gameObject.transform.forward, gameObject.transform.up);

        return view;
    }
    public Matrix4x4 GetMatrix()
    {
        return GetViewMatrix()*GetProjectionMatrix();
    }
}