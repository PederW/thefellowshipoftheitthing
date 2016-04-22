using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Views.Animations;

namespace LogInScreenR
{
    public class OnSignUpEventArgs : EventArgs
    {
        private string mFirstName;
        private string mLastName;
        private string mEmail;
        private string mPassWord;

        public string FirstName
        {
            get { return mFirstName; }
            set { mFirstName = value; }
        }
        public string LastName
        {

            get { return mLastName; }
            set { mLastName = value; }
        }
        public string Email
        {

            get { return mEmail; }
            set { mEmail = value; }
        }
        public string PassWord
        {

            get { return mPassWord; }
            set { mPassWord = value; }
        }


        public OnSignUpEventArgs(string firstname, string lastname, string email, string password) : base()
        {
            FirstName = firstname;
            LastName = lastname;
            Email = email;
            PassWord = password;

        }
    }

    class dialog_SignUp : DialogFragment
    {
        private EditText mtxtFirstName;
        private EditText mtxtLastName;
        private EditText mtxtEmail;
        private EditText mtxtPassword;
        private EditText mtxtRepeatPassword;
        private Button mbtnDialogEmail;
        public event EventHandler<OnSignUpEventArgs> mOnSignUpComplete;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.dialog_sign_up, container, false);
            mtxtFirstName = view.FindViewById<EditText>(Resource.Id.txtFirstName);
            mtxtLastName = view.FindViewById<EditText>(Resource.Id.txtLastName);
            mtxtEmail = view.FindViewById<EditText>(Resource.Id.txtEmail);
            mtxtPassword = view.FindViewById<EditText>(Resource.Id.txtPassword);
            mtxtRepeatPassword = view.FindViewById<EditText>(Resource.Id.txtPasswordAgain);
            mbtnDialogEmail = view.FindViewById<Button>(Resource.Id.btnDialogEmail);
            mbtnDialogEmail.Click += mbtnDialogEmail_Click;
            

            return view;
        }
        void mbtnDialogEmail_Click(object sender, EventArgs e)
        {
            //user signed the registerbutton
            if (mtxtPassword.Text != mtxtRepeatPassword.Text)
            {
                var rotateAboutCenterAnimation = AnimationUtils.LoadAnimation(this.Activity, Resource.Animation.shake);
                mtxtPassword.StartAnimation(rotateAboutCenterAnimation);
                mtxtRepeatPassword.StartAnimation(rotateAboutCenterAnimation);
                mtxtPassword.Text = "";
                mtxtRepeatPassword.Text = "";
            }

            else
            {
                mOnSignUpComplete.Invoke(this, new OnSignUpEventArgs(mtxtFirstName.Text, mtxtLastName.Text, mtxtEmail.Text, mtxtPassword.Text));
                this.Dismiss();
            }


        } 
        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            Dialog.Window.RequestFeature(WindowFeatures.NoTitle); 
            base.OnActivityCreated(savedInstanceState);
            Dialog.Window.Attributes.WindowAnimations = Resource.Style.dialog_animation;
        }
    }
}