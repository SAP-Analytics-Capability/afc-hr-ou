using System;
using System.Collections.Generic;

using hrsync.Models;

namespace hrsync.Interface
{
    public interface IMailSender
    {
        bool SendMail(List<string> content);
    }
}