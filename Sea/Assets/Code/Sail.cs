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
			List<int> _ii=new List<int>();
//			for(int i=0;i<100;++i)
//			{
//				_list.Add(new Vector3(Random.Range(-10,10),0,i));
//			}
			
			for(int i=0;i<m_cell_num;++i)
			{
				for(int j=0;j<m_cell_num;++j)
				{
					_list.Add(new Vector3(j,i,0));
					m_vec_list.Add(new Vector3(j,i,0));					
				}
			}
			
			
			
			for(int i=0;i<m_cell_num-1;++i)
			{
				for(int j=0;j<m_cell_num-1;++j)
				{
					_ii.Add(i*m_cell_num+j);
					_ii.Add(i*m_cell_num+j+1);
					_ii.Add(i*m_cell_num+j+1+m_cell_num);
					
					_ii.Add(i*m_cell_num+j);
					_ii.Add(i*m_cell_num+j+1+m_cell_num);
					_ii.Add(i*m_cell_num+j+m_cell_num);
				}
			}
			
			
			
			

//			_list.Add(new Vector3(0,0,0));
//			_list.Add(new Vector3(1,0,0));
//			_list.Add(new Vector3(0,1,0));
			m_mesh.vertices=_list.ToArray();
		
//			_ii.Add(0);
//			_ii.Add(1);
//			_ii.Add(2);
			m_mesh.SetTriangles(_ii.ToArray(),0);
			m_mesh.RecalculateNormals();
			_go.AddComponent<MeshRenderer>();
			
			
			for(int i=m_cell_num;i<m_cell_num*(m_cell_num-1);++i)
			{
				m_node_list.Add(i);
			}
			
			
			UnityEngine.Debug.Log("VeCNum:"+m_vec_list.Count+" NodeNum:"+m_node_list.Count);
			
			 

			
			for(int i=0;i<m_cell_num-2;++i)
			{
				for(int j=0;j<m_cell_num;++j)
				{
					int _index=m_node_list[i*m_cell_num+j];
					int a=m_cell_half-j;
					
//					float _y=Mathf.Sqrt((1-m_m*a*a)/m_n);
					
//					Debug.Log(a+"  "+_y);
					//m_vec_list[_index]=m_mesh.vertices[_index]+new Vector3(0,0,_y);
				}
			}
			//m_mesh.vertices=m_vec_list.ToArray();
			//m_mesh.
			
			
		}
	}
	Mesh m_mesh;
	int m_cell_num=21;//5;
	int m_cell_half=10;//1;
	List<int> m_node_list=new List<int>();
	List<Vector3> m_vec_list=new List<Vector3>();
	
	float m_shift_offset=0.2f;  
	
	float m_a=2.0f;
	float m_b=5.0f;
}
