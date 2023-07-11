using System.Numerics;
using GameEngine.ECS;
using GameEngine;
using GameEngine.ECS.Components;
using GameEngine.Rendering;
using System.Drawing;

var scene = new Scene();
var app = new Application("My Game", new Vector2(800, 800));
Shader shaders = Shader.LoadShader(@"C:\Users\Mulca\Desktop\VITAN\C#\GameEngine\Sandbox\shaders\vertex.vert", @"C:\Users\Mulca\Desktop\VITAN\C#\GameEngine\Sandbox\shaders\fragment.frag");
app.SetShader(shaders);

// Create a camera
var cam = new GameObject("Camera", true);
cam.AddComponent<Camera>();
cam.GetComponent<Camera>().FOV = 45f;
cam.transform.position = new(0, 0, -.1f);
cam.transform.rotation = new(new(0), 1);

scene.AddObject(cam);

float[] verts =
{
    0.5f, 0.5f, 0.5f
    -0.5f, 0.5f, -0.5f,
    -0.5f, 0.5f, 0.5f,
    0.5f, -0.5f, -0.5f,
    -0.5f, -0.5f, -0.5f,
    0.5f, 0.5f, -0.5f,
    0.5f, -0.5f, 0.5f,
    -0.5f, -0.5f, 0.5f,
};
uint[] indic =
{
    0, 1, 2,
    1, 3, 4,
    5, 6, 3,
    7, 3, 6,
    2, 4, 7,
    0, 7, 6,
    0, 5, 1,
    1, 5, 3,
    5, 0, 6,
    7, 4, 3,
    2, 1, 4,
    0, 2, 7,
};

GameObject cube = new("Cube");
MeshRenderer rend = cube.AddComponent<MeshRenderer>();
rend.SetMesh(new(verts, indic));
rend.SetColor(Color.Red);

scene.AddObject(cube);

app.SetScene(scene);
app.Run();
