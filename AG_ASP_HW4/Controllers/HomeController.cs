using AP_project.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AP_project.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMessageService messageService;
        public HomeController(IMessageService messageService)
        {
            this.messageService = messageService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetMessages()
        {
            var messages = await messageService.GetMessagesAsync();
            string response = JsonConvert.SerializeObject(messages);
            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddMessage(string messageText)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return Json("������: ������������ �� �����������");
            }
            if (!string.IsNullOrWhiteSpace(messageText))
            {
                await messageService.AddMessageAsync(userId.Value, messageText);
                return Json("��������� ������� ���������!");
            }
            return Json("������: ��������� �� ����� ���� ������!");
        }

        [HttpGet]
        public IActionResult GetUserStatus()
        {
            bool isUser = !string.IsNullOrEmpty(HttpContext.Session.GetString("UserLogin"));
            bool isGuest = (!isUser && HttpContext.Session.GetString("IsGuest") == "true");

            string message;
            if (isUser)
            {
                message = $"������������, {HttpContext.Session.GetString("UserLogin")}!";
            }
            else if (isGuest)
            {
                message = "������������, �����!";
            }
            else
            {
                message = "������������!";
            }

            return Json(new { message, isUser, isGuest });
        }
    }
}