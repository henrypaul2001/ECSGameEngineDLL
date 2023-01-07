using OpenGL_Game.Components;
using OpenGL_Game.Managers;
using OpenGL_Game.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGL_Game.Systems
{
    class SystemCollisionCameraSphere : ISystem
    {
        const ComponentTypes MASK = (ComponentTypes.COMPONENT_POSITION | ComponentTypes.COMPONENT_COLLISION_SPHERE);

        CollisionManager collisionManager;
        Camera camera;

        public SystemCollisionCameraSphere(CollisionManager collisionManager, Camera camera)
        {
            this.collisionManager = collisionManager;
            this.camera = camera;
        }

        public string Name
        {
            get { return "SystemCollisionCameraSphere"; }
        }

        public void OnAction(Entity entity)
        {
            if ((entity.Mask & MASK) == MASK)
            {
                List<IComponent> components = entity.Components;

                IComponent collComponent = components.Find(delegate (IComponent component)
                {
                    return component.ComponentType == ComponentTypes.COMPONENT_COLLISION_SPHERE;
                });
                ComponentCollisionSphere collision = (ComponentCollisionSphere)collComponent;

                IComponent positionComponent = components.Find(delegate (IComponent component)
                {
                    return component.ComponentType == ComponentTypes.COMPONENT_POSITION;
                });
                ComponentPosition position = (ComponentPosition)positionComponent;

                Collision(entity, position, collision);
            }
        }

        public void Collision(Entity entity, ComponentPosition position, ComponentCollisionSphere collision)
        {
            if ((position.Position - camera.cameraPosition).Length < collision.CollisionRadius + camera.collisionRadius)
            {
                collisionManager.CollisionBetweenCamera(entity, COLLISIONTYPE.CAMERA_SPHERE);
            }
        }
    }
}
