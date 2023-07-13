using System.Numerics;
using GameEngine.ECS;
using GameEngine;
using GameEngine.ECS.Components;
using GameEngine.Rendering;

var scene = new Scene();
var app = new Application("My Game", new Vector2(800, 800));

// Load the shaders (make sure to set "Copy to Output Directory" to "Copy always")
Shader shaders = Shader.LoadShader("shaders/vertex.vert", "shaders/fragment.frag");
app.SetShader(shaders);

#region Camera
// Create a camera
var cam = new GameObject("Camera", true);
cam.AddComponent<Camera>();
cam.GetComponent<Camera>().FOV = 60f;
// Add the custom CameraController class
cam.AddComponent<CameraController>();
// Set the camera's position
cam.transform.position = new(0, 0f, -3f);
#endregion

#region Cube
// Load the cube mesh
Mesh mesh = MeshLoader.Load("models/Cube.model");

GameObject cube = new("Cube");
// Add the custom Rotator class
cube.AddComponent<Rotator>().Speed = 50_000_000f;
// Add and store the MeshRenderer
MeshRenderer rend = cube.AddComponent<MeshRenderer>();
// Set the mesh to render
rend.SetMesh(mesh);
// Set the color
rend.SetColor(new(40, 40, 40));
#endregion

#region Light

var light = new GameObject("Light");
light.transform.position = new(1.5f, 1.5f, -2f);
light.transform.scale = new(.4f);
MeshRenderer r = light.AddComponent<MeshRenderer>();
r.SetMesh(mesh);
r.SetColor(Color.White);

#endregion

// Add the objects to scene
scene.AddObject(cam);
scene.AddObject(light);
scene.AddObject(cube);

// Start the app
app.SetScene(scene);
app.Run();
