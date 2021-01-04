#version 330 core

layout (location = 0) in vec3 position;
layout (location = 1) in vec3 vertexColor;
layout (location = 2) in vec3 _normal;
out vec3 fragPosition;

out vec3 color;
out vec3 surfaceNormal;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

void main() {
    color = vertexColor;
    surfaceNormal = mat3(transpose(inverse(model))) * _normal;
    fragPosition = vec3(model * vec4(position, 1.0));

    gl_Position = projection * view * vec4(fragPosition,1.0);
}