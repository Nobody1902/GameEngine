using GameEngine.ECS;
using GameEngine;
using GameEngine.ECS.Components;
using GameEngine.Rendering;
using System.Dynamic;
using AutoMapper;

var scene = new Scene();
var app = new Application("Sandbox", new Vector2(800, 800));

// Load the shaders
Shader shader = Shader.LoadShader("shaders/vertex.vert", "shaders/fragment.frag");
app.SetShader(shader);

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
// Load the sphere mesh
Mesh mesh = MeshLoader.Load("models/Icosphere.model");
GameObject sphere = new("Cube");
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

#region Lights

var light = new GameObject("Light");
light.AddComponent<Light>().Color = Color.Red;
light.GetComponent<Light>().Intensity = .8f;
light.transform.position = new(2, 0, 0f);
light.transform.scale = new(.4f);


var light2 = new GameObject("Light2");
light2.AddComponent<Light>().Color = Color.Blue;
light2.GetComponent<Light>().Intensity = .8f;
light2.transform.position = new(-2, 0f, 0f);
light2.transform.scale = new(.4f);

var light3 = new GameObject("Light3");
light3.AddComponent<Light>().Color = Color.Green;
light3.GetComponent<Light>().Intensity = .8f;
light3.transform.position = new(0, 2f, 0f);
light3.transform.scale = new(.4f);

var light4 = new GameObject("Light4");
light4.AddComponent<Light>().Color = Color.White;
light4.GetComponent<Light>().Intensity = .2f;
light4.transform.position = new(0, -2f, 0f);
light4.transform.scale = new(0.4f);

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