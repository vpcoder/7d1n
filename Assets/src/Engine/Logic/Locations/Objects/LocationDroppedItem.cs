using Engine.Data;
using Engine.Data.Factories;
using UnityEngine;

namespace Engine.Logic.Locations
{

    /// <summary>
    /// Объект, выкинутый в локации
    /// </summary>
    public class LocationDroppedItem : MonoBehaviour
    {

        /// <summary>
        /// Ссылка на тело объекта (оно будет позиционироваться в пространстве)
        /// </summary>
        [SerializeField] private GameObject body;

        /// <summary>
        /// Ссылка на отрисовщика спрайта
        /// </summary>
        [SerializeField] private new SpriteRenderer renderer;

        /// <summary>
        /// Сериализованные данные о предмете, который выкинули
        /// </summary>
        public ItemInfo Item { get; set; }

        /// <summary>
        /// Инициализирует выкинутый предмет
        /// </summary>
        /// <param name="item">Сериализованные данные о предмете</param>
        /// <param name="worldPosition">Положение выброшенного предмета в мире</param>
        public void Init(ItemInfo item, Vector3 worldPosition)
        {
            this.Item = item;
            // Устанавливаем спрайт предмета
            this.renderer.sprite = SpriteFactory.Instance.Get(item.ID);
            // К положению в мире добавляем рандомное смещение в радиусе 0.5ед., чтобы сымитировать небрежное падение
            this.body.transform.position = worldPosition + Random.insideUnitCircle.ToVector3() * 0.5f;
            // Случайно вращаем предмет, типо он так упал
            this.body.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, Random.Range(0f, 360f)));
        }

    }

}
