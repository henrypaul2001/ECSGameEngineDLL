using OpenGL_Game.Components;
using OpenGL_Game.Managers;
using OpenGL_Game.Objects;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGL_Game.Systems
{
    class SystemCollisionCameraLine : ISystem
    {
        const ComponentTypes MASK = (ComponentTypes.COMPONENT_POSITION | ComponentTypes.COMPONENT_COLLISION_LINE);

        CollisionManager collisionManager;
        Camera camera;

        public SystemCollisionCameraLine(CollisionManager collisionManager, Camera camera)
        {
            this.collisionManager = collisionManager;
            this.camera = camera;
        }

        public string Name
        {
            get { return "SystemCollisionLine"; }
        }

        public void OnAction(Entity entity)
        {
            if ((entity.Mask & MASK) == MASK)
            {
                List<IComponent> components = entity.Components;

                // Get components
                IComponent collComponent = components.Find(delegate (IComponent component)
                {
                    return component.ComponentType == ComponentTypes.COMPONENT_COLLISION_LINE;
                });
                ComponentCollisionLine collision = (ComponentCollisionLine)collComponent;

                IComponent positionComponent = components.Find(delegate (IComponent component)
                {
                    return component.ComponentType == ComponentTypes.COMPONENT_POSITION;
                });
                ComponentPosition position = (ComponentPosition)positionComponent;

                LineCollision(entity, position, collision);
            }
        }

        public void LineCollision(Entity entity, ComponentPosition position, ComponentCollisionLine collision)
        {
            Vector3 testNormal = Vector3.Cross(new Vector3(1, 0, 2), new Vector3(5, 0, 2));

            Vector3 lineVector = Vector3.Subtract(collision.LineEnd, collision.LineStart);
            Vector3 collisionNormal = Vector3.Cross(lineVector, new Vector3(0, 1, 0));
            collisionNormal.Normalize(); //n1

            Vector3 oldPos = camera.lastPosition; //v2
            Vector3 nextPos = camera.cameraPosition; //v1

            Vector3 v1 = Vector3.Subtract(collision.LineStart, nextPos);
            Vector3 v2 = Vector3.Subtract(collision.LineStart, oldPos);

            float V1DotN1 = Vector3.Dot(v1, collisionNormal);
            float V2DotN1 = Vector3.Dot(v2, collisionNormal);

            if ((V1DotN1 * V2DotN1) < 0)
            {
                // Infinite line collision
                lineVector = Vector3.Subtract(nextPos, oldPos);
                collisionNormal = Vector3.Cross(lineVector, new Vector3(0, 1, 0));
                collisionNormal.Normalize();

                Vector3 v3 = Vector3.Subtract(oldPos, collision.LineEnd);
                Vector3 v4 = Vector3.Subtract(oldPos, collision.LineStart);

                float V3DotN2 = Vector3.Dot(v3, collisionNormal);
                float V4DotN2 = Vector3.Dot(v4, collisionNormal);

                if ((V3DotN2 * V4DotN2) < 0)
                {
                    // Line segment collision
                    collisionManager.CollisionBetweenCamera(entity, COLLISIONTYPE.LINE_CAMERA);
                }
            }
        }
    }
}
