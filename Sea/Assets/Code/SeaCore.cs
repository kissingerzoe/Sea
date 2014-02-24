//ybzuo
using UnityEngine;
public class SeaCore:MonoBehaviour
{
    #region single
    static SeaCore instance = null;
    static public SeaCore get_single()
    {
        if (!instance)
        {
            instance = FindObjectOfType(typeof(SeaCore)) as SeaCore;
            if (!instance)
                Debug.LogError("There needs to be one active test script on a SeaCore in your scene.");
        }
        return instance;
    }
    #endregion		
	void Start(){
		MonoBehaviour.DontDestroyOnLoad(gameObject);
		m_sys_mgr=new SSysMgr();
		m_net_mgr=new NetMsgMgr();
	}
	void Update(){
		m_sys_mgr.update();
		m_net_mgr.update();
	}
	public SSysMgr get_sys_mgr(){
		return m_sys_mgr;
	}
	public NetMsgMgr get_net_mgr(){
		return m_net_mgr;
	}
	SSysMgr m_sys_mgr;
	public Transform ui_root;
	NetMsgMgr m_net_mgr;
}
