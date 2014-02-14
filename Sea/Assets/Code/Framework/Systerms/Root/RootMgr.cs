using UnityEngine;
using System.Collections;

public class RootMgr : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void sea_b()
	{
		SeaCore.get_single().get_sys_mgr().open_sys(SystermType.SEA);
	}
	
	void fac_b()
	{
		SeaCore.get_single().get_sys_mgr().open_sys(SystermType.MAKER);
	}
}
