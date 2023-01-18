using Engine.Data;
using Engine.Logic.Locations;
using Engine.Logic.Locations.Objects;
using System.Collections.Generic;
using UnityEngine;

namespace Engine.Logic
{

    public class GroundBag : Bag
    {

        private readonly IDictionary<IItem, LocationDroppedItemBehaviour> groundspace = new Dictionary<IItem, LocationDroppedItemBehaviour>();

        /// <summary>
        /// Сканирует область вокруг персонажа игрока и находит предметы, лежащие "на земле"
        /// </summary>
        public void ScanGround()
        {
            if (Game.Instance.Runtime.Scene == Scenes.SceneName.Map)
                return;

            var character = ObjectFinder.Find<LocationCharacter>();

            this.groundspace.Clear();
            this.Items.Clear();
            foreach(var hit in Physics.SphereCastAll(character.transform.position, character.PickUpDistance, Vector3.right))
            {
                var dropped = hit.collider.gameObject.GetComponent<LocationDroppedItemBehaviour>();
                if (dropped == null)
                    continue;

                var item = dropped.Item;
                groundspace.Add(item, dropped);
                Items.Add(item);
            }

            Redraw();
        }

        public override void ClickItem(IItem item)
        {
            var dropped = groundspace[item];
            groundspace.Remove(item);
            GameObject.Destroy(dropped.gameObject);

            Game.Instance.Character.Inventory.Add(item);
            Items.Remove(item);
            Redraw();
            ObjectFinder.Find<CharacterBag>().Redraw();
        }

    }

}
