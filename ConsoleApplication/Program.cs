using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using ConsoleApplication.Models;

namespace ConsoleApplication
{
    class Program
    {
        public int Id { get; set; }
        public string Fname { get; set; }
        public string LName { get; set; }
        public string City { get; set; }
        public int Salary { get; set; }

        static HttpClient client = new HttpClient();

        static void ShowEmployee( Employee emp)
        {
            Console.WriteLine($"Fname: {emp.Fname}\tLName: {emp.LName}\tSalary: {emp.Salary}\tCity: {emp.City}");
        }

        static async Task<Uri> CreateEmployeeAsync(Employee emp)
        {

            HttpResponseMessage response = await client.PostAsJsonAsync("api/Emps", emp);
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.Headers.Location;
        }

        static async Task<Employee> GetEmployeeAsync(string path)
        {
            Employee emp = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                emp = await response.Content.ReadAsAsync<Employee>();
            }
            return emp;
        }

        static async Task<Employee> UpdateEmployeeAsync(Employee emp)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync($"api/Emps/{emp.Id}", emp);
            response.EnsureSuccessStatusCode();

            // Deserialize the updated emp from the response body.
            emp = await response.Content.ReadAsAsync<Employee>();
            return emp;
        }

        static async Task<HttpStatusCode> DeleteEmployeeAsync(int id)
        {
            HttpResponseMessage response = await client.DeleteAsync($"api/Emps/{id}");
            return response.StatusCode;
        }
        
        static void Main()
        {
            RunAsync().Wait();
        }

        static async Task RunAsync()
        {
            client.BaseAddress = new Uri("http://localhost:51474");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                // Create a new emp
                Employee emp = new Employee { Fname = "Gizmo", LName = "Sharma", City = "Jaipur", Salary = 20000 };

                var url = await CreateEmployeeAsync(emp);
                Console.WriteLine($"Created at {url}");

                // Get the emp
                emp = await GetEmployeeAsync(url.PathAndQuery);
                ShowEmployee(emp);

                // Update the emp
                Console.WriteLine("Updating city...");
                emp.City = "Pune";
                await UpdateEmployeeAsync(emp);

                // Get the updated emp
                emp = await GetEmployeeAsync(url.PathAndQuery);
                ShowEmployee(emp);

                // Delete the emp
                var statusCode = await DeleteEmployeeAsync(emp.Id);
                Console.WriteLine($"Deleted (HTTP Status = {(int)statusCode})");

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
    }
}
