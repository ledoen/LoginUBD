using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public enum LoginStatus
    {
        UserNameSizeError = -3,
        PasswordSizeError = -2,
        UserNameExist = -1,
        NomalState = 0,
        UserNameNotExist,
        DataNotMatch
    }
}