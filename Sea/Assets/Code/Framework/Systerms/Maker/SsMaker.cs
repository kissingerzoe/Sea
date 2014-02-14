//ybzuo
using UnityEngine;
public class SsMaker:SSysterm
{
	public override void open()
	{
		Application.LoadLevel("Maker");
		base.open();
	}
	public override void res_init()
	{
		m_panel_list.Add(new SuMakerPanel());
	}
	public override void update()
	{
	}
}

