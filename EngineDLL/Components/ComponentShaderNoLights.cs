using OpenGL_Game.OBJLoader;
using OpenGL_Game.Scenes;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGL_Game.Components
{
    class ComponentShaderNoLights : ComponentShader
    {
        public int uniform_stex;
        public int uniform_mmodelviewproj;
        public int uniform_mmodel;
        public int uniform_diffuse;

        public ComponentShaderNoLights(Camera camera) : base("Engine/Shaders/vs.glsl", "Engine/Shaders/fs_nolights.glsl", camera)
        {
            uniform_stex = GL.GetUniformLocation(pgmID, "s_texture");
            uniform_mmodelviewproj = GL.GetUniformLocation(pgmID, "ModelViewProjMat");
            uniform_mmodel = GL.GetUniformLocation(pgmID, "ModelMat");
            uniform_diffuse = GL.GetUniformLocation(pgmID, "v_diffuse");
        }

        public override void ApplyShader(Matrix4 model, Geometry geometry)
        {
            GL.UseProgram(pgmID);

            GL.Uniform1(uniform_stex, 0);
            GL.ActiveTexture(TextureUnit.Texture0);

            GL.UniformMatrix4(uniform_mmodel, false, ref model);
            Matrix4 modelViewProjection = model * camera.view * camera.projection;
            GL.UniformMatrix4(uniform_mmodelviewproj, false, ref modelViewProjection);

            geometry.Render(uniform_diffuse);

            GL.UseProgram(0);
        }

        public void Close()
        {
        }
    }
}
