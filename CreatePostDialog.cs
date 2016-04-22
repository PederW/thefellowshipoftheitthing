
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
using System.Net.Http;
using Newtonsoft.Json;

namespace LogInScreenR
{
    public class CreatePostEventArgs : EventArgs
    {
        public string Name { get; set; }
        public string Message { get; set; }
        

        public CreatePostEventArgs(string name, string message)
        {
            Name = name;
            Message = message;
        }
    }
    
    class CreatePostDialog : DialogFragment
    {
        private Button mButtonCreatePost;
        private EditText txtName;
        private EditText txtMessage;
        private DateTime Time;
        

        public event EventHandler<CreatePostEventArgs> OnCreatePost;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.dialog_post, container, false);
            mButtonCreatePost = view.FindViewById<Button>(Resource.Id.btnCreatePost);
            txtName = view.FindViewById<EditText>(Resource.Id.txtName);
            txtMessage = view.FindViewById<EditText>(Resource.Id.txtMessage);

            mButtonCreatePost.Click += mButtonCreatePost_Click;
            Time = DateTime.Now.ToLocalTime();
            Time = Time.AddHours(2);
            return view;
        }

        void mButtonCreatePost_Click(object sender, EventArgs e)
        {

            PostVoid();


            if (OnCreatePost != null)
            {              
                OnCreatePost.Invoke(this, new CreatePostEventArgs(txtName.Text, txtMessage.Text));
                
                this.Dismiss();
            }

            this.Dismiss();
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            Dialog.Window.RequestFeature(WindowFeatures.NoTitle);
            base.OnActivityCreated(savedInstanceState);
            Dialog.Window.Attributes.WindowAnimations = Resource.Style.dialog_animation;
        }
        async void PostVoid()
        {

            var client = new HttpClient();

            using (var response = await client.PostAsync(
                 "http://130.238.15.193:8080/posts",
                 new StringContent(
                     JsonConvert.SerializeObject(new
                     {
                         //name = txtName.Text här kan vi specifiera användaren med id
                         content = txtMessage.Text,
                         timeMade = Time.ToString()

                     }), Encoding.UTF8, "application/json")))
            {
                response.EnsureSuccessStatusCode();

            }

        }
    }
}