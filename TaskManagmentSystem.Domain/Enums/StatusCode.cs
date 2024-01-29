using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagmentSystem.Domain.Enums
{
    public enum StatusCode
    {
        OK = 10,
        ObjectNotFound = 11,
        DataAlreadyExist = 12,
        InternalException = 500
    }
}
