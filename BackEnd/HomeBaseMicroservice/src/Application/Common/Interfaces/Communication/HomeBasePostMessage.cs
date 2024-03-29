﻿using Domain.DTOs;

namespace Application.Common.Interfaces.Communication
{
    public class HomeBasePostMessage : IMessage<HomeBaseResponse>
    {
        public string MessageType { get; set; }
        public string Method { get; set; }
        public HomeBaseResponse Message { get; set; }
    }
}