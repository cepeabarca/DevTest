using DevTest.Shared.DTO;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.Http.Json;
using DevTest.Shared.DTO;

namespace DevTest.FrontEnd.WebApp.Pages
{
    public partial class EmployeeCRUD
    {
        [Inject]
        private IHttpClientFactory ClientFactory { get; set; }

        private List<DevTest.Shared.DTO.EmployeeDTO> _employee;
        private string _rfcFilter;
        private DateTime? _bornDateFilter;


        protected override async Task OnInitializedAsync()
        {
            try
            {
                HttpClient client = ClientFactory.CreateClient("BackEndApi");
                FilterDTO filter = new FilterDTO { RFC = _rfcFilter, BornDate = _bornDateFilter };
                HttpResponseMessage response = await client.PostAsJsonAsync("Employee/employeesFilter", filter);
                if (response.IsSuccessStatusCode)
                {
                    _employee = await response.Content.ReadFromJsonAsync<List<EmployeeDTO>>();
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    throw new Exception(errorMessage);
                }
                //_employee = await client.GetFromJsonAsync<List<EmployeeDTO>>("Employee");
            }
            catch (Exception ex)
            { }
        }



        private async Task Delete(int id)
        {
            HttpClient client = ClientFactory.CreateClient("BackEndApi");
            await client.DeleteAsync($"Employee/{id}");
            _employee = await client.GetFromJsonAsync<List<EmployeeDTO>>("Employee");
            StateHasChanged();
            await Filter();
        }

        private async Task Filter()
        {
            HttpClient client = ClientFactory.CreateClient("BackEndApi");
            FilterDTO filter = new FilterDTO { RFC = _rfcFilter, BornDate = _bornDateFilter };
            HttpResponseMessage response = await client.PostAsJsonAsync("Employee/employeesFilter", filter);
            if (response.IsSuccessStatusCode)
            {
                _employee = await response.Content.ReadFromJsonAsync<List<EmployeeDTO>>();
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new Exception(errorMessage);
            }
        }
        private void Edit(int id)
        {
            Navigation.NavigateTo($"/Employee/edit/{id}");
        }

        private void Create()
        {
            try
            {
                Navigation.NavigateTo("/Employee/create");
            }
            catch(Exception ex) { }
        }
    }
}
