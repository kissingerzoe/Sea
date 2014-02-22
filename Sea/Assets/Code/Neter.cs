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
	
	void OnGUI()
	{
		if(GUI.Button(new Rect(Screen.width*0.5f,Screen.height*0.5f,100,30),"login"))
		{
			NetMsg _nm=m_net_mgr.send_msg(NetMsgType.LOGIN);
			_nm.args.Add("uid","10000");
		}
	}
	NetMsgMgr m_net_mgr=new NetMsgMgr();
}
