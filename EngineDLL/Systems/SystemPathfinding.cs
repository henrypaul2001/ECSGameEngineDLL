using EngineDLL.Components;
using EngineDLL.Objects;
using EngineDLL.Systems;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;

namespace EngineDLL.Engine.Systems
{
    public class SystemPathfinding : ISystem
    {
        const ComponentTypes MASK = (ComponentTypes.COMPONENT_POSITION | ComponentTypes.COMPONENT_PATHFINDING);

        public SystemPathfinding() {}

        public string Name
        {
            get { return "SystemPathfinding"; }
        }

        public void OnAction(Entity entity)
        {
            if ((entity.Mask & MASK) == MASK)
            {
                List<IComponent> components = entity.Components;

                IComponent pathComponent = components.Find(delegate (IComponent component)
                {
                    return component.ComponentType == ComponentTypes.COMPONENT_PATHFINDING;
                });
                ComponentPathfinding pathfinder = (ComponentPathfinding)pathComponent;

                IComponent positionComponent = components.Find(delegate (IComponent component)
                {
                    return component.ComponentType == ComponentTypes.COMPONENT_POSITION;
                });
                ComponentPosition position = (ComponentPosition)positionComponent;

                MoveToNextPoint(position, pathfinder);
            }
        }

        public void MoveToNextPoint(ComponentPosition position, ComponentPathfinding pathfinder)
        {
            if (pathfinder.enabled)
            {
                List<Vector3> waypoints = pathfinder.Waypoints;
                int currentIndex = pathfinder.CurrentIndex;
                Vector3 currentPosition = position.Position;

                if (currentIndex != waypoints.Count() - 1)
                {
                    Vector3 nextPoint = waypoints[currentIndex + 1];
                    if (Vector3.Distance(currentPosition, nextPoint) > 0.1f)
                    {
                        Vector3 direction = nextPoint - currentPosition;
                        direction.Normalize();

                        currentPosition += direction * pathfinder.MoveSpeed;
                    }
                    else
                    {
                        // Point has been reached
                        currentIndex++;
                        if (currentIndex == waypoints.Count() - 1)
                        {
                            // End reached - reverse
                            waypoints.Reverse();
                            currentIndex = 0;
                        }
                    }
                }

                // Update pathfinding component with the most recent point visited and the updated list of points
                pathfinder.CurrentIndex = currentIndex;
                pathfinder.Waypoints = waypoints;

                // Update position component with most recent position
                position.Position = currentPosition;
            }
        }
    }
}
