using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NbaLitJson;
using System.IO;
public class ShipMaker : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		
		//StringReader _sr=new StringReader()
		
		
		
		//m_current_ship=new Ship(GameObject.Instantiate(ShipPref) as GameObject,Mat);
		//m_ship_list.Add(m_current_ship);
		
		m_www=new WWW("file:///" + Application.dataPath + "/../GameData/Ship/data.json");		
			
	}
	
	// Update is called once per frame
	void Update ()
	{
		
		if(m_www!=null)
		{
			if(m_www.isDone)
			{
				if(m_www.error==null)
				{
					Debug.Log(m_www.text);
					ShipDataColl _sdc= JsonMapper.ToObject<ShipDataColl>(m_www.text);
					foreach(ShipData _sd in _sdc.m_ship_list)
					{
						Ship _s=new Ship(GameObject.Instantiate(ShipPref) as GameObject,Mat);
						_s.init(_sd);
						m_ship_list.Add(_s);
						if(m_current_ship==null)
						{
							m_current_ship=_s;
						}
					}
				}
				m_www=null;
			}
		}
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
		if(Input.GetKeyDown(KeyCode.U))
		{
			m_current_ship.serialize();
		}
		
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			Application.LoadLevel("Root");
		}
		if(m_current_ship!=null)
		{
			m_current_ship.update(m_wind,false);
		}
		
	}
	void OnGUI()
	{
		GUI.Label(new Rect(0,Screen.height-20,100,20),"Wind:"+(int)(m_wind.x*100)+","+(int)(m_wind.z*100));
		if(GUI.Button(new Rect(100,Screen.height-20,100,20),"Save"))
		{
			ShipDataColl _sdc=new ShipDataColl();
			foreach(Ship _s in m_ship_list)
			{
				_sdc.m_ship_list.Add(_s.get_data());
			}
			//string _sd=JsonMapper.ToJson(_ship_data);
	        StreamWriter temp_sw = new StreamWriter("./GameData/Ship/data.json", false, System.Text.Encoding.ASCII);
			temp_sw.Write(JsonMapper.ToJson(_sdc));
			temp_sw.Close();			
			//save all ships
		}
	}
	WWW m_www;
	public Material Mat;
	public GameObject ShipPref;
	Ship m_current_ship;
	List<Ship> m_ship_list=new List<Ship>();
	Vector3 m_wind=new Vector3(0,0,0);
	float m_speed=1.0f;
}
