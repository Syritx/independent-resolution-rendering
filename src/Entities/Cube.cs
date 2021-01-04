using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;
using renderer.src.Shaders;
using renderer.src.Etc;

namespace renderer.src.Entities {

    class Cube : Renderable {

        public int vao;
        Vector3 position;

        public Cube(string VSP, string FSP, Vector3 POSITION) : base(VSP, FSP, POSITION) {

            vertices = VertexData.GenerateCube(10, new Vector3(0,0,0));
            position = POSITION;
            shader = new Shader(VSP, FSP);

            vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length*sizeof(float), vertices, BufferUsageHint.StaticDraw);

            vao = GL.GenVertexArray();
            GL.BindVertexArray(vao);

            GL.VertexAttribPointer(
            0,
            3,
            VertexAttribPointerType.Float,
            false,
            9 * sizeof(float), 0);

            GL.VertexAttribPointer(
            1,
            3,
            VertexAttribPointerType.Float,
            false,
            9 * sizeof(float), 3 * sizeof(float));

            GL.VertexAttribPointer(
            2,
            3,
            VertexAttribPointerType.Float,
            false,
            9 * sizeof(float), 6 * sizeof(float));

            GL.EnableVertexAttribArray(0);
            GL.EnableVertexAttribArray(1);
            GL.EnableVertexAttribArray(2);
        }

        public override void Render(Vector3 cameraPosition, Vector3 cameraEye, Vector3 cameraUp) {

            base.Render(cameraPosition, cameraEye, cameraUp);

            shader.Use();

            GL.EnableVertexAttribArray(0);
            GL.EnableVertexAttribArray(1);
            GL.EnableVertexAttribArray(2);
            GL.BindVertexArray(vao);

            GL.Enable(EnableCap.DepthTest);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            Matrix4.CreateTranslation(position.X, position.Y, position.Z, out Camera.world);

            int modelPosition = GL.GetUniformLocation(shader.program, "model"),
                viewPosition = GL.GetUniformLocation(shader.program, "view"),
                projectionPosition = GL.GetUniformLocation(shader.program, "projection");

            int cPosition = GL.GetUniformLocation(shader.program, "cameraPosition");

            GL.UniformMatrix4(modelPosition, false, ref Camera.world);
            GL.UniformMatrix4(viewPosition, false, ref Camera.view);
            GL.UniformMatrix4(projectionPosition, false, ref Camera.proj);
            
            GL.Uniform3(cPosition, cameraPosition);

            //GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);
        }

        public override void SetPosition(Vector3 position)
        {
            this.position = position*100;
            base.SetPosition(position);
        }
    }
}