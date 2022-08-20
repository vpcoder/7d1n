using Mapbox.Unity.Location;
using Mapbox.Unity.Map;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Engine.Logic
{

    public class SceneStatus : MonoBehaviour
    {
        private float timestamp;

        [SerializeField] private AbstractLocationProvider locationProvider;
        [SerializeField] private AbstractMap map;
        [SerializeField] private GameObject loadingBackground;
        [SerializeField] private Text txtDescription;

        private InetWeb web = new InetWeb();
        private StringBuilder builder = new StringBuilder();

        private void Update()
        {
            if (Time.time - timestamp < 1f)
                return;

            timestamp = Time.time;

            GpsStatus = locationProvider.CurrentLocation.IsLocationServiceEnabled && locationProvider.CurrentLocation.IsLocationUpdated;
            InetStatus = web.Ping("https://google.com");

            builder.Clear();

            PutMessage(Localization.Instance.Get("msg_load_inet"), InetStatus);
            PutMessage(Localization.Instance.Get("msg_load_gps"), GpsStatus);

            txtDescription.text = builder.ToString();

            if(InetStatus && GpsStatus)
            {
                Loaded();
                Destroy(loadingBackground);
                Destroy(this);
            }

        }

        private void Loaded()
        {
            var pos = locationProvider.CurrentLocation.LatitudeLongitude;
            Debug.Log("loaded pos: " + pos);
            map.Initialize(pos, 17.5f);
            map.UpdateMap();
        }

        private void PutMessage(string title, bool enabled)
        {
            builder.Append(title);
            builder.Append(enabled ? ": <color=\"#0f0\">" : ": <color=\"#f00\">");
            builder.Append(enabled ? Localization.Instance.Get("msg_load_on") : Localization.Instance.Get("msg_load_off"));
            builder.AppendLine("</color>");
        }

        public bool InetStatus { get; private set; } = false;

        public bool GpsStatus { get; private set; } = false;

        public bool Checked { get; private set; } = false;

    }

}
