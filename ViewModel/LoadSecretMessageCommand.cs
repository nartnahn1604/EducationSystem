using MVVMEssentials.Commands;
using IT008_UIT.Responses;
using IT008_UIT.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace IT008_UIT.ViewModel
{
    public class LoadSecretMessageCommand : AsyncCommandBase
    {
        private readonly LoginViewModel _loginViewModel;
        private readonly IGetSecretMessageQuery _getSecretMessageQuery;

        public LoadSecretMessageCommand(LoginViewModel homeViewModel, IGetSecretMessageQuery getSecretMessageQuery)
        {
            _loginViewModel = homeViewModel;
            _getSecretMessageQuery = getSecretMessageQuery;
        }

        protected override async Task ExecuteAsync(object parameter)
        {
            try
            {
                SecretMessageResponse secretMessageResponse = await _getSecretMessageQuery.Execute();

                _loginViewModel.SecretMessage = secretMessageResponse.Message;
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to load secret message. Please try again later.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
