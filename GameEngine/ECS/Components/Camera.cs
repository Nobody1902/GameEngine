using GameEngine.Rendering;
using Newtonsoft.Json;

namespace GameEngine.ECS.Components;

public sealed class Camera : Component
{

    public float FOV = 45;
    public float nearClip = .7f;
    public float farClip = 100;

    public static Camera camera;

    public Camera(GameObject gameObject) : base(gameObject)
    {
        /*if (camera != null)
        {
            camera = this;
            throw new Exception("Multiple cameras in scene!");
        }*/

        // Set the static camera refrence
        camera = this;
    }

    public const float speed = 1_000_000f;

    public Matrix4x4 GetProjectionMatrix()
    {
        float FOV_radians = ((float)EngineMath.PI / 180) * FOV;

        Matrix4x4 proj = Matrix4x4.CreatePerspectiveFieldOfView(FOV_radians, Window._size.X / Window._size.Y, nearClip, farClip);

        return proj;
    }
    public Matrix4x4 GetViewMatrix()
    {
        Matrix4x4 view = Matrix4x4.CreateLookAt(transform.position, transform.position+transform.forward, transform.up);

        return view;
    }
    public Matrix4x4 GetMatrix()
    {
        return GetViewMatrix()*GetProjectionMatrix();
    }
}