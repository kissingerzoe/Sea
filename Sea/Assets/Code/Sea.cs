using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Sea
{
	public Sea()
	{
		
		foreach(GameObject g in Dummer.get_single().ship_list)
		{
			m_ship_list.Add(new Ship(g));
		}
	}
	public void update()
	{
		if(m_wind_rand_tick>0.0f)
		{
			m_wind_rand_tick-=Time.deltaTime;
			if(m_wind_rand_tick<=0.0f)
			{
				m_wind_rand_tick=Random.Range(mc_wind_rand_min_time,mc_wind_rand_max_time);
				m_wind.x=Random.Range(-mc_wind_range,mc_wind_range);
				m_wind.z=Random.Range(-mc_wind_range,mc_wind_range);
			}
		}
		foreach(Ship _s in m_ship_list)
		{
			_s.update(m_wind);
		}
	}
	public void on_gui()
	{
		GUI.Label(new Rect(0,Screen.height-20,200,20),"Wind:"+(int)(m_wind.x*100)+","+(int)(m_wind.z*100));
	}
	List<Ship> m_ship_list=new List<Ship>();
	Vector3 m_wind=Vector3.zero;
	const float mc_wind_range=1.0f;
	float m_wind_rand_tick=5.0f;
	const float mc_wind_rand_max_time=20.0f;
	const float mc_wind_rand_min_time=5.0f;
}
