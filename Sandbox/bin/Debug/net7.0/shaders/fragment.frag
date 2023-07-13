#version 330 core

uniform vec4 u_color;

in vec4 oColor;

out vec4 FragColor;

void main() 
{
    FragColor = oColor;
}