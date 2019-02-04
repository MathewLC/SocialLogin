using SocialLogin.Model;
using SocialLogin.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SocialLogin.ViewModel
{
    public class LoginFacebookPageViewModel:INotifyPropertyChanged
    {
        private FacebookProfile _facebookProfile;

        public FacebookProfile FacebookProfile
        {
            get { return _facebookProfile; }
            set
            {
                _facebookProfile = value;
                OnPropertyChanged();
            }
        }

        public async Task SetFacebookUserProfileAsync(string accessToken)
        {
            var facebookServices = new FacebookService();

            FacebookProfile = await facebookServices.GetFacebookProfileAsync(accessToken);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
