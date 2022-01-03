using UnityEngine;
using UnityEngine.UI;
using Engine.IO;

public class GpsTest : MonoBehaviour
{

	[SerializeField]
	private Text text;

	[SerializeField]
	private Text log;

	private float lastUpdate;

	private IGps gps;

	void Start()
	{
		text = GetComponent<Text>();
		gps = ObjectFinder.Find<IOGpsBase>();
		gps.StartGps();
	}

	void Update()
	{
		text.text = gps.ToString();
	}

}
