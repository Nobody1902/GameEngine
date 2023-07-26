#version 330 core

uniform vec4 u_color;

in vec3 oNormal;

out vec4 FragColor;

void main() 
{
    FragColor = u_color; //* vec4(abs(normalize(oNormal)), 1.0);
}