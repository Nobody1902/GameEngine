#version 330 core

layout (location = 0) in vec3 aPosition;
layout (location = 1) in vec3 aNormal;

uniform mat4 u_proj;
uniform mat4 u_model;

out vec3 oNormal;
out vec3 oPosition;

void main() 
{
    oNormal = mat3(u_model) * aNormal;
    oPosition = vec3(u_model * vec4(aPosition, 1.0));

    gl_Position = u_proj * vec4(oPosition, 1.0);
}