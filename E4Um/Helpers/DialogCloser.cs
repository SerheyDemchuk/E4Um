using System.Windows;

namespace E4Um.Helpers
{
    class DialogCloser
    {
       public static readonly DependencyProperty DialogResultProperty =
       DependencyProperty.RegisterAttached(
       "DialogResult",
       typeof(bool?),
       typeof(DialogCloser),
       new PropertyMetadata(DialogResultChanged));

        private static void DialogResultChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var window = d as Window;
            if (window != null)
                window.Hide();
        }
        public static void SetDialogResult(Window target, bool? value)
        {
            target.SetValue(DialogResultProperty, value);
        }
    }
}
