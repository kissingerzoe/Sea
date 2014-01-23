using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Sail : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		
		init();

	}
	
	// Update is called once per frame
	void Update ()
	{
		if(Input.GetKey(KeyCode.UpArrow))
		{
			set_offset(m_shift_offset+mc_shift_speed*Time.deltaTime);
			
		}
		if(Input.GetKey(KeyCode.DownArrow))
		{
			set_offset(m_shift_offset-mc_shift_speed*Time.deltaTime); 
		}		
	}
	
	void init()
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
					_list.Add(new Vector3(j*m_sail_unit,i*m_sail_unit,0));
					m_vec_list.Add(new Vector3(j*m_sail_unit,i*m_sail_unit,0));
					m_base_vec_list.Add(new Vector3(j*m_sail_unit,i*m_sail_unit,0));
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
		
		    List<Vector2> _uv_list=new List<Vector2>();
			for(int i=0;i<m_cell_num;++i)
			{
				for(int j=0;j<m_cell_num;++j)
				{
					_uv_list.Add(new Vector2(j/(float)m_cell_num,i/(float)m_cell_num));
				}
			}		
		    
		
		
			m_mesh.vertices=_list.ToArray();
			m_mesh.SetTriangles(_ii.ToArray(),0);
			m_mesh.uv=_uv_list.ToArray();
		
			m_mesh.RecalculateNormals();
		
			MeshRenderer _mr= _go.AddComponent<MeshRenderer>();
			for(int i=0;i<m_cell_num*m_cell_num;++i)
			{
				m_node_list.Add(i);
			}
			_mr.material=mat;
		
		    
			UnityEngine.Debug.Log("VeCNum:"+m_vec_list.Count+" NodeNum:"+m_node_list.Count);		
	}
	
	public void set_offset(float _offset)
	{
		m_shift_offset=_offset;
		if(m_shift_offset>=1.0f)
		{
			m_shift_offset=1.0f;
		}
		if(m_shift_offset<=0.0f)
		{
			m_shift_offset=0.0f;
		}
		
		for(int i=0;i<m_cell_num;++i)
		{
			for(int j=0;j<m_cell_num;++j)
			{
				int _index=m_node_list[i*m_cell_num+j];
				float a=m_cell_num/2.0f;
				float b=m_cell_num*m_shift_offset; 
				float _y=(_index/m_cell_num)-m_cell_half;
				float _z=Mathf.Sqrt(1.0f-(_y*_y)/(a*a))*b;;
				m_vec_list[_index]=m_base_vec_list[_index]+new Vector3(0,_y*m_sail_unit,_z*m_sail_unit);
			}
		}
		m_mesh.vertices=m_vec_list.ToArray();		
	}
	public Material mat;
	Mesh m_mesh;
	int m_cell_num=11;//5;
	int m_cell_half=5;//1;
	List<int> m_node_list=new List<int>();
	List<Vector3> m_vec_list=new List<Vector3>();
	List<Vector3> m_base_vec_list=new List<Vector3>();
	
	float m_shift_offset=0.7f;  
	float mc_shift_speed=0.3f;
	
	float m_sail_unit=0.1f;
	

}
