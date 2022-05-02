using UnityEngine;

namespace Engine.Logic.Load
{

    public class LoadController : MonoBehaviour
    {

        private void Start()
        {
            ObjectFinder.Find<SceneLoadProcessorBase>().StartLoad();
            Destroy(this);
        }

    }

}
