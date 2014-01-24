//ybzuo
//ship
using System.Collections.Generic;
using UnityEngine;
public class Ship
{
	public Ship(GameObject _go,Material _mat)
	{
		m_go=_go;
		
		Sail _sail=new Sail(_mat,new Vector2(1.0f,2.0f));
		_sail.get_go().transform.parent=m_go.transform;
		_sail.get_go().transform.localPosition=new Vector3(-0.5f,1.0f,-0.68f);
		_sail.get_go().transform.localRotation=Quaternion.identity;
		m_sail_list.Add(_sail);
		
		_sail=new Sail(_mat,new Vector2(1.0f,1.5f));
		_sail.get_go().transform.parent=m_go.transform;
		_sail.get_go().transform.localPosition=new Vector3(-0.5f,1.7f,0.38f);
		_sail.get_go().transform.localRotation=Quaternion.identity;
		m_sail_list.Add(_sail);		
		
		
		
	}
	public void update(Vector3 _wind)
	{
        //m_speed=Input.ge
		m_go.transform.position+=(m_speed+_wind)*Time.deltaTime;
		foreach(Sail _s in m_sail_list)
		{
			_s.update(_wind);
		}
	}
	public GameObject get_go()
	{
		return m_go;
	}
	GameObject m_go; 
	Vector3 m_speed=Vector3.zero;
	List<Sail> m_sail_list=new List<Sail>();
}
