using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TrasfusionaleApp.Model;
using TrasfusionaleApp.Service;
using TrasfusionaleApp.Views;
using Xamarin.Forms;

namespace TrasfusionaleApp.ModelView
{
    public class ListaTrasfusionaleModelView : INotifyPropertyChanged
    {
        private Operatore operatore;
        private List<Reparto> reparti;
        private List<DatiTrasfusione> trasfusionali;
        public event PropertyChangedEventHandler PropertyChanged;
        public List<Reparto> _reparti
        {
            get { return reparti; }
            set
            {
                OnPropertyChanged();
                reparti = value;
            }
        }
        public List<DatiTrasfusione> _trasfusionali
        {
            get { return trasfusionali; }
            set
            {
                OnPropertyChanged();
                trasfusionali = value;
            }
        }

        public ICommand _terminaTrasfusione
        {
            get
            {
                return new Command(async (e) =>
                {
                    var item = e as DatiTrasfusione;
                    await terminaTrasfusione(item);
                });
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public ListaTrasfusionaleModelView(Operatore operatore)
        {
            this.operatore = operatore;
            reparti = new List<Reparto>();
            recuperaReparti();
        }

        public async void recuperaListaTrasfusionali(int index)
        {
            REST<Reparto, DatiTrasfusione> rEST = new REST<Reparto, DatiTrasfusione>();
            List<Header> headers = new List<Header>();
            headers.Add(new Header("access-token", App.Current.Properties["access-token"].ToString()));
            headers.Add(new Header("reparto", reparti[index].id));
            var response = await rEST.GetListJson(DatiTrasfusione.restListTrasfusioniForReparto,headers);
            if (rEST.responseMessage == System.Net.HttpStatusCode.OK)
                _trasfusionali = response;
            else if (rEST.responseMessage == System.Net.HttpStatusCode.NotFound)
                await App.Current.MainPage.DisplayAlert("Attenzione","Nessun processo in esecuzione nel reparto scelto","OK");
            else
                await App.Current.MainPage.DisplayAlert(rEST.responseMessage.ToString(), rEST.warning, "OK");
        }

        private async void recuperaReparti()
        {
            REST<object, Reparto> rEST = new REST<object, Reparto>();
            List<Header> headers = new List<Header>();
            headers.Add(new Header("access-token", App.Current.Properties["access-token"].ToString()));
            var response = await rEST.GetListJson(Reparto.restListaReparti, headers);
            if (rEST.responseMessage == System.Net.HttpStatusCode.OK)
                _reparti = response;
            else
                await App.Current.MainPage.DisplayAlert(rEST.responseMessage.ToString(), rEST.warning, "OK");
        }

        private async Task terminaTrasfusione(DatiTrasfusione item)
        {
            var risp = await App.Current.MainPage.DisplayAlert("Trasfusionale", "La trasfusione è terminata?", "SI", "NO");
            if (risp)
            {
                REST<DatiTrasfusione, string> rEST = new REST<DatiTrasfusione, string>();
                List<Header> headers = new List<Header>();
                headers.Add(new Header("access-token", App.Current.Properties["access-token"].ToString()));
                headers.Add(new Header("_id", item._id));
                var response = await rEST.getString(DatiTrasfusione.restFineTrasfusione, headers);
                if (rEST.responseMessage == System.Net.HttpStatusCode.OK)
                    await App.Current.MainPage.Navigation.PushAsync(new FineTrasfusione());
            }
        }
    }
}
