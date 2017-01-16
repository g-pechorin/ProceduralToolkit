using UnityEngine;
using System.Collections.Generic;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class TerrainMakeup : ScriptableObject
{
	public Material material = null;

	public float terrainHeight = 3.14f;
	public Gradient terrainGradient;

	TerrainMakeup()
	{
		terrainGradient = ProceduralToolkit.ColorE.Gradient(ProceduralToolkit.ColorE.navy32, ProceduralToolkit.ColorE.olive32);
	}

	public Mesh For(CellI2 cell, float span, bool offset = false)
	{
		Vector2 noiseOffset = new Vector2(cell.i * span, cell.j * span);

		var terrainDraft = ProceduralToolkit.Examples.LowPolyTerrainGenerator.TerrainDraft(new ProceduralToolkit.Examples.LowPolyTerrainGenerator.Config()
		{
			terrainSize = new Vector3(span, terrainHeight, span),
			gradient = terrainGradient
		}, noiseOffset);

		terrainDraft.Move(Vector3.one * -0.5f * span);

		return terrainDraft.ToMesh();
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
