using System.Collections.Generic;
using UnityEngine;

namespace Engine.Logic.Locations
{

    public struct SmartPath
    {
        public List<Vector3> ActivePath;
        public List<Vector3> ErrorPath;
        public ActivePoint EdgePoint;

        /// <summary>
        /// Сколько потребуется потратить ОД чтобы совершить активную часть пути
        /// </summary>
        public int ActivePathAP
        {
            get
            {
                if (EdgePoint.IsFullPath)
                    return (int)PathHelper.CalcLength(ActivePath) + 1;
                return (int)EdgePoint.ActivePathInMeters;
            }
        }

        /// <summary>
        /// Сколько бы потребовалось ОД чтобы совершить весь путь
        /// </summary>
        public int FullPathAP
        {
            get
            {
                if (EdgePoint.IsFullPath)
                    return ActivePathAP;
                return (int)(EdgePoint.ActivePathInMeters + EdgePoint.ErrorPathInMeters + 1);
            }
        }
    }

    public struct ActivePoint
    {
        public static ActivePoint EMPTY = new ActivePoint { IsEmpty = true };
        public bool IsEmpty;
        public bool IsFullPath;
        public int Index;
        public float ToNLen;
        public float IntervalLen;
        public float ActivePathInMeters;
        public float ErrorPathInMeters;
    }

    public class PathHelper
    {

        /// <summary>
        /// Рассчитывает и возвращает длину отрезка [p0, p1]
        /// </summary>
        /// <param name="p0">Первая точка отрезка</param>
        /// <param name="p1">Вторая точка отрезка</param>
        /// <returns>Длину отрезка в метрах</returns>
        public static float CalcLength(Vector3 p0, Vector3 p1)
        {
            return (p0 - p1).magnitude;
        }

        /// <summary>
        /// Рассчитывает длину пути представленному в виде последовательности точек
        /// </summary>
        /// <param name="path">Путь в виде последовательности точек</param>
        /// <returns>Длина пути в метрах</returns>
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
        /// Функция Простетника-Ларина (Prostetnic-OpenGL)
        /// Возвращает индекс точки, до которой хватает указанной активной длины
        /// </summary>
        /// <param name="path">Путь в виде точек</param>
        /// <param name="activeLength">Указанная активная длина</param>
        /// <returns>Последний индекс точки в пути до которой гарантированно хватает активной длины</returns>
        public static ActivePoint GetLastActivePoint(List<Vector3> path, float activeLength)
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
        /// Получает координату границы для активной части пути
        /// </summary>
        /// <param name="path">Весь путь</param>
        /// <param name="activeLength">Длина активной части пути в метрах</param>
        public static SmartPath GetSmartPath(Vector3[] path, float activeLength)
        {
            return GetSmartPath(new List<Vector3>(path), activeLength);
        }

        /// <summary>
        /// Получает координату границы для активной части пути
        /// </summary>
        /// <param name="path">Весь путь</param>
        /// <param name="activeLength">Длина активной части пути в метрах</param>
        public static SmartPath GetSmartPath(List<Vector3> path, float activeLength)
        {
            var lastActiveIndex = GetLastActivePoint(path, activeLength);
            if (lastActiveIndex.IsEmpty || lastActiveIndex.IsFullPath)
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
