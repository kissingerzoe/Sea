//ybzuo
using UnityEngine;
using System.Collections.Generic;
public enum NetMsgType
{
	LOGIN,
}
public class NetMsg
{
	public NetMsgType msg_type;
	public string sub_url;
	public Dictionary<string,string> args=new Dictionary<string, string>();
	public WWW www;
}

public class NetMsgMgr
{
	public void update()
	{
		
		if((m_waitl_que.Count>0)&&(m_work_list.Count<mc_max_work_num))
		{
			m_change_list.Clear();
			for(int i=0;i<Mathf.Min(mc_max_work_num-m_work_list.Count,m_waitl_que.Count);++i)
			{
				NetMsg _msg=m_waitl_que.Dequeue();
				WWWForm _form=new WWWForm();
				foreach(KeyValuePair<string,string> _kv in _msg.args)
				{
					_form.AddField(_kv.Key,_kv.Value);
				}
				_form.headers["Authorize"] = "authorize";
				_msg.www=new WWW(m_url_head+_msg.sub_url,_form);
				m_change_list.Add(_msg);				
			}
			
			foreach(NetMsg _nm in m_change_list)
			{
				m_work_list.Add(_nm);
			}
			m_change_list.Clear();
		}
		foreach(NetMsg _nm in m_work_list)
		{
			if(_nm.www.isDone)
			{
				if(_nm.www.error==null)
				{
					Debug.Log(_nm.www.text);
				}else
				{
					Debug.LogWarning(_nm.www.error);
				}
				m_change_list.Add(_nm);
			}
		}
		if(m_change_list.Count>0)
		{
			foreach(NetMsg _nm in m_change_list)
			{
				m_work_list.Remove(_nm);
			}
			m_change_list.Clear();
		}
		
	}
	public NetMsg send_msg(NetMsgType _type)
	{
		NetMsg _nm=new NetMsg();
		switch(_type)
		{
			case NetMsgType.LOGIN:
			default:
				_nm.sub_url="login";
				break;
		}
		m_waitl_que.Enqueue(_nm);
		return _nm;	
	}
	const int mc_max_work_num=3;
	string m_url_head="127.0.0.1:3000/";
	Queue<NetMsg> m_waitl_que=new Queue<NetMsg>();
	List<NetMsg> m_work_list=new List<NetMsg>();
	List<NetMsg> m_change_list=new List<NetMsg>();
}