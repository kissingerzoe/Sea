using UnityEngine;
using System.Collections;

public class Rudder : MonoBehaviour
{
	void Awake()
	{
		m_rudder_sprite=GetComponent<UISprite>();
	}

	void Start()
	{
		
	}
	void Update()
	{
		if(Input.GetKey(KeyCode.LeftArrow))
		{
			m_rotate+=mc_speed*Time.deltaTime;
		}
		if(Input.GetKey(KeyCode.RightArrow))
		{
			m_rotate-=mc_speed*Time.deltaTime;
		}
		m_rudder_sprite.transform.localRotation=Quaternion.Euler(0,0,m_rotate);
		if(ship!=null)
		{
			ship.transform.localRotation=Quaternion.Euler(0,-m_rotate,0);
		}
	}
	public GameObject ship;
	UISprite m_rudder_sprite;
	const float mc_speed=30.0f;
	float m_rotate;
}
