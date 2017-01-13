using UnityEngine;
using System.Collections.Generic;

public class CellMonitor : MonoBehaviour
{
	public float span = 19.83f;
	public int reach = 3;

	public GameObject[] subjects = new GameObject[0];

	private HashSet<TerrainMakeup.CellId> cells = new HashSet<TerrainMakeup.CellId>();

	void Update()
	{
		Debug.Assert(0 < span);
		Debug.Assert(0 < reach);

		Debug.Log("TODO ; eject a random cell>");

		// locate all missing cells
		var added = new HashSet<TerrainMakeup.CellId>();
		foreach (var subject in subjects)
		{
			var offset = subject.transform.position - transform.position;
			offset *= (1.0f / span);

			for (int j = -reach; j <= reach; ++j)
				for (int i = -reach; i <= reach; ++i)
				{
					if ((reach * reach) < ((i * i) + (j * j)))
						continue;

					var cellId = new TerrainMakeup.CellId(Mathf.FloorToInt(offset.x), Mathf.FloorToInt(offset.z)).Add(i, j);

					if (!cells.Contains(cellId))
						added.Add(cellId);
				}
		}

		// create missing cells
		foreach (var each in added)
		{
			cells.Add(each);
			Debug.Log("TODO ; create a cell and terrain for " + each);
		}
	}

#if UNITY_EDITOR
	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.yellow;

		foreach (var cell in cells)
		{
			Gizmos.DrawWireCube(cell.ToVector3(span) + transform.position + (Vector3.one * 0.5f * span), Vector3.one * span);
		}
	}
#endif
}
