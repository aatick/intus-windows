using IntusWindows.BLL.Services;
using IntusWindows.DAL.DataModels;
using Microsoft.AspNetCore.Components;
using static IntusWindows.Shared.AlertBox;

namespace IntusWindows.Client.Pages
{
    partial class SubElementSetup
    {
        [Parameter]
        public int? Id { get; set; }
        public SubElement aSubElement = new SubElement() { Id = 0 };
        public List<Window> windowList = new List<Window>();
        public List<Order> orderList = new List<Order>();
        public int SelectedOrderId { get; set; } = 0;
        public int SelectedWindowId { get; set; } = 0;
        [Inject]
        public IClientOrderService _orderService { get; set; }
        [Inject]
        public IClientWindowService _windowService { get; set; }
        [Inject]
        public IClientSubElementService _subElemtService { get; set; }
        [Inject]
        public NavigationManager _navigationManager { get; set; }
        public AlertBox alertBox { get; set; } = new AlertBox();

        protected override async Task OnInitializedAsync()
        {
            orderList = await _orderService.GetOrders();
        }

        protected override async Task OnParametersSetAsync()
        {
            if (Id == null)
                return;
            if (Id > 0)
            {
                aSubElement = await _subElemtService.GetSubElementById(Id.Value);
                aSubElement.Window.Order = await _orderService.GetOrderById(aSubElement.Window.OrderId);
                windowList = await _windowService.GetWindowsByOrderId(aSubElement.Window.OrderId);
            }
            StateHasChanged();
        }
        public async void OnOrderSelect(ChangeEventArgs e)
        {
            windowList = await _windowService.GetWindowsByOrderId(int.Parse(e.Value.ToString()));
            aSubElement.WindowId = 0;
            StateHasChanged();
        }
        public void OnWindowSelect(ChangeEventArgs e)
        {
            aSubElement.WindowId = int.Parse(e.Value.ToString());
            StateHasChanged();
        }
        public async void HandleSubmitAsync()
        {
            var response = "";
            var isValid = true;
            if (aSubElement.WindowId == 0)
            {
                isValid = false;
                response = "Please select window.";
            }
            if (aSubElement.Element==0)
            {
                isValid = false;
                response = "Please enter element.";
            }
            else if (string.IsNullOrEmpty(aSubElement.Type))
            {
                isValid = false;
                response = "Please enter subelement type.";
            }
            else if (aSubElement.Height == 0)
            {
                isValid = false;
                response = "Please enter height.";
            }
            else if (aSubElement.Width == 0)
            {
                isValid = false;
                response = "Please enter width.";
            }
            if (isValid)
            {
                if (Id == null || Id == 0)
                {
                    response = await _subElemtService.CreateSubElement(aSubElement);
                }
                else
                {
                    response = await _subElemtService.UpdateSubElement(aSubElement);
                }
                await alertBox.Show("Alert", response, AlertBoxType.Message, RedirectToWindowList);
            }
            else
            {
                await alertBox.Show("Alert", response, AlertBoxType.Message);
            }
            StateHasChanged();
        }
        private void RedirectToWindowList()
        {
            alertBox.Hide();
            _navigationManager.NavigateTo("subelementlist");
        }
    }
}
