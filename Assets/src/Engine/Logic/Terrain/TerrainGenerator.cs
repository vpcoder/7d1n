using Engine.DB;
using System;
using UnityEngine;

namespace Engine.Logic
{

    /// <summary>
    /// Класс, позволяющий работать с местностью рядом с персонажем и игроком
    /// </summary>
    public class TerrainGenerator
    {

        public Vector3 ColliderSize { get; } = new Vector3(100, 1, 100);

        /// <summary>
        /// Размер ячейки биома
        /// </summary>
        public Vector2 TileSize { get; } = new Vector2(140f, 140f);

        /// <summary>
        /// Половина размера ячейки биома
        /// </summary>
        public Vector2 TileHalfSize { get; } = new Vector2(70f, 70f);

        /// <summary>
        /// Число ячеек-индексов внутри ячейки биома
        /// </summary>
        public Vector2Int LocalIndexCount { get; } = new Vector2Int(10, 10);

        /// <summary>
        /// Размер ячеек индексов (TileSize / LocalIndexCount)
        /// </summary>
        public Vector2 LocalIndexItemSize { get; } = new Vector2(14f, 14f);

        #region Singleton

        private static Lazy<TerrainGenerator> instance = new Lazy<TerrainGenerator>(() => new TerrainGenerator());
        public static TerrainGenerator Instance
        {
            get
            {
                return instance.Value;
            }
        }

        private TerrainGenerator()
        {

        }

        #endregion

        /// <summary>
        /// Вычисляет тип биома по координатам ячейки биома
        /// </summary>
        /// <param name="x">Координаты ячейки</param>
        /// <param name="y">Координаты ячейки</param>
        /// <returns>Тип биома в ячейке</returns>
        public BiomType GetBiom(int x, int y)
        {
            var biomCount = Enums<BiomType>.Count;
            var index = Mathf.RoundToInt(Mathf.PerlinNoise(x / 3f, y / 3f) * 32f);
            var flatIndex = index % biomCount;
            //var flatIndex = ((x * 1000000L + y) % 1000007L) % 4L; //(Enums<BiomType>.Count)

            if (flatIndex < 0 || flatIndex >= biomCount)
                throw new Exception("x: " + x + ", y: " + y);

            return (BiomType)flatIndex;
        }

        /// <summary>
        /// Генерирует предметы в указанной ячейке и возвращает их, либо возвращает ранее сгенерированные предметы из этой ячейки
        /// </summary>
        /// <param name="cellIndex">Индекс ячейки</param>
        /// <param name="actionType">Действие, которое совершает игрок</param>
        /// <param name="info">Информация о ячейке</param>
        /// <returns>Набор из информации и предметов</returns>
        public LocationCellData GetOrGenerateCellData(Vector2Int cellIndex, Vector2Int biomIndex, GenerationActionType actionType, LocalCellInfo info)
        {
            var data = LocationCellFactory.Instance.TryFindData(cellIndex, biomIndex);
            if (data == null)
            {
                data = GenerateCellData(cellIndex, actionType, info);
                LocationCellFactory.Instance.SaveData(data);
            }
            return data;
        }

        /// <summary>
        /// Генерация информации о ячейке
        /// </summary>
        /// <param name="cellIndex">Индекс ячейки</param>
        /// <param name="worldPosition">Позиция персонажа</param>
        /// <returns>Информация о ячейке</returns>
        public LocalCellInfo GetCellInfo(Vector2Int cellIndex, Vector2Int biomIndex)
        {
            var info = new LocalCellInfo();
            info.PosX = cellIndex.x;
            info.PosY = cellIndex.y;
            info.BiomPosX = biomIndex.x;
            info.BiomPosY = biomIndex.y;
            info.Biom = GetBiom(biomIndex.x, biomIndex.y);
            info.Timestamp = DateTime.Now.Ticks;
            info.Wealth = SearshLootCalculationService.GetWealth((byte)(new System.Random((int)info.Biom * cellIndex.x * cellIndex.y).Next() % 100));
            return info;
        }

        private LocationCellData GenerateCellData(Vector2Int cellIndex, GenerationActionType actionType, LocalCellInfo info)
        {
            var data = new LocationCellData();
            data.Info = info;
            switch(actionType)
            {
                case GenerationActionType.Hunt:
                    data.Items = LocationCellGenerationFactory.Instance.GetGenerator(info.Biom).Hunt(info);
                    break;
                case GenerationActionType.Inspect:
                    data.Items = LocationCellGenerationFactory.Instance.GetGenerator(info.Biom).Inspect(info);
                    break;
            }
            return data;
        }

    }

}
