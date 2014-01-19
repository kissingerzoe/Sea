//ybzuo
//ship
using UnityEngine;
public class Ship
{
	public Ship(GameObject _go)
	{
		m_go=_go;
	}
	public void update(Vector3 _wind)
	{
		m_go.transform.position+=(m_speed+_wind)*Time.deltaTime;
	}
	GameObject m_go;
	Vector3 m_speed=Vector3.zero;
}
