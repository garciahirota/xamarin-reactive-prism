using Colorful.DemoApp.Contracts;
using Colorful.DemoApp.Models;
using Prism.Navigation;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

namespace Colorful.DemoApp.ViewModels
{
    public class PalletesPageViewModel : ViewModelBase
    {
        IColorPalleteServices _colorPalleteServices;

        public ReactiveProperty<List<PalleteItem>> Palletes { get; private set; }
        public ReactiveProperty<PalleteItem> SelectedPallete { get; private set; }
        public ReactiveProperty<bool> IsLoading { get; private set; }

        public AsyncReactiveCommand GetPalletesCommand { get; private set; }
    
        public ReactiveCommand NavigateChildCommand { get; private set; }

        public ReactiveProperty<string> SearchWord { get; private set; }

        public PalletesPageViewModel(IColorPalleteServices colorPalleteServices, INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Pallete Details";
            _colorPalleteServices = colorPalleteServices;

            SearchWord = new ReactiveProperty<string>(string.Empty);
            IsLoading = new ReactiveProperty<bool>(false);
            Palletes = new ReactiveProperty<List<PalleteItem>>();
            SelectedPallete = new ReactiveProperty<PalleteItem>();

            this.GetPalletesCommand = new AsyncReactiveCommand()
                .WithSubscribe
                (async () => {
                    this.IsLoading.Value = true;
                    var palletesFounded = await _colorPalleteServices.GetPalletsAsync(SearchWord.Value);
                    this.Palletes.Value = palletesFounded;
                    this.IsLoading.Value = false;
                });

            this.NavigateChildCommand = this.SelectedPallete.Select(v => v != null).ToReactiveCommand();
            this.NavigateChildCommand.Subscribe(async () => {
                if (SelectedPallete.Value != null)
                {
                    var navigationParams = new NavigationParameters
                    {
                        { "selectedPallete", SelectedPallete.Value }
                    };
                    await this.NavigationService.NavigateAsync("NavigationPage/PalletesPage/PalleteDetailsPage", navigationParams, useModalNavigation: true);
                }
            }); 
            this.SelectedPallete.Subscribe(x => NavigateChildCommand.Execute());

            this.GetPalletesCommand.Execute();
        }
    }
}
