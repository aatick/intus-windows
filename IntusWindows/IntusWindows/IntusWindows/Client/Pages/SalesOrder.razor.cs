using IntusWindows.BLL.Services;
using IntusWindows.Shared;
using IntusWindows.DAL.DataModels;
using Microsoft.AspNetCore.Components;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using static IntusWindows.Shared.Helper;
using static IntusWindows.Shared.AlertBox;

namespace IntusWindows.Client.Pages
{
    partial class SalesOrder
    {
        [Inject]
        public IClientOrderService _orderService { get; set; }
        [Inject]
        public NavigationManager _navigationManager { get; set; }
        public List<Order>? orderList = null;
        public bool showDeleteModal = false;
        public Order ToBeDelete{ get; set; } =null;
        public AlertBox alertBox { get; set; } = new AlertBox();

        protected override async Task OnInitializedAsync()
        {
            orderList = await _orderService.GetOrders();
        }
        public void GoToNewOrder()
        {
            _navigationManager.NavigateTo("ordersetup");
        }
        private async void GetAllOrders()
        {
            orderList = await _orderService.GetOrders();
            await alertBox.Hide();
            StateHasChanged();
        }
        public void EditOrder(int id)
        {
            _navigationManager.NavigateTo($"ordersetup/{id}");
        }
        public async void ShowModal(int id)
        {
            await alertBox.Show("Delete Order?", "Are you sure to delete this order.", AlertBoxType.Confirm, () => DeleteOrderAsync());
            await ShowDeleteModalAsync(id);
        }
        private async Task ShowDeleteModalAsync(int id)
        {
            try
            {
                ToBeDelete = await _orderService.GetOrderById(id);
            }
            catch(Exception ex)
            {
                await alertBox.Hide();
            }
        }
        private async Task DeleteOrderAsync()
        {
            await alertBox.Hide();
            var result = "";
            if (ToBeDelete != null)
            {
                if (ToBeDelete.Id > 0)
                {
                    result = await _orderService.DeleteOrderById(ToBeDelete.Id);
                }
            }
            await alertBox.Show("Alert", result, AlertBoxType.Message, GetAllOrders);
            StateHasChanged();
        }
    }
}
