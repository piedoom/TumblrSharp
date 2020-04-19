using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace Common.Interfaces
{
    [ServiceContract]
    public interface IArgsReceiver
    {
        [OperationContract]
        void PassArgs(string[] args);
    }
}
