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
                //rand_wind();
			}
		}
		foreach(Ship _s in m_ship_list)
		{
			_s.update(m_wind);
		}
	}
    void rand_wind()
    {
        m_wind.x = Random.Range(-mc_wind_range, mc_wind_range);
        m_wind.z = Random.Range(-mc_wind_range, mc_wind_range);

        Dummer.get_single().water.renderer.material.SetVector("_WaveSpeed", new Vector4(-m_wind.x * 100, -m_wind.z * 100, -m_wind.x * 50, -m_wind.z * 50));
    }
	public void on_gui()
	{
        if (GUI.Button(new Rect(0, Screen.height - 40, 100, 40), ""))
        {
            rand_wind();
        }
		GUI.Label(new Rect(0,Screen.height-20,100,20),"Wind:"+(int)(m_wind.x*100)+","+(int)(m_wind.z*100));
	}
	List<Ship> m_ship_list=new List<Ship>();
	Vector3 m_wind=Vector3.zero;
	const float mc_wind_range=1.0f;
	float m_wind_rand_tick=5.0f;
	const float mc_wind_rand_max_time=20.0f;
	const float mc_wind_rand_min_time=5.0f;
}
