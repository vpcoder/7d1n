using Engine.Data;
using System;
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
        }

    }

}
