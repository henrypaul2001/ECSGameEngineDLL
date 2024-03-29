﻿using System;

namespace EngineDLL.Components
{
    [FlagsAttribute]
    public enum ComponentTypes {
        COMPONENT_NONE     = 0,
	    COMPONENT_POSITION = 1 << 0,
        COMPONENT_GEOMETRY = 1 << 1,
        COMPONENT_TEXTURE  = 1 << 2,
        COMPONENT_VELOCITY = 1 << 3,
        COMPONENT_SHADER = 1 << 4,
        COMPONENT_AUDIO = 1 << 5,
        COMPONENT_COLLISION_SPHERE = 1 << 6,
        COMPONENT_COLLISION_LINE = 1 << 7,
        COMPONENT_PATHFINDING = 1 << 8
    }

    public interface IComponent
    {
        ComponentTypes ComponentType
        {
            get;
        }

        void Close();
    }
}
