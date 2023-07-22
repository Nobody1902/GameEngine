using GameEngine;
using GameEngine.ECS;

public class CameraController : Component
{
    public CameraController(GameObject gameObject) : base(gameObject)
    {
    }
    public float speed = 1_000_000f;

    public override void Update()
    {
        if (Input.KeyPressed(GLFW.Keys.W))
        {
            gameObject.transform.position += new Vector3(0, 0, Time.DeltaTime * speed) * gameObject.transform.forward.Normalized();
        }
        if (Input.KeyPressed(GLFW.Keys.S))
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
}