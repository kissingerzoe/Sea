using UnityEngine;
using System.Collections;

public class Dummer : MonoBehaviour {	
	    
	    #region single
	    static Dummer instance = null;
	    static public Dummer get_single()
	    {
	        if (!instance)
	        {
	            instance = FindObjectOfType(typeof(Dummer)) as Dummer;
	            if (!instance)
	                Debug.LogError("There needs to be one active test script on a Dummer in your scene.");
	        }
	        return instance;
	    }
	    #endregion	

	
	
	

	// Use this for initialization
	void Start () 
	{
		m_sea=new Sea();
	}
	
	// Update is called once per frame
	void Update ()
	{
		m_sea.update();
	
	}
	void OnGUI()
	{
		m_sea.on_gui();
	}
	
	Sea m_sea=null;
	
	public GameObject ship_pref;
	public Material mat;
    public GameObject water;
	
	public Rudder rudder;
}
