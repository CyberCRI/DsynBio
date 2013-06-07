using UnityEngine;
using System.Collections;

public class MapGenerator : MonoBehaviour {

  public Mesh _mesh;
  public MeshFilter _meshFilter;
  public Vector3[] _vertices;

	// Use this for initialization
	void Start () {
          _mesh = new Mesh();
          _meshFilter = GetComponent<MeshFilter>();
//           _meshFilter = new MeshFilter();
          _meshFilter.mesh = _mesh;
          _vertices = new Vector3[4];

          Vector3 newVertice = new Vector3(0, 0, 0);
          _vertices[0] = newVertice;
          newVertice = new Vector3(1, 0, 0);
          _vertices[1] = newVertice;
          newVertice = new Vector3(0, 1, 0);
          _vertices[2] = newVertice;
          newVertice = new Vector3(1, 1, 0);
          _vertices[3] = newVertice;

          _mesh.vertices = _vertices;


          int[] tri = new int[6];

          //Lower left triangle.
          tri[0] = 0;
          tri[1] = 2;
          tri[2] = 1;

          //Upper right triangle.
          tri[3] = 2;
          tri[4] = 3;
          tri[5] = 1;

          _mesh.triangles = tri;

          Vector3[] normals = new Vector3[4];

          normals[0] = -Vector3.forward;
          normals[1] = -Vector3.forward;
          normals[2] = -Vector3.forward;
          normals[3] = -Vector3.forward;
          _mesh.normals = normals;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
