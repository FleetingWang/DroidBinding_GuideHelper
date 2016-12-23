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
using Android.Util;
using Java.Util;
using static Android.Provider.Settings;
using Android.Annotation;
using Java.Lang;

namespace Sample.Utils
{
    public class DisplayUtils
    {
        public static bool IsTablet(Context context)
        {
            return (context.Resources.Configuration.ScreenLayout & Android.Content.Res.ScreenLayout.SizeMask) >= Android.Content.Res.ScreenLayout.SizeLarge;
        }

        public static bool IsTvbox(Context context)
        {
            return GetDeviceType(context) == 3;
        }

        private static int deviceType = -1;
        public static int GetDeviceType(Context context)
        {
            if (deviceType == -1)
            {
                if (context.PackageManager.HasSystemFeature("android.hardware.telephony"))
                {
                    // Check if android.hardware.touchscreen feature is available.
                    deviceType = 1;
                }
                else if (context.PackageManager.HasSystemFeature("android.hardware.touchscreen"))
                {
                    deviceType = 2;
                }
                else
                {
                    deviceType = 3;
                }
            }

            return deviceType;
        }

        /**
	     * 根据dip值转化成px值
	     * 
	     * @param context
	     * @param dip
	     * @return
	     */
        public static int DipToPix(Context context, int dip)
        {
            int size = (int)TypedValue.ApplyDimension(ComplexUnitType.Dip, dip, context.Resources.DisplayMetrics);
            return size;
        }

        public static float SpToPx(Context context, float sp)
        {
            return TypedValue.ApplyDimension(ComplexUnitType.Sp, sp, context.Resources.DisplayMetrics);
        }

        public static int GetDimenValue(Context context, int dimenId)
        {
            return (int)context.Resources.GetDimension(dimenId);
        }

        public static DisplayMetrics GetDisplayMetrics(Context context)
        {
            // DisplayMetrics dm = new DisplayMetrics();
            // activity.getResources().getDisplayMetrics();
            // activity.getWindowManager().getDefaultDisplay().getMetrics(dm);
            return context.Resources.DisplayMetrics;
        }

        /**
	     * 判断设备是否是模拟器
	     * 
	     * @return
	     */
        public static bool IsEmulator()
        {
            // Java 原始: "sdk".equals(Build.PRODUCT) || "google_sdk".equals(Build.PRODUCT) || "generic".equals(Build.BRAND.toLowerCase(Locale.getDefault()));
            return "sdk".Equals(Build.Product) || "google_sdk".Equals(Build.Product) || "generic".Equals(Build.Brand.ToLower());
        }

        /**
         * 得到设备id
         * 
         * @param context
         * @return
         */
        public static string GetAndroidId(Context context)
        {
            string android_id = Secure.GetString(context.ContentResolver, Secure.AndroidId);
            return android_id;
        }

        public static int GetStatusBarHeight(Context context)
        {
            int result = 0;
            int resourceId = context.Resources.GetIdentifier("status_bar_height", "dimen", "android");
            if (resourceId > 0)
            {
                result = context.Resources.GetDimensionPixelSize(resourceId);
            }
            return result;
        }

        [SuppressWarnings(Value = new[] { "deprecation" })]
        [TargetApi(Value = 11)]
        public static void CopyToClipboard(Context context, string text)
        {
            // if()
            if (Build.VERSION.SdkInt < BuildVersionCodes.Honeycomb)
            {
                Android.Text.ClipboardManager clipboard = (Android.Text.ClipboardManager)context.GetSystemService(Context.ClipboardService);
                clipboard.Text = text;
            }
            else
            {
                Android.Content.ClipboardManager clipboard = (Android.Content.ClipboardManager)context.GetSystemService(Context.ClipboardService);
                clipboard.PrimaryClip = ClipData.NewPlainText(null, text);
            }
        }
    }
}