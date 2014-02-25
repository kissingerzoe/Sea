using System.Collections.Generic;
public class NmLoginResultData
{
    public string name;
    public string url;
}
public class NmLoginResult:NetMsgBase
{
    public List<NmLoginResultData> game_server_list;
}