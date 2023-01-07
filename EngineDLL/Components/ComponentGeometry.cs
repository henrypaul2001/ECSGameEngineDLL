using EngineDLL.Managers;
using EngineDLL.OBJLoader;

namespace EngineDLL.Components
{
    public class ComponentGeometry : IComponent
    {
        Geometry geometry;

        public ComponentGeometry(string geometryName)
        {
            this.geometry = ResourceManager.LoadGeometry(geometryName);
        }

        public ComponentTypes ComponentType
        {
            get { return ComponentTypes.COMPONENT_GEOMETRY; }
        }

        public Geometry Geometry()
        {
            return geometry;
        }

        public void Close()
        {
        }
    }
}
