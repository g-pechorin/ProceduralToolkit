using UnityEngine;
using System.Collections.Generic;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class TerrainMakeup : ScriptableObject
{
	public Material material = null;
	public float height = 3.14f;

	public class CellId
	{
		public int i { get; private set; }
		public int j { get; private set; }

		public CellId(int i, int j)
		{
			this.i = i;
			this.j = j;
		}

		public CellId Add(int i, int j)
		{
			return new CellId(this.i + i, this.j + j);
		}

		public Vector3 ToVector3(float f)
		{
			return new Vector3(i * f, 0, j * f);
		}

		public override bool Equals(object obj)
		{
			var other = obj as CellId;

			return null != other && ToString() == ("" + other);
		}


		public override int GetHashCode()
		{
			return ToString().GetHashCode();
		}

		public override string ToString()
		{
			return GetType().Name + "(" + (i > 0 ? "+" + i : "" + i) + "," + (j > 0 ? "+" + j : "" + j) + ")";
		}
	}

	class MeshBuilder
	{
		List<Vector3> vertices = new List<Vector3>();
		List<int> indices = new List<int>();

		public void AddQuad(Vector3 v0, Vector3 v1, Vector3 v2, Vector3 v3)
		{
			AddTriangle(v0, v1, v2);
			AddTriangle(v0, v2, v3);
		}

		public void AddTriangle(Vector3 v0, Vector3 v1, Vector3 v2)
		{
			addVertex(v0);
			addVertex(v1);
			addVertex(v2);
		}

		private void addVertex(Vector3 v)
		{
			var i = vertices.IndexOf(v);
			if (-1 == i)
			{
				i = vertices.Count;
				vertices.Add(v);
			}
			indices.Add(i);
		}

		public Mesh Produce(string name)
		{
			var mesh = new Mesh();

			mesh.vertices = vertices.ToArray();
			mesh.triangles = indices.ToArray();

			mesh.RecalculateNormals();
			//mesh.RecalculateBounds();

			mesh.name = name;

			return mesh;
		}
	}

	public Mesh For(CellId cell, float span, bool offset = false)
	{
		var foot = (offset ? cell.ToVector3(span) : Vector3.zero) - (Vector3.one * 0.5f * span);

		var meshBuilder = new MeshBuilder();

		// TODO ; do this in an "unscaled" manner

		var y0 = (float)(new System.Random(cell.Add(0, 0).GetHashCode()).NextDouble() * height);
		var y1 = (float)(new System.Random(cell.Add(0, 1).GetHashCode()).NextDouble() * height);
		var y2 = (float)(new System.Random(cell.Add(1, 1).GetHashCode()).NextDouble() * height);
		var y3 = (float)(new System.Random(cell.Add(1, 0).GetHashCode()).NextDouble() * height);

		meshBuilder.AddQuad(
			foot + new Vector3(0, y0, 0),
			foot + new Vector3(0, y1, span),
			foot + new Vector3(span, y2, span),
			foot + new Vector3(span, y3, 0));

		return meshBuilder.Produce(cell.ToString());
	}

#if UNITY_EDITOR
	[MenuItem("Assets/Create/TerrainMakeup")]
	public static void CreateAutonoFighterMind()
	{
		Assets.CreateScriptableObjectFile<TerrainMakeup>();
	}
	[CustomEditor(typeof(TerrainMakeup))]
	public class Inspector : Editor
	{
		public override void OnInspectorGUI()
		{
			DrawDefaultInspector();
		}
	}
#endif
}
