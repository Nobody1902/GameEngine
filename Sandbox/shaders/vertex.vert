#version 330 core

layout (location = 0) in vec3 aPosition;

uniform mat4 u_proj;

uniform mat4 u_model;

void main() 
{
    gl_Position = u_proj * u_model * vec4(aPosition, 1.0);
}