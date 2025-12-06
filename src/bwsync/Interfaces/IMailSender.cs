using System.Collections.Generic;

namespace bwsync.Interfaces
{
    public interface IMailSender
    {
         bool SendMail(List<string> content);
    }
}