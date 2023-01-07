using OpenTK;

namespace OpenGL_Game.Components
{
    class ComponentPosition : IComponent
    {
        Vector3 position;
        Vector3 lastPosition;

        public ComponentPosition(float x, float y, float z)
        {
            position = new Vector3(x, y, z);
            lastPosition = position;
        }

        public ComponentPosition(Vector3 pos)
        {
            position = pos;
        }

        public Vector3 Position
        {
            get { return position; }
            set { position = value; }
        }

        public Vector3 LastPosition
        {
            get { return lastPosition; }
            set { lastPosition = value; }
        }

        public ComponentTypes ComponentType
        {
            get { return ComponentTypes.COMPONENT_POSITION; }
        }

        public void Close()
        {
        }
    }
}
