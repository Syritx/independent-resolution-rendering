using OpenTK.Mathematics;
using renderer.src.Shaders;

namespace renderer.src.Entities {
    abstract class Renderable {

        public int vbo;
        public float[] vertices;
        public Shader shader;

        public Renderable(string VERTEX_SHADER_PATH, string FRAGMENT_SHADER_PATH, Vector3 POSITION) {}
        public virtual void Render(Vector3 cameraPosition , Vector3 cameraEye, Vector3 cameraUp) {}

        public virtual int GetVAO() {return 0;}

        public virtual void SetPosition(Vector3 position) {}
    }
}