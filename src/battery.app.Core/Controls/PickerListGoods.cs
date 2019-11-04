

using Xamarin.Forms;

namespace battery.app.Controls
{
	/// <summary>
	/// Представляет <see cref="Picker"/> для выбора товаров.
	/// </summary>
	public class PickerListGoods : Picker
    {
		/// <summary>
		/// Привязываемое свойство для картинки.
		/// </summary>
        public static readonly BindableProperty ImageProperty =
            BindableProperty.Create(nameof(Image), typeof(string), typeof(PickerListGoods), string.Empty);

		/// <summary>
		/// Возвращает или устанавливает название картинки.
		/// </summary>
        public string Image
        {
            get => (string)GetValue(ImageProperty);
			set => SetValue(ImageProperty, value);
		}
    }
}
