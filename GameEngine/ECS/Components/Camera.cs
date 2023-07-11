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

    public Matrix4x4 GetProjectionMatrix()
    {
        float FOV_radians = ((float)Math.PI / 180) * FOV;

        Matrix4x4 proj = Matrix4x4.CreatePerspectiveFieldOfView(FOV_radians, Window.window._size.X / Window.window._size.Y, nearClip, farClip);

        Matrix4x4 scale = Matrix4x4.CreateScale(10);

        return scale * proj;
    }
    public Matrix4x4 GetViewMatrix()
    {
        Vector3 position = -gameObject.transform.position;
        Vector3 forward = -gameObject.transform.forward;
        Vector3 up = gameObject.transform.up;

        Matrix4x4 view = Matrix4x4.CreateLookAt(position, position + forward, position + up);

        return view;
    }
    public Matrix4x4 GetMatrix()
    {
        return GetViewMatrix() * GetProjectionMatrix();
    }
}