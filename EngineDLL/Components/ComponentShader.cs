using OpenGL_Game.Managers;
using OpenGL_Game.OBJLoader;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;

namespace OpenGL_Game.Components
{
    abstract class ComponentShader : IComponent
    {
        protected Camera camera;
        public int pgmID;

        public ComponentShader(string vertexShaderName, string fragmentShaderName, Camera camera)
        {
            this.camera = camera;

            pgmID = GL.CreateProgram();
            GL.AttachShader(pgmID, ResourceManager.LoadShader(vertexShaderName, ShaderType.VertexShader));
            GL.AttachShader(pgmID, ResourceManager.LoadShader(fragmentShaderName, ShaderType.FragmentShader));
            GL.LinkProgram(pgmID);
            Console.WriteLine(GL.GetProgramInfoLog(pgmID));
        }

        public abstract void ApplyShader(Matrix4 model, Geometry geometry);

        public ComponentTypes ComponentType
        {
            get { return ComponentTypes.COMPONENT_SHADER; }
        }

        public void Close()
        {
        }
    }
}
