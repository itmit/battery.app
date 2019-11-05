using Android.Content;
using battery.app.Core.Controls;
using battery.app.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Color = Android.Graphics.Color;

[assembly: ExportRenderer(typeof(AuthorizationEntry), typeof(AuthorizationEntryRenderer))]

namespace battery.app.Droid.Renderers
{
    public class AuthorizationEntryRenderer : EntryRenderer
    {
        #region .ctor
        public AuthorizationEntryRenderer(Context context):base(context)
        {
        }
        #endregion

        #region Overrided
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            Control?.SetBackgroundColor(Color.Transparent);
        }
        #endregion
    }
}