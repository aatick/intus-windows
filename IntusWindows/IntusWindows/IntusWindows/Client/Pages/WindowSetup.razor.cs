using IntusWindows.BLL.Services;
using IntusWindows.DAL.DataModels;
using Microsoft.AspNetCore.Components;
using static IntusWindows.Shared.AlertBox;

namespace IntusWindows.Client.Pages
{
    partial class WindowSetup
    {
        [Parameter]
        public int? Id { get; set; }
        public Window aWindow = new Window() { Id = 0 };
        public List<Order> orderList = new List<Order>();
        public int SelectedOrderId { get; set; } = 0;
        [Inject]
        public IClientOrderService _orderService { get; set; }
        [Inject]
        public IClientWindowService _windowService { get; set; }
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
                aWindow = await _windowService.GetWindowById(Id.Value);
            }
            StateHasChanged();
        }
        public void OnOrderSelect(ChangeEventArgs e)
        {
            aWindow.OrderId = int.Parse(e.Value.ToString());
            StateHasChanged();
        }
        public async void HandleSubmitAsync()
        {
            var response = "";
            var isValid = true;
            if (string.IsNullOrEmpty(aWindow.Name))
            {
                isValid = false;
                response = "Please enter window name.";
            }
            else if (aWindow.QualityOfWindows == 0)
            {
                isValid = false;
                response = "Please enter window quality.";
            }
            else if (aWindow.OrderId == 0)
            {
                isValid = false;
                response = "Please select order.";
            }
            if (isValid)
            {
                if (Id == null || Id == 0)
                {
                    response = await _windowService.CreateWindow(aWindow);
                }
                else
                {
                    response = await _windowService.UpdateWindow(aWindow);
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
            _navigationManager.NavigateTo("windowlist");
        }
    }
}
