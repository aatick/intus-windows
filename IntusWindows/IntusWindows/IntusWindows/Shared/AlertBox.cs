using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static IntusWindows.Shared.Helper;

namespace IntusWindows.Shared
{
    public class AlertBox
    {
        public enum AlertBoxType
        {
            Message, Confirm
        }
        public bool Visible { get; set; } = false;
        public AlertBoxType AlertType { get; set; } = AlertBoxType.Message;
        public string Message { get; set; } = "";
        public string Title { get; set; } = "";
        public Action AlertBoxAction { get; set; } = null;
        public async Task Show(string title, string message, AlertBoxType type, [Optional] Action callback)
        {
            Visible = true;
            AlertType = type;
            Message = message.Trim();
            Title = title;
            if (callback != null)
            {
                AlertBoxAction = callback;
            }
        }
        public async Task Hide()
        {
            Visible = false;
            Message = "";
            Title = "";
            AlertBoxAction = null;
        }
    }
}
