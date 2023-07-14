using System.Collections.Generic;
using Engine.Logic.Locations.Animation;
using UnityEngine;

namespace Engine.Logic.Locations
{

    /// <summary>
    /// 
    /// Контекст перемещения
    /// Содержит маршрут перемещения, текущее начальное значение перемещения по отрезку на траектории,
    /// начальный поворот для обзора, скорость выполнения перемещения и обзора, время начала совершения операций на отрезке пути
    /// ---
    /// The movement context
    /// Contains the movement route, the current initial value of the segment on the trajectory,
    /// the initial turn for the look around, the speed of the movement and look around, the start time of the operations on the segment
    /// 
    /// </summary>
    public class NpcMoveActionContext : NpcBaseActionContext
    {

        /// <summary>
        ///     Начальная позиция на текущем отрезке пути
        ///     ---
        ///     Starting position on the current track segment
        /// </summary>
        public Vector3 StartPosition { get; set; }

        /// <summary>
        ///     Начальный поворот на текущем отрезке пути
        ///     ---
        ///     Initial turn on the current track segment
        /// </summary>
        public Quaternion StartRotation { get; set; }

        /// <summary>
        ///     Время начала движения по отрезку пути
        ///     ---
        ///     Start time of the track segment
        /// </summary>
        public float Timestamp { get; set; } = 0f;

        /// <summary>
        ///     Траектория перемещения
        ///     ---
        ///     Trajectory of movement
        /// </summary>
        public List<Vector3> Path { get; set; }

        /// <summary>
        ///     Тип скорости перемещения (шагом, бегом, спринт)
        ///     ---
        ///     Type of travel speed (walking, jogging, sprinting)
        /// </summary>
        public MoveSpeedType MoveSpeedType { get; set; } = MoveSpeedType.Run;

        /// <summary>
        ///     Скорость совершения операций перемещения и поворотов
        ///     ---
        ///     The speed at which movement and turning operations are performed
        /// </summary>
        public float Speed { get; set; } = 1f;

    }

}
