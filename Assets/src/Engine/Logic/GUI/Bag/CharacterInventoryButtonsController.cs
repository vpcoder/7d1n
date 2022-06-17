using Engine.Data;
using Engine.Data.Factories;
using Engine.Logic.Locations;
using UnityEngine;
using UnityEngine.Assertions;

namespace Engine.Logic
{

    public class CharacterInventoryButtonsController : MonoBehaviour
    {

        public void OnDropClick()
        {
            var panel = ObjectFinder.Find<InventoryActionPanelController>();
            var inventory = ObjectFinder.Find<CharacterBag>();
            var ground = ObjectFinder.Find<GroundBag>();
            var character = ObjectFinder.Find<LocationCharacter>();
            var hands = ObjectFinder.Find<HandsController>();
            var dropController = ObjectFinder.Find<ItemsDropController>();
            var item = panel.Item;

            // Убираем предмет из инвентаря
            Game.Instance.Character.Inventory.RemoveByAddress(item);
            inventory.Items.Remove(item);
            inventory.Redraw();

            // Убираем предмет из экипировки
            Game.Instance.Character.Equipment.TryRemoveItem(item);
            if(hands != null)
                hands.TryRemoveItem(item);

            if(dropController != null) // Выкидываем предмет на карту
                dropController.Drop(character.transform.position, true, item);
            
            // Очищаем панель с выкинутым предметом
            panel.Item = null;

            if(ground != null) // Сканируем всё вокруг персонажа
                ground.ScanGround();
        }

        public void OnUseClick()
        {
            var panel = ObjectFinder.Find<InventoryActionPanelController>();
            var item = panel.Item;

            if(item.Type.IsOneOf(GroupType.Used, GroupType.Food, GroupType.MedKit))
            {
                var used = item as IUsed;
                if(used.UseAction.DoAction())
                {
                    AudioController.Instance.PlaySound(used.UseSoundType);
                    var inventory = ObjectFinder.Find<CharacterBag>();
                    item.Count--;
                    if (item.Count == 0)
                    {
                        panel.Item = null;
                        Game.Instance.Character.Inventory.RemoveByAddress(item);
                    }
                    inventory.Redraw();
                }
            }
        }

    }

}
