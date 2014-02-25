//ybzuo
using UnityEngine;
using System.Collections.Generic;
public enum SuRootSubType{
      LOGIN,
      SERVER,
      NORMAL,
}
public class SuRootPanel:SUIPanel{

    public SuRootPanel(){
      GameObject _pref=Resources.Load("UI/RootPanel") as GameObject;
      //Debug.Log(_pref==null);
      m_go=GameObject.Instantiate(_pref) as GameObject;
      m_go.transform.parent=SeaCore.get_single().ui_root;
      m_go.transform.localPosition=Vector3.zero;
      m_go.transform.localRotation=Quaternion.identity;
      m_go.transform.localScale=Vector3.one;
		
      m_main_p=m_go.transform.FindChild("MainP").gameObject;	
      m_login_p=m_go.transform.FindChild("LoginP").gameObject;
      m_server_p=m_go.transform.FindChild("ServerP").gameObject;
      m_server_panel=m_server_p.GetComponent<SuServerPanel>();
      m_root_mgr=m_go.GetComponent<RootMgr>();
    }
    public void bound_systerm(SsRoot _sroot){
      // m_ss_root=_sroot;
      m_root_mgr.bound_systerm(_sroot);
    }
public void set_server_info(List<NmLoginResultData> _nm_list){
  m_server_panel.init(_nm_list);
}
    public void switch_mode(SuRootSubType _sub_type){
      switch(_sub_type){
	case SuRootSubType.LOGIN:
	  m_login_p.SetActive(true);
	  m_main_p.SetActive(false);
	  m_server_p.SetActive(false);
	  break;
	case SuRootSubType.SERVER:
	  m_login_p.SetActive(false);
	  m_main_p.SetActive(false);
	  m_server_p.SetActive(true);
     	  break;
	case SuRootSubType.NORMAL:
	  m_login_p.SetActive(false);
	  m_main_p.SetActive(true);
	  m_server_p.SetActive(false);
	  break;
	}
    }
  RootMgr m_root_mgr;
  GameObject m_main_p;
  GameObject m_login_p;
  GameObject m_server_p;
  SuServerPanel m_server_panel;
  //SsRoot m_ss_root;
}
