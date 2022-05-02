using UnityEngine;

namespace Engine.Logic.Locations
{

    /// <summary>
    /// 
    /// Контекст обзора.
    /// Содержит информацию необходимую для совершения поворотов NPC для обзора
    /// ---
    /// Look around context.
    /// Contains the information needed to make NPC turns for the look around
    /// 
    /// </summary>
    public class NpcLookActionContext : NpcBaseActionContext
    {

        /// <summary>
        /// Начальное вращение до операции обзора
        /// ---
        /// Initial rotation before the look around operation
        /// </summary>
        public Quaternion StartRotation { get; set; }

        /// <summary>
        /// Точка интереса, куда NPC должен посмотреть
        /// ---
        /// The point of interest where the NPC should look
        /// </summary>
        public Vector3 LookPoint { get; set; }

        /// <summary>
        /// Скорость выполнения операции обзора
        /// ---
        /// Speed of execution of the look around operation
        /// </summary>
        public float Speed { get; set; } = 1f;

    }

}
