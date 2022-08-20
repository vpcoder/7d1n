
using Engine.Generator;

namespace Engine.Data.Generation
{

    /// <summary>
    /// 
    /// </summary>
    public interface IGenerationElementFactory
    {

        LocationType LocationType { get; }

    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T">
    /// 
    /// </typeparam>
    /// <typeparam name="E">
    /// 
    /// </typeparam>
    public interface IGenerationElementFactory<T, E> : IGenerationElementFactory
                                                     where T : class, IElementIdentity<E>
                                                     where E : struct
    {

        int GetCount(E type);

        T Get(E type, long id);

    }

}
