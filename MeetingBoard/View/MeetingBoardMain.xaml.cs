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
using System.Windows.Threading;
using System.Diagnostics;

namespace MeetingBoard.View
{
    /// <summary>
    /// Interaction logic for MeetingBoard.xaml
    /// </summary>
    public partial class MeetingBoardMain : Window
    {
        ToolBox toolbox;

        //StopWatches
        DispatcherTimer dtGoals = new DispatcherTimer();
        DispatcherTimer dtEssence = new DispatcherTimer();
        DispatcherTimer dtConstraints = new DispatcherTimer();
        DispatcherTimer dtAlternatives = new DispatcherTimer();
        DispatcherTimer dtAssumptions = new DispatcherTimer();
        DispatcherTimer dtImpDec = new DispatcherTimer();
        DispatcherTimer dtActItems = new DispatcherTimer();

        Stopwatch swGoals = new Stopwatch();
        Stopwatch swEssence = new Stopwatch();
        Stopwatch swConstraints = new Stopwatch();
        Stopwatch swAlternatives = new Stopwatch();
        Stopwatch swAssumptions = new Stopwatch();
        Stopwatch swImpDec = new Stopwatch();
        Stopwatch swActItems = new Stopwatch();
        string currentTime = string.Empty;

        InkCanvas _activeCanvas;

        public MeetingBoardMain()
        {
            InitializeComponent();
            toolbox = new ToolBox(this);
            SetUpStopWatches();
            //TO DO: Init Goals as default active canvas
        }
        //StopWatches //TO REFACTOR
        private void SetUpStopWatches()
        {
            dtGoals.Tick += new EventHandler(dt_Tick);
            dtGoals.Interval = new TimeSpan(0, 0, 0, 1, 0);
            dtGoals.Tag = "dtGoals";
            dtEssence.Tick += new EventHandler(dt_Tick);
            dtEssence.Interval = new TimeSpan(0, 0, 0, 1, 0);
            dtEssence.Tag = "dtEssence";
            dtConstraints.Tick += new EventHandler(dt_Tick);
            dtConstraints.Interval = new TimeSpan(0, 0, 0, 1, 0);
            dtConstraints.Tag = "dtConstraints";
            dtAlternatives.Tick += new EventHandler(dt_Tick);
            dtAlternatives.Interval = new TimeSpan(0, 0, 0, 1, 0);
            dtAlternatives.Tag = "dtAlternatives";
            dtAssumptions.Tick += new EventHandler(dt_Tick);
            dtAssumptions.Interval = new TimeSpan(0, 0, 0, 1, 0);
            dtAssumptions.Tag = "dtAssumptions";
            dtImpDec.Tick += new EventHandler(dt_Tick);
            dtImpDec.Interval = new TimeSpan(0, 0, 0, 1, 0);
            dtImpDec.Tag = "dtImpDec";
            dtActItems.Tick += new EventHandler(dt_Tick);
            dtActItems.Interval = new TimeSpan(0, 0, 0, 1, 0);
            dtActItems.Tag = "dtActItems";
        }

        //TO REFACTOR
        private void StopAllWatches()
        {
            if (swGoals.IsRunning) { swGoals.Stop(); }
            if (swEssence.IsRunning) { swEssence.Stop(); }
            if (swConstraints.IsRunning) { swConstraints.Stop(); }
            if (swAlternatives.IsRunning) { swAlternatives.Stop(); }
            if (swAssumptions.IsRunning) { swAssumptions.Stop(); }
            if (swImpDec.IsRunning) { swImpDec.Stop(); }
            if (swActItems.IsRunning) { swActItems.Stop(); }
        }
        private void StartWatch(DispatcherTimer dt, Stopwatch sw)
        {
            sw.Start();
            dt.Start();
        }

        private void DisplayUpdatedTime(Stopwatch sw, TextBlock txt)
        {
            if (sw.IsRunning)
            {
                /*
                 currentTime = String.Format("{0:00}:{1:00}:{2:00}",
                 ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                 */ //OLD

                TimeSpan ts = sw.Elapsed;
                currentTime = String.Format("{0:00}:{1:00}:{2:00}",
                ts.Hours, ts.Minutes, ts.Seconds);
                txt.Text = "";
                txt.Text = currentTime;
            }
        }

        void dt_Tick(object sender, EventArgs e)
        {
            DispatcherTimer dt = sender as DispatcherTimer;
            switch (dt.Tag.ToString())
            {
                case "dtGoals":
                    //Debug.WriteLine("dt name:" + dt.Tag.ToString());
                    DisplayUpdatedTime(swGoals, GoalsCurrentElapsedTimeDisplay);
                    break;
                case "dtEssence":
                    DisplayUpdatedTime(swEssence, EssenceCurrentElapsedTimeDisplay);
                    break;
                case "dtConstraints":
                    DisplayUpdatedTime(swConstraints, ConstraintsCurrentElapsedTimeDisplay);
                    break;
                case "dtAlternatives":
                    DisplayUpdatedTime(swAlternatives, AlternativesCurrentElapsedTimeDisplay);
                    break;
                case "dtAssumptions":
                    DisplayUpdatedTime(swAssumptions, AssumptionsCurrentElapsedTimeDisplay);
                    break;
                case "dtImpDec":
                    DisplayUpdatedTime(swImpDec, ImpDecCurrentElapsedTimeDisplay);
                    break;
                case "dtActItems":
                    DisplayUpdatedTime(swActItems, ActionItemsCurrentElapsedTimeDisplay);
                    break;
                /* you can have any number of case statements */
                default:
                    //activeCanvas = this.GoalsCanvas;  //First Canvas by default
                    break;
            }
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TabItem ti = MainTabControl.SelectedItem as TabItem;
            StopAllWatches();
            switch (ti.Name)
            {
                case "TabGoals":
                    StartWatch(dtGoals, swGoals);
                    _activeCanvas = this.GoalsCanvas;
                    //Debug.WriteLine("Active canvas changing to: " + _activeCanvas.Name);
                    toolbox.ActiveCanvas = _activeCanvas;
                    break;
                case "TabEssence":
                    StartWatch(dtEssence, swEssence);
                    _activeCanvas = this.EssenceCanvas;
                    //Debug.WriteLine("Active canvas changing to: " + _activeCanvas.Name);
                    toolbox.ActiveCanvas = _activeCanvas;
                    break;
                case "TabConstraints":
                    StartWatch(dtConstraints, swConstraints);
                    _activeCanvas = this.ConstraintsCanvas;
                    //Debug.WriteLine("Active canvas changing to: " + _activeCanvas.Name);
                    toolbox.ActiveCanvas = _activeCanvas;
                    break;
                case "TabAlternatives":
                    StartWatch(dtAlternatives, swAlternatives);
                    _activeCanvas = this.AlternativesCanvas;
                    //Debug.WriteLine("Active canvas changing to: " + _activeCanvas.Name);
                    toolbox.ActiveCanvas = _activeCanvas;
                    break;
                case "TabAssumptions":
                    StartWatch(dtAssumptions, swAssumptions);
                    _activeCanvas = this.AssumptionsCanvas;
                    //Debug.WriteLine("Active canvas changing to: " + _activeCanvas.Name);
                    toolbox.ActiveCanvas = _activeCanvas;
                    break;
                case "TabImpDec":
                    StartWatch(dtImpDec, swImpDec);
                    _activeCanvas = this.ImpDecCanvas;
                    //Debug.WriteLine("Active canvas changing to: " + _activeCanvas.Name);
                    toolbox.ActiveCanvas = _activeCanvas;
                    break;
                case "TabActionItems":
                    StartWatch(dtActItems, swActItems);
                    _activeCanvas = this.ActionItemsCanvas;
                    //Debug.WriteLine("Active canvas changing to: " + _activeCanvas.Name);
                    toolbox.ActiveCanvas = _activeCanvas; 
                    break;
                default:
                    //activeCanvas = this.GoalsCanvas;  //First Canvas by default
                    break;
            }
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            toolbox.Close();
            /*strokeMenu.Close();
            _highlighterStrokes.CompleteAdding();*/
        }

        private void cmdToolbox_Checked(object sender, RoutedEventArgs e)
        {
            toolbox.Owner = this;
            toolbox.ActiveCanvas = _activeCanvas;
            toolbox.Show();
        }

        private void cmdToolbox_Unchecked(object sender, RoutedEventArgs e)
        {
            toolbox.ActiveCanvas = _activeCanvas;
            toolbox.Hide();
        }

        //TO DO: Check all inkCanvas selection events
        private void inkCanvas_SelectionChanged(object sender, EventArgs e)
        {
            //showCopyMenu();
        }

        private void inkCanvas_StrokeCollected(object sender, InkCanvasStrokeCollectedEventArgs e)
        {
            /*if (icCanvas.DefaultDrawingAttributes.IsHighlighter)
            {
                _highlighterStrokes.Add(e.Stroke);
            }
            else
            {
                _undoStack.Push(new KeyValuePair<Stroke, int>(e.Stroke, 1));
                _viewModel.activeCanvas.RefreshThumbnail();
            }*/
        }

        private void inkCanvas_StrokeErasing(object sender, InkCanvasStrokeErasingEventArgs e)
        {
            /*if (undoTracking)
                _undoStack.Push(new KeyValuePair<Stroke, int>(e.Stroke, 0));

            _viewModel.activeCanvas.RefreshThumbnail();*/
        }

        private void inkCanvas_SelectionMoved(object sender, EventArgs e)
        {
            /*showCopyMenu();
            _viewModel.activeCanvas.RefreshThumbnail();*/
        }

        private void cmdDetails_Click(object sender, RoutedEventArgs e)
        {
            //Open detail editor
            /*DetailsDialog d = new DetailsDialog(_viewModel.activeCanvas, _viewModel.canvasModel);
            d.Owner = Window.GetWindow(this);
            d.ShowDialog();*/
        }

        private void cmdNew_Click(object sender, RoutedEventArgs e)
        {
            //NewCanvas();
        }

        private void cmdClone_Click(object sender, RoutedEventArgs e)
        {
            /*_viewModel.CloneCanvas();
            RebindStrokes();
            _viewModel.activeCanvas.RefreshThumbnail();
            this.ApplyAllFilters();
            SelectActiveCanvas();*/
        }

        private void cmdQuit_Click(object sender, RoutedEventArgs e)
        {
            //this.DoneEditing();
        }
    }
}
