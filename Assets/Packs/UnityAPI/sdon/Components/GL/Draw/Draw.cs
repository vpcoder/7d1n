using System.Collections.Generic;
using UnityEngine;

namespace Engine {

	public static class Draw {

		private static Material defaultMaterial = GameObject.FindObjectOfType<Material>();

		public static void Line(IEnumerable<Vector3> points)
		{
			Line(points, defaultMaterial);
		}

		public static void Line(IEnumerable<Vector3> points, Material material) {

			GL.PushMatrix();
			material.SetPass(0);
			GL.LoadOrtho();

			GL.Begin(GL.LINES);
			GL.Color(Color.red);

            foreach (var point in points)
				GL.Vertex(point);
			
			GL.End();
			GL.PopMatrix();

		}

	}

}
