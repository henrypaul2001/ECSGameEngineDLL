using OpenTK;
using EngineDLL.Managers;

namespace EngineDLL.Scenes
{
    public abstract class Scene : IScene
    {
        protected SceneManager sceneManager;

        public static float dt = 0;

        public Scene(SceneManager sceneManager)
        {
            this.sceneManager = sceneManager;
        }

        public abstract void Render(FrameEventArgs e);

        public abstract void Update(FrameEventArgs e);

        public abstract void Close();
    }
}
