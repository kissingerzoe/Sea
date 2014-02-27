//ybzuo
using UnityEngine;
using System.Collections.Generic;
using NbaLitJson;
using System.Text;
using System;
public enum NetMsgType
{
	LOGIN,
        ENTER_SERVER,
	SAVE_SHIP_DATA,
	GET_SHIP_DATA,
}
public delegate void NetMsgDel(object _obj);
public class NetMsg
{
	public NetMsgType msg_type;
	public string sub_url;
	public Dictionary<string,string> args=new Dictionary<string, string>();
	public WWW www;
	public object m_obj;
	public NetMsgDel m_del=null;
}
public class NetMsgMgr
{
 
  /*       
        public static string UrlEncode(string text){
	  return text;
	  if (string.IsNullOrEmpty(text)) return string.Empty;
	  StringBuilder buffer = new StringBuilder(text.Length);
	  byte[] data = Encoding.UTF8.GetBytes(text);
	  foreach (byte b in data){
	    char c = (char)b;
	    if (!(('0'<= c && c <= '9') || ('a'<= c && c <= 'z') || ('A'<= c && c <= 'Z'))
		&& "-_.~".IndexOf(c) == -1){
	      buffer.Append('%' + Convert.ToString(c, 16).ToUpper().PadLeft(2, '0'));
	    }else{
	      buffer.Append(c);
	    }}
	  return buffer.ToString();
	}
  */       

	public void update()
	{
		if((m_waitl_que.Count>0)&&(m_work_list.Count<mc_max_work_num)){
			m_change_list.Clear();
			for(int i=0;i<Mathf.Min(mc_max_work_num-m_work_list.Count,m_waitl_que.Count);++i){
				NetMsg _msg=m_waitl_que.Dequeue();
				WWWForm _form=new WWWForm();
				string _arg_list="";
				foreach(KeyValuePair<string,string> _kv in _msg.args){
					_form.AddField(_kv.Key,_kv.Value);
					_arg_list+=(_kv.Key+":"+_kv.Value);
					//_kv.Value= UrlEncode(_kv.Value);
				}
				_form.headers["Authorize"] = "authorize";
				_msg.www=new WWW(m_url_head+_msg.sub_url,_form);
				Debug.Log("[Post "+_msg.msg_type+ "]"+_msg.www.url+"  "+_arg_list); 
				m_change_list.Add(_msg);				
			}
			foreach(NetMsg _nm in m_change_list){
				m_work_list.Add(_nm);
			}
			m_change_list.Clear();
		}
		foreach(NetMsg _nm in m_work_list){
			if(_nm.www.isDone){
				if(_nm.www.error==null){
				  Debug.Log("[Get "+_nm.msg_type+"]"+_nm.www.text);
				  _nm.m_obj=net_object_parse(_nm.www.text,_nm.msg_type);
					if(_nm.m_del!=null){
						_nm.m_del(_nm.m_obj);
					}
				}else{
					Debug.LogWarning(_nm.www.error);
				}
				m_change_list.Add(_nm);
			}
		}
		if(m_change_list.Count>0){
			foreach(NetMsg _nm in m_change_list){
				m_work_list.Remove(_nm);
			}
			m_change_list.Clear();
		}
		
	} 
	public NetMsg send_msg(NetMsgType _type,NetMsgDel _del)
	{
		NetMsg _nm=new NetMsg();
		switch(_type){
			case NetMsgType.LOGIN:
			default:
				_nm.sub_url="login";
				break;
		        case NetMsgType.ENTER_SERVER:
			        _nm.sub_url="enter";
		                break;
		        case NetMsgType.SAVE_SHIP_DATA:
		                _nm.sub_url="save_ship_data";
		                 break;
		        case NetMsgType.GET_SHIP_DATA:
		                _nm.sub_url="get_ship_data";
		                 break;
		}
		_nm.msg_type=_type;
		_nm.m_del=_del;
		m_waitl_que.Enqueue(_nm);
		return _nm;	
	}
	object net_object_parse(string _msg,NetMsgType _msg_type){
		switch(_msg_type){
		default:
		        return  NbaLitJson.JsonMapper.ToObject<NetMsgBase>(_msg);
		case NetMsgType.LOGIN:
			return  NbaLitJson.JsonMapper.ToObject<NmLoginResult>(_msg);
		case NetMsgType.ENTER_SERVER:
		        return  NbaLitJson.JsonMapper.ToObject<NmEnterServerResult>(_msg);
		case NetMsgType.GET_SHIP_DATA:
		        return  NbaLitJson.JsonMapper.ToObject<NmGetShipResult>(_msg);
		}
	}
        public void set_server_ip(string _ip){
	  m_url_head=_ip;
	}
	const int mc_max_work_num=3;
	string m_url_head="127.0.0.1:3000/";
	Queue<NetMsg> m_waitl_que=new Queue<NetMsg>();
	List<NetMsg> m_work_list=new List<NetMsg>();
	List<NetMsg> m_change_list=new List<NetMsg>();
}
