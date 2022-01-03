using System;
using System.Collections.Generic;

namespace com.baensi.sdon.logic.calculation
{

    public static class LevelCalculator
    {

        /// <summary>
        /// Возвращает количество опыта на текущем уровне, необходимого для перехода на следующий уровень
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public static long GetMaxExp(int level)
        {
            return 50L + (long)(5d * (double)level);
        }



    }

}
