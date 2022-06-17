using System;
using System.Collections.Generic;

namespace Engine.Data
{

    /// <summary>
    /// 
    /// Ресурс, из которого состоят предметы, так же является предметом.
    /// Ресурсы более примитивный вид предметов, которые не состоят из других частей и не являются инструментами.
    /// ---
    /// The resource that makes up the objects is also an object.
    /// Resources are a more primitive kind of objects that do not consist of other parts and are not tools.
    /// 
    /// </summary>
    [Serializable]
    public class Resource : Entity, IResource
    {

        ///<summary>
        ///     Группа Resource
        ///     ---
        ///     Resource Group
        ///</summary>
        public override GroupType Type { get { return GroupType.Resource; } set { } }

        ///<summary>
        ///     Ресурс не может быть инструментом!
        ///     ---
        ///     A resource cannot be a tool!
        ///</summary>
        public override ISet<ToolType> ToolType { get { return null; } set { } }

        ///<summary>
        ///     Ресурс не состоит из частей, его нельзя разделить!
        ///     ---
        ///     A resource is not made up of parts, it cannot be divided!
        ///</summary>
        public List<Part> Parts { get { return null; } set { } }

        ///<summary>
        ///     Количество этого ресурса
        ///     ---
        ///     The amount of this resource
        ///</summary>
        public long Count { get; set; }

        ///<summary>
        ///     Максимальное количество ресурсов в пачке
        ///     ---
        ///     Maximum number of resources in a stack
        ///</summary>
        public long StackSize { get; set; }

        /// <summary>
        ///     Копирует текущую сущность в новый экземпляр
        ///     ---
        ///     Copies the current entity into a new instance
        /// </summary>
        /// <returns>
        ///     Копия сущности
        ///     ---
        ///     Entity Copy
        /// </returns>
        public override IIdentity Copy()
        {
            return new Resource()
            {
                ID = ID,
                Name = Name,
                Description = Description,
                Count = Count,
                Weight = Weight,
                StackSize = StackSize,
            };
        }

    }

}
