//ybzuo
using UnityEngine;
public class SuMakerPanel:SUIPanel
{
	public SuMakerPanel()
	{
		GameObject _pref=Resources.Load("UI/MakerPanel") as GameObject;
		//Debug.Log(_pref==null);
		m_go=GameObject.Instantiate(_pref) as GameObject;
		m_go.transform.parent=SeaCore.get_single().ui_root;
		m_go.transform.localPosition=Vector3.zero;
		m_go.transform.localRotation=Quaternion.identity;
		m_go.transform.localScale=Vector3.one;
	}
}

