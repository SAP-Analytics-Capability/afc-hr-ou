using System.Collections.Generic;

namespace masterdata.Interfaces
{
    public interface IMailSender
    {
        bool SendMail(List<string> content);
    }
}