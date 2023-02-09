using IntusWindows.BLL.Services;
using IntusWindows.DAL.DataModels;
using Microsoft.AspNetCore.Components;
using static IntusWindows.Shared.AlertBox;

namespace IntusWindows.Client.Pages
{
    partial class WindowList
    {
        [Inject]
        public IClientOrderService _orderService { get; set; }
        [Inject]
        public IClientWindowService _windowService { get; set; }
        [Inject]
        public NavigationManager _navigationManager { get; set; }
        public List<Window>? windowList = null;
        public List<Order> orderList = new List<Order>();
        public bool showDeleteModal = false;
        public Window ToBeDelete { get; set; } = null;
        public AlertBox alertBox { get; set; } = new AlertBox();

        protected override async Task OnInitializedAsync()
        {
            windowList = await _windowService.GetWindows();
            orderList = await _orderService.GetOrders();
        }
        public void GoToNewWindow()
        {
            _navigationManager.NavigateTo("windowsetup");
        }
        public async void OnOrderSelect(ChangeEventArgs e)
        {
            if (int.Parse(e.Value.ToString()) == 0)
            {
                windowList = await _windowService.GetWindows();
            }
            else {
                windowList = await _windowService.GetWindowsByOrderId(int.Parse(e.Value.ToString()));
            }
            StateHasChanged();
        }
        private async void GetAllWindows()
        {
            windowList = await _windowService.GetWindows();
            await alertBox.Hide();
            StateHasChanged();
        }
        public void EditWindow(int id)
        {
            _navigationManager.NavigateTo($"windowsetup/{id}");
        }
        public async void ShowModal(int id)
        {
            await alertBox.Show("Delete Window?", "Are you sure to delete this window.", AlertBoxType.Confirm, () => DeleteWindowAsync());
            await ShowDeleteModalAsync(id);
        }
        private async Task ShowDeleteModalAsync(int id)
        {
            try
            {
                ToBeDelete = await _windowService.GetWindowById(id);
            }
            catch (Exception ex)
            {
                await alertBox.Hide();
            }
        }
        private async Task DeleteWindowAsync()
        {
            await alertBox.Hide();
            var result = "";
            if (ToBeDelete != null)
            {
                if (ToBeDelete.Id > 0)
                {
                    result = await _windowService.DeleteWindowById(ToBeDelete.Id);
                }
            }
            await alertBox.Show("Alert", result, AlertBoxType.Message, GetAllWindows);
            StateHasChanged();
        }
    }
}
