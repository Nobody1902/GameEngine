#version 330 core

struct Light
{
    vec3 position;
    vec4 color;
    float intensity;
};


uniform vec4 u_color;
uniform mat4 u_model;

const int MAX_LIGHTS = 128;

uniform Light u_light[MAX_LIGHTS];

uniform int u_LightSize = MAX_LIGHTS;

uniform vec3 u_viewDir;

in vec3 oNormal;
in vec3 oPosition;

out vec4 FragColor;

const float ambientIntensity = .1;
vec3 ambientColor = vec3(1, 1, 1);

void main()
{
    // Normalize the normal vector
    vec3 normal = normalize(oNormal);

    // Ambient
    vec3 ambientCombined = ambientIntensity * ambientColor;

    // Diffuse & specular
    vec3 diffuse = vec3(0.0);
    vec4 color = vec4(1, 1, 1, 1);
    vec3 specular = vec3(0.0);

    // Loop over all lights in the scene
    for(int i = 0; i < u_LightSize; i++)
    {
        vec3 lightDir = normalize(u_light[i].position - oPosition);

        // Diffuse
        float diff = max(dot(normal, lightDir), 0.0);
        diffuse += diff * u_light[i].intensity/2;

        color += u_light[i].color * u_light[i].intensity/2 * diff;

        // Specular
        float specularLight = .5;
        vec3 reflectDir = reflect(-lightDir, normal);
        float specAmount = pow(max(dot(u_viewDir, reflectDir), 0.0), 20);
        specular += specAmount * specularLight * u_light[i].intensity/2;
    }

    // Calculate the avarage color
    color /= u_LightSize;

    // Muttiply by the object's color
    color *= u_color;

    // Calculate the pixel color
    FragColor = color * vec4(ambientCombined + diffuse + specular, 1.0);
}