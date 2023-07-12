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
cam.GetComponent<Camera>().FOV = 45;
cam.transform.position = new(0, 0f, -3f);

scene.AddObject(cam);

float[] verts =
{
    //     -Positions             -Colors
    /* 0 */ -0.5f, -0.5f,  0.5f,  .2f, .2f, .2f,
    /* 1 */ 0.5f, -0.5f,  0.5f,   .3f, .3f, .3f,
    /* 2 */ -0.5f,  0.5f,  0.5f,  .4f, .4f, .4f,
    /* 3 */ 0.5f,  0.5f,  0.5f,   .2f, .2f, .2f,
    /* 4 */ -0.5f, -0.5f, -0.5f,  .3f, .3f, .3f,
    /* 5 */ 0.5f, -0.5f, -0.5f,   .4f, .4f, .4f,
    /* 6 */ -0.5f,  0.5f, -0.5f,  .3f, .3f, .3f,
    /* 7 */ 0.5f,  0.5f, -0.5f,   .2f, .2f, .2f,
};
uint[] indic =
{
    // Back face
    0, 1, 2,
    3, 1, 2,
    // Right face
    0, 2, 4,
    2, 6, 4,
    // Front face
    6, 4, 5,
    5, 6, 7,
    // Left face
    7, 5, 1,
    7, 3, 1,
    // Top face
    2, 3, 6,
    6, 7, 3,
    // Bottom face
    0, 1, 4,
    4, 5, 1
};

GameObject cube = new("Cube");
cube.transform.position = new(0f, 0f, 0);
cube.transform.rotation = new(0);
MeshRenderer rend = cube.AddComponent<MeshRenderer>();
rend.SetMesh(new(verts, indic));
rend.SetColor(Color.LightGray);


scene.AddObject(cube);

app.SetScene(scene);
app.Run();
