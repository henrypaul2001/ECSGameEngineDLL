using EngineDLL.Objects;

namespace EngineDLL.Systems
{
    public interface ISystem
    {
        void OnAction(Entity entity);

        // Property signatures: 
        string Name
        {
            get;
        }
    }
}
