using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class SuServerPanel : MonoBehaviour {
    // Use this for initialization
    void Start () {
	
    }
    // Update is called once per frame
    void Update () {
	
    }
    public void init(List<NmLoginResultData> _list){
	Debug.Log(_list.Count);
	if(_list.Count>0){
	    m_item_list.Add(m_item);
	    for(int i=1;i<_list.Count;++i){
		UILabel  _go=GameObject.Instantiate(m_item) as UILabel;
		_go.transform.parent=m_item.transform.parent;
		_go.transform.localPosition=m_item.transform.localPosition+new Vector3(0,i*mc_unit_height,0);
		_go.transform.localScale=m_item.transform.localScale;

		m_item_list.Add(_go);
	    }
	}
	for(int i=0;i<_list.Count;++i){
	    m_item_list[i].GetComponent<UIButtonKeyMessage>().key=i;
	    m_item_list[i].text=_list[i].name;
	}
    }
    const int mc_unit_height=45;
    public UILabel m_item;
    List<UILabel> m_item_list=new List<UILabel>();
}

/*
 */
