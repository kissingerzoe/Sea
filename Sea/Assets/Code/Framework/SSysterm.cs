using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public abstract class SSysterm
{
	public virtual void update()
	{
		foreach(SUIPanel _p in m_panel_list)
		{
			_p.update();
		}
	}
	public virtual void close()
	{
		foreach(SUIPanel  _p in m_panel_list)
		{
			_p.close();
		}
	}
	public virtual  void open()
	{
		res_check();
		foreach(SUIPanel  _p in m_panel_list)
		{
			_p.open();
		}		
	}
	public void release_panels()
	{
		foreach(SUIPanel _panel in m_panel_list)
		{
			_panel.release();
		}
		m_panel_list.Clear();
	}
	public void refresh_life(SystermType _type)
	{
		if(m_sys_type==_type)
		{
			m_lifes=mc_max_life;
		}
		else
		{
			m_lifes--;
			if(m_lifes<=0)
			{
				release_panels();
			}			
		}
	}
	protected void res_check()
	{
		if(!m_res_init)
		{
			res_init();
			m_res_init=true;
		}
	}
	public abstract void res_init();
	protected SystermType m_sys_type=SystermType.ROOT;
	protected List<SUIPanel> m_panel_list=new List<SUIPanel>();
	const int mc_max_life=3;	
	int m_lifes=mc_max_life;
	
	bool m_res_init=false;
}
