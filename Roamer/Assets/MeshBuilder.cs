using UnityEngine;
using System.Collections.Generic;

public class MeshBuilder
{
	List<Vector3> vertices = new List<Vector3>();
	List<Vector3> normals = new List<Vector3>();
	List<int> indices = new List<int>();

	public readonly bool hardNormals;
	public bool softNormals { get { return !hardNormals; } }

	public MeshBuilder(bool hardNormals = true)
	{
		this.hardNormals = hardNormals;
	}

	public void AddQuad(Vector3 v0, Vector3 v1, Vector3 v2, Vector3 v3)
	{
		AddTriangle(v0, v1, v2);
		AddTriangle(v0, v2, v3);
	}

	public void AddTriangle(Vector3 v0, Vector3 v1, Vector3 v2)
	{
		Vector3 n = hardNormals ? Vector3.Cross((v1 - v0).normalized, (v2 - v1).normalized).normalized : Vector3.zero;

		addVertex(v0, n);
		addVertex(v1, n);
		addVertex(v2, n);
	}

	private void addVertex(Vector3 v, Vector3 n)
	{
		var iV = vertices.IndexOf(v);
		var iN = vertices.IndexOf(v);
		if (-1 == iV || (hardNormals && (-1 == iN || iN != iV)))
		{
			Debug.Assert(softNormals || vertices.Count == normals.Count);
			Debug.Assert(softNormals || Vector3.zero != n);
			iV = vertices.Count;
			vertices.Add(v);
			if (hardNormals) normals.Add(n);
		}
		indices.Add(iV);
	}

	public Mesh Produce(string name)
	{
		var mesh = new Mesh();

		mesh.vertices = vertices.ToArray();
		mesh.triangles = indices.ToArray();

		mesh.RecalculateNormals();
		mesh.RecalculateBounds();

		mesh.name = name;

		return mesh;
	}
}