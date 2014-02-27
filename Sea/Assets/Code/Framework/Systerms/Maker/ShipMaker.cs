using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NbaLitJson;
using System.IO;
public class ShipMaker : MonoBehaviour {
  // Use this for initialization
  void Start(){
    if(m_file_mode){
      m_www=new WWW("file:///" + Application.dataPath + "/../GameData/Ship/data.json");
    }
    else{
      NetMsg _nm= SeaCore.get_single().get_net_mgr().send_msg(NetMsgType.GET_SHIP_DATA,get_ship_data_del);
      _nm.args.Add("wuid",GlobalInfoer.user_wuid);
    }
  }
  // Update is called once per frame
  void Update (){
    if(m_file_mode){
      if(m_www!=null){
	if(m_www.isDone){
	  if(m_www.error==null){
	    Debug.Log(m_www.text);
	    ShipDataColl _sdc= JsonMapper.ToObject<ShipDataColl>(m_www.text);
	    foreach(ShipData _sd in _sdc.m_ship_list){
	      Ship _s=new Ship(GameObject.Instantiate(ShipPref) as GameObject,Mat);
	      _s.init(_sd);
	      m_ship_list.Add(_s);
	      if(m_current_ship==null){
		m_current_ship=_s;
	      }
	    }
	  }
	  m_www=null;
	}
      }
    }
    //update
    if(Input.GetKey(KeyCode.UpArrow)){
      m_wind+=new Vector3(0,0,Time.deltaTime*m_speed);
    }
    if(Input.GetKey(KeyCode.DownArrow)){
      m_wind+=new Vector3(0,0,-Time.deltaTime*m_speed);
    }
    if(Input.GetKey(KeyCode.LeftArrow)){
      m_wind+=new Vector3(-Time.deltaTime*m_speed,0,0);
    }
    if(Input.GetKey(KeyCode.RightArrow)){
      m_wind+=new Vector3(Time.deltaTime*m_speed,0,0);
    }
    if(Input.GetKeyDown(KeyCode.U)){
      m_current_ship.serialize();
    }  
    if(Input.GetKeyDown(KeyCode.Escape)){
      Application.LoadLevel("Root");
    }
    if(m_current_ship!=null){
      m_current_ship.update(m_wind,false);
    }
  }
  void OnGUI(){
    GUI.Label(new Rect(0,Screen.height-20,100,20),"Wind:"+(int)(m_wind.x*100)+","+(int)(m_wind.z*100));
    if(GUI.Button(new Rect(100,Screen.height-20,100,20),"Save")){
      NetMsg _msg=SeaCore.get_single().get_net_mgr().send_msg(NetMsgType.SAVE_SHIP_DATA,save_ship_data_del);
      ShipDataColl _sdc=new ShipDataColl();
      foreach(Ship _s in m_ship_list){
	_sdc.m_ship_list.Add(_s.get_data());
      }
      _msg.args.Add("wuid",GlobalInfoer.user_wuid.ToString());
      _msg.args.Add("data",NetMsgMgr.UrlEncode(JsonMapper.ToJson(_sdc)));
      //string _sd=JsonMapper.ToJson(_ship_data);
      //  StreamWriter temp_sw = new StreamWriter("./GameData/Ship/data.json", false, System.Text.Encoding.ASCII);
      //temp_sw.Write(JsonMapper.ToJson(_sdc));
      // temp_sw.Close();			
      //save all ships
    }
  }
  void save_ship_data_del(object _obj){
  }
  void get_ship_data_del(object _obj){
    NmGetShipResult _ship_result= _obj as NmGetShipResult;
    // if(_shi))
    foreach(ShipData _sd in _ship_result.ship_data.m_ship_list){
	    Ship _s=new Ship(GameObject.Instantiate(ShipPref) as GameObject,Mat);
	    _s.init(_sd);
	    m_ship_list.Add(_s);
	    if(m_current_ship==null){
	      m_current_ship=_s;
	    }
	  }
  }
  bool m_file_mode=false;
  WWW m_www;
  public Material Mat;
  public GameObject ShipPref;
  Ship m_current_ship;
  List<Ship> m_ship_list=new List<Ship>();
  Vector3 m_wind=new Vector3(0,0,0);
  float m_speed=1.0f;
}
