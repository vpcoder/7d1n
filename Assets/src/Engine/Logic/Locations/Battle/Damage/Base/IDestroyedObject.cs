
namespace Engine.Logic.Locations
{

    /// <summary>
    /// 
    /// Уничтожаемый неживой объект
    /// ---
    /// Destructible inanimate object
    /// 
    /// </summary>
    public interface IDestroyedObject : IDamagedObject
    {

        /// <summary>
        ///     Вызывает проверку состояния объекта, может спровоцировать уничтожение этого объекта
        ///     ---
        ///     Causes a check of the object's state, can provoke the destruction of this object
        /// </summary>
        void CheckDestroy();

        /// <summary>
        ///     Вызывается когда необходимо уничтожить объект
        ///     ---
        ///     Called when it is necessary to destroy an object
        /// </summary>
        void DoDestroy();

    }

}
