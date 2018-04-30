using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Phase2.SimpleTodo.Web.Services
{
    public interface INotificationService {
        void NotifyAddTodoItem();
    }

    public class NotificationService: INotificationService
    {
        public void NotifyAddTodoItem()
        {
            Thread.Sleep(1000);
        }
    }
}
