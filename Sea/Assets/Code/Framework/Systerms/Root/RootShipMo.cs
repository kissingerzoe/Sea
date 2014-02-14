using UnityEngine;
using System.Collections;

public class RootShipMo : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		m_tick=1.0f;
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(m_tick>=0.0f) 
		{
			m_tick+=Time.deltaTime;
			if(m_tick>=mc_next_time)
			{
				transform.localPosition=m_next_epos;
				m_next_bpos=m_next_epos;
				if(m_next_epos.y>m_next_raw_pos.y)
				{
					m_next_epos=m_next_raw_pos;
				}
				else
				{
					m_next_epos=m_next_raw_pos+new Vector3(0,mc_next_shift_dis,0);
				}
				m_tick=0.0f;
			}
			else
			{
				transform.localPosition=Vector3.Lerp(m_next_bpos,m_next_epos,m_tick/mc_next_time);
			}				
		}			
	}
	float m_tick=-1.0f;
	const float mc_next_time=5.0f;
	Vector3 m_next_raw_pos;
	Vector3 m_next_bpos;
	Vector3 m_next_epos;
	const float mc_next_shift_dis=4.0f;	
}
