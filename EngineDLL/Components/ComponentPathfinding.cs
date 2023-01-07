using OpenTK;
using System.Collections.Generic;

namespace OpenGL_Game.Components
{
    class ComponentPathfinding : IComponent
    {
        List<Vector3> waypoints = new List<Vector3>();
        float moveSpeed;
        int currentIndex;
        public bool enabled;

        public ComponentPathfinding(List<Vector3> waypoints, float moveSpeed)
        {
            this.waypoints = waypoints;
            this.moveSpeed = moveSpeed;
            currentIndex = 0;
            enabled = true;
        }

        public ComponentTypes ComponentType
        {
            get { return ComponentTypes.COMPONENT_PATHFINDING; }
        }

        public List<Vector3> Waypoints
        {
            get { return waypoints; }
            set { waypoints = value; }
        }

        public int CurrentIndex
        {
            get { return currentIndex; }
            set { currentIndex = value; }
        }

        public float MoveSpeed
        {
            get { return moveSpeed; }
            set { moveSpeed = value; }
        }

        public void Close()
        {
            waypoints.Clear();
        }
    }
}