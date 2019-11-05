using Android.Support.Design.BottomNavigation;
using Android.Support.Design.Widget;
using Android.Views;
using battery.app.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ResolutionGroupName("itmit")]
[assembly: ExportEffect(typeof(NoShiftEffectRenderer), "NoShiftEffect")]
namespace battery.app.Droid
{
    public class NoShiftEffectRenderer : PlatformEffect
    {
        protected override void OnAttached()
        {
            if (!(Container.GetChildAt(0) is ViewGroup layout))
            {
                return;
            }

            if (!(layout.GetChildAt(1) is BottomNavigationView bottomNavigationView))
            {
                return;
            }

            // This is what we set to adjust if the shifting happens
            bottomNavigationView.LabelVisibilityMode = LabelVisibilityMode.LabelVisibilityUnlabeled;
        }

        protected override void OnDetached()
        {
        }
    }
}