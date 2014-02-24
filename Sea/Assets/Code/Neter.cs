using UnityEngine;
using System.Collections;

public class Neter : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		m_net_mgr.update();
	}
	
	void OnGUI(){
		string _value=GUI.TextArea(new Rect(Screen.width*0.5f,Screen.height*0.5f-60,100,30),m_user);
		if(_value!=m_user){
			m_user=_value;
		}
		_value=GUI.TextArea(new Rect(Screen.width*0.5f,Screen.height*0.5f-30,100,30),m_pass);
		if(_value!=m_pass){
			m_pass=_value;
		}
		if(GUI.Button(new Rect(Screen.width*0.5f,Screen.height*0.5f,100,30),"login")){
			NetMsg _nm=m_net_mgr.send_msg(NetMsgType.LOGIN,login_del);
			_nm.args.Add("user",m_user);
			_nm.args.Add("pass",m_pass);
		}
	}
	void login_del(object _obj)
	{
		NetMsgBase _mb=_obj as NetMsgBase;
		Debug.Log(_mb.msg);
	}
	string m_user="user";
	string m_pass="pass";
	NetMsgMgr m_net_mgr=new NetMsgMgr();
}
