using System.Collections.Generic;

namespace VMP.Runtime
{
    public class VMPPingResult
    {
        public string Result = "";
    }
    public class VMPInstanceJoinResult
    {
        public int UserId;
    }

    public class VMPInstanceRegisterResult
    {
        public int InstanceId;
    }

    [System.Serializable]
    public class VMPInstancesResult
    {
        public List<VMPInstanceDTO> Instances;
    }
    [System.Serializable]
    public class VMPInstanceDTO
    {
        public string InstanceName;
    }
}