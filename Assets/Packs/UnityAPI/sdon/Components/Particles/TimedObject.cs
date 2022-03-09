
namespace UnityEngine
{

    public class TimedObject : MonoBehaviour
    {

        [SerializeField] private float timeOfLife = 5f;

        private float timestamp;

        private void Start()
        {
            timestamp = Time.time;
        }

        private void Update()
        {
            if (Time.time - timestamp > timeOfLife)
                Destroy(gameObject);
        }

    }

}