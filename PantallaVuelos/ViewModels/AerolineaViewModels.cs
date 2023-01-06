using PantallaVuelos.Models;
using PantallaVuelos.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Navigation;
using System.Windows.Threading;

namespace PantallaVuelos.ViewModels
{
    public class AerolineaViewModels :INotifyPropertyChanged
    {

        readonly AerolineaSerives server = new();

        public Aerolinea Aerolinea { get; set; } = null!;

        public ObservableCollection<Aerolinea> AerolineaList { get; set; } = new ObservableCollection<Aerolinea>();
        public ObservableCollection<Aerolinea> AerolineaListCancelados { get; set; } = new ObservableCollection<Aerolinea>();


        private static System.Timers.Timer timer = new();
        private static System.Timers.Timer timer2 = new();

        public AerolineaViewModels() 
        {
            timer.Interval = 5000;
            timer.Elapsed += ActualizarTimer;
            timer.Start();
            timer2.Interval = 13000;
            timer2.Elapsed += ActualizarTimer2;
            timer2.Start();
            cargarProductos();
            cargarCacelados();

        }

        private async void ActualizarTimer(object? sender, ElapsedEventArgs e)
        {
            App.Current.Dispatcher.Invoke(cargarProductos);
           
        }
        private async void ActualizarTimer2(object? sender, ElapsedEventArgs e)
        {
            App.Current.Dispatcher.Invoke(cargarProductos);

            if (AerolineaListCancelados.Count >= 1 )
            {

                await server.Delete(Aerolinea);


            }
            else 
            {
                timer2.Stop();
            }
            //if (AerolineaList.Count == 0)
            //{
            //    timer2.Start();



            //}
        }


        void Actualizar(string? property = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        async void cargarProductos()
        {
            AerolineaList.Clear();
            var datos = await server.GetAerolineas();
            datos.ForEach(x => AerolineaList.Add(x));
            Actualizar(nameof(AerolineaList));

        }
        async void cargarCacelados()
        {
            AerolineaListCancelados.Clear();
            var datos = await server.GetAero();
            datos.ForEach(x => AerolineaListCancelados.Add(x));
            Actualizar(nameof(AerolineaListCancelados));
        }


        public event PropertyChangedEventHandler? PropertyChanged;

    }
}
