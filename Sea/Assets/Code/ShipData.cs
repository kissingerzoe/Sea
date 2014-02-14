using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class SailData
{
	public int m_cell_num=41;//5;
	public int m_cell_half=20;//1;
	public List<int> m_node_list=new List<int>();
	public List<double> m_vec_xlist=new List<double>();
	public List<double> m_vec_ylist=new List<double>();
	public List<double> m_vec_zlist=new List<double>();
	public double m_shift_offset=0.6;  
	public double m_sail_unit_x=0.1;
	public double m_sail_unit_y=0.2;
	//public Vector2 m_sail_unit=new Vector2(0.1f,0.2f);	
}

public class ShipData
{
	public string name="BlackPeal";
	public List<SailData> m_sail_list=new List<SailData>();
}

public class ShipDataColl
{
	public List<ShipData> m_ship_list=new List<ShipData>();
}




