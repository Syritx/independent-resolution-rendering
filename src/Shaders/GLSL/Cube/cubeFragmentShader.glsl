#version 330 core

out vec4 FragColor;
in vec3 color;

in vec3 surfaceNormal;
in vec3 fragPosition;

uniform vec3 lightPosition = vec3(1000,1000,1000);
uniform vec3 cameraPosition = vec3(0,0,0);
uniform vec3 lightColor = vec3(1,1,1);

void main() {

    float ambientStrength = 0.5;
    vec3 ambient = ambientStrength * vec3(.5, .5, 1.0)*3;

    vec3 normal = normalize(surfaceNormal);
    vec3 lightDirection = normalize(lightPosition - fragPosition);

    float diff = max(dot(normal, lightDirection), 0.0)/10;
    vec3 diffuse = lightColor*diff;

    const float kPi = 3.14159265;
    const float kShininess = 16.0;

    const float kEnergyConservation = ( 8.0 + kShininess ) / ( 8.0 * kPi );

    float specularStrength = 0.6;
    vec3 viewDirection = normalize(cameraPosition - fragPosition);
    vec3 reflectionDirection = normalize(lightDirection + viewDirection);
    float spec = kEnergyConservation * pow(max(dot(normal, reflectionDirection), 0.0), 16.0);
    vec3 specular = specularStrength * spec * lightColor;

    vec3 fC = (diffuse+ambient+specular)*color;
    FragColor = vec4(fC, 1.0);
    float gamma = 0.7;
    FragColor.rgb = pow(fC.rgb, vec3(1.0/gamma)); 
}