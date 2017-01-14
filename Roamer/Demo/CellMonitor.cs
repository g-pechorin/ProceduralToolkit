using UnityEngine;
using System.Collections.Generic;

public class CellMonitor : MonoBehaviour
{
	public float span = 19.83f;
	public float reach = 3.14f;

	public GameObject[] subjects = new GameObject[0];

	private Dictionary<TerrainMakeup.CellId, Monitored> cells = new Dictionary<TerrainMakeup.CellId, Monitored>();

	class Monitored : MonoBehaviour
	{
		public TerrainMakeup.CellId cell;
		CellMonitor parent;
		public bool marked = true;
		public void Mount(CellMonitor parent, TerrainMakeup.CellId cell)
		{
			this.cell = cell;
			this.parent = parent;

			transform.parent = parent.transform;
			transform.localPosition = cell.ToVector3(parent.span);

			Debug.Log("TODO ; create a cell and terrain for " + cell);

			parent.cells[cell] = this;
		}

#if UNITY_EDITOR
		public void OnDrawGizmosSelected()
		{
			Gizmos.color = Color.yellow;

			Gizmos.DrawWireCube(transform.position, Vector3.one * parent.span);
		}
#endif
	}

	void Update()
	{
		Debug.Assert(0 < span);
		Debug.Assert(0 < reach);
		Debug.Assert(Vector3.zero == transform.position);
		Debug.Assert(Vector3.zero == transform.localPosition);
		Debug.Assert(Vector3.zero == transform.eulerAngles);
		Debug.Assert(Vector3.zero == transform.localEulerAngles);
		Debug.Assert(Vector3.one == transform.localScale);
		Debug.Assert(Vector3.one == transform.lossyScale);
		
		Debug.Assert(0 < subjects.Length);

		// unmark all cells
		foreach (var cell in cells.Values)
			cell.marked = false;

		// locate all missing cells
		var added = new HashSet<TerrainMakeup.CellId>();
		foreach (var subject in subjects)
		{
			var offset = subject.transform.position - transform.position;
			offset *= (1.0f / span);

			for (int i = Mathf.FloorToInt(-reach); i <= Mathf.CeilToInt(reach); ++i)
				for (int j = Mathf.FloorToInt(-reach); j <= Mathf.CeilToInt(reach); ++j)
				{
					if ((reach * reach) < ((i * i) + (j * j)))
						continue;

					var cell = new TerrainMakeup.CellId(Mathf.FloorToInt(offset.x), Mathf.FloorToInt(offset.z)).Add(i, j);


					if (cells.ContainsKey(cell))
						cells[cell].marked = true;
					else
						// create missing cells
						if (!added.Contains(cell))
						if (added.Add(cell))
							new GameObject(cell.ToString()).AddComponent<Monitored>().Mount(this, cell);
				}
		}

		// remove any unneeded cells
		foreach (var cell in new HashSet<Monitored>(cells.Values))
			if (!cell.marked)
			{
				cells.Remove(cell.cell);
				Destroy(cell.gameObject);
			}
	}

#if UNITY_EDITOR
	public bool debugAlwaysDrawGizmos = false;
	void OnDrawGizmos()
	{
		if (debugAlwaysDrawGizmos)
			OnDrawGizmosSelected();
	}
	void OnDrawGizmosSelected()
	{
		foreach (var cell in GetComponentsInChildren<Monitored>())
		{
			cell.OnDrawGizmosSelected();
		}
	}
#endif

}
