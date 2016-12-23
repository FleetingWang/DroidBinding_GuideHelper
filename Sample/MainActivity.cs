using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Com.Shizhefei.Guide;
using static Com.Shizhefei.Guide.GuideHelper;
using Sample.Utils;
using static Android.Views.View;

namespace Sample
{
    [Activity(
        Label = "@string/app_name", 
        MainLauncher = true, 
        Theme = "@style/AppTheme",
        Icon = "@drawable/icon")]
    public class MainActivity : AppCompatActivity
    {
        private View iconView;
        private View citysView;
        private View infoLayout;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            iconView = FindViewById(Resource.Id.icon);
            citysView = FindViewById(Resource.Id.citys);
            infoLayout = FindViewById(Resource.Id.infoLayout);

            View button = FindViewById<Button>(Resource.Id.button1);
            button.Click += (sender, e) => {
                GuideHelper guideHelper = new GuideHelper(this);

                View test = guideHelper.Inflate(Resource.Layout.custom_view_show);
                guideHelper.AddPage(new TipData(test, (int)GravityFlags.Center));

                TipData tipData1 = new TipData(Resource.Drawable.tip1, (int)(GravityFlags.Right | GravityFlags.Bottom), iconView);
                tipData1.SetLocation(0, -DisplayUtils.DipToPix(ApplicationContext, 50));
                guideHelper.AddPage(tipData1);

                TipData tipData2 = new TipData(Resource.Drawable.tip2, citysView);
                guideHelper.AddPage(tipData2);

                TipData tipData3 = new TipData(Resource.Drawable.tip3, infoLayout);
                TipData tipData4 = new TipData(Resource.Drawable.next, (int)(GravityFlags.Bottom | GravityFlags.CenterHorizontal));
                tipData4.SetLocation(0, -DisplayUtils.DipToPix(ApplicationContext, 100));
                tipData4.SetOnClickListener(new tipData4OnClickListener(guideHelper));
                guideHelper.AddPage(false, tipData3, tipData4);

                guideHelper.AddPage(tipData1, tipData2, tipData3);

                //add custom view
                View testView = guideHelper.Inflate(Resource.Layout.custom_view_with_close);
                testView.FindViewById<ImageButton>(Resource.Id.guide_close).Click += (sender2, e2) => {
                    guideHelper.Dismiss();
                };
                TipData tipDataCustom = new TipData(testView, (int)GravityFlags.Center);
                guideHelper.AddPage(false, tipDataCustom);

                guideHelper.Show(false);
            };
        }

        private class tipData4OnClickListener : Java.Lang.Object, IOnClickListener
        {
            private GuideHelper guideHelper;
            public tipData4OnClickListener(GuideHelper guideHelper)
            {
                this.guideHelper = guideHelper;
            }

            public void OnClick(View v)
            {
                guideHelper.NextPage();
            }
        }
    }
}

