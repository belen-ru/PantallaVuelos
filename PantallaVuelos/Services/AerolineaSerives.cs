using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PantallaVuelos.Models;


namespace PantallaVuelos.Services
{
    public class AerolineaSerives
    {
        HttpClient client;

        public AerolineaSerives()
        {
            client = new HttpClient()
            {
                BaseAddress = new Uri("https://aerotec.sistemas19.com/")
            };
        }
        public event Action<Aerolinea>? comand;

        public async Task<List<Aerolinea>> GetAerolineas()
        {
            List<Aerolinea>? aerolineas = null;
            var response = await client.GetAsync("api/Aerolinea");

            if (response.IsSuccessStatusCode) 
            {
                var json = await response.Content.ReadAsStringAsync();
                aerolineas = JsonConvert.DeserializeObject<List<Aerolinea>?>(json);
            }
            if(aerolineas != null) 
            {
                return aerolineas;
            }
            else
            {
                return new List<Aerolinea>();
            }

        }
        public async Task<bool> Delete(Aerolinea p)
        {
            var response = await client.DeleteAsync("api/Aerolinea/Cancelados" );
            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest) //BadRequest
            {
                var errores = await response.Content.ReadAsStringAsync();
                return false;
            }
         
            return true;
        }

        public async Task<List<Aerolinea>> GetAero()
        {
            List<Aerolinea>? aerolineas = null;
            var response = await client.GetAsync("api/Aerolinea/Cancelados");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                aerolineas = JsonConvert.DeserializeObject<List<Aerolinea>?>(json);
            }
            if (aerolineas != null)
            {
                return aerolineas;
            }
            else
            {
                return new List<Aerolinea>();
            }

        }

    }
}
