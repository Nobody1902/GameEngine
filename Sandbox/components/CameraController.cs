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
            gameObject.transform.position += new Vector3(0, Time.DeltaTime * speed, 0) * gameObject.transform.up.Normalized();
        }
        if (Input.KeyPressed(GLFW.Keys.S))
        {
            gameObject.transform.position += new Vector3(0, -Time.DeltaTime * speed, 0) * gameObject.transform.up.Normalized();
        }
        if (Input.KeyPressed(GLFW.Keys.D))
        {
            gameObject.transform.position += new Vector3(-Time.DeltaTime * speed, 0, 0) * gameObject.transform.right.Normalized();
        }
        if (Input.KeyPressed(GLFW.Keys.A))
        {
            gameObject.transform.position += new Vector3(Time.DeltaTime * speed, 0, 0) * gameObject.transform.right.Normalized();
        }

        gameObject.transform.position += new Vector3(0, 0, Input.Scroll * 20 * Time.DeltaTime * speed) * gameObject.transform.forward.Normalized();

        if (Input.KeyPressed(GLFW.Keys.Q))
        {
            gameObject.transform.rotation += new Vector3(Time.DeltaTime * speed, 0, 0);
        }
        if (Input.KeyPressed(GLFW.Keys.E))
        {
            gameObject.transform.rotation -= new Vector3(Time.DeltaTime * speed, 0, 0);
        }
    }
}