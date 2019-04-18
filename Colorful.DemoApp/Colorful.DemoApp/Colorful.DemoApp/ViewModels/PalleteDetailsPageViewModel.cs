using Colorful.DemoApp.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Colorful.DemoApp.ViewModels
{
	public class PalleteDetailsPageViewModel : ViewModelBase, INavigationAware
    {
        public ReactiveProperty<PalleteItem> SelectedPallete { get; private set; }

        public PalleteDetailsPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {            
            Title = "Pallete Details";
            this.SelectedPallete = new ReactiveProperty<PalleteItem>();
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            this.SelectedPallete.Value = parameters.GetValue<PalleteItem>("selectedPallete");
        }
    }
}
