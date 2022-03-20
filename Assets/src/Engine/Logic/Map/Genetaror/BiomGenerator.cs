using Mapbox.Unity.Map.Interfaces;
using Mapbox.Unity.MeshGeneration.Data;
using System.Collections.Generic;
using UnityEngine;

namespace Engine.Logic.Map
{

    public class BiomGenerator
    {

        #region Singleton

        private static System.Lazy<BiomGenerator> instance = new System.Lazy<BiomGenerator>(() => new BiomGenerator());
        public static BiomGenerator Instance
        {
            get
            {
                return instance.Value;
            }
        }

        #endregion

        public void Generate(IMapReadable map, UnityTile tile)
        {
            var tileId = tile.UnwrappedTileId;
            var biom = TerrainGenerator.Instance.GetBiom(tileId.X, tileId.Y);

            var biomGeneratorData = ObjectFinder.Find<BiomGeneratorData>();
            tile.Biom = biom;
            tile.transform.DestroyAllChilds();

            Random.InitState(tileId.X * tileId.Y * 7);
            GenerateEntities(biomGeneratorData, tile, biom);
            GenerateSpecial(biomGeneratorData, tile, biom);
        }

        private void GenerateEntities(BiomGeneratorData biomGeneratorData, UnityTile tile, BiomType biom)
        {
            var entitiesList = biomGeneratorData.GetBiomObjects(biom);
            if (entitiesList.Count == 0)
                return;

            int generationCount;
            switch(biom)
            {
                case BiomType.Water:
                    generationCount = 30;
                    break;
                case BiomType.Wooden:
                    generationCount = 60;
                    break;
                case BiomType.Exhausted:
                    generationCount = 10;
                    break;
                default:
                    generationCount = 20;
                    break;
            }

            for (int i = 0; i < generationCount; i++)
                CreateEntity(tile, entitiesList, biom, i);
        }

        private void GenerateSpecial(BiomGeneratorData biomGeneratorData, UnityTile tile, BiomType biom)
        {
            var planePrefab = biomGeneratorData.GetPlane(biom);
            if(planePrefab == null)
                return;
            GameObject plane = GameObject.Instantiate<GameObject>(planePrefab, tile.transform);

            switch (biom)
            {
                case BiomType.Water:
                    WaterEdgeController waterController = plane.GetComponent<WaterEdgeController>();
                    int x = tile.UnwrappedTileId.X;
                    int y = tile.UnwrappedTileId.Y;

                    if(TerrainGenerator.Instance.GetBiom(x, y + 1) == BiomType.Water)
                        waterController.HideItems(WaterEdgeDirection.Forward);

                    if (TerrainGenerator.Instance.GetBiom(x, y - 1) == BiomType.Water)
                        waterController.HideItems(WaterEdgeDirection.Back);

                    if (TerrainGenerator.Instance.GetBiom(x + 1, y) == BiomType.Water)
                        waterController.HideItems(WaterEdgeDirection.Right);

                    if (TerrainGenerator.Instance.GetBiom(x - 1, y) == BiomType.Water)
                        waterController.HideItems(WaterEdgeDirection.Left);

                    break;
                case BiomType.Wooden:

                    break;
                case BiomType.Sand:
                    planePrefab.transform.rotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);
                    break;
                case BiomType.Exhausted:

                    break;
            }
        }

        private void CreateEntity(UnityTile tile, List<GameObject> entitiesList, BiomType biom, int generationIndex)
        {
            var tileId = tile.UnwrappedTileId;

            var entityIndex = Random.Range(0, entitiesList.Count);

            var entity = GameObject.Instantiate<GameObject>(entitiesList[entityIndex], tile.transform);
            entity.transform.position = new Vector3(0f, -1000f, 0f); // Смещаем, чтобы не мешал
            entity.transform.name = tileId.X + "/" + tileId.Y + "/" + tileId.Z;

            var tileSize = TerrainGenerator.Instance.TileSize;

            // Коллидер добавляемого объекта
            var collider = entity.AddComponent<BoxCollider>();

            // Крайняя левая верхняя позиция тайла, на котором мы располагаем сгенерированный объект
            var tilePosition = tile.transform.position - new Vector3(tileSize.x * 0.5f, 0, tileSize.y * 0.5f);

            var newPos = Random.onUnitSphere;
            newPos.y = 0;
            newPos.x = Random.Range(-tileSize.x * 0.35f, + tileSize.x * 0.35f);
            newPos.z = Random.Range(-tileSize.y * 0.35f, + tileSize.y * 0.35f);

            entity.transform.localPosition = newPos;
            entity.transform.localRotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);

            /*
            var destroy = false;
            var hits = Physics.OverlapBox(collider.transform.position, collider.bounds.size);
            if (hits != null && hits.Length > 0)
            {
                foreach (var hit in hits)
                {
                    if (hit.transform == entity.transform
                        || hit.transform.GetComponent<UnityTile>() != null)
                        continue;
                    destroy = true;
                    break;
                }
            }

            if (destroy)
            {
                Debug.LogWarning("entities: " + entitiesList.Count + ", current: " + entityIndex + ", biom:" + biom + ", tile: " + tile.transform.name);
                var marker = new GameObject();
                marker.transform.name = "marker_" + tile.transform.name;
                marker.transform.position = entity.transform.position;
                GameObject.Destroy(entity);
                return;
            }
            */
        }

    }

}
