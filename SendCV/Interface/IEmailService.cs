using System;
using System.Collections.Generic;
using System.Text;

namespace SendCV.Interface
{
    public interface IEmailService
    {
        void SendEmail(string emailToSend, bool isSendAtt, string companyName);
    }
}
