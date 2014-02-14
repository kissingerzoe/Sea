using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class SSysMgr
{
	public SSysMgr()
	{
		m_sys_dic.Add(SystermType.ROOT,new SsRoot());
		m_sys_dic.Add(SystermType.MAKER,new SsMaker());
		m_sys_dic.Add(SystermType.SEA,new SsSea());
		m_current_systerm=m_sys_dic[SystermType.ROOT];
		m_current_systerm.open();
	}
	
	public void update()
	{
		if(m_current_systerm!=null)
		{
			m_current_systerm.update();
		}
		for(int i=0;i<m_optin_systerm.Count;++i)
		{
			m_optin_systerm[i].update();
		}
	}
	
	public void open_sys(SystermType _type)
	{
		if(m_current_systerm!=null)
		{
			m_current_systerm.close();
		}
		m_current_systerm=m_sys_dic[_type];
		m_current_systerm.open();
		foreach(KeyValuePair<SystermType,SSysterm> _kv in m_sys_dic)
		{
			_kv.Value.refresh_life(_type);
		}
	}
	
	public void switch_optin_sys(SystermType _type,bool _switch)
	{
		if(_switch)
		{
			m_sys_dic[_type].open();
		}
		else
		{
			m_sys_dic[_type].close();
		}
	}
	Dictionary<SystermType,SSysterm> m_sys_dic=new Dictionary<SystermType, SSysterm>();	
	SSysterm m_current_systerm;
	List<SSysterm> m_optin_systerm=new List<SSysterm>();
}
