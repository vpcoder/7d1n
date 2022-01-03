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
            Game.Instance.Character.Inventory.Add(1000L, 1);
            Game.Instance.Character.Inventory.Add(2000L, 20);
            Game.Instance.Character.Inventory.Add(2000L, 20);
            Game.Instance.Character.Inventory.Add(2000L, 20);

            Game.Instance.Character.Inventory.Add(3000L, 1);
            Game.Instance.Character.Inventory.Add(4000L, 1);
            Game.Instance.Character.Inventory.Add(4001L, 1);

            ObjectFinder.Find<BattleManager>().EnterToBattle();
        }

    }

}
