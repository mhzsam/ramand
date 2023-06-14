using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interface
{
    public interface IRabbitService
    {
        public void SendMessage(string channelName,string message);
    }
}
