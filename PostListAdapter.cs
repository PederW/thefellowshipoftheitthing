
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
using Android.Graphics;

namespace LogInScreenR
{
    class PostListAdapter : BaseAdapter<Post>
    {
        private Context mContext;
        private int mLayout;
        private List<Post> mPosts;
        private Action<ImageView> mActionPicSelected;

        public PostListAdapter(Context context, int layout, List<Post> posts, Action<ImageView> picSelected)
        {
            mContext = context;
            mLayout = layout;
            mPosts = posts;
            mActionPicSelected = picSelected;
        }

        public override Post this[int position]
        {
            get { return mPosts[position]; }
        }

        public override int Count
        {
            get { return mPosts.Count; }
        }

        public override long GetItemId(int position)
        {
            return position;


        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View row = convertView;

            if (row == null)
            {
                row = LayoutInflater.From(mContext).Inflate(mLayout, parent, false);
            }

            row.FindViewById<TextView>(Resource.Id.txtName).Text = mPosts[position].Name;
            row.FindViewById<TextView>(Resource.Id.txtMessage).Text = mPosts[position].Message;

            ImageView pic = row.FindViewById<ImageView>(Resource.Id.imgPic);

            if (mPosts[position].Image != null)
            {
                pic.SetImageBitmap(BitmapFactory.DecodeByteArray(mPosts[position].Image, 0, mPosts[position].Image.Length));
            }

            pic.Click -= pic_Click;
            pic.Click += pic_Click;
            return row;
        }

        void pic_Click(object sender, EventArgs e)
        {
            //Picture has been clicked
            mActionPicSelected.Invoke((ImageView)sender);
        }
    }
}