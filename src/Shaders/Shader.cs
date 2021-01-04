using renderer.src.Etc;
using OpenTK.Graphics.OpenGL;
using System;

namespace renderer.src.Shaders {

    class Shader {

        int vertexShader, fragmentShader;
        public int program;

        public Shader(string VERTEX_SHADER_PATH, string FRAGMENT_SHADER_PATH) {

            string VERTEX_SHADER_SOURCE = FileReader.ReadFileContents(VERTEX_SHADER_PATH),
                   FRAGMENT_SHADER_SOURCE = FileReader.ReadFileContents(FRAGMENT_SHADER_PATH);

            vertexShader = GL.CreateShader(ShaderType.VertexShader);
            fragmentShader = GL.CreateShader(ShaderType.FragmentShader);

            GL.ShaderSource(vertexShader, VERTEX_SHADER_SOURCE);
            GL.ShaderSource(fragmentShader, FRAGMENT_SHADER_SOURCE);
            GL.CompileShader(vertexShader);
            GL.CompileShader(fragmentShader);

            string info;

            GL.GetShaderInfoLog(vertexShader, out info);
            Console.WriteLine(info);

            GL.GetShaderInfoLog(fragmentShader, out info);
            Console.WriteLine(info);

            program = GL.CreateProgram();
            GL.AttachShader(program, vertexShader);
            GL.AttachShader(program, fragmentShader);
            GL.LinkProgram(program);

            GL.DeleteShader(vertexShader);
            GL.DeleteShader(fragmentShader);
        }

        public void Use() {
            GL.UseProgram(program);
        }
    }
}