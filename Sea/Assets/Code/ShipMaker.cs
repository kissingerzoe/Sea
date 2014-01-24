using UnityEngine;
using System.Collections;

public class ShipMaker : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		m_ship=new Ship(GameObject.Instantiate(ShipPref) as GameObject,Mat);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(Input.GetKey(KeyCode.UpArrow))
		{
			m_wind+=new Vector3(0,0,Time.deltaTime*m_speed);
		}
		if(Input.GetKey(KeyCode.DownArrow))
		{
			m_wind+=new Vector3(0,0,-Time.deltaTime*m_speed);
		}
		if(Input.GetKey(KeyCode.LeftArrow))
		{
			m_wind+=new Vector3(-Time.deltaTime*m_speed,0,0);
		}
		if(Input.GetKey(KeyCode.RightArrow))
		{
			m_wind+=new Vector3(Time.deltaTime*m_speed,0,0);
		}		
		m_ship.update(m_wind);
	}
	void OnGUI()
	{
		GUI.Label(new Rect(0,Screen.height-20,100,20),"Wind:"+(int)(m_wind.x*100)+","+(int)(m_wind.z*100));
	}	
	public Material Mat;
	public GameObject ShipPref;
	Ship m_ship;
	Vector3 m_wind=new Vector3(0,0,0);
	float m_speed=1.0f;
}
