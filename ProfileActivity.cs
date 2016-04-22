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
    [Activity(Label = "ProfileActivity", Theme = "@style/CustomActionBarTheme")]
    public class ProfileActivity : Activity
    {
        private ImageView mGoToFeedView;
        private ImageView mGoToChatView;
        private Button mbtnAddActivity;
        List<int> checkedIds;
        private List<KeyValuePair<String, Boolean>> Aktiviteter = new List<KeyValuePair<String, Boolean>>();
      
        

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            ActionBar.SetCustomView(Resource.Layout.action_bar);
            ActionBar.SetDisplayShowCustomEnabled(true);
            SetContentView(Resource.Layout.ProfileMain);
            mGoToFeedView = FindViewById<ImageView>(Resource.Id.goToFeedView);
            mGoToFeedView.Click += (object sender, EventArgs args) =>
            {
                StartActivity(typeof(FeedActivity));
            };

            mbtnAddActivity = FindViewById<Button>(Resource.Id.btnAddActivity);
            mbtnAddActivity.Click += (object sender, EventArgs args) =>
            {
                StartActivity(typeof(dialog_AddActivity));
                this.OverridePendingTransition(Resource.Animation.slide_in_top, Resource.Animation.slide_out_bottom);

            };
            mGoToChatView = FindViewById<ImageView>(Resource.Id.goToChatView);
            mGoToChatView.Click += (object sender, EventArgs args) =>
            {
                StartActivity(typeof(ChatActivity));
            };
            checkedIds = Intent.GetIntArrayExtra("CheckedIds").ToList();
            var layout = FindViewById<LinearLayout>(Resource.Id.activitylayoutbar);
            if (checkedIds == null)
            {
                Toast.MakeText(this, "No ids provided", ToastLength.Long);
                Finish();
            }

            var checkbox = FindViewById<CheckBox>(Resource.Id.checkBoxGym);

            foreach (var checkBoxId in checkedIds)
            {

                if (checkBoxId == 2131099779)
                {
                    ImageView gym = new ImageView(this);
                    gym.SetImageResource(Resource.Drawable.Gym);
                    layout.AddView(gym);
                    Aktiviteter.Add(new KeyValuePair<string, Boolean>("Gym", true));
                }
                else {
                    Aktiviteter.Add(new KeyValuePair<string, Boolean>("Gym", false));
                }
          
               
                
                if (checkBoxId == 2131099780)
                {
                    ImageView gym = new ImageView(this);
                    gym.SetImageResource(Resource.Drawable.Running);
                    layout.AddView(gym);
                    Aktiviteter.Add(new KeyValuePair<string, Boolean>("Löpning", true));

                }
                else
                {
                    Aktiviteter.Add(new KeyValuePair<string, Boolean>("Löpning", false));
                }
                if (checkBoxId == 2131099781)
                {
                    ImageView gym = new ImageView(this);
                    gym.SetImageResource(Resource.Drawable.Tennis);
                    layout.AddView(gym);
                }
                if (checkBoxId == 2131099782)
                {
                    ImageView gym = new ImageView(this);
                    gym.SetImageResource(Resource.Drawable.Football);
                    layout.AddView(gym);
                }
                if (checkBoxId == 2131099783)
                {
                    ImageView gym = new ImageView(this);
                    gym.SetImageResource(Resource.Drawable.Walking);
                    layout.AddView(gym);

                }
                if (checkBoxId == 2131099784)
                {
                    ImageView gym = new ImageView(this);
                    gym.SetImageResource(Resource.Drawable.Dance);
                    layout.AddView(gym);
                }
                if (checkBoxId == 2131099785)
                {
                    ImageView gym = new ImageView(this);
                    gym.SetImageResource(Resource.Drawable.Cycling);
                    layout.AddView(gym);
                }
                if (checkBoxId == 2131099786)
                {
                    ImageView gym = new ImageView(this);
                    gym.SetImageResource(Resource.Drawable.Golf);
                    layout.AddView(gym);

                }
                if (checkBoxId == 2131099787)
                {
                    ImageView gym = new ImageView(this);
                    gym.SetImageResource(Resource.Drawable.Yoga);
                    layout.AddView(gym);
                }
                if (checkBoxId == 2131099788)
                {
                    ImageView gym = new ImageView(this);
                    gym.SetImageResource(Resource.Drawable.Volleyball);
                    layout.AddView(gym);
                }
                if (checkBoxId == 2131099789)
                {
                    ImageView gym = new ImageView(this);
                    gym.SetImageResource(Resource.Drawable.Skateboarding);
                    layout.AddView(gym);

                }
                if (checkBoxId == 2131099790)
                {
                    ImageView gym = new ImageView(this);
                    gym.SetImageResource(Resource.Drawable.Basketball);
                    layout.AddView(gym);
                }
                //SendActivites();
            }

        }
        async void SendActivites()
        {

            var client = new HttpClient();

            using (var response = await client.PostAsync(
                 "http://130.238.15.193:8080/accounts",
                 new StringContent(
                     JsonConvert.SerializeObject(new
                     {
                         activities = Aktiviteter
                     }), Encoding.UTF8, "application/json")))
            {
                response.EnsureSuccessStatusCode();

            }

        }


    }

    }

   