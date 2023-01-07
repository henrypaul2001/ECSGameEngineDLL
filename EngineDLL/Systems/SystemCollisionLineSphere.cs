using OpenGL_Game.Components;
using OpenGL_Game.Managers;
using OpenGL_Game.Objects;
using OpenGL_Game.Systems;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGL_Game.Engine.Systems
{
    class SystemCollisionLineSphere : ISystem
    {
        const ComponentTypes LINE_MASK = (ComponentTypes.COMPONENT_POSITION | ComponentTypes.COMPONENT_COLLISION_LINE);
        const ComponentTypes SPHERE_MASK = (ComponentTypes.COMPONENT_POSITION | ComponentTypes.COMPONENT_COLLISION_SPHERE);

        CollisionManager collisionManager;
        EntityManager entityManager;

        public SystemCollisionLineSphere(CollisionManager collisionManager, EntityManager entityManager)
        {
            this.collisionManager = collisionManager;
            this.entityManager = entityManager;
        }

        public string Name
        {
            get { return "SystemCollisionLineSphere"; }
        }

        public void OnAction(Entity entity)
        {
            if ((entity.Mask & LINE_MASK) == LINE_MASK)
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

                // Loop through other entities
                foreach (Entity e in entityManager.Entities())
                {
                    // Check if other entity has correct components
                    if ((e.Mask & SPHERE_MASK) == SPHERE_MASK)
                    {
                        components = e.Components;

                        // Get components
                        IComponent otherCollComponent = components.Find(delegate (IComponent component)
                        {
                            return component.ComponentType == ComponentTypes.COMPONENT_COLLISION_SPHERE;
                        });
                        ComponentCollisionSphere otherCollision = (ComponentCollisionSphere)otherCollComponent;

                        IComponent otherPositionComponent = components.Find(delegate (IComponent component)
                        {
                            return component.ComponentType == ComponentTypes.COMPONENT_POSITION;
                        });
                        ComponentPosition otherPosition = (ComponentPosition)otherPositionComponent;

                        // Check for collision
                        LineSphereCollision(entity, position, collision, e, otherPosition, otherCollision);
                    }

                }
            }
        }

        public void LineSphereCollision(Entity lineEntity, ComponentPosition linePosition, ComponentCollisionLine lineCollision, Entity sphereEntity, ComponentPosition spherePosition, ComponentCollisionSphere sphereCollision)
        {
            Vector3 testNormal = Vector3.Cross(new Vector3(1, 0, 2), new Vector3(5, 0, 2));

            Vector3 lineVector = Vector3.Subtract(lineCollision.LineEnd, lineCollision.LineStart);
            Vector3 collisionNormal = Vector3.Cross(lineVector, new Vector3(0, 1, 0));
            collisionNormal.Normalize(); //n1

            Vector3 oldPos = spherePosition.LastPosition; //v2
            Vector3 nextPos = spherePosition.Position; //v1

            Vector3 v1 = Vector3.Subtract(lineCollision.LineStart, nextPos);
            Vector3 v2 = Vector3.Subtract(lineCollision.LineStart, oldPos);

            float V1DotN1 = Vector3.Dot(v1, collisionNormal);
            float V2DotN1 = Vector3.Dot(v2, collisionNormal);

            if ((V1DotN1 * V2DotN1) < 0)
            {
                // Infinite line collision
                lineVector = Vector3.Subtract(nextPos, oldPos);
                collisionNormal = Vector3.Cross(lineVector, new Vector3(0, 1, 0));
                collisionNormal.Normalize();

                Vector3 v3 = Vector3.Subtract(oldPos, lineCollision.LineEnd);
                Vector3 v4 = Vector3.Subtract(oldPos, lineCollision.LineStart);

                float V3DotN2 = Vector3.Dot(v3, collisionNormal);
                float V4DotN2 = Vector3.Dot(v4, collisionNormal);

                if ((V3DotN2 * V4DotN2) < 0)
                {
                    // Line segment collision
                    collisionManager.CollisionBetweenEntity(lineEntity, sphereEntity, COLLISIONTYPE.LINE_SPHERE);
                }
            }
        }
    }
}
