using System.Numerics;
using GameEngine.ECS;
using GameEngine;
using GameEngine.ECS.Components;

var scene = new Scene();
var app = new Application("My Game", new Vector2(800, 600));

var g = new GameObject("GameObject");
var cam = new GameObject("Camera");
cam.AddComponent<Camera>();

MeshRenderer r = g.AddComponent<MeshRenderer>();
float[] verts = new float[]
{
    0.5f,  0.5f, 0.0f,  // top right
         0.5f, -0.5f, 0.0f,  // bottom right
        -0.5f, -0.5f, 0.0f,  // bottom left
        -0.5f,  0.5f, 0.0f   // top left 
};
float[] indic = new float[]
{
    0, 1, 3,  // first Triangle
    1, 2, 3   // second Triangle
};

var mesh = new GameEngine.Rendering.Mesh(verts, indic);

r.SetMesh(mesh, new GameEngine.Rendering.Material(), MeshUsage.STATIC_DRAW);

scene.AddObject(cam);
scene.AddObject(g);

app.SetScene(scene);
app.Run();
