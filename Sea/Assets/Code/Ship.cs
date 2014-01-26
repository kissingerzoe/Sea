//ybzuo
//ship
using System.Collections.Generic;
using UnityEngine;
using NbaLitJson;
using System.IO;
public class Ship
{
	public Ship(GameObject _go,Material _mat)
	{
		m_go=_go;
		
		Sail _sail=new Sail(_mat,new Vector2(1.0f,0.7f));
		_sail.get_go().transform.parent=m_go.transform;
		_sail.get_go().transform.localPosition=new Vector3(-0.5f,2.04f,0.7f);
		_sail.get_go().transform.localRotation=Quaternion.identity;
		m_sail_list.Add(_sail);
		
//		_sail=new Sail(_mat,new Vector2(1.0f,0.8f));
//		_sail.get_go().transform.parent=m_go.transform;
//		_sail.get_go().transform.localPosition=new Vector3(-0.5f,1.7f,0.7f);
//		_sail.get_go().transform.localRotation=Quaternion.identity;
//		m_sail_list.Add(_sail);		
	}
	public Ship(string _info)
	{
		m_info=_info;
	}
	
	public void serialize()
	{
		ShipData _ship_data=new ShipData();
		foreach(Sail _s in m_sail_list)
		{
			_ship_data.m_sail_list.Add(_s.get_sail_data());
		}
		string _sd=JsonMapper.ToJson(_ship_data);
        StreamWriter temp_sw = new StreamWriter("./GameData/Ship/data.json", false, System.Text.Encoding.UTF8);
		temp_sw.Write(_sd);
		temp_sw.Close();				
//			#if UNITY_EDITOR
//				UnityEditor.AssetDatabase.Refresh();
//			#endif				
	}
	
	public void update(Vector3 _wind,bool _ship_move)
	{
        //m_speed=Input.ge
		if(_ship_move)
		{
			m_go.transform.position+=(m_speed+_wind)*Time.deltaTime;
		}
		foreach(Sail _s in m_sail_list)
		{
			_s.update(_wind);
		}
	}
	public GameObject get_go()
	{
		return m_go;
	}
	string m_info;
	GameObject m_go; 
	Vector3 m_speed=Vector3.zero;
	List<Sail> m_sail_list=new List<Sail>();
}
