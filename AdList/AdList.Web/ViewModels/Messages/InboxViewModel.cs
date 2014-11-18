namespace AdList.Web.ViewModels.Messages
{
    using AdList.Data.Models;
    using System.Collections.Generic;

    public class InboxViewModel
    {
        public User User { get; set; }

        public IEnumerable<Message> Messages { get; set; }

        public IEnumerable<Message> MessagesByMe { get; set; }
    }
}