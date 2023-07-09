using GameEngine.Rendering;

namespace GameEngine.ECS.Components;

public sealed class Camera : Component
{
    public float FOV;
    public float nearClip;
    public float farClip;

    public static Camera camera;

    public Camera(GameObject gameObject) : base(gameObject)
    {
        if(camera != null)
        {
            throw new Exception("Multiple cameras in scene!");
        }
        FOV = 60f;
        nearClip = .1f;
        farClip = 100;

        camera = this;
    }

    public Matrix4x4 GetProjectionMatrix()
    {
        float FOV_radians = ((float)Math.PI / 180) * FOV;
        return gameObject.transform.GetTransformation() * Matrix4x4.CreatePerspectiveFieldOfView(FOV_radians, Window.window._size.X/Window.window._size.Y, nearClip, farClip);
    }

}