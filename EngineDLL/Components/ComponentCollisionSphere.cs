using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGL_Game.Components
{
    class ComponentCollisionSphere : IComponent
    {
        float collisionRadius;

        public ComponentCollisionSphere(float pCollisionRadius)
        {
            collisionRadius = pCollisionRadius;
        }

        public float CollisionRadius
        {
            get { return collisionRadius; }
            set { collisionRadius = value; }
        }

        public ComponentTypes ComponentType
        {
            get { return ComponentTypes.COMPONENT_COLLISION_SPHERE; }
        }

        public void Close()
        {

        }
    }
}
