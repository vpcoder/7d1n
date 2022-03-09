using Engine.Data;
using Engine.Data.Factories;
using Engine.Logic.Locations;
using System.Collections;
using UnityEngine;

namespace Engine.Logic.Load
{

    public class LocationLoadProcessor : SceneLoadProcessorBase
    {
        private const float MIN_WAIT = 0.001f;
        private bool isLoaded = false;

        private void Awake()
        {
            if(!isLoaded)
                StartCoroutine(LoadProcess());
        }

        private IEnumerator LoadProcess()
        {
            isLoaded = true;
            StartLoad();

            SetTitle(Localization.Instance.Get("ui_loading"));
            yield return new WaitForSeconds(MIN_WAIT);

            // Генерируем навмеш, для работы с путями
            SetDescription(Localization.Instance.Get("ui_location_load_navmesh"));
            yield return new WaitForSeconds(MIN_WAIT);
            ObjectFinder.Find<NavMeshGenerator>().CreateNavMesh();


            Test();


            // Конец загрузки
            CompleteLoad();
        }

        private GameObject character;

        public override void OnCompleteLoad()
        {
            character.SetActive(true);

            ObjectFinder.Find<BattleManager>().EnterToBattle();

            Destroy(gameObject);
        }

        public override void OnStartLoad()
        {
            character = ObjectFinder.Find<LocationCharacter>().gameObject;
            character.SetActive(false);
        }
        
        private void Test()
        {
            var character = Game.Instance.Character;

            character.Inventory.Add(1000L, 1);
            character.Inventory.Add(2000L, 20);

            character.Inventory.Add(3000L, 1);
            character.Inventory.Add(4000L, 1);
            character.Inventory.Add(4001L, 1);

            var pm = (IFirearmsWeapon)character.Inventory.GetFirstById(1000L);
            pm.AmmoCount = 4;
            character.Equipment.Use1 = pm;
            ObjectFinder.Find<HandsController>().GetCell(0).Weapon = pm;

            for (int i = 0; i < 10; i++)
            {
                var behaviour = NpcFactory.Instance.GetBehaviour(100L);
                var pos = Random.insideUnitSphere * 10;
                pos.y = 0;
                pos.x = pos.x > 0 ? -pos.x : pos.x;
                pos.z = pos.z < 0 ? -pos.z : pos.z;

                var rot = Random.rotation.eulerAngles;
                rot.x = 0;
                rot.z = 0;

                var npc = GameObject.Instantiate<GameObject>(behaviour, pos, Quaternion.Euler(rot));
            }

        }

    }

}
