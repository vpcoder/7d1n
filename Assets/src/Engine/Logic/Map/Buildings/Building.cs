using Engine.Map;
using Mapbox.Examples;
using Mapbox.Unity.MeshGeneration.Data;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Engine.Logic.Map
{

    public interface IMapEntity
    {
        long ID { get; }
    }

    public class Building : MonoBehaviour, IMapEntity
    {
        private static Building selected;

        public static void Unselect()
        {
            if (selected == null)
                return;

            selected.DoUnselect();
            ObjectFinder.Find<FeatureUiMarker>().Show(null);
        }

        /// <summary>
        /// Объект здания
        /// </summary>
        private VectorEntity entity;

        /// <summary>
        /// Тайл, на котором расположено здание
        /// </summary>
        private UnityTile tile;

        private FeatureUiMarker marker;

        private List<Material> normalMaterialList = new List<Material>();
        private List<Material> selectedMaterialList = new List<Material>();

        public Generator.LocationInfo Info
        {
            get
            {
                return entity.Feature.Info;
            }
        }

        public long ID
        {
            get
            {
                return entity.ID;
            }
        }

        /// <summary>
        /// Нажали на здание
        /// </summary>
        private void OnMouseUpAsButton()
        {
            Unselect();
            selected = this;
            selected.DoSelect();
        }

        public void DoSelect()
        {
            ObjectFinder.Find<MapCharacter>().Target = gameObject;
            marker.Show(entity);
            entity.MeshRenderer.materials = selectedMaterialList.ToArray();
        }

        public void DoUnselect()
        {
            entity.MeshRenderer.materials = normalMaterialList.ToArray();
        }

        /// <summary>
        /// Инициализирует здание
        /// </summary>
        public void Init(VectorEntity entity, UnityTile tile)
        {
            this.entity = entity;
            this.tile = tile;
            this.marker = ObjectFinder.Find<FeatureUiMarker>();

            foreach(var material in entity.MeshRenderer.materials)
            {
                normalMaterialList.Add(material);
                var selectedMaterial = Material.Instantiate<Material>(material);
                selectedMaterial.color = new Color(0.3f, 1f, 0f);
                selectedMaterialList.Add(selectedMaterial);
            }
        }

    }

}
