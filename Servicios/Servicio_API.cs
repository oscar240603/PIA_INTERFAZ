using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using API_INTERFAZ.Models;
using System.Security.AccessControl;
using System;

namespace API_INTERFAZ.Servicios 
{
    public class Servicio_API : IServicio_API
    {
        private static string _usuario;
        private static string _contra;
        private static string _baseurl;
        private static string _token;


        public Servicio_API()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            _usuario = builder.GetSection("ApiSettings:usuario").Value;
            _contra = builder.GetSection("ApiSettings:contra").Value;
            _baseurl = builder.GetSection("ApiSettings:baseUrl").Value;

        }

        public async Task Autenticar()
        {
            var cliente = new HttpClient();

            cliente.BaseAddress = new Uri(_baseurl);

            var credenciales = new Credencial() { usuario = _usuario, contra = _contra };
            var content = new StringContent(JsonConvert.SerializeObject(credenciales), Encoding.UTF8,"application/json");
            var response = await cliente.PostAsync("api/Autenticacion/Validar", content);
            var json_respuesta = await response.Content.ReadAsStringAsync();

            var resultado = JsonConvert.DeserializeObject<ResultadoCredencial>(json_respuesta);

            _token = resultado.token;
        }

        public async Task<List<Alumno>> Lista()
        {
            List<Alumno> lista = new List<Alumno>();
            await Autenticar();
            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseurl);
            cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            var response = await cliente.GetAsync("api/Alumno/Lista");

            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content?.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<ResultadoApi>(json_respuesta);
                lista = resultado.lista;
            }
            return lista;
        }

        public async Task<Alumno> Obtener(int IdMatricula)
        {
            Alumno objeto = new Alumno();
            await Autenticar();
            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseurl);
            cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            var response = await cliente.GetAsync($"api/Alumno/Obtener/{IdMatricula}");

            if (response.IsSuccessStatusCode)
            {
                var json_respuesta = await response.Content?.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<ResultadoApi>(json_respuesta);
                objeto = resultado.objeto;
            }
            return objeto;
        }

        public async Task<bool> Guardar(Alumno objeto)
        {
            bool respuesta = false;
            await Autenticar();
            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseurl);
            cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            var content = new StringContent(JsonConvert.SerializeObject(objeto), Encoding.UTF8, "application/json");
            var response = await cliente.PostAsync("api/Alumno/Guardar/", content);

            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }
            return respuesta;
        }

        public async Task<bool> Editar(Alumno objeto)
        {
            bool respuesta = false;
            await Autenticar();
            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseurl);
            cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            var content = new StringContent(JsonConvert.SerializeObject(objeto), Encoding.UTF8, "application/json");
            var response = await cliente.PutAsync($"api/Alumno/Editar/", content);

            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }
            return respuesta;
        }

        public async Task<bool> Eliminar(int IdMatricula)
        {
            bool respuesta = false;
            await Autenticar();
            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseurl);
            cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            var response = await cliente.DeleteAsync($"api/Alumno/Eliminar/{IdMatricula}");

            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }
            return respuesta;
        }

    }
}
