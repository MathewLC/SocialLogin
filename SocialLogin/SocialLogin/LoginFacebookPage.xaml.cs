﻿using SocialLogin.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SocialLogin
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginFacebookPage : ContentPage
	{
        //your client id must be put here
        private string ClientId = "******************";
        public LoginFacebookPage ()
		{
			InitializeComponent ();
            string quote = "\"";
            var apiRequest =
                    "https://www.facebook.com/v3.2/dialog/oauth?client_id="
                +   ClientId
                +   "&display=popup&response_type=token&redirect_uri=http://www.facebook.com/connect/login_success.html"+
                "   &auth_type=rerequest"
                +   "&state ={st=state123abc,ds=123456789}";

            var webView = new WebView
            {
                Source = apiRequest,
                HeightRequest = 1
            };

            webView.Navigated += WebViewOnNavigated;

            Content = webView;
        }
        private async void WebViewOnNavigated(object sender, WebNavigatedEventArgs e)
        {

            var accessToken = ExtractAccessTokenFromUrl(e.Url);

            if (accessToken != "")
            {
                var vm = BindingContext as LoginFacebookPageViewModel;

                await vm.SetFacebookUserProfileAsync(accessToken);

                Content = MainStackLayout;
            }
        }

        private string ExtractAccessTokenFromUrl(string url)
        {
            if (url.Contains("access_token") && url.Contains("&expires_in="))
            {
                var at = url.Replace("https://www.facebook.com/connect/login_success.html#access_token=", "");

                
                if (Device.RuntimePlatform == Device.UWP || Device.RuntimePlatform == Device.WPF)
                {
                    at = url.Replace("http://www.facebook.com/connect/login_success.html#access_token=", "");
                }

                var accessToken = at.Remove(at.IndexOf("&expires_in="));

                return accessToken;
            }

            return string.Empty;
        }
    }
}