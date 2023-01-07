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
    class SystemCollisionEntitySphere : ISystem
    {
        const ComponentTypes MASK = (ComponentTypes.COMPONENT_POSITION | ComponentTypes.COMPONENT_COLLISION_SPHERE);

        CollisionManager collisionManager;
        EntityManager entityManager;

        public SystemCollisionEntitySphere(CollisionManager collisionManager, EntityManager entityManager)
        {
            this.collisionManager = collisionManager;
            this.entityManager = entityManager;
        }

        public string Name
        {
            get { return "SystemCollisionEntitySphere"; }
        }

        public void OnAction(Entity entity)
        {
            // Check for collision sphere components and position
            if ((entity.Mask & MASK) == MASK)
            {
                List<IComponent> components = entity.Components;

                // Get components
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

                // Loop through other entities
                foreach (Entity e in entityManager.Entities())
                {
                    // Check if other entity has correct components
                    if ((e.Mask & MASK) == MASK)
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
                        SphereCollision(entity, position, collision, e, otherPosition, otherCollision);
                    }
                }
            }
        }

        public void SphereCollision(Entity entity, ComponentPosition position, ComponentCollisionSphere collision, Entity otherEntity, ComponentPosition otherPosition, ComponentCollisionSphere otherCollision)
        {
            // Check if colliding with self
            if (entity != otherEntity)
            {
                if ((position.Position - otherPosition.Position).Length < collision.CollisionRadius + otherCollision.CollisionRadius)
                {
                    collisionManager.CollisionBetweenEntity(entity, otherEntity, COLLISIONTYPE.SPHERE_SPHERE);
                }
            }
        }
    }
}