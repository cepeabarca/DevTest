using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using DevTest.Shared.DTO;

namespace DevTest.FrontEnd.WebApp.Pages
{
    public partial class EmployeeEdit
    {
        [Parameter]
        public int Id { get; set; }

        [Inject]
        private IHttpClientFactory ClientFactory { get; set; }

        private EmployeeDTO _employee = null;

        protected override async Task OnInitializedAsync()
        {
            HttpClient client = ClientFactory.CreateClient("BackEndApi");
            _employee = await client.GetFromJsonAsync<EmployeeDTO>($"Employee/{Id}");
        }


        private async Task Put()
        {

            HttpClient client = ClientFactory.CreateClient("BackEndApi");
            var response = await client.PutAsJsonAsync<EmployeeDTO>($"Employee/{_employee.ID}", _employee);
            if (response.IsSuccessStatusCode)
            {
                Navigation.NavigateTo("Employee");
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new Exception(errorMessage);
            }
        }
    }
}
