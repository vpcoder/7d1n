namespace Engine.Logic.Locations.Generator.Environment.Building.Arrangement
{
    
    public interface IItemPutProcessor<E> where E : struct
    {

        E Type { get; }

        bool TryPutItem(GenerationRoomContext context, IEnvironmentItem<E> currentInsertItem);

    }
    
}