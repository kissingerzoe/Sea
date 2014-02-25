using UnityEngine;
using System.Collections;
public class RootMgr : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void sea_b(){
		SeaCore.get_single().get_sys_mgr().open_sys(SystermType.SEA);
	}
	
	void fac_b(){
		SeaCore.get_single().get_sys_mgr().open_sys(SystermType.MAKER);
	}
	
	void login_b(){
		m_ss_root.login(name_label.text,pass_label.text);
		//SeaCore.get_single().get_net_mgr().send_msg(NetMsgType.LOGIN,
	}
        void server_b(int _index){
	  m_ss_root.enter_server(_index);
        }
	public void bound_systerm(SsRoot _sr){
	  m_ss_root=_sr;
	}
	SsRoot m_ss_root;
	public UILabel name_label;
	public UILabel pass_label;
}
