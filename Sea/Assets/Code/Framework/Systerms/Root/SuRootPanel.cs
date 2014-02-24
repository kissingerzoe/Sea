//ybzuo
using UnityEngine;
public class SuRootPanel:SUIPanel
{
	public SuRootPanel()
	{
		GameObject _pref=Resources.Load("UI/RootPanel") as GameObject;
		//Debug.Log(_pref==null);
		m_go=GameObject.Instantiate(_pref) as GameObject;
		m_go.transform.parent=SeaCore.get_single().ui_root;
		m_go.transform.localPosition=Vector3.zero;
		m_go.transform.localRotation=Quaternion.identity;
		m_go.transform.localScale=Vector3.one;
		
		m_main_p=m_go.transform.FindChild("MainP").gameObject;	
		m_create_p=m_go.transform.FindChild("CreateP").gameObject;
		m_root_mgr=m_go.GetComponent<RootMgr>();
	}
	public void bound_systerm(SsRoot _sroot)
	{
		//m_ss_root=_sroot;
		m_root_mgr.bound_systerm(_sroot);
	}
	public void switch_mode(bool _man){
		if(_man){
			m_main_p.SetActive(true);
			m_create_p.SetActive(false);
		}
		else{
			m_main_p.SetActive(false);
			m_create_p.SetActive(true);			
		}
	}
	RootMgr m_root_mgr;
	GameObject m_main_p;
	GameObject m_create_p;
	//SsRoot m_ss_root;
}
