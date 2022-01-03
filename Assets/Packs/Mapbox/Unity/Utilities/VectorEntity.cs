namespace Mapbox.Unity.MeshGeneration.Data
{
	using UnityEngine;

	public class VectorEntity
	{
		public GameObject GameObject { get; set; }
        public MeshFilter MeshFilter { get; set; }
        public Mesh Mesh { get; set; }
        public MeshRenderer MeshRenderer { get; set; }
		public VectorFeatureUnity Feature { get; set; }
        public Transform Transform { get; set; }

        /// <summary>
        /// Идентификатор объекта на карте
        /// </summary>
        public long ID
        {
            get
            {
                return (long)Feature.Data.Id;
            }
        }
	}
}
