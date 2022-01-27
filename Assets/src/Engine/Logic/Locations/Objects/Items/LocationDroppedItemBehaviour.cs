using Engine.Data;
using UnityEngine;

namespace Engine.Logic.Locations.Objects
{

    /// <summary>
    /// Объект, выкинутый в локации
    /// </summary>
    public class LocationDroppedItemBehaviour : ItemBehaviour
    {

        /// <summary>
        /// Ссылка на тело объекта (оно будет позиционироваться в пространстве)
        /// </summary>
        [SerializeField] private GameObject body;

        /// <summary>
        /// Инициализирует выкинутый предмет
        /// </summary>
        /// <param name="item">Сериализованные данные о предмете</param>
        /// <param name="worldPosition">Положение выброшенного предмета в мире</param>
        public void Init(ItemInfo itemInfo, Vector3 worldPosition)
        {
            this.ItemInfo = itemInfo;
            // К положению в мире добавляем рандомное смещение в радиусе 0.5ед., чтобы сымитировать небрежное падение
            this.body.transform.position = worldPosition + Random.insideUnitCircle.ToVector3() * 0.5f;
            // Случайно вращаем предмет, типо он так упал
            this.body.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, Random.Range(0f, 360f)));


            this.body.AddComponent<Rigidbody>();
        }

    }

}
