using OpenGL_Game.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGL_Game.Managers
{
    enum COLLISIONTYPE
    {
        CAMERA_SPHERE,
        SPHERE_SPHERE,
        LINE_CAMERA,
        LINE_SPHERE
    }

    struct Collision
    {
        public Entity entity;
        public Entity otherEntity;
        public COLLISIONTYPE collisionType;
    }

    abstract class CollisionManager
    {
        protected List<Collision> collisionManifold = new List<Collision>();

        public CollisionManager() {}

        public void ClearManifold() { collisionManifold.Clear(); }

        public void CollisionBetweenCamera(Entity entity, COLLISIONTYPE collisionType)
        {
            // If we already have this collision in the manifold then do not add it
            foreach(Collision coll in collisionManifold)
            {
                if (coll.entity == entity) return;
            }
            Collision collision;
            collision.entity = entity;
            collision.otherEntity = null;
            collision.collisionType = collisionType;
            collisionManifold.Add(collision);
        }

        public void CollisionBetweenEntity(Entity entity, Entity otherEntity, COLLISIONTYPE collisionType)
        {
            // If we already have this collision in the manifold then do not add it
            foreach(Collision coll in collisionManifold)
            {
                if (coll.entity == entity && coll.otherEntity == otherEntity)
                {
                    return;
                }
                else if (coll.entity == otherEntity && coll.otherEntity == entity)
                {
                    return;
                }
            }

            Collision collision;
            collision.entity = entity;
            collision.otherEntity = otherEntity;
            collision.collisionType = collisionType;
            collisionManifold.Add(collision);
        }

        public abstract void ProcessCollisions();
    }
}
