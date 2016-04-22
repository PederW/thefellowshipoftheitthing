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
    public class Post
    {
        public string Name { get; set; }
        public string Message { get; set; }
        public byte[] Image { get; set; }
     
    }
}