using Colorful.DemoApp.Contracts;
using Prism.Navigation;
using Reactive.Bindings;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reactive.Linq;

namespace Colorful.DemoApp.ViewModels
{
    public class LogingPageViewModel : ViewModelBase
    {
        ILoginServices _loginServices;

        [Required(ErrorMessage = "The UserName is required.")]
        [RegularExpression("^\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*$", ErrorMessage ="Please enter a valid email")]
        public ReactiveProperty<string> UserName { get; private set; }

        [Required(ErrorMessage = "The Password is required.")]
        public ReactiveProperty<string> Password { get; private set; }

        public ReactiveProperty<string> UserErrorMessage { get; }

        public ReactiveProperty<string> PasswordErrorMessage { get; }

        public ReactiveProperty<string> LoginErrorMessage { get; }

        public ReactiveProperty<bool> IsLoading { get; private set; }
        public ReactiveProperty<bool> CanLogin { get; private set; }
        public AsyncReactiveCommand LoginCommand { get; private set; }

        public LogingPageViewModel(ILoginServices loginServices, INavigationService navigationService)
            : base(navigationService)
        {
            _loginServices = loginServices;
            Title = "Login";
            UserName = new ReactiveProperty<string>("user@domain.com").SetValidateAttribute(() => UserName);
            Password = new ReactiveProperty<string>("2bad4you").SetValidateAttribute(() => Password);
            IsLoading = new ReactiveProperty<bool>(false);
            CanLogin = new ReactiveProperty<bool>(true, ReactivePropertyMode.RaiseLatestValueOnSubscribe);
            LoginErrorMessage = new ReactiveProperty<string>();

            UserErrorMessage = UserName.ObserveErrorChanged
                .Select(x => x?.OfType<string>()?.FirstOrDefault())
                .ToReactiveProperty();

            PasswordErrorMessage = Password.ObserveErrorChanged
                .Select(x => x?.OfType<string>()?.FirstOrDefault())
                .ToReactiveProperty();

            this.CanLogin = new[]
                {
                    this.UserName.ObserveHasErrors,
                    this.Password.ObserveHasErrors
                }
            .CombineLatest(x => !x.Any(y => y))
            .ToReactiveProperty(mode: ReactivePropertyMode.RaiseLatestValueOnSubscribe);

            this.LoginCommand = new AsyncReactiveCommand(CanLogin)
                .WithSubscribe
                (async () => {
                    this.IsLoading.Value = true;
                    var loginResult = await _loginServices.LoginAsync(UserName.Value, Password.Value);
                    this.IsLoading.Value = false;
                    if (loginResult)
                        await this.NavigationService.NavigateAsync("NavigationPage/PalletesPage", useModalNavigation: true);
                    else
                        LoginErrorMessage.Value = "Can't login, please verify yor credentials.";
                });
        }
    }
}
