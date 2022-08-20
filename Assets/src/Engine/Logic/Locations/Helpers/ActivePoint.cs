
namespace Engine.Logic.Locations
{

    /// <summary>
    /// 
    /// Параметры точки, которая делит траекторию пути на две части - путь на который хватает ОД, и путь на который не хватает ОД.
    /// Если на весь путь хватает ОД, эта точка находится в самом конце, и поднят флаг IsFullPath.
    /// ---
    /// Parameters of the point that divides the path into two parts - the path that has enough APs, and the path that does not have enough APs.
    /// If there is enough APs for the whole path, this point is at the very end, and the IsFullPath flag is raised.
    /// 
    /// </summary>
    public struct ActivePoint
    {

        /// <summary>
        ///     Экземпляр пустой точки, определяющий что путь пустой, либо что путь состоит из 2 точек
        ///     ---
        ///     An instance of an empty point that determines that the path is empty, or that the path consists of 2 points
        /// </summary>
        public static ActivePoint EMPTY { get; } = new ActivePoint { IsEmpty = true, IsFullPath = true };

        #region Fields

        /// <summary>
        ///     Флаг того что путь либо пустой, либо состоит из 2 точек
        ///     ---
        ///     Flag that the path is either empty or consists of 2 points
        /// </summary>
        public bool IsEmpty;

        /// <summary>
        ///     Флаг того что персонажу хватает ОД для совершения всего пути
        ///     ---
        ///     Flag that the character has enough APs to complete the entire journey
        /// </summary>
        public bool IsFullPath;

        /// <summary>
        ///     Индекс точки, в которой траектория пути делится на доступную часть пути, и часть пути на которую не хватает ОД
        ///     ---
        ///     The index of the point at which the path divides into the available part of the path, and the part of the path for which there is not enough APs
        /// </summary>
        public int Index;

        /// <summary>
        ///     Длина пути до точки на траектории пути. Ранее этой точки будет точка которая делит траекторию на две части - доступная часть траектории, и недостижимая часть траектории.
        ///     ---
        ///     The length of the path to a point on the path trajectory. Earlier than this point will be the point that divides the trajectory into two parts - the accessible part of the trajectory, and the unattainable part of the trajectory.
        /// </summary>
        public float ToNLen;

        /// <summary>
        ///     Длина до которой хватает доступных ОД, на основании этой длины вычисляется положение точки которая делит траекторию на две части траектории
        ///     ---
        ///     The length to which there is enough available APs, based on this length the position of the point that divides the trajectory into two parts of the trajectory is calculated
        /// </summary>
        public float IntervalLen;

        /// <summary>
        ///     Длина доступной части траектории пути (на который хватает ОД) в метрах
        ///     ---
        ///     Length of the accessible part of the path (for which there is enough APs) in meters
        /// </summary>
        public float ActivePathInMeters;

        /// <summary>
        ///     Длина недостижимой части траектории (на которую не хватает ОД) в метрах
        ///     ---
        ///     Length of the unreachable part of the trajectory (for which there is not enough APs) in meters
        /// </summary>
        public float ErrorPathInMeters;

        #endregion

    }

}
