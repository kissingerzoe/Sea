//ybzuo
public class SsRoot:SSysterm
{
	public override void open(){
		base.open();
		m_root_panel=m_panel_list[0] as SuRootPanel;
		m_root_panel.switch_mode(m_login);
		m_root_panel.bound_systerm(this);
	}
	public override void res_init(){
		m_panel_list.Add(new SuRootPanel());	
	}
	public override void update(){
	
	}
	public void login(string _name,string _pass)
	{
		NetMsg _nm= SeaCore.get_single().get_net_mgr().send_msg(NetMsgType.LOGIN,login_del);
		_nm.args.Add("user",_name);
		_nm.args.Add("pass",_pass);
	}
	void login_del(object _obj)
	{
		NetMsgBase _mb=_obj as NetMsgBase;
		if(_mb.code>0)
		{
			m_login=true;
			m_root_panel.switch_mode(m_login);			
		}
		else
		{
			UnityEngine.Debug.LogWarning("Login Fail");
		}
	}
	bool m_login=false;
	SuRootPanel m_root_panel;
}
