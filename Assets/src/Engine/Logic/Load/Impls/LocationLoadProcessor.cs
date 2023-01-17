using Engine.Data;
using Engine.Logic.Locations;
using System.Collections;
using Engine.Scenes;
using Engine.Scenes.Loader;
using src.Engine.Scenes.Loader;
using UnityEngine;

namespace Engine.Logic.Load
{

    public class LocationLoadProcessor : SceneLoadProcessorBase
    {
        
        public override IEnumerator LoadProcess()
        {
            // Инициализатор загрузчика
            StartLoad();

            // Выполняем загрузку локации из хранилища локаций или генерируем новую
            ObjectFinder.Find<LocationLoader>().LoadLocation(this, Game.Instance.Runtime.Location);

            SetTitle(Localization.Instance.Get("ui_loading"));
            yield return new WaitForSeconds(MIN_WAIT);

            // Генерируем навмеш, для работы с путями
            SetDescription(Localization.Instance.Get("ui_location_load_navmesh"));
            yield return new WaitForSeconds(MIN_WAIT);
            ObjectFinder.Find<NavMeshGenerator>().CreateNavMesh();
            
            // Загружаем сцену
            LoadFactory.Instance.PostLoad(SceneName.Build, new LoadContext()
            {
                EnemyListInfo = Game.Instance.Runtime.GenerationInfo.EnemyInfo.EnemyStartPoints
            });

            // Конец загрузки (деструктор загрузчика)
            CompleteLoad();
        }

        private GameObject character;
        
        public override void OnCompleteLoad()
        {
            character.SetActive(true);
            Destroy(gameObject);
        }

        public override void OnStartLoad()
        {
            character = ObjectFinder.Find<LocationCharacter>().gameObject;
            character.SetActive(false);
        }

    }

}
