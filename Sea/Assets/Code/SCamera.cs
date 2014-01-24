using UnityEngine;
using System.Collections;

public class SCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	
	}
	
	// Update is called once per frame
	void Update () {
		
		if(m_follow_ship!=null)
		{
			transform.localPosition=m_follow_ship.transform.position+m_follow_vec;
		}
	}
	public void set_follow_ship(GameObject _follow_ship)
	{
		m_follow_ship=_follow_ship;	
	}
	GameObject m_follow_ship;
	Vector3 m_follow_vec=new Vector3(0,13,-13);
}
