using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Ink;
using Xceed.Wpf.Toolkit;
using System.Drawing;
//using MeetingBoard.Model;
using System.Diagnostics;

namespace MeetingBoard.View
{
    /// <summary>
    /// Interaction logic for ToolBox.xaml
    /// </summary>
    public partial class ToolBox : Window
    {
        MeetingBoardMain parent;
        DrawingAttributes _penSettings = new DrawingAttributes();
        DrawingAttributes _highlighterSettings = new DrawingAttributes();
        private InkCanvas _activeCanvas;

        public InkCanvas ActiveCanvas
        {
            get
            {
                return _activeCanvas;
            }
            set
            {
                _activeCanvas = value;
                Debug.WriteLine("Active canvas: "+value.Name);
            }
        }

        public ToolBox(MeetingBoardMain w)
        {
            InitializeComponent();
            parent = w;
            this.Top = 50;
            this.Left = 50;
            _activeCanvas = w.GoalsCanvas;//CHECK IT: passing first canvas as default when the form is created
            _initDrawingAttributes();
        }

        private void _initDrawingAttributes()
        {
            _penSettings = _activeCanvas.DefaultDrawingAttributes;
            _highlighterSettings.IsHighlighter = true;
            _highlighterSettings.Height = 10;
            _highlighterSettings.Width = 10;
            _highlighterSettings.Color = Colors.Yellow; 
        }

        private void cmdErase_Click(object sender, RoutedEventArgs e)
        {
            _activeCanvas.EditingMode = InkCanvasEditingMode.EraseByPoint;
            Debug.WriteLine("Active canvas is:" + _activeCanvas.Name);
        }

        private void cmdDraw_Click(object sender, RoutedEventArgs e)
        {
            _activeCanvas.EditingMode = InkCanvasEditingMode.Ink;
            _activeCanvas.DefaultDrawingAttributes = _penSettings;
        }

        private void cmdEraseStroke_Click(object sender, RoutedEventArgs e)
        {
            _activeCanvas.EditingMode = InkCanvasEditingMode.EraseByStroke;
        }

        private void cmdHighlighter_Click(object sender, RoutedEventArgs e)
        {
            /*activeCanvas.EditingMode = InkCanvasEditingMode.Ink;
            activeCanvas.DefaultDractiveCanvasawingAttributes = _highlighterSettings;*/ //CHECK IT: Does this work?
        }

        private void cmdSelect_Click(object sender, RoutedEventArgs e)
        {
            _activeCanvas.EditingMode = InkCanvasEditingMode.Select;
        }

        private void ColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color> e)
        {
            ColorPicker cp = sender as ColorPicker;
            _penSettings.Color = cp.SelectedColor;
            _activeCanvas.DefaultDrawingAttributes = _penSettings;
            _activeCanvas.EditingMode = InkCanvasEditingMode.Ink;
            cmdDraw.IsChecked = true;            
        }

        private void BrushSize_Click(object sender, RoutedEventArgs e)
        {
            RadioButton b = sender as RadioButton;
            Rectangle r = b.Content as Rectangle;

            _penSettings.Height = r.Height;
            _penSettings.Width = r.Height;
            _activeCanvas.DefaultDrawingAttributes = _penSettings;
            _activeCanvas.EditingMode = InkCanvasEditingMode.Ink;
            cmdDraw.IsChecked = true;    
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void cmdUndo_Click(object sender, RoutedEventArgs e)
        {
            //parent.undo(); //TO DO
        }

        private void cmdRedo_Click(object sender, RoutedEventArgs e)
        {
            //parent.redo(); //TO DO
        }

        private void cmdClear_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = System.Windows.MessageBox.Show("Are you sure you want to clear the canvas?  This action cannot be undone.", "Confirm Action", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
                _activeCanvas.Strokes.Clear();
        }

        private void BrushSizeDown_Click(object sender, RoutedEventArgs e)
        {
            BrushSizeSlider.Value--;
        }

        private void BrushSizeUp_Click(object sender, RoutedEventArgs e)
        {
            BrushSizeSlider.Value++;
        }

        private void BrushSizeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _penSettings.Height = e.NewValue;
            _penSettings.Width = e.NewValue;
            if (parent != null)
            {
                _activeCanvas.DefaultDrawingAttributes = _penSettings;
                _activeCanvas.EditingMode = InkCanvasEditingMode.Ink;
                cmdDraw.IsChecked = true;
            }
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            this.Opacity = 1;
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            this.Opacity = 0.5;
        }
    }
}
