using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace battery.app.Core.Design
{
    public class CustomButton : Frame
    {
        public CustomButton()
        {
            var tapped = new TapGestureRecognizer();
            tapped.Tapped += Tapped_Pressed;
            GestureRecognizers.Add(tapped);
        }

        private async void Tapped_Pressed(object sender, EventArgs e)
        {
            Opacity = 0.5;
            Opacity = await GetOpacity();
        }

        private async Task<double> GetOpacity()
        {
            await Task.Delay(100);
            return 1;
        }
    }
}
