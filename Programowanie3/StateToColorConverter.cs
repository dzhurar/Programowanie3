using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Programowanie3
{
    public class StateToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is CellState state)
            {
                string mode = parameter as string;

                switch (state)
                {
                    case CellState.Hit:
                        return Brushes.Red;
                    case CellState.Miss:
                        return Brushes.White;
                    case CellState.Ship:
                        return mode == "Enemy" ? Brushes.LightBlue : Brushes.DimGray;
                    case CellState.Empty:
                    default:
                        return Brushes.LightBlue;
                }
            }

            return Brushes.LightBlue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
