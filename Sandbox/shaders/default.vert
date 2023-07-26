#version 330 core
layout(location = 0) in vec3 aPosition;
layout(location = 1) in vec3 aNormal;
uniform mat4 u_proj;
uniform mat4 u_model;
out vec3 oNormal;
void main()
{
oNormal = aNormal;
gl_Position = u_proj * u_model * vec4(aPosition, 1.0);
}