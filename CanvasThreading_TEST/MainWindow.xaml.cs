using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace CanvasThreading_TEST;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        Width = Height = CELL_SIZE * GRID_SIZE;

        Thread thread = new(() => DrawGreyGradientFromTwoCorners());
        thread.SetApartmentState(ApartmentState.STA);

        thread.Start();
    }

    const int CELL_SIZE = 50;
    const int GRID_SIZE = 16;

    static readonly Random random = new();

    static Color RandomColor => Color.FromRgb((byte)random.Next(0, 256), (byte)random.Next(0, 256), (byte)random.Next(0, 256));

    static SolidColorBrush GetGreyShadeColor(int shadeIndex)
    {
        shadeIndex *= 8;

        byte shade = (byte)shadeIndex;

        return new(Color.FromRgb(shade, shade, shade));
    }

    private void DrawRectangle(int posX, int posY, SolidColorBrush color)
    {
        Rectangle rectangle = new()
        {
            Width = CELL_SIZE,
            Height = CELL_SIZE,
            Fill = color
        };

        Field.Children.Add(rectangle);

        Canvas.SetLeft(rectangle, posX);
        Canvas.SetTop(rectangle, posY);
    }

    public void DrawGreyGradientFromTwoCorners()
    {
        for (int x = 0; x < GRID_SIZE; x++)
        {
            for (int y = 0; y < GRID_SIZE; y++)
            {
                var index = x + 1 + y;
                var shadeIndex = index < GRID_SIZE ? index
                                                   : GRID_SIZE - (index - GRID_SIZE);

                Application.Current.Dispatcher.Invoke(() =>
                {
                    DrawRectangle(x * CELL_SIZE, y * CELL_SIZE,
                                  GetGreyShadeColor(shadeIndex));
                });

                Thread.Sleep(10);
            }
        }
    }

    public void DrawCornerShapedGreyGradient()
    {
        for (int x = 0; x < GRID_SIZE; x++)
        {
            for (int y = 0; y < GRID_SIZE; y++)
            {
                var shadeIndex = x + y < GRID_SIZE ? GRID_SIZE + 1 - (y + (GRID_SIZE - y) - x)
                                                   : x + (GRID_SIZE - x) - y;

                Application.Current.Dispatcher.Invoke(() =>
                {
                    DrawRectangle(x * CELL_SIZE, y * CELL_SIZE,
                                  GetGreyShadeColor(shadeIndex));
                });

                Thread.Sleep(10);
            }
        }
    }

    public void DrawGreyGradientFromLeftCorner()
    {
        for (int x = 0; x < GRID_SIZE; x++)
        {
            for (int y = 0; y < GRID_SIZE; y++)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    DrawRectangle(x * CELL_SIZE, y * CELL_SIZE,
                                  GetGreyShadeColor(x + y));
                });

                Thread.Sleep(10);
            }
        }        
    }
}