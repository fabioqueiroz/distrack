using DisTrack.Constants;
using DisTrack.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DisTrack
{
    public class FakeDatabase
    {
        public IDictionary<string, string> FakeUsers = new Dictionary<string, string>
        {
            { SystemConstants.Email, PasswordEncryption.SHA512ComputeHash(SystemConstants.Password) }
        };
    }
}
