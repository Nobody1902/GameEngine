#version 330 core

layout (location = 0) in vec3 aPosition;

uniform mat4 u_proj;

void main() 
{
    // u_proj is not  working;
    gl_Position = u_proj * vec4(aPosition, 1.0);
}