using System;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

using renderer.src.Entities;

namespace renderer.src {

    class GameScene : GameWindow {

        Vector2 windowSize;

        public Renderer renderer;
        Camera camera;

        public GameScene(GameWindowSettings GWS, NativeWindowSettings NWS) : base(GWS, NWS) {
            Run();
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);
            GL.Clear(ClearBufferMask.ColorBufferBit);

            renderer.RenderAll(camera);

            SwapBuffers();
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);

            int screenWidth = e.Size.X;
            int screenHeight = e.Size.Y;

            int scale = screenWidth > screenHeight ? screenWidth : screenHeight;
            int ypos = 0, xpos = 0;

            if (screenWidth > screenHeight) ypos = -screenHeight/2;
            else xpos = -screenWidth/2;

            windowSize = new Vector2(screenWidth, screenHeight);
            GL.Viewport(xpos, ypos, scale*2, scale*2);
        }

        protected override void OnMove(WindowPositionEventArgs e)
        {
            base.OnMove(e);

            int screenWidth = (int)windowSize.X;
            int screenHeight = (int)windowSize.Y;

            int scale = screenWidth > screenHeight ? screenWidth : screenHeight;
            int ypos = 0, xpos = 0;

            if (screenWidth > screenHeight)  ypos = -screenHeight/2;
            else xpos = -screenWidth/2;

            windowSize = new Vector2(screenWidth, screenHeight);
            GL.Viewport(xpos, ypos, scale*2, scale*2);
        }

        protected override void OnLoad()
        {
            base.OnLoad();
            GL.ClearColor(0,0,0,1);
            camera = new Camera(this);
            renderer = new Renderer();

            int virtualWidth = 1000, virtualHeight = 1000;
            
            float targetAspectRatio = (float)virtualWidth/virtualHeight;

            int width = 1000;
            int height = (int)(width/targetAspectRatio + .5f);

            if (height > 720) {
                height = 720;
                width = (int)(height * targetAspectRatio + .5f);
            }

            int vpx = (1000/2) - (width/2);
            int vpy = (720/2) - (height/2);

            GL.Viewport(vpx, vpy, width*10, height*10);

            renderer.AddEntity(new Cube("src/Shaders/GLSL/Cube/cubeVertexShader.glsl", "src/Shaders/GLSL/Cube/cubeFragmentShader.glsl", new Vector3(0,0,1)));
        }
    }
}