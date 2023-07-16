using System.Numerics;
using GameEngine.ECS;
using GameEngine;
using GameEngine.ECS.Components;
using GameEngine.Rendering;

var scene = new Scene();
var app = new Application("Sandbox", new Vector2(800, 800));

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

#region Sphere
// Load the sphere mesh
Mesh mesh = MeshLoader.Load("models/Icosphere.model");
Console.WriteLine(mesh._verticies.Length);
GameObject sphere = new("Sphere");
sphere.transform.scale = Vector3.One;
// Add the custom Rotator class
sphere.AddComponent<Rotator>().Speed = 50_000_000f;
// Add and store the MeshRenderer
MeshRenderer rend = sphere.AddComponent<MeshRenderer>();
// Set the mesh to render
rend.SetMesh(mesh);
// Set the color
rend.SetColor(Color.White);
#endregion

#region Light

Mesh lightMesh = MeshLoader.Load("models/Cube.model");

var light = new GameObject("Light");
light.AddComponent<Light>().Color = Color.Red;
light.GetComponent<Light>().Intensity = .8f;
light.transform.position = new(2, 0, 0f);
light.transform.scale = new(.4f);

light.AddComponent<MeshRenderer>().SetMesh(lightMesh);

var light2 = new GameObject("Light2");
light2.AddComponent<Light>().Color = Color.Blue;
light2.GetComponent<Light>().Intensity = .8f;
light2.transform.position = new(-2, 0f, 0f);
light2.transform.scale = new(.4f);

light2.AddComponent<MeshRenderer>().SetMesh(lightMesh);


var light3 = new GameObject("Light3");
light3.AddComponent<Light>().Color = Color.Green;
light3.GetComponent<Light>().Intensity = .8f;
light3.transform.position = new(0, 2f, 0f);
light3.transform.scale = new(.4f);

light3.AddComponent<MeshRenderer>().SetMesh(lightMesh);

var light4 = new GameObject("Light4");
light4.AddComponent<Light>().Color = Color.White;
light4.GetComponent<Light>().Intensity = .2f;
light4.transform.position = new(0, -2f, 0f);
light4.transform.scale = new(.4f);
light4.AddComponent<MeshRenderer>().SetMesh(lightMesh);

#endregion

// Add the objects to scene
scene.AddObject(cam);

scene.AddObject(light);
scene.AddObject(light2);
scene.AddObject(light3);
scene.AddObject(light4);

scene.AddObject(sphere);

// Start the app
app.SetScene(scene);
app.Run();
