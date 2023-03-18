using DevTest.Shared.DTO;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace DevTest.FrontEnd.WebApp.Pages
{
    public partial class EmployeeCreate
    {
        [Inject]
        private IHttpClientFactory ClientFactory { get; set; }
        private EmployeeDTO _employee = new();

        private async Task Post()
        {
            HttpClient client = ClientFactory.CreateClient("BackEndApi");
            var response = await client.PostAsJsonAsync<EmployeeDTO>("Employee", _employee);
            if (response.IsSuccessStatusCode)
            {
                Navigation.NavigateTo("/Employee");
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new Exception(errorMessage);
            }
            
        }
    }
}
