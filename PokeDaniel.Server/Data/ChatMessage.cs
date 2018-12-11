using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace PokeDaniel.Server.Data
{

    public class ChatMessage
    {
        public Guid ChatMessageId { get; set; }

        public virtual ApplicationUser Sender { get; set; }

        public DateTime DateSent { get; set; }

        public string Body { get; set; }
    }
}
