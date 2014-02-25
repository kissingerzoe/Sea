//ybzuo
public class SsRoot:SSysterm
{
    public enum SsRootStage{
      DUMMY,
      LOGIN,
      SERVER,
      IDLE,
    }
    public override void open(){
      base.open();
      m_root_panel=m_panel_list[0] as SuRootPanel;
      m_root_panel.bound_systerm(this);
      refresh_stage();
    }
    public override void res_init(){
      m_panel_list.Add(new SuRootPanel());	
    }
    public override void update(){
	
    }
    public void login(string _name,string _pass){
      NetMsg _nm= SeaCore.get_single().get_net_mgr().send_msg(NetMsgType.LOGIN,login_del);
      _nm.args.Add("user",_name);
      _nm.args.Add("pass",_pass);
    }

    void login_del(object _obj){
    NmLoginResult  _mb=_obj as NmLoginResult;
    UnityEngine.Debug.Log(_mb.code+" "+_mb.msg);
    if(_mb.code>0){
      m_current_stage=SsRootStage.SERVER;
      refresh_stage();
      m_root_panel.set_server_info(_mb.game_server_list);
    }else{
      UnityEngine.Debug.LogWarning(_mb.msg);
    }
  }
  void refresh_stage(){
    switch(m_current_stage){
    case SsRootStage.DUMMY:
      m_root_panel.switch_mode(SuRootSubType.LOGIN);
      break;
    case SsRootStage.LOGIN:
      //m_root_panel.switch_mode(SuRootSubType.LOGIN);
      break;
    case SsRootStage.SERVER:
      m_root_panel.switch_mode(SuRootSubType.SERVER);
      break;
    case SsRootStage.IDLE:
      m_root_panel.switch_mode(SuRootSubType.NORMAL);
      break;
    }
  }
  public void enter_server(int _index){
      if(m_login_result!=null){
	  NetMsg _nm= SeaCore.get_single().get_net_mgr().send_msg(NetMsgType.ENTER_SERVER,enter_server_del);
	  //	  _nm.args.Add("user",_name);
	  //	  _nm.args.Add("pass",_pass);
      }
  }
  void enter_server_del(object _obj){
    NmLoginResult  _mb=_obj as NmLoginResult;
    UnityEngine.Debug.Log(_mb.code+" "+_mb.msg);
    if(_mb.code>0){
      m_current_stage=SsRootStage.SERVER;
      refresh_stage();
      m_root_panel.set_server_info(_mb.game_server_list);
    }else{
      UnityEngine.Debug.LogWarning(_mb.msg);
    }
  }
  SsRootStage m_current_stage=SsRootStage.DUMMY;
  SuRootPanel m_root_panel;
  NmLoginResult m_login_result=null;
}
