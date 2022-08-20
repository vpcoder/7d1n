namespace Mapbox.Examples
{
	using Mapbox.Unity.MeshGeneration.Data;
	using UnityEngine;
	using UnityEngine.UI;
	using System.Linq;
    using Engine.EGUI;
    using System.Text;
    using Engine;
    using Engine.Generator;

    public class FeatureUiMarker : Panel
	{
		[SerializeField]
		private Transform _wrapperMarker;
		[SerializeField]
		private Transform _infoPanel;
		[SerializeField]
		private Text _info;

		private Vector3[] _targetVerts;
		private VectorEntity _selectedFeature;
        private string textInfo;

		void Update()
		{
			Snap();
		}

		internal void Clear()
		{
            Hide();
		}

		public void Show(VectorEntity selectedFeature)
		{
			if (selectedFeature == null)
			{
				Clear();
				return;
			}
			_selectedFeature = selectedFeature;
			transform.position = new Vector3(0, 0, 0);
			var mesh = selectedFeature.MeshFilter;

			if (mesh != null)
			{
                textInfo = CreateTextInfo();
                _targetVerts = mesh.mesh.vertices;
				Snap();
			}
            Show();
		}

		private void Snap()
		{
			if (_targetVerts == null || _selectedFeature == null)
				return;

			var left = float.MaxValue;
			var right = float.MinValue;
			var top = float.MinValue;
			var bottom = float.MaxValue;
			foreach (var vert in _targetVerts)
			{
				var pos = Camera.main.WorldToScreenPoint(_selectedFeature.Transform.position + (_selectedFeature.Transform.lossyScale.x * vert));
				if (pos.x < left)
					left = pos.x;
				else if (pos.x > right)
					right = pos.x;
				if (pos.y > top)
					top = pos.y;
				else if (pos.y < bottom)
					bottom = pos.y;
			}

			_wrapperMarker.position = new Vector2(left - 10, top + 10);
			(_wrapperMarker as RectTransform).sizeDelta = new Vector2(right - left + 20, top - bottom + 20);

			_infoPanel.position = new Vector2(right + 10, top + 10);
            _info.text = textInfo; // string.Join(" \r\n ", _selectedFeature.Feature.Properties.Select(x => x.Key + " - " + x.Value.ToString()).ToArray());
		}

        private string CreateTextInfo()
        {
            var info = _selectedFeature.Feature.Info;
            var builder = new StringBuilder();
            builder.Append(Localization.Instance.Get("msg_location_name")).Append(": <color=\"#0f0\">").Append(info.Type.ToLocalText()).Append("</color>\n");
            builder.Append(Localization.Instance.Get("msg_location_size_name")).Append(": <color=\"#0f0\">").Append(info.Size.ToLocalText()).Append("</color>\n");
            builder.Append("высота").Append(": <color=\"#0f0\">").Append(info.Height).Append("</color>\n");
            return builder.ToString();
        }

	}
}