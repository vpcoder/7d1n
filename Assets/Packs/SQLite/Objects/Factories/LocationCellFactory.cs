using Engine.Collections;
using System.Linq;
using Engine.Logic;
using UnityEngine;
using System;

namespace Engine.DB
{

    public class LocationCellFactory : DbCollection<LocationCell>
    {

        private static readonly Lazy<LocationCellFactory> instance = new Lazy<LocationCellFactory>(() => new LocationCellFactory());
        public static LocationCellFactory Instance { get { return instance.Value; } }


        public LocationCellData TryFindData(Vector2Int cellIndex, Vector2Int biomIndex)
        {
            // Ищем ячейку по запросу
            var first = Query("pos_x = ? AND pos_y = ? AND biom_pos_x = ? AND biom_pos_y = ?", cellIndex.x, cellIndex.y, biomIndex.x, biomIndex.y)?.FirstOrDefault();
            if (first == null)
                return null; // В БД ничего нет по этой ячейке

            // Получаем объект
            var data = JsonUtility.FromJson<LocationCellData>(first.Data);
            // Информация была сгенерирована более 24 часов назад, уже протухла

            if (TimeSpan.FromTicks(DateTime.Now.Ticks - data.Info.Timestamp).TotalHours >= 24d)
            {
                // Удаляем тухлую запись из БД
                Delete(first.ID);
                data = null;
            }
            return data;
        }

        public void SaveData(LocationCellData data)
        {
            // Удаляем предыдущую запись по этом индексу ячейки, если она есть
            DeleteQuery("pos_x = ? AND pos_y = ? AND biom_pos_x = ? AND biom_pos_y = ?", data.Info.PosX, data.Info.PosY, data.Info.BiomPosX, data.Info.BiomPosY);

            // Подготавливаем данные
            var item = new LocationCell();
            item.ID = NextID;
            item.PosX = data.Info.PosX;
            item.PosY = data.Info.PosY;
            item.BiomPosX = data.Info.BiomPosX;
            item.BiomPosY = data.Info.BiomPosY;
            item.Data = JsonUtility.ToJson(data);

            // Записываем в БД
            Save(item);
        }

    }

}
