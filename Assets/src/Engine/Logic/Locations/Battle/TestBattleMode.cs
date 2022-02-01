using Engine.Data;
using Engine.Data.Factories;
using UnityEngine;

namespace Engine.Logic.Locations
{

    public class TestBattleMode : MonoBehaviour
    {

        [SerializeField] private float delay = 2f;

        private float timestamp;

        private void Start()
        {
            timestamp = Time.time;
        }

        private void Update()
        {
            if (Time.time - timestamp < delay)
                return;

            SwitchBattle();

            Destroy(this);
        }

        private void SwitchBattle()
        {
            var character = Game.Instance.Character;

            character.Inventory.Add(1000L, 1);
            character.Inventory.Add(2000L, 20);
            
            character.Inventory.Add(3000L, 1);
            character.Inventory.Add(4000L, 1);
            character.Inventory.Add(4001L, 1);

            var pm = (IFirearmsWeapon)character.Inventory.Items[0];
            pm.AmmoCount = 4;
            character.Equipment.Use1 = pm;
            ObjectFinder.Find<HandsController>().GetCell(0).Weapon = pm;
            ObjectFinder.Find<BattleManager>().EnterToBattle();


            for (int i = 1; i < 5; i++)
            {
                var behaviour = NpcFactory.Instance.GetBehaviour(100L);
                var pos = Random.insideUnitSphere * 5;
                pos.y = 0;
                var rot = Random.rotation.eulerAngles;
                rot.x = 0;
                rot.z = 0;

                var npc = GameObject.Instantiate<GameObject>(behaviour, pos, Quaternion.Euler(rot));
            }
        }

    }

}
