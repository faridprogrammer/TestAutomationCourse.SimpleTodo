using Phase2.SimpleTodo.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phase2.SimpleTodo.Test
{
    public class MockNotificationService : INotificationService
    {
        public bool NotifyAddTodoItem_CalledOnce = false;
        public void NotifyAddTodoItem()
        {
            NotifyAddTodoItem_CalledOnce = true;
        }
    }
}
