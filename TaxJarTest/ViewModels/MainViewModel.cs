using System;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Prism.Commands;
using Prism.Navigation;
using TaxJarTest.Interfaces;
using TaxJarTest.Models;

namespace TaxJarTest.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IGetDataCalls getData;

        public DelegateCommand FigureTaxForOrderBtn { get; }
        public DelegateCommand FigureTaxRateBtn { get; }

        public MainViewModel(INavigationService navigationService, IGetDataCalls getData) : base(navigationService)
        {
            this.getData = getData;
            FigureTaxForOrderBtn = new DelegateCommand(async () => await FigureTaxForOrderBtnCmd());
            FigureTaxRateBtn = new DelegateCommand(async()=> await FigureTaxBtnCmd());
            //TODO added for testing.
            CityTxt = "Santa Monica";
            CountryTxt = "US";
            ZipTxt = "90404";
        }

        TaxRate _TaxRateObj = new TaxRate();
        public TaxRate TaxRateObj
        {
            get { return _TaxRateObj; }
            set { SetProperty(ref _TaxRateObj, value); }
        }

        TaxCalculations _Calculations;
        public TaxCalculations Calculations
        {
            get { return _Calculations; }
            set { SetProperty(ref _Calculations, value); }
        }

        string _ShippingFeeTxt;
        public string ShippingFeeTxt
        {
            get { return _ShippingFeeTxt; }
            set { SetProperty(ref _ShippingFeeTxt, value); }
        }

        string _TotalTxt;
        public string TotalTxt
        {
            get { return _TotalTxt; }
            set { SetProperty(ref _TotalTxt, value); }
        }

        string _CountryTxt;
        public string CountryTxt
        {
            get { return _CountryTxt; }
            set { SetProperty(ref _CountryTxt, value); }
        }

        string _StateTxt;
        public string StateTxt
        {
            get { return _StateTxt; }
            set { SetProperty(ref _StateTxt, value); }
        }

        string _CityTxt;
        public string CityTxt
        {
            get { return _CityTxt; }
            set { SetProperty(ref _CityTxt, value); }
        }

        string _ZipTxt;
        public string ZipTxt
        {
            get { return _ZipTxt; }
            set { SetProperty(ref _ZipTxt, value); }
        }

        private async Task FigureTaxBtnCmd()
        {
            try
            {
                JObject jObject = new JObject
                {
                    { "country", CountryTxt },
                    { "zipcode", ZipTxt },
                    { "city", CityTxt }
                };

                var answer = await this.getData.GetDataFromApi(jObject);
                var tempTaxRate = TaxRate.FromJson(answer);
                TaxRateObj = tempTaxRate;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"                MainViewModel:FigureTaxBtnCmd {ex.Message}");
            }
        }

        private async Task FigureTaxForOrderBtnCmd()
        {
            //TODO convert the shipping to float
            float.TryParse(ShippingFeeTxt, out var shippingAmountFlt);
            float.TryParse(TotalTxt, out var totalAmountFlt);
            JObject orderInfo = new JObject
                {
                    { "to_country", CountryTxt },
                    { "to_zip", ZipTxt },
                    { "to_state", StateTxt },
                    { "shipping", shippingAmountFlt },
                    { "amount", totalAmountFlt },
                    { "from_zip", "07001"},
                    { "from_state", "NJ"},
                };
            var answer = await this.getData.GetTaxForOrderFromApi(orderInfo);
            var tempTaxCalculations = TaxCalculations.FromJson(answer);
            Calculations = tempTaxCalculations;
        }
    }
}