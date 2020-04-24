﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WorkFlowIdentity.Models
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }

    public class AuthSMSMessageSender : ISmsSender
    {
        public Task SendSmsAsync(string number, string message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }
}
