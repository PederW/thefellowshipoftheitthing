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

namespace LogInScreenR
{
    [Activity(Label = "dialog_AddActivity")]
    public class dialog_AddActivity : Activity
    {
        private List<int> checkedIds = new List<int>();
        
        

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.dialog_add_activity);
            var checkboxgym = FindViewById<CheckBox>(Resource.Id.checkBoxGym);
            var checkboxrun = FindViewById<CheckBox>(Resource.Id.checkBoxRun);
            var checkboxtennis = FindViewById<CheckBox>(Resource.Id.checkBoxTennis);
            var checkboxfootball = FindViewById<CheckBox>(Resource.Id.checkBoxFootBall);
            var checkboxwalking = FindViewById<CheckBox>(Resource.Id.checkBoxWalking);
            var checkboxdancing = FindViewById<CheckBox>(Resource.Id.checkBoxDancing);
            var checkboxcycling = FindViewById<CheckBox>(Resource.Id.checkBoxCycling);
            var checkboxgolfing = FindViewById<CheckBox>(Resource.Id.checkBoxGolfing);
            var checkboxyogaing = FindViewById<CheckBox>(Resource.Id.checkBoxYogaing);
            var checkboxvolleyballing= FindViewById<CheckBox>(Resource.Id.checkBoxVolleyBalling);
            var checkboxskateboarding = FindViewById<CheckBox>(Resource.Id.checkBoxSkateBoarding);
            var checkboxbasketballing = FindViewById<CheckBox>(Resource.Id.checkBoxBasketballing);
            checkboxgym.CheckedChange += Checked_Changed;
            checkboxrun.CheckedChange += Checked_Changed;
            checkboxtennis.CheckedChange += Checked_Changed;
            checkboxfootball.CheckedChange += Checked_Changed;
            checkboxwalking.CheckedChange += Checked_Changed;
            checkboxdancing.CheckedChange += Checked_Changed;
            checkboxcycling.CheckedChange += Checked_Changed;
            checkboxgolfing.CheckedChange += Checked_Changed;
            checkboxyogaing.CheckedChange += Checked_Changed;
            checkboxvolleyballing.CheckedChange += Checked_Changed;
            checkboxskateboarding.CheckedChange += Checked_Changed;
            checkboxbasketballing.CheckedChange += Checked_Changed;
            //Every checkbox use this event handler
           

            FindViewById<Button>(Resource.Id.submitActivities).Click += delegate
            {
                var intent = new Intent(this, typeof(ProfileActivity));
                intent.PutExtra("CheckedIds", checkedIds.ToArray()); //Add the ids of the checked boxes to the intent

                StartActivity(intent);
                this.OverridePendingTransition(Resource.Animation.slide_in_top_two, Resource.Animation.slide_out_bottom_two);
            };

        }
        void Checked_Changed(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            var checkBox = sender as CheckBox; //get the checkbox that fires the event to get the mapping between checkbox and phrase
            
            if (checkBox == null) //if it was not a checkbox that fired the event do nothing
                return;

            if (e.IsChecked)
            {
             
                    checkedIds.Add(checkBox.Id); //you need some identifier to know the mapping between phrase and checkbox.
            }
            else
                checkedIds.Remove(checkBox.Id); //remove id if checkbox got unchecked
        }
    }


}


