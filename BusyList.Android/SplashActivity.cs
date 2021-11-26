
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace BusyList.Droid
{
    [Activity(Theme = "@style/MainTheme.Splash", MainLauncher = true, NoHistory = true, Label = "BusyList", Icon = "@mipmap/ic_launcher")]
    public class SplashActivity : AppCompatActivity
    {
        // Launches the startup task
        protected override void OnResume()
        {
            base.OnResume();
            Task startupWork = new Task(() => { OnStartup(); });
            startupWork.Start();
        }

        async void OnStartup()
        {
            //TODO: Extra startup items go here

            //End startup routine by opening main page
            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
        }
    }
}
