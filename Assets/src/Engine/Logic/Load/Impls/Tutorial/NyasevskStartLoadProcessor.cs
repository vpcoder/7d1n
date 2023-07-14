using System.Collections;
using UnityEngine;

namespace Engine.Logic.Load.Tutorial
{
    
    public class NyasevskStartLoadProcessor : SceneLoadProcessorBase
    {
        
        public override IEnumerator LoadProcess()
        {
            // Инициализатор загрузчика
            StartLoad();
            
            yield return new WaitForSeconds(MIN_WAIT);
            
            // Конец загрузки (деструктор загрузчика)
            CompleteLoad();
        }

        public override void OnCompleteLoad()
        {
            Destroy(gameObject);
        }

        public override void OnStartLoad()
        {
            
        }

    }
}