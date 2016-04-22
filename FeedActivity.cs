using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V7.Widget;
using System.Collections.Generic;
using Android.Content.PM;
using Android.Support.V4.View;
using System.IO;
using Android.Graphics;
using System.Net;
using System.Collections.Specialized;
using System.Text;


namespace LogInScreenR
{
    [Activity(Label = "FeedActivity", Theme="@style/CustomActionBarTheme")]
    public class FeedActivity : Activity
    {

        
        private ImageView mGoToProfileView;
        private ListView mListView;
        private BaseAdapter<Post> mAdapter;
        private List<Post> mPosts;
        private ImageView mSelectedPic;
        Button btnPost;
     

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            ActionBar.SetCustomView(Resource.Layout.action_bar);
            ActionBar.SetDisplayShowCustomEnabled(true);
            SetContentView(Resource.Layout.FeedMain);

            mGoToProfileView = FindViewById<ImageView>(Resource.Id.goToProfileView);
            mGoToProfileView.Click += (object sender, EventArgs args) =>
            {
                StartActivity(typeof(FeedActivity));
            };

            mListView = FindViewById<ListView>(Resource.Id.listView);
            mPosts = new List<Post>();

            Action<ImageView> action = PicSelected;

            mAdapter = new PostListAdapter(this, Resource.Layout.row_post, mPosts, action);
            mListView.Adapter = mAdapter;

            btnPost = FindViewById<Button>(Resource.Id.PostBtn);
            btnPost.Click += btnPost_Click;
            //gotoprofile
            mGoToProfileView = FindViewById<ImageView>(Resource.Id.goToProfileView);
            mGoToProfileView.Click += (object sender, EventArgs args) =>
            {
                StartActivity(typeof(dialog_AddActivity));
            };

        }

        void btnPost_Click(object sender, EventArgs e)
        {
            CreatePostDialog dialog = new CreatePostDialog();
            FragmentTransaction transaction = FragmentManager.BeginTransaction();

            //Subscribe to event
            dialog.OnCreatePost += dialog_OnCreatePost;
            dialog.Show(transaction, "create post");

        }


        private void PicSelected(ImageView selectedPic)
        {
            mSelectedPic = selectedPic;
            Intent intent = new Intent();
            intent.SetType("image/*");
            intent.SetAction(Intent.ActionGetContent);
            this.StartActivityForResult(Intent.CreateChooser(intent, "Selecte a Photo"), 0);
        }
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (resultCode == Result.Ok)
            {
                Stream stream = ContentResolver.OpenInputStream(data.Data);
                mSelectedPic.SetImageBitmap(DecodeBitmapFromStream(data.Data, 150, 150));
            }
        }

        private Bitmap DecodeBitmapFromStream(Android.Net.Uri data, int requestedWidth, int requestedHeight)
        {
            //Decode with InJustDecodeBounds = true to check dimensions
            Stream stream = ContentResolver.OpenInputStream(data);
            BitmapFactory.Options options = new BitmapFactory.Options();
            options.InJustDecodeBounds = true;
            BitmapFactory.DecodeStream(stream);

            //Calculate InSamplesize
            options.InSampleSize = CalculateInSampleSize(options, requestedWidth, requestedHeight);

            //Decode bitmap with InSampleSize set
            stream = ContentResolver.OpenInputStream(data); //Must read again
            options.InJustDecodeBounds = false;
            Bitmap bitmap = BitmapFactory.DecodeStream(stream, null, options);
            return bitmap;
        }
        private int CalculateInSampleSize(BitmapFactory.Options options, int requestedWidth, int requestedHeight)
        {
            //Raw height and widht of image
            int height = options.OutHeight;
            int width = options.OutWidth;
            int inSampleSize = 1;

            if (height > requestedHeight || width > requestedWidth)
            {
                //the image is bigger than we want it to be
                int halfHeight = height / 2;
                int halfWidth = width / 2;

                while ((halfHeight / inSampleSize) > requestedHeight && (halfWidth / inSampleSize) > requestedWidth)
                {
                    inSampleSize *= 2;
                }

            }

            return inSampleSize;
        }


        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.PostBtn:

                    CreatePostDialog dialog = new CreatePostDialog();
                    FragmentTransaction transaction = FragmentManager.BeginTransaction();

                    //Subscribe to event
                    dialog.OnCreatePost += dialog_OnCreatePost;
                    dialog.Show(transaction, "create post");
                    return true;

                default:
                    return base.OnOptionsItemSelected(item);
            }

        }

        void dialog_OnCreatePost(object sender, CreatePostEventArgs e)
        {
            mPosts.Add(new Post() { Name = e.Name, Message = e.Message });
            mAdapter.NotifyDataSetChanged();
        }
    }



}


