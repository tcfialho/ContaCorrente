using ContaCorrente.CrossCutting.Notifications;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ContaCorrente.Controllers
{
    public abstract class ControllerBase : Controller
    {
        protected readonly Notifications _notifications = Notifications.Instance; 

        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> ApiResponse<TResult>(Task<TResult> result = null)
        {
            IActionResult response;

            if (_notifications.HasNotifications())
                response = StatusCode((int)_notifications.GetStatusCode(), _notifications.GetMessages());
            else
                response = Ok(await result);

            _notifications.Clear();

            return response;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> ApiResponse(Task result = null)
        {
            IActionResult response = null;

            if (_notifications.HasNotifications())
                response = StatusCode((int)_notifications.GetStatusCode(), _notifications.GetMessages());
            else
            {
                await result;
                response = Ok();
            }

            _notifications.Clear();

            return response;
        }
    }
}
