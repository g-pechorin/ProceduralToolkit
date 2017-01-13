using UnityEngine;
using System.Collections.Generic;

namespace ProceduralToolkit
{
    /// <summary>
    /// Mesh extensions and constructors for primitives
    /// </summary>
    public static partial class MeshE
    {
        /// <summary>
        /// Moves mesh vertices by <paramref name="vector"/>
        /// </summary>
        public static void Move(this Mesh mesh, Vector3 vector)
        {
            var vertices = mesh.vertices;
            for (int i = 0; i < vertices.Length; i++)
            {
                vertices[i] += vector;
            }
            mesh.vertices = vertices;
        }

        /// <summary>
        /// Rotates mesh vertices by <paramref name="rotation"/>
        /// </summary>
        public static void Rotate(this Mesh mesh, Quaternion rotation)
        {
            var vertices = mesh.vertices;
            var normals = mesh.normals;
            for (int i = 0; i < vertices.Length; i++)
            {
                vertices[i] = rotation*vertices[i];
                normals[i] = rotation*normals[i];
            }
            mesh.vertices = vertices;
            mesh.normals = normals;
        }

        /// <summary>
        /// Scales mesh vertices uniformly by <paramref name="scale"/>
        /// </summary>
        public static void Scale(this Mesh mesh, float scale)
        {
            var vertices = mesh.vertices;
            for (int i = 0; i < vertices.Length; i++)
            {
                vertices[i] *= scale;
            }
            mesh.vertices = vertices;
        }

        /// <summary>
        /// Scales mesh vertices non-uniformly by <paramref name="scale"/>
        /// </summary>
        public static void Scale(this Mesh mesh, Vector3 scale)
        {
            var vertices = mesh.vertices;
            var normals = mesh.normals;
            for (int i = 0; i < vertices.Length; i++)
            {
                vertices[i] = Vector3.Scale(vertices[i], scale);
                normals[i] = Vector3.Scale(normals[i], scale).normalized;
            }
            mesh.vertices = vertices;
            mesh.normals = normals;
		}

#if UNITY_5_1_4
		public static void SetColors(this Mesh mesh, List<Color> colors)
		{
			mesh.colors = colors.ToArray();
		}

		public static void SetNormals(this Mesh mesh, List<Vector3> normals)
		{
			mesh.normals = normals.ToArray();
		}

		public static void SetTangents(this Mesh mesh, List<Vector4> tangents)
		{
			mesh.tangents = tangents.ToArray();
		}

		public static void SetTriangles(this Mesh mesh, List<int> triangles, int subLayer)
		{
			if (0 != subLayer)
				throw new UnityException();

			mesh.triangles = triangles.ToArray();
		}

		public static void SetUVs(this Mesh mesh, int layer, List<Vector2> uv)
		{
			var array = uv.ToArray();

			// ick ...
			switch (layer)
			{
				case 0:
					mesh.uv = array;
					return;
				case 1:
					mesh.uv2 = array;
					return;
				case 2:
					mesh.uv3 = array;
					return;
				case 3:
					mesh.uv4 = array;
					return;
				default:
					throw new UnityException();
			}
		}

		public static void SetVertices(this Mesh mesh, List<Vector3> vertices)
		{
			mesh.vertices = vertices.ToArray();
		}
#endif

		/// <summary>
		/// Paints mesh vertices with <paramref name="color"/>
		/// </summary>
		public static void Paint(this Mesh mesh, Color color)
        {
            var colors = new Color[mesh.vertexCount];
            for (int i = 0; i < mesh.vertexCount; i++)
            {
                colors[i] = color;
            }
            mesh.colors = colors;
        }

        /// <summary>
        /// Flips mesh faces
        /// </summary>
        public static void FlipFaces(this Mesh mesh)
        {
            mesh.FlipTriangles();
            mesh.FlipNormals();
        }

        /// <summary>
        /// Reverses winding order of mesh triangles
        /// </summary>
        public static void FlipTriangles(this Mesh mesh)
        {
            for (int i = 0; i < mesh.subMeshCount; i++)
            {
                var triangles = mesh.GetTriangles(i);
                for (int j = 0; j < triangles.Length; j += 3)
                {
                    PTUtils.Swap(ref triangles[j], ref triangles[j + 1]);
                }
                mesh.SetTriangles(triangles, i);
            }
        }

        /// <summary>
        /// Reverses direction of mesh normals
        /// </summary>
        public static void FlipNormals(this Mesh mesh)
        {
            var normals = mesh.normals;
            for (int i = 0; i < normals.Length; i++)
            {
                normals[i] = -normals[i];
            }
            mesh.normals = normals;
        }
    }
}