using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineDLL.Components
{
    public class ComponentCollisionLine : IComponent
    {
        Vector3 lineStart;
        Vector3 lineEnd;

        public ComponentCollisionLine(Vector3 pLineStart, Vector3 pLineEnd)
        {
            lineStart = pLineStart;
            lineEnd = pLineEnd;
        }

        public Vector3 LineStart
        {
            get { return lineStart; }
            set { lineStart = value; }
        }

        public Vector3 LineEnd
        {
            get { return lineEnd; }
            set { lineEnd = value; }
        }

        public ComponentTypes ComponentType
        {
            get { return ComponentTypes.COMPONENT_COLLISION_LINE; }
        }

        public void Close()
        {

        }
    }
}
