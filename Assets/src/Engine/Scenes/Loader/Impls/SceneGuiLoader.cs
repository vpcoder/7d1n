using System.Collections.Generic;
using Engine.Data;
using Engine.Data.Factories;
using Engine.Data.Repositories;
using Engine.Logic.Locations;
using Engine.Scenes;
using Engine.Scenes.Loader;
using UnityEngine;

namespace src.Engine.Scenes.Loader.Impls
{
    
    public class SceneGuiLoader : SceneLoaderBase
    {
        
        protected override void OnLoad(LoadContext context)
        {
            //Debug.Log("save character data...");
            //CharacterRepository.Instance.SaveAll(Game.Instance.Character);
            
            Debug.Log("load location ui...");
            var canvas = ObjectFinder.Get<Canvas>("Canvas");

            var panel = Object.Instantiate(context.TopPanel, canvas.transform);
            panel.transform.name = "TopPanel";
            panel.transform.SetAsFirstSibling();

            //var gui = Object.Instantiate(context.LocationGUI, canvas.transform);
            //gui.transform.name = "GUI";
            //gui.transform.SetAsFirstSibling();

            ObjectFinder.Get("SceneView").SetAsFirstSibling();
        }

        protected override void OnPostLoad(LoadContext context)
        {
            var characterPointsList = context.EnemyListInfo;
            var character = Game.Instance.Character;
            
            character.Inventory.Add(DataDictionary.Weapons.WEAPON_PISTOL_PM, 1);
            character.Inventory.Add(DataDictionary.Weapons.AMMO_9X18, 20);

            character.Inventory.Add(DataDictionary.Weapons.WEAPON_KNIFE_KITCHEN, 1);
            character.Inventory.Add(DataDictionary.Weapons.WEAPON_GRENADE_RGD5, 1);
            character.Inventory.Add(DataDictionary.Weapons.WEAPON_GRENADE_F1, 1);
            
            character.Inventory.Add(DataDictionary.Resources.RES_WOOD, 1);
            character.Inventory.Add(DataDictionary.Resources.RES_SCRAP, 1);
            character.Inventory.Add(DataDictionary.Resources.RES_IRON, 1);
            character.Inventory.Add(DataDictionary.Resources.RES_LEAD, 1);
            character.Inventory.Add(DataDictionary.Resources.RES_COOPER, 1);
            character.Inventory.Add(DataDictionary.Resources.RES_ALUMINUM, 1);
            character.Inventory.Add(DataDictionary.Resources.RES_SILVER, 1);
            character.Inventory.Add(DataDictionary.Resources.RES_GOLD, 1);
            character.Inventory.Add(DataDictionary.Resources.RES_TITANIUM, 1);
            character.Inventory.Add(DataDictionary.Resources.RES_PLASTIC, 1);
            character.Inventory.Add(DataDictionary.Resources.RES_PAPER, 1);
            character.Inventory.Add(DataDictionary.Resources.RES_ELECTRIC, 1);
            character.Inventory.Add(DataDictionary.Resources.RES_STONE, 1);
            character.Inventory.Add(DataDictionary.Resources.RES_CERAMICS, 1);
            character.Inventory.Add(DataDictionary.Resources.RES_SAND, 1);
            character.Inventory.Add(DataDictionary.Resources.RES_SALT, 1);
            character.Inventory.Add(DataDictionary.Resources.RES_COAL, 1);
            character.Inventory.Add(DataDictionary.Resources.RES_WATER, 1);
            character.Inventory.Add(DataDictionary.Resources.RES_ACID, 1);
            character.Inventory.Add(DataDictionary.Resources.RES_LYE, 1);
            character.Inventory.Add(DataDictionary.Resources.RES_BRIMSTONE, 1);
            character.Inventory.Add(DataDictionary.Resources.RES_SALTPETRE, 1);
            character.Inventory.Add(DataDictionary.Resources.RES_FUEL, 1);
            character.Inventory.Add(DataDictionary.Resources.RES_FIBER, 1);
            character.Inventory.Add(DataDictionary.Resources.RES_RUBBER, 1);
            character.Inventory.Add(DataDictionary.Resources.RES_POWDER, 1);
            character.Inventory.Add(DataDictionary.Resources.RES_EXPLOSIVE, 1);
            character.Inventory.Add(DataDictionary.Resources.RES_DIRTY_WATER, 1);
            character.Inventory.Add(DataDictionary.Resources.RES_RAW_IRON, 1);
            character.Inventory.Add(DataDictionary.Resources.RES_RAW_LEAD, 1);
            character.Inventory.Add(DataDictionary.Resources.RES_RAW_COOPER, 1);
            character.Inventory.Add(DataDictionary.Resources.RES_RAW_ALUMINUM, 1);
            character.Inventory.Add(DataDictionary.Resources.RES_RAW_SILVER, 1);
            character.Inventory.Add(DataDictionary.Resources.RES_RAW_GOLD, 1);
            character.Inventory.Add(DataDictionary.Resources.RES_RAW_TITANIUM, 1);
            
            character.Inventory.Add(DataDictionary.Foods.FOOD_RAW_ROACH, 2);
            character.Inventory.Add(DataDictionary.Foods.FOOD_RAW_LARVAE, 3);
            character.Inventory.Add(DataDictionary.Foods.FOOD_RAW_FRY_FISH, 1);
            
            character.Inventory.Add(5000, 1);
            character.Inventory.Add(5001, 1);
            character.Inventory.Add(5002, 1);
            character.Inventory.Add(5003, 1);
            character.Inventory.Add(5004, 1);
            character.Inventory.Add(5005, 1);
            character.Inventory.Add(5006, 1);
            character.Inventory.Add(5007, 1);
            character.Inventory.Add(5008, 1);
            character.Inventory.Add(5009, 1);
            character.Inventory.Add(5010, 1);
            character.Inventory.Add(5011, 1);
            character.Inventory.Add(5012, 1);
            character.Inventory.Add(5013, 1);
            character.Inventory.Add(5014, 1);
            character.Inventory.Add(5015, 1);
            character.Inventory.Add(5016, 1);
            character.Inventory.Add(5017, 1);

            var pm = (IFirearmsWeapon)character.Inventory.GetFirstById(DataDictionary.Weapons.WEAPON_PISTOL_PM);
            pm.AmmoCount = 4;
            character.Equipment.Use1 = pm;
            ObjectFinder.Find<HandsController>().GetCell(0).Weapon = pm;

            var battleManager = ObjectFinder.Find<BattleManager>();
            var enemies = new List<CharacterNpcBehaviour>();
            
            for (int i = 0; i < 5; i++)
            {
                if (characterPointsList.Count == 0)
                    break;

                var randomPoint = characterPointsList[Random.Range(0, characterPointsList.Count)];
                
                var behaviour = NpcFactory.Instance.GetBehaviour(Random.Range(100, 101 + 1));
                var pos = randomPoint.Position;

                var rot = Random.rotation.eulerAngles;
                rot.x = 0;
                rot.z = 0;

                var npc = GameObject.Instantiate(behaviour, pos, Quaternion.Euler(rot));
                var npcBehaviour = npc.GetComponent<CharacterNpcBehaviour>();
                npcBehaviour.CharacterContext.Status.State = CharacterStateType.Fighting;
                characterPointsList.Remove(randomPoint);
                enemies.Add(npcBehaviour);
            }
            
            battleManager.EnterToBattle();
            Game.Instance.Runtime.BattleContext.Order.Add(OrderGroup.PlayerGroup);
        }
    }
    
}
