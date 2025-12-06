using System.Collections.Generic;

namespace rulesmngt.Interfaces
{
    public interface IMailSender
    {
         bool SendMail(List<string> content);
    }
}