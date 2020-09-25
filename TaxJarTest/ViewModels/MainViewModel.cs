using System;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Navigation;
using TaxJarTest.Interfaces;

namespace TaxJarTest.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IGetDataCalls getData;

        public DelegateCommand FigureTaxBtn { get; }

        public MainViewModel(INavigationService navigationService, IGetDataCalls getData) : base(navigationService)
        {
            this.getData = getData;

            FigureTaxBtn = new DelegateCommand(async()=> await FigureTaxBtnCmd());
        }

        private async Task FigureTaxBtnCmd()
        {
            var answer = await this.getData.GetDataFromApi();
            Console.WriteLine(answer);
        }
    }
}