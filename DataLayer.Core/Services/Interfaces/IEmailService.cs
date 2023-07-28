using ToCook.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToCook.Core.Services.Interfaces
{
    public interface IEmailService
    {
        public void SendEmail(EmailDTO request);
    }
}
