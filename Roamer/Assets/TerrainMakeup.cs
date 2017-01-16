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



	public Mesh For(CellI2 cell, float span, bool offset = false)
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
