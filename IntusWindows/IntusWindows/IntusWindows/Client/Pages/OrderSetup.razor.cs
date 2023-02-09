using IntusWindows.BLL.Services;
using IntusWindows.DAL.DataModels;
using Microsoft.AspNetCore.Components;
using static IntusWindows.Shared.AlertBox;

namespace IntusWindows.Client.Pages
{
    partial class OrderSetup
    {
        [Parameter]
        public int? Id { get; set; }
        public Order order = new Order() { Id = 0 };
        [Inject]
        public IClientOrderService _orderService { get; set; }
        [Inject]
        public NavigationManager _navigationManager { get; set; }
        public AlertBox alertBox { get; set; } = new AlertBox();

        protected override async Task OnParametersSetAsync()
        {
            if (Id == null)
                return;
            if (Id > 0)
            {
                order = await _orderService.GetOrderById(Id.Value);
            }
            StateHasChanged();
        }
        public async void HandleSubmitAsync()
        {
            var response = "";
            var isValid = true;
            if (string.IsNullOrEmpty(order.Name))
            {
                isValid = false;
                response = "Please enter order name.";
            }
            else if (string.IsNullOrEmpty(order.Name))
            {
                isValid = false;
                response = "Please enter state.";
            }

            if (isValid)
            {
                if (Id == null || Id == 0)
                {
                    response = await _orderService.CreateOrder(order);
                }
                else
                {
                    response = await _orderService.UpdateOrder(order);
                }
                await alertBox.Show("Alert", response, AlertBoxType.Message, RedirectToOrderList);
            }
            else
            {
                await alertBox.Show("Alert", response, AlertBoxType.Message);
            }
            
            StateHasChanged();
        }
        private void RedirectToOrderList()
        {
            alertBox.Hide();
            _navigationManager.NavigateTo("salesorders");
        }
    }
}
