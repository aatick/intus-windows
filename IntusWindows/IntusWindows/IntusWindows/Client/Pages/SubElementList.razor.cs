using IntusWindows.BLL.Services;
using IntusWindows.DAL.DataModels;
using Microsoft.AspNetCore.Components;
using static IntusWindows.Shared.AlertBox;

namespace IntusWindows.Client.Pages
{
    partial class SubElementList
    {
        [Inject]
        public IClientOrderService _orderService { get; set; }
        [Inject]
        public IClientWindowService _windowService { get; set; }
        [Inject]
        public IClientSubElementService _subElementService { get; set; }
        [Inject]
        public NavigationManager _navigationManager { get; set; }
        public List<SubElement>? subElementList = null;
        public List<Order> orderList = new List<Order>();
        public List<Window> windowList = new List<Window>();
        public bool showDeleteModal = false;
        public SubElement ToBeDelete { get; set; } = null;
        public AlertBox alertBox { get; set; } = new AlertBox();

        protected override async Task OnInitializedAsync()
        {
            subElementList = await _subElementService.GetSubElements();
            orderList = await _orderService.GetOrders();
        }
        private async void FilterSubElements(int orderId = 0, int windowId = 0)
        {
            
        }
        public async void OnOrderSelect(ChangeEventArgs e)
        {
            windowList = await _windowService.GetWindowsByOrderId(int.Parse(e.Value.ToString()));
            subElementList= await _subElementService.GetSubElementsByOrderId(int.Parse(e.Value.ToString()));
            StateHasChanged();
        }
        public async void OnWindowSelect(ChangeEventArgs e)
        {
            subElementList = await _subElementService.GetSubElementsByWindowId(int.Parse(e.Value.ToString()));
            StateHasChanged();
        }
        public void GoToNewSubElement()
        {
            _navigationManager.NavigateTo("subelementsetup");
        }

        private async void GetAllSubElements()
        {
            subElementList = await _subElementService.GetSubElements();
            await alertBox.Hide();
            StateHasChanged();
        }
        public void EditSubElement(int id)
        {
            _navigationManager.NavigateTo($"subelementsetup/{id}");
        }
        public async void ShowModal(int id)
        {
            await alertBox.Show("Delete SubElement?", "Are you sure to delete this window.", AlertBoxType.Confirm, () => DeleteSubElementAsync());
            await ShowDeleteModalAsync(id);
        }
        private async Task ShowDeleteModalAsync(int id)
        {
            try
            {
                ToBeDelete = await _subElementService.GetSubElementById(id);
            }
            catch (Exception ex)
            {
                await alertBox.Hide();
            }
        }
        private async Task DeleteSubElementAsync()
        {
            await alertBox.Hide();
            var result = "";
            if (ToBeDelete != null)
            {
                if (ToBeDelete.Id > 0)
                {
                    result = await _subElementService.DeleteSubElementById(ToBeDelete.Id);
                }
            }
            await alertBox.Show("Alert", result, AlertBoxType.Message, GetAllSubElements);
            StateHasChanged();
        }
    }
}
