using Engine.Data;
using Engine.DB;
using Engine.EGUI;
using Engine.Logic.Map;
using Engine.Map;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Engine.Logic
{

    /// <summary>
    /// Сканер местности, в которой находится персонаж
    /// </summary>
    public class MapScanController : Panel
    {

        [SerializeField] private Sprite backgroundWaterBiom;
        [SerializeField] private Sprite backgroundWoodenBiom;
        [SerializeField] private Sprite backgroundSandBiom;
        [SerializeField] private Sprite backgroundEarthquakeBiom;
        [SerializeField] private Sprite backgroundExhaustedBiom;

        [SerializeField] private Image imgBackground;
        [SerializeField] private Text txtCellWealth;
        [SerializeField] private Text txtCellInfo;

        [SerializeField] private MapCellBag bag;

        [SerializeField] private GameObject toolsPanel;

        /// <summary>
        /// Информация о текущей ячейке в которой было выполнено сканирование
        /// </summary>
        private LocalCellInfo info;

        private Sprite GetSpriteByBiom(BiomType biom)
        {
            switch(biom)
            {
                case BiomType.Water:      return backgroundWaterBiom;
                case BiomType.Wooden:     return backgroundWoodenBiom;
                case BiomType.Sand:       return backgroundSandBiom;
                case BiomType.Exhausted:  return backgroundExhaustedBiom;
                default:
                    throw new NotSupportedException("biom '" + biom.ToString() + "' not supported!");
            }
        }

        /// <summary>
        /// Текущий локальный индекс ячейки, в котором находится персонаж
        /// </summary>
        private Vector2Int CurrentCellIndex
        {
            get
            {
                var size = TerrainGenerator.Instance.TileSize; // Размер тайла
                var indexSize = TerrainGenerator.Instance.LocalIndexItemSize;
                
                var character = ObjectFinder.Find<MapCharacter>(); // Персонаж
                var localPos = character.CurrentTile.transform.position - character.transform.position; // Относительная позиция персонажа на тайле
                localPos.x -= (size.x / 2);
                localPos.z -= (size.y / 2);

                var indexX = Mathf.RoundToInt(Mathf.Abs(localPos.x) / indexSize.x); // Расчитываем индексы
                var indexY = Mathf.RoundToInt(Mathf.Abs(localPos.z) / indexSize.y);

                Debug.Log("current index: " + indexX + ", " + indexY);

                return new Vector2Int(indexX, indexY);
            }
        }

#if UNITY_EDITOR && DEBUG

        private void OnDrawGizmos()
        {
            var character = ObjectFinder.Find<MapCharacter>();
            var tile = character.CurrentTile?.transform;
            var size = TerrainGenerator.Instance.TileSize;
            var count = TerrainGenerator.Instance.LocalIndexCount;
            var indexSize = TerrainGenerator.Instance.LocalIndexItemSize;

            if (tile == null)
                return;

            Gizmos.color = Color.red;
            for(int x = 0; x < count.x; x++)
            {
                var posX1 = -(size.x / 2) + tile.position.x + x * (size.x / count.x);
                var posX2 = -(size.x / 2) + tile.position.x + x * (size.x / count.x);
                var posZ1 = -(size.y / 2) + tile.position.z;
                var posZ2 = -(size.y / 2) + tile.position.z + TerrainGenerator.Instance.TileSize.y;
                Gizmos.DrawLine(new Vector3(posX1, 0, posZ1), new Vector3(posX2, 0, posZ2));
            }

            for (int z = 0; z < count.y; z++)
            {
                var posX1 = -(size.x / 2) + tile.position.x;
                var posX2 = -(size.x / 2) + tile.position.x + TerrainGenerator.Instance.TileSize.y;
                var posZ1 = -(size.y / 2) + tile.position.z + z * (size.y / count.y);
                var posZ2 = -(size.y / 2) + tile.position.z + z * (size.y / count.y);
                Gizmos.DrawLine(new Vector3(posX1, 0, posZ1), new Vector3(posX2, 0, posZ2));
            }
            
            Gizmos.color = Color.blue;
            var localPos = character.CurrentTile.transform.position - character.transform.position; // Относительная позиция персонажа на тайле
            localPos.x -= (size.x / 2);
            localPos.z -= (size.y / 2);

            var indexX = -Mathf.RoundToInt(localPos.x / indexSize.x); // Расчитываем индексы
            var indexZ = -Mathf.RoundToInt(localPos.z / indexSize.y);

            var pos = new Vector3((indexSize.x * indexX) + tile.position.x - (size.x / 2), 0, (indexSize.y * indexZ) + tile.position.z - (size.y / 2));
            Gizmos.DrawCube(pos, Vector3.one* 10);
        }

#endif

        /// <summary>
        /// Текущий локальный индекс ячейки, в котором находится персонаж
        /// </summary>
        private Vector2Int CurrentBiomIndex
        {
            get
            {
                var character = ObjectFinder.Find<MapCharacter>(); // Персонаж
                var tileID = character.CurrentTile.CanonicalTileId; // Тайл, на котором сейчас находится персонаж
                return new Vector2Int(tileID.X, tileID.Y);
            }
        }

        public void UpdateInfo()
        {
            // Генерируем информацию о сканируемой ячейке
            info = TerrainGenerator.Instance.GetCellInfo(CurrentCellIndex, CurrentBiomIndex);
            Show();
            imgBackground.sprite = GetSpriteByBiom(info.Biom);
            txtCellWealth.text = Localization.Instance.Get("msg_map_scan_wealth") + ": " + info.Wealth + "%";
            txtCellInfo.text = CreateInfoText();

            LoadBagData();
        }

        /// <summary>
        /// Информация о биоме, которая отобразится игроку
        /// </summary>
        private string CreateInfoText()
        {
            var biom = info.Biom;
            switch(biom)
            {
                case BiomType.Water:      return Localization.Instance.Get("msg_map_scan_info_water_biom");
                case BiomType.Wooden:     return Localization.Instance.Get("msg_map_scan_info_wooden_biom");
                case BiomType.Sand:       return Localization.Instance.Get("msg_map_scan_info_sand_biom");
                case BiomType.Exhausted:  return Localization.Instance.Get("msg_map_scan_info_exhausted_biom");
                default:
                    throw new NotSupportedException();
            }
        }

        private void SaveBagData()
        {
            var data = LocationCellFactory.Instance.TryFindData(CurrentCellIndex, CurrentBiomIndex); // Получаем значение из БД
            if (data != null) // Если для ячейки была генерация
            {
                // Сохраняем текущее состояние ячейки в БД
                data.Items = bag.Items.Select(ItemSerializator.Convert).ToList();
                LocationCellFactory.Instance.SaveData(data);
            }
            bag.Data = null; // Чистим сумку ячейки и закрываем интерфейс
        }

        private void LoadBagData()
        {
            var data = LocationCellFactory.Instance.TryFindData(CurrentCellIndex, CurrentBiomIndex);
            if (data != null)
            {
                bag.gameObject.SetActive(true);
                bag.Data = data;
                toolsPanel.SetActive(false);
            }
            else
            {
                bag.gameObject.SetActive(false);
                toolsPanel.SetActive(true);
            }
        }

        /// <summary>
        /// Сканирование местности
        /// </summary>
        public void DoScanClick()
        {
            if (Visible)
            {
                SaveBagData();
                Hide();
            }
            else
            {
                ObjectFinder.Find<EnterBuildControll>().Hide();
                Show();
                UpdateInfo();
            }
        }

        /// <summary>
        /// Игрок выполняет исследование местности
        /// </summary>
        public void DoInspectClick()
        {
            toolsPanel.SetActive(false);
            var data = TerrainGenerator.Instance.GetOrGenerateCellData(CurrentCellIndex, CurrentBiomIndex, GenerationActionType.Inspect, info);
            bag.gameObject.SetActive(true);
            bag.Data = data;
        }

        /// <summary>
        /// Игрок выполняет мелкую охоту
        /// </summary>
        public void DoHuntClick()
        {
            toolsPanel.SetActive(false);
            var data = TerrainGenerator.Instance.GetOrGenerateCellData(CurrentCellIndex, CurrentBiomIndex, GenerationActionType.Hunt, info);
            bag.gameObject.SetActive(true);
            bag.Data = data;
        }

    }

}
