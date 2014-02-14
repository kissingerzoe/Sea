//ybzuo
//SUI_panel 
//
using UnityEngine;
using System.Collections;
public abstract class SUIPanel 
{
	//realse all res
	public virtual void release()
	{
		GameObject.Destroy(m_go);
		Resources.UnloadUnusedAssets();
	}
	public virtual void close()
	{
		m_go.SetActive(false);
	}
	public virtual void open()
	{
		m_go.SetActive(true);
	}
	public virtual void update()
	{
	}
	protected GameObject m_go;
}
