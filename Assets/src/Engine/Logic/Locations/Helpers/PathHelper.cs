using System.Collections.Generic;
using UnityEngine;

namespace Engine.Logic.Locations
{

    /// <summary>
    /// 
    /// Хелпер для рассчётов траектории перемещения персонажа на локации
    /// ---
    /// Helper for calculating the trajectory of the character on the location
    /// 
    /// </summary>
    public class PathHelper
    {

        /// <summary>
        ///     Рассчитывает и возвращает длину отрезка [p0, p1]
        ///     ---
        ///     Calculates and returns the length of the segment [p0, p1]
        /// </summary>
        /// <param name="p0">
        ///     Первая точка отрезка
        ///     ---
        ///     The first point of the segment
        /// </param>
        /// <param name="p1">
        ///     Вторая точка отрезка
        ///     ---
        ///     The second point of the segment
        /// </param>
        /// <returns>
        ///     Длину отрезка в метрах
        ///     ---
        ///     The length of the segment in meters
        /// </returns>
        private static float CalcLength(Vector3 p0, Vector3 p1)
        {
            return (p0 - p1).magnitude;
        }

        /// <summary>
        ///     Рассчитывает длину пути представленному в виде последовательности точек
        ///     ---
        ///     Calculates the length of the path represented as a sequence of points
        /// </summary>
        /// <param name="path">
        ///     Путь в виде последовательности точек
        ///     ---
        ///     The path as a sequence of points
        /// </param>
        /// <returns>
        ///     Длина пути в метрах
        ///     ---
        ///     Path length in meters
        /// </returns>
        public static float CalcLength(List<Vector3> path)
        {
            if (path == null || path.Count < 2)
                return 0;

            var length = 0f;
            var prev = path[0];
            for(int i = 1; i < path.Count; i++)
            {
                length += CalcLength(prev, path[i]);
                prev = path[i];
            }
            return length;
        }

        /// <summary>
        ///     Функция "Простетника-Ларина" (Prostetnic-OpenGL ники ребят с forum.sources.ru)
        ///     Возвращает индекс точки, до которой хватает указанной активной длины
        ///     ---
        ///     Prostetnic-Larin function (Prostetnic-OpenGL nick-names from forum.sources.ru)
        ///     Returns the index of the point up to which the specified active length is missing
        /// </summary>
        /// <param name="path">
        ///     Путь в виде точек
        ///     ---
        ///     A path in the form of dots
        /// </param>
        /// <param name="activeLength">
        ///     Указанная активная длина
        ///     ---
        ///     Specified active length
        /// </param>
        /// <returns>
        ///     Последний индекс точки в пути до которой гарантированно хватает активной длины
        ///     ---
        ///     The last index of the point in the path to which the active length is guaranteed
        /// </returns>
        private static ActivePoint GetLastActivePoint(List<Vector3> path, float activeLength)
        {
            if (path == null || path.Count < 2)
                return ActivePoint.EMPTY;
            var allPathSize = 0f;
            var pathCount = path.Count;
            var index = path.Count - 1;
            var isFullPath = true;
            var nLen = 0f;
            var intervalLen = 0f;
            var activeLen = 0f;
            bool needCalc = true;
            for (int i = 1; i < pathCount; ++i)
            {
                var len = CalcLength(path[i - 1], path[i]); // Длина текущего отрезка
                var nextPointMagnitude = allPathSize + len;
                if(needCalc && nextPointMagnitude > activeLength)
                {
                    needCalc = false;
                    index = i - 1;
                    isFullPath = false;
                    nLen = allPathSize == 0 ? activeLength : activeLength - allPathSize;
                    intervalLen = allPathSize == 0 ? nextPointMagnitude : len;
                    activeLen = allPathSize + nLen;
                }
                allPathSize = nextPointMagnitude;
            }

            return new ActivePoint
            {
                Index = index,
                ToNLen = nLen,
                IntervalLen = intervalLen,
                IsFullPath = isFullPath,
                ActivePathInMeters = activeLen,
                ErrorPathInMeters = allPathSize - activeLen,
            };
        }

        /// <summary>
        ///     Получает координату границы для активной части пути
        ///     ---
        ///     Gets the border coordinate for the active part of the path
        /// </summary>
        /// <param name="path">
        ///     Весь путь
        ///     ---
        ///     All the way
        /// </param>
        /// <param name="activeLength">
        ///     Длина активной части пути в метрах
        ///     ---
        ///     Length of the active part of the track in meters
        /// </param>
        public static SmartPath GetSmartPath(List<Vector3> path, float activeLength)
        {
            var lastActiveIndex = GetLastActivePoint(path, activeLength);
            if (lastActiveIndex.IsFullPath)
                return new SmartPath { EdgePoint = lastActiveIndex, ActivePath = path };

            // Вычисляем прогресс-расстояние между следующими точкам
            var progress = (1f / lastActiveIndex.IntervalLen) * lastActiveIndex.ToNLen;
            var point = Vector3.Lerp(path[lastActiveIndex.Index], path[lastActiveIndex.Index + 1], progress);

            var active = new List<Vector3>();
            var error = new List<Vector3>();

            for(int i = 0; i <= lastActiveIndex.Index; i++)
                active.Add(path[i]);

            active.Add(point);
            error.Add(point);

            for (int i = lastActiveIndex.Index + 1; i < path.Count ; i++)
                error.Add(path[i]);

            return new SmartPath
            {
                EdgePoint = lastActiveIndex,
                ActivePath = active,
                ErrorPath = error,
            };
        }

    }

}
