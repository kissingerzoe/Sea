using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Sail
{
	public void update(Vector3 _wind)
	{

		set_offset(_wind);
//		if(Input.GetKey(KeyCode.UpArrow))
//		{
//			set_offset(m_shift_offset+mc_shift_speed*Time.deltaTime);
//			
//		}
//		if(Input.GetKey(KeyCode.DownArrow))
//		{
//			set_offset(m_shift_offset-mc_shift_speed*Time.deltaTime); 
//		}		
	}
	
	public Sail(Material _mat,Vector2 _size)
	{
		//m_mat=_mat;
		m_sail_unit=new Vector2(_size.x/m_cell_num,_size.y/m_cell_num);
		m_go=new GameObject("Sail");
		m_mesh=m_go.AddComponent<MeshFilter>().mesh;	
		List<Vector3> _list=new List<Vector3>();
		List<int> _ii=new List<int>();	
		for(int i=0;i<m_cell_num;++i)
		{
			for(int j=0;j<m_cell_num;++j)
			{
				_list.Add(new Vector3(j*m_sail_unit.x,i*m_sail_unit.y,0));
				m_vec_list.Add(new Vector3(j*m_sail_unit.x,i*m_sail_unit.y,0));
				m_base_vec_list.Add(new Vector3(j*m_sail_unit.x,i*m_sail_unit.y,0));
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
		MeshRenderer _mr= m_go.AddComponent<MeshRenderer>();
		for(int i=0;i<m_cell_num*(m_cell_num-1);++i)
		{
			m_node_list.Add(i);
		}
		_mr.material=_mat;	
	}
	
	public void set_offset(Vector3 _wind)
	{
		
		float _power=_wind.sqrMagnitude;
		if(_power<=0.0f)
		{
			return;
		}
		
		float _angle=Vector3.Angle(m_go.transform.forward,_wind.normalized);
		

		
		//Debug.Log(m_go.transform.forward+"  "+_wind.normalized+" "+_angle);
		
		float _dot=Vector3.Dot(m_go.transform.right,_wind.normalized);
		float _offset=1.0f-Mathf.Abs(_dot);		
		m_shift_offset=_power*_offset;
		
		if((_angle>90.0f)&&(_angle<270.0f))
		{
			m_shift_offset=-m_shift_offset;
		}
		
		if(m_shift_offset>=1.0f)
		{
			m_shift_offset=1.0f;
		}
		if(m_shift_offset<=-1.0f)
		{
			m_shift_offset=-1.0f;
		}
		_wind.Normalize();
		for(int i=0;i<m_cell_num-1;++i)
		{
			for(int j=0;j<m_cell_num;++j)
			{
				int _index=m_node_list[i*m_cell_num+j];
				float a=m_cell_num/2.0f;
				float b=m_cell_num*m_shift_offset; 
				float _y=(_index/m_cell_num)-m_cell_half;
				float _z=Mathf.Sqrt(1.0f-(_y*_y)/(a*a))*b;
				
				
				float _x=Mathf.Sqrt(1.0f-(_y*_y)/(a*a))*b;
				_x*=(1.0f-_offset);
				//Debug.Log(_x);
				
				m_vec_list[_index]=/*m_base_vec_list[_index]+*/new Vector3(m_base_vec_list[_index].x+_x*m_sail_unit.x*_wind.x,_y*m_sail_unit.y+(m_cell_num-1)*m_sail_unit.y*0.5f,_z*m_sail_unit.x);
			}
		}
		m_mesh.vertices=m_vec_list.ToArray();		
	}
	public GameObject get_go()
	{
		return m_go;
	}
	
	public SailData get_sail_data()
	{
		SailData _sd=new SailData();
		_sd.m_cell_num=m_cell_num;
		_sd.m_cell_half=m_cell_half;
		_sd.m_node_list=new List<int>(m_node_list);
		foreach(Vector3 _v in m_base_vec_list)
		{
			_sd.m_vec_xlist.Add(_v.x);
			_sd.m_vec_ylist.Add(_v.y);
			_sd.m_vec_zlist.Add(_v.z);
		}
		_sd.m_sail_unit_x=m_sail_unit.x;
		_sd.m_sail_unit_y=m_sail_unit.y;
		return _sd;
	}
	//Material m_mat;
	GameObject m_go;
	Mesh m_mesh;
	int m_cell_num=41;//5;
	int m_cell_half=20;//1;
	List<int> m_node_list=new List<int>();
	List<Vector3> m_vec_list=new List<Vector3>();
	List<Vector3> m_base_vec_list=new List<Vector3>();
	
	float m_shift_offset=0.6f;  
//	float mc_shift_speed=0.3f;
	
	Vector2 m_sail_unit=new Vector2(0.1f,0.2f);
	

}
