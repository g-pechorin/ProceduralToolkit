using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class TerrainMakeup : ScriptableObject
{
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
