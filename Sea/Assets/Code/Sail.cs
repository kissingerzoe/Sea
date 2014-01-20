using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Sail : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		
		

	}
	
	// Update is called once per frame
	void Update ()
	{
		if(Input.GetKeyDown(KeyCode.T))
		{
			GameObject _go=new GameObject("Sail");
			m_mesh=_go.AddComponent<MeshFilter>().mesh;
			
		
			

			
			List<Vector3> _list=new List<Vector3>();
//			for(int i=0;i<100;++i)
//			{
//				_list.Add(new Vector3(Random.Range(-10,10),0,i));
//			}
			_list.Add(new Vector3(0,0,0));
			_list.Add(new Vector3(1,0,0));
			_list.Add(new Vector3(0,1,0));
			m_mesh.vertices=_list.ToArray();
			List<int> _ii=new List<int>();
			_ii.Add(0);
			_ii.Add(1);
			_ii.Add(2);
			m_mesh.SetTriangles(_ii.ToArray(),0);
			m_mesh.RecalculateNormals();
			_go.AddComponent<MeshRenderer>();
			//m_mesh.
		}
	}
	Mesh m_mesh;
}
