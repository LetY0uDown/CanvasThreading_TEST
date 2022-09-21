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

        Thread thread = new(() => DrawGreyGradientFromLeftCorner(ColorShadeType.Green));
        thread.SetApartmentState(ApartmentState.STA);

        thread.Start();
    }

    const int CELL_SIZE = 50;
    const int GRID_SIZE = 16;

    const int DELAY = 10;

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

    public void DrawGreyGradientFromTwoCorners(ColorShadeType colorShade)
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
                                  ColorTool.GetShadedColor(colorShade, shadeIndex));
                });

                Thread.Sleep(DELAY);
            }
        }
    }

    public void DrawCornerShapedGreyGradient(ColorShadeType colorShade)
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
                                  ColorTool.GetShadedColor(colorShade, shadeIndex));
                });

                Thread.Sleep(DELAY);
            }
        }
    }

    public void DrawGreyGradientFromLeftCorner(ColorShadeType colorShade)
    {
        for (int x = 0; x < GRID_SIZE; x++)
        {
            for (int y = 0; y < GRID_SIZE; y++)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    DrawRectangle(x * CELL_SIZE, y * CELL_SIZE,
                                  ColorTool.GetMultipliedColor(x + y, 0.5, 0.5, 1));
                });

                Thread.Sleep(DELAY);
            }
        }        
    }
}