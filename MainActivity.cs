using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Net;
using Newtonsoft.Json;
using System.Collections.Specialized;
using System.Net.Http;
using System.Text;
using Android.Content.PM;
using Android.Views.Animations;

namespace LogInScreenR
{
    [Activity(Label = "LogInScreenR", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        
        private TextView mRegisterText;
        private Button mLogIn;
        private EditText mtxtEmail;
        private EditText mtxtPassword;


        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            mtxtEmail = FindViewById<EditText>(Resource.Id.txtEmailLogIn);
            mtxtPassword = FindViewById<EditText>(Resource.Id.txtPassWordLogIn);

            mRegisterText = FindViewById<TextView>(Resource.Id.registerText);
            mRegisterText.Click += (object sender, EventArgs args) =>
            {
                FragmentTransaction transaction = FragmentManager.BeginTransaction();
                dialog_SignUp signUpDialog = new dialog_SignUp();
                signUpDialog.Show(transaction, "signup fragment");
                signUpDialog.mOnSignUpComplete += signUpDialog_mOnSignUpComplete;
            };

            //här loggar vi in 
            mLogIn = FindViewById<Button>(Resource.Id.buttonSignIn);
            mLogIn.Click += (object sender, EventArgs args) =>
            {
                //Console.WriteLine(mtxtEmail.ToString());
                //Console.WriteLine(mtxtPassword.Text);
                //LogInVoid();
                 StartActivity(typeof(FeedActivity));

            };
        }



        async void LogInVoid()
        {

            var client = new HttpClient();

            using (var response = await client.PostAsync(
                 "http://130.238.15.193:8080/accounts",
                 new StringContent(
                     JsonConvert.SerializeObject(new
                     {
                         email = mtxtEmail.Text,
                         password = mtxtPassword.Text
                     }), Encoding.UTF8, "application/json")))
            {
                response.EnsureSuccessStatusCode();

            }

        }





        async void signUpDialog_mOnSignUpComplete(object sender, OnSignUpEventArgs e)
        {
            //client = new WebClient();
            //uri = new Uri("http://130.238.92.26:8080/accounts");
            //NameValueCollection parameters = new NameValueCollection();
            //parameters.Add("firstName", e.FirstName);
            //parameters.Add("lastName", e.LastName);
            //client.UploadValuesCompleted += client_UploadValuesCompleted;
            // client.UploadValuesAsync(uri, parameters);

            var client = new HttpClient();

            using (var response = await client.PostAsync(
                 "http://130.238.251.241:8080/accounts",
                 new StringContent(
                     JsonConvert.SerializeObject(new
                     {
                         firstName = e.FirstName,
                         lastName = e.LastName,
                         email = e.Email,
                         password = e.PassWord
                        
                     }), Encoding.UTF8, "application/json")))
            {
                response.EnsureSuccessStatusCode();

            }
        }





    }
}
