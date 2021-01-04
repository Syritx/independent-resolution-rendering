using renderer.src.Entities;
using System.Collections.Generic;

using OpenTK.Graphics.OpenGL;

namespace renderer.src {

    class Renderer {

        public List<Renderable> renderables = new List<Renderable>();
        int vbo;

        public Renderer() {
            vbo = GL.GenBuffer();
        }

        public void AddEntity(Renderable renderable) {
            renderables.Add(renderable);
        }

        public void RenderAll(Camera camera) {

            foreach (Renderable r in renderables) {
                GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
                GL.BufferData(BufferTarget.ArrayBuffer, r.vertices.Length * sizeof(float), r.vertices, BufferUsageHint.StaticDraw);

                r.shader.Use();
                r.Render(camera.position, camera.eye, camera.up);
                GL.DrawArrays(PrimitiveType.Triangles, 0, r.vertices.Length);
            }
        }
    }
}