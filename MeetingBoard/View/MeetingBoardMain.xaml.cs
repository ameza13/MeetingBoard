using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using System.Diagnostics;
using System.Windows.Ink;
using MeetingBoard.Model;
using System.Reflection;

namespace MeetingBoard.View
{
    /// <summary>
    /// Interaction logic for MeetingBoard.xaml
    /// </summary>
    public partial class MeetingBoardMain : Window
    {
        ToolBox toolbox;
        Workspace ws;

        //StopWatches
        DispatcherTimer dtGoals = new DispatcherTimer();
        DispatcherTimer dtEssence = new DispatcherTimer();
        DispatcherTimer dtConstraints = new DispatcherTimer();
        DispatcherTimer dtAlternatives = new DispatcherTimer();
        DispatcherTimer dtAssumptions = new DispatcherTimer();
        DispatcherTimer dtImpDec = new DispatcherTimer();
        DispatcherTimer dtActItems = new DispatcherTimer();
        DispatcherTimer dtActive;

        Stopwatch swGoals = new Stopwatch();
        Stopwatch swEssence = new Stopwatch();
        Stopwatch swConstraints = new Stopwatch();
        Stopwatch swAlternatives = new Stopwatch();
        Stopwatch swAssumptions = new Stopwatch();
        Stopwatch swImpDec = new Stopwatch();
        Stopwatch swActItems = new Stopwatch();
        Stopwatch swActive;



        InkCanvas _activeCanvas;
        Border _activeBorder;
        DrawingAttributes _penSettings = new DrawingAttributes();
        DrawingAttributes _highlighterSettings = new DrawingAttributes();

        ScaleTransform st = new ScaleTransform();
        const double ScaleRate = 1.1;

        StrokeCollection redo = new StrokeCollection();
        StrokeCollection all_strokes = new StrokeCollection();


        int goal_count = 0;
        int essence_count = 0;
        int assumptions_count = 0;
        int alternatives_count = 0;
        int constraints_count = 0;
        int impDec_count = 0;

        bool isNew = false;



        public MeetingBoardMain()
        {
            InitializeComponent();
            toolbox = new ToolBox(this);
            ws = new Workspace();

            _activeCanvas = GoalsCanvas;
            _activeBorder = GoalsBorder;

            SetUpStopWatches();
            _initDrawingAttributes();

          
        }

        private void _initDrawingAttributes()
        {
            _penSettings = _activeCanvas.DefaultDrawingAttributes;
            /*_highlighterSettings.IsHighlighter = true;
            _highlighterSettings.Height = 10;
            _highlighterSettings.Width = 10;
            _highlighterSettings.Color = Colors.Yellow;*/
        }
        private void ClearAllCanvas()
        {
            GoalsCanvas.Strokes.Clear();
            EssenceCanvas.Strokes.Clear();
            ConstraintsCanvas.Strokes.Clear();
            AlternativesCanvas.Strokes.Clear();
            AssumptionsCanvas.Strokes.Clear();
            ImpDecCanvas.Strokes.Clear();
            //ActionItemsCanvas.Strokes.Clear();
        }
        private void SetUpMainGrid(int MenuOption)
        {
            /* 
             * 1 If 'New Workspace'
             *  1- Save unsaved work
             *  2- Clear tabs
             *  3- Clear workspace model     
             *  4- Configure other menu options
             * 2 If 'open workspace'
             *  1- If another workspace is open, call 'close'
             *  2- Call open workspace
             *  3- Configure other menu options
             * 4 If save 'all workspace'
             *  1- If a workspace is open, call 'Save All'
             *  2- Configure other menu options
             * 3 If save 'active tab'
             *  1- If the worspace is saved, call 'Save active canvas'
             *  2- In not, ...
             *  2- Configure other menu options
             * 5 If 'close workspace'
             *  1- Save unsaved work
             *  2- clear tabs
             *  3- clear workspace model
             *  4- Hide MainGrid
             *  5- Configure other menu options
             */

            //TO DO: watches control
            switch (MenuOption) //TO REFACTOR: Change case numbers for constants
            {
                case 1: //New Workspace
                    StopAllWatches();                
                    ws.ResetWorkspace(); //Reset timing?
                    ClearAllCanvas();

                    if(SaveWorkspace()) 
                    {
                        /*lblGoals.Visibility = Visibility.Visible;
                        lblEssence.Visibility = Visibility.Visible;
                        lblAssumptions.Visibility = Visibility.Visible;
                        lblConstraints.Visibility = Visibility.Visible;
                        lblAlternatives.Visibility = Visibility.Visible;
                        lblImpDec.Visibility = Visibility.Visible;*/

                        //TO DO!
                        StrokeCollection goal_temp = null; 
                        StrokeCollection essence_temp = null;
                        StrokeCollection assumptions_temp = null;
                        StrokeCollection alternatives_temp = null;
                        StrokeCollection constraints_temp = null;
                        StrokeCollection impDec_temp = null;

                        isNew = true;

                        string[] Documents = System.IO.Directory.GetFiles("../../templates/");
                     

            
                        string goals_file_path = Documents[4];
                        string essence_file_path = Documents[3];
                        string assumptions_file_path = Documents[1];
                        string alternatives_file_path = Documents[0];
                        string constraints_file_path = Documents[2];
                        string impDec_file_path = Documents[5];

                 


                        goal_temp = UpdateStrokeInWorkspaceModel(goals_file_path, "GoalsCanvas");
                        essence_temp = UpdateStrokeInWorkspaceModel(essence_file_path, "EssenceCanvas");
                        assumptions_temp = UpdateStrokeInWorkspaceModel(assumptions_file_path, "AssumptionsCanvas");
                        alternatives_temp = UpdateStrokeInWorkspaceModel(alternatives_file_path, "AlternativesCanvas");
                        constraints_temp = UpdateStrokeInWorkspaceModel(constraints_file_path, "ConstraintsCanvas");
                        impDec_temp = UpdateStrokeInWorkspaceModel(impDec_file_path, "ImpDecCanvas");

             

                        GoalsCanvas.Strokes = goal_temp;
                        EssenceCanvas.Strokes = essence_temp;
                        AssumptionsCanvas.Strokes = assumptions_temp;
                        AlternativesCanvas.Strokes = alternatives_temp;
                        ConstraintsCanvas.Strokes = constraints_temp;
                        ImpDecCanvas.Strokes = impDec_temp;

                        //AlternativesCanvas.ResizeEnabled = true;  
                        // the above line maks it so that drawing on edges grows the canvas
             


                        MainGrid.Visibility = Visibility.Visible;
                        ResetAllWatches();

                        //Select default tab
                        MainTabControl.SelectedIndex = TabGoals.TabIndex;
                        MainTabControl.SelectedItem = TabGoals;
                        TabGoals.IsSelected = true;

                        //Restart watch
                        StartWatch(dtGoals, swGoals);

                        //Set Options menu
                        miClose.IsEnabled = true;
                        //miSave.IsEnabled = true;
                        miSaveAll.IsEnabled = true;
                    }
              

                    break;
                case 2://Open Workspace
                    StopAllWatches();
                    //ClearAllCanvas();
                    if (OpenWorkspace())
                    {
                        ResetAllWatches();
                        MainGrid.Visibility = Visibility.Visible;

                        //Select default tab
                        MainTabControl.SelectedIndex = TabGoals.TabIndex;
                        MainTabControl.SelectedItem = TabGoals;
                        TabGoals.IsSelected = true;

                        //Restart watch
                        StartWatch(dtGoals, swGoals);

                        miClose.IsEnabled = true;
                        //miSave.IsEnabled = true;
                        miSaveAll.IsEnabled = true;
                    }                   
                    break;
                case 3: //Save
                    //TO DO: Save active tab only
                    break;
                case 4: //save 'all workspace'
                    StopAllWatches();
                    if (SaveWorkspace())
                    {
                        
                    }
                    //Restart watch
                    StartWatch(dtActive, swActive);
                    break;
                case 5:
                    StopAllWatches();
                    ResetAllWatches();
                    ws.ResetWorkspace();
                    ClearAllCanvas();
                    MainGrid.Visibility = Visibility.Hidden;

                    miClose.IsEnabled = false;
                    //miSave.IsEnabled = false;
                    miSaveAll.IsEnabled = false;
                    //TO DO:
                    /*-If changes, ask the user if wants to save them?
                     *  SaveAll
                     * -Reset watches
                     * */
                    break;
            }           
        }

        private void SaveToWorkspaceModel() 
        {
            StopAllWatches(); //Lock watches
            ws.AddCanvas(ws.getWorkspaceName() + "_Goals", GoalsCanvas, GoalsCurrentElapsedTimeDisplay.Text); 
            ws.AddCanvas(ws.getWorkspaceName() + "_Essence", EssenceCanvas, EssenceCurrentElapsedTimeDisplay.Text);
            ws.AddCanvas(ws.getWorkspaceName() + "_Constraints", ConstraintsCanvas, ConstraintsCurrentElapsedTimeDisplay.Text); 
            ws.AddCanvas(ws.getWorkspaceName() + "_Alternatives", AlternativesCanvas, AlternativesCurrentElapsedTimeDisplay.Text);
            ws.AddCanvas(ws.getWorkspaceName() + "_Assumptions", AssumptionsCanvas, AssumptionsCurrentElapsedTimeDisplay.Text);
            ws.AddCanvas(ws.getWorkspaceName() + "_ImportantDecisions", ImpDecCanvas, ImpDecCurrentElapsedTimeDisplay.Text);
            //ws.AddCanvas(ws.getWorkspaceName() + "_ActionItems", ActionItemsCanvas, ActionItemsCurrentElapsedTimeDisplay.Text);

            //StartWatch(_activeCanvas, ) //TO DO: Start the active canvas watch
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

        private void ResetAllWatches()
        {
            swGoals.Reset();
            swEssence.Reset();
            swConstraints.Reset();
            swAlternatives.Reset();
            swAssumptions.Reset();
            swImpDec.Reset();
            swActItems.Reset();

            //TO REFACTOR: Change string for constant
            GoalsCurrentElapsedTimeDisplay.Text = "(00:00:00)";
            EssenceCurrentElapsedTimeDisplay.Text = "(00:00:00)";
            ConstraintsCurrentElapsedTimeDisplay.Text = "(00:00:00)";
            AlternativesCurrentElapsedTimeDisplay.Text = "(00:00:00)";
            AssumptionsCurrentElapsedTimeDisplay.Text = "(00:00:00)";
            ImpDecCurrentElapsedTimeDisplay.Text = "(00:00:00)";
            //ActionItemsCurrentElapsedTimeDisplay.Text = "(00:00:00)";
        }

        //TO DO: Before stopping it, save the active sw and dt into a global variable.
        private void StopAllWatches()
        {
            if (swGoals.IsRunning)
            {
                swGoals.Stop();
                swActive = swGoals;
                dtActive = dtGoals;
            }
            if (swEssence.IsRunning)
            {
                swEssence.Stop();
                swActive = swEssence;
                dtActive = dtEssence;
            }
            if (swConstraints.IsRunning)
            {
                swConstraints.Stop();
                swActive = swConstraints;
                dtActive = dtConstraints;
            }
            if (swAlternatives.IsRunning)
            {
                swAlternatives.Stop();
                swActive = swAlternatives;
                dtActive = dtAlternatives;
            }
            if (swAssumptions.IsRunning)
            {
                swAssumptions.Stop();
                swActive = swAssumptions;
                dtActive = dtAssumptions;
            }
            if (swImpDec.IsRunning)
            {
                swImpDec.Stop();
                swActive = swImpDec;
                dtActive = dtImpDec;
            }
            if (swActItems.IsRunning)
            {
                swActItems.Stop();
                swActive = swActItems;
                dtActive = dtActItems;
            }
        }

        private void StartWatch(DispatcherTimer dt, Stopwatch sw)
        {
            if (!sw.IsRunning)
            {
                sw.Start();
                dt.Start();
            }
        }

        private void ResetWatch(Stopwatch sw)
        {
            sw.Reset();
        }
        private string DisplayUpdatedTime(Stopwatch sw, string lastTime)
        {
            string currentTime = lastTime;
            if (sw.IsRunning)
            {
                /*
                 *OLD
                 currentTime = String.Format("{0:00}:{1:00}:{2:00}",
                 ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                 */
                
                TimeSpan ts = sw.Elapsed;
                currentTime = String.Format("{0:00}:{1:00}:{2:00}",
                ts.Hours, ts.Minutes, ts.Seconds);

            }
            return "("+currentTime+")";
        }

        void dt_Tick(object sender, EventArgs e)
        {
            DispatcherTimer dt = sender as DispatcherTimer;
            string strTemp = "";
            switch (dt.Tag.ToString())
            {
                case "dtGoals":
                    //Debug.WriteLine("dt name:" + dt.Tag.ToString());
                    strTemp = GoalsCurrentElapsedTimeDisplay.Text;
                    GoalsCurrentElapsedTimeDisplay.Text = DisplayUpdatedTime(swGoals, strTemp.Substring(1,strTemp.Length-2));
                    break;
                case "dtEssence":
                    strTemp = EssenceCurrentElapsedTimeDisplay.Text;
                    EssenceCurrentElapsedTimeDisplay.Text = DisplayUpdatedTime(swEssence, strTemp.Substring(1, strTemp.Length - 2));
                    break;
                case "dtConstraints":
                    strTemp = ConstraintsCurrentElapsedTimeDisplay.Text;
                    ConstraintsCurrentElapsedTimeDisplay.Text = DisplayUpdatedTime(swConstraints, strTemp.Substring(1, strTemp.Length - 2));
                    break;
                case "dtAlternatives":
                    strTemp = AlternativesCurrentElapsedTimeDisplay.Text;
                    AlternativesCurrentElapsedTimeDisplay.Text = DisplayUpdatedTime(swAlternatives, strTemp.Substring(1, strTemp.Length - 2));
                    break;
                case "dtAssumptions":
                    strTemp = AssumptionsCurrentElapsedTimeDisplay.Text;
                    AssumptionsCurrentElapsedTimeDisplay.Text = DisplayUpdatedTime(swAssumptions, strTemp.Substring(1, strTemp.Length - 2));
                    break;
                case "dtImpDec":
                    strTemp = ImpDecCurrentElapsedTimeDisplay.Text;
                    ImpDecCurrentElapsedTimeDisplay.Text = DisplayUpdatedTime(swImpDec, strTemp.Substring(1, strTemp.Length - 2));
                    break;
                /*case "dtActItems":                    
                    ActionItemsCurrentElapsedTimeDisplay.Text = DisplayUpdatedTime(swActItems, ActionItemsCurrentElapsedTimeDisplay.Text);
                    break;*/
                default:
                    //activeCanvas = this.GoalsCanvas;  //First Canvas by default
                    break;
            }
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if(MainGrid.Visibility == Visibility.Visible)
            //{
                TabItem ti = MainTabControl.SelectedItem as TabItem;
                StopAllWatches();
                switch (ti.Name)
                {
                    case "TabGoals":
                        StartWatch(dtGoals, swGoals);
                        _activeCanvas = this.GoalsCanvas;
                        _activeBorder = this.GoalsBorder;
                        Debug.WriteLine("Active canvas changing to: " + _activeCanvas.Name);
                        toolbox.ActiveCanvas = _activeCanvas;
                        redo = new StrokeCollection();
                        break;
                    case "TabEssence":
                        StartWatch(dtEssence, swEssence);
                        _activeCanvas = this.EssenceCanvas;
                        _activeBorder = this.EssenceBorder;
                        Debug.WriteLine("Active canvas changing to: " + _activeCanvas.Name);
                        toolbox.ActiveCanvas = _activeCanvas;
                        redo = new StrokeCollection();
                        break;
                    case "TabConstraints":
                        StartWatch(dtConstraints, swConstraints);
                        _activeCanvas = this.ConstraintsCanvas;
                        _activeBorder = this.ConstraintsBorder;
                        Debug.WriteLine("Active canvas changing to: " + _activeCanvas.Name);
                        toolbox.ActiveCanvas = _activeCanvas;
                        redo = new StrokeCollection();
                        break;
                    case "TabAlternatives":
                        StartWatch(dtAlternatives, swAlternatives);
                        _activeCanvas = this.AlternativesCanvas;
                        _activeBorder = this.AlternativesBorder;
                        Debug.WriteLine("Active canvas changing to: " + _activeCanvas.Name);
                        toolbox.ActiveCanvas = _activeCanvas;
                        redo = new StrokeCollection();
                        break;
                    case "TabAssumptions":
                        StartWatch(dtAssumptions, swAssumptions);
                        _activeCanvas = this.AssumptionsCanvas;
                        _activeBorder = this.AssumptionsBorder;
                        Debug.WriteLine("Active canvas changing to: " + _activeCanvas.Name);
                        toolbox.ActiveCanvas = _activeCanvas;
                        redo = new StrokeCollection();
                        break;
                    case "TabImpDec":
                        StartWatch(dtImpDec, swImpDec);
                        _activeCanvas = this.ImpDecCanvas;
                        _activeBorder = this.ImpDecBorder;
                        Debug.WriteLine("Active canvas changing to: " + _activeCanvas.Name);
                        toolbox.ActiveCanvas = _activeCanvas;
                        redo = new StrokeCollection();
                        break;
                    /*case "TabActionItems":
                        StartWatch(dtActItems, swActItems);
                        _activeCanvas = this.ActionItemsCanvas;
                        //Debug.WriteLine("Active canvas changing to: " + _activeCanvas.Name);
                        toolbox.ActiveCanvas = _activeCanvas;
                        break;*/
                    default:
                        //activeCanvas = this.GoalsCanvas;  //First Canvas by default
                        break;
                }

            //}

        }
        private void Window_Closed(object sender, EventArgs e)
        {
            toolbox.Close();
            /*strokeMenu.Close();
            _highlighterStrokes.CompleteAdding();*/
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
            Debug.WriteLine("Active canvas is:" + _activeCanvas.Name);
          
        




            redo = new StrokeCollection();
            

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
      
        //TO REFACTOR: Move to a util unit
        private bool SaveWorkspaceStrokes()
        {
            
            bool successfulOperation = false;       
            string workspaceFullPath = ""; //TO REFACTOR: Change for default workspace location
            try
            {
                //Save model to files
                foreach (KeyValuePair<string, InkCanvas> entry in ws.getWorkspaceCanvases())
                {
                    workspaceFullPath = ws.getWorkspacePath() + "\\" + entry.Key + ".isf"; //TO REFACOR:Change for constant
                    Debug.WriteLine(workspaceFullPath);
                    SaveStroke(workspaceFullPath, entry.Value);
                }
                successfulOperation = true;
            }
            finally
            {

            }
            Debug.WriteLine(all_strokes);
            return successfulOperation;
            //Debug.WriteLine(all_strokes);
        }

        //TO DO: Move to a util unit
        private void SaveStroke(string filePath, InkCanvas canvas)
        {
            FileStream fs = null;
            try
            {
                   
                //Only save canvas files with strokes  
                if (canvas.Strokes.Count>0)
                {
                    fs = new FileStream(filePath, FileMode.Create);
                    canvas.Strokes.Save(fs);
                    Debug.WriteLine("Strokes saved.");
                }      
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                }
            }
        }

        private XmlDocument CreateSessionData(string xmlContent)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root;
            doc.LoadXml(xmlContent);
            //select your specific node ..

            //< SESSION >< TIMING ></ TIMING ></ SESSION >

            //Add session element
            root = (XmlElement)doc.SelectSingleNode("/SESSIONS");
            if (root != null)
            {
                XmlElement sessionElement = doc.CreateElement("SESSION");
                sessionElement.SetAttribute("ID", DateTime.Now.ToString("yyyyMMddHHmmssfff"));
                root.InsertAfter(sessionElement, root.LastChild);
                //el.AppendChild(sessionElement);

                //Add Timing section
                XmlElement timingElement = doc.CreateElement("TIMING");
                sessionElement.InsertAfter(timingElement, sessionElement.LastChild);

                XmlElement canvasElement;
                foreach (KeyValuePair<string, string> entry in ws.getWorkspaceCanvasesTime())
                {      
                    canvasElement = doc.CreateElement("CANVAS");
                    canvasElement.SetAttribute("NAME", entry.Key);
                    canvasElement.SetAttribute("TIME", entry.Value);
                    timingElement.InsertAfter(canvasElement, timingElement.LastChild);                   
                }

            }
            /*el = (XmlElement)doc.SelectSingleNode("/SESSIONS/" + "SESSION");
            if (el != null)
            {
                XmlElement idElement = doc.CreateElement("ID");
                idElement.InnerText = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                el.AppendChild(idElement);
            }

            XmlElement newElement;
            
            foreach (KeyValuePair<string, string> entry in ws.getWorkspaceCanvasesTime())
            {
                el = (XmlElement)doc.SelectSingleNode("/SESSIONS/SESSION/" + "TIMING");
                if (el != null)
                {
                    newElement = doc.CreateElement("CANVAS");
                    newElement.SetAttribute("NAME", entry.Key);
                    el.AppendChild(newElement);

                    el = (XmlElement)doc.SelectSingleNode("/SESSIONS/SESSION/TIMING/" + "CANVAS");
                    if (el != null)
                    {
                        newElement = doc.CreateElement("TIME");
                        newElement.InnerText = entry.Value;
                        el.AppendChild(newElement);
                    }
                }
            }*/ //OLD
            return doc;
        }

        //TO REFACTOR: Chanf for a xml/json structure
        private void CreateNewConfigFile(string filePath)
        {
            // Create the XmlDocument.
            XmlDocument doc;
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;

            XmlWriter writer = XmlWriter.Create(filePath, settings);

            doc = CreateSessionData("<SESSIONS></SESSIONS>");           
            if (doc != null)
            {
                doc.Save(writer);
            }           
            // Create a file to write to.
            /*using (StreamWriter sw = File.CreateText(filePath)) //TO REFACTOR: Change for constant
            {
                sw.WriteLine("session id:" + DateTime.Now.ToString("yyyyMMddHHmmssfff"));                
                //write the time spend on each canvas
                foreach (KeyValuePair<string, string> entry in ws.getWorkspaceCanvasesTime())
                {
                    sw.WriteLine(entry.Key + ":"+ entry.Value);
                }
            }*/ //OLD

        }
        private void UpdateConfigFile(string filePath)
        {
            // Create the XmlDocument.
            XmlDocument doc = new XmlDocument();
            doc.PreserveWhitespace = true;

            doc.Load(filePath);           

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;

            XmlWriter writer = XmlWriter.Create(filePath, settings);

            doc = CreateSessionData(doc.DocumentElement.OuterXml);
            if (doc != null)
            {
                doc.Save(writer);
            }
         
            /*XmlDocument doc = new XmlDocument();
            doc.PreserveWhitespace = true;
            try { doc.Load("booksData.xml"); }
            catch (System.IO.FileNotFoundException)
            {
                doc.LoadXml("<?xml version=\"1.0\"?> \n" +
                "<books xmlns=\"http://www.contoso.com/books\"> \n" +
                "  <book genre=\"novel\" ISBN=\"1-861001-57-8\" publicationdate=\"1823-01-28\"> \n" +
                "    <title>Pride And Prejudice</title> \n" +
                "    <price>24.95</price> \n" +
                "  </book> \n" +
                "  <book genre=\"novel\" ISBN=\"1-861002-30-1\" publicationdate=\"1985-01-01\"> \n" +
                "    <title>The Handmaid's Tale</title> \n" +
                "    <price>29.95</price> \n" +
                "  </book> \n" +
                "</books>");
            }*/
        }
        private bool SaveWorkspaceConfig()
        {
            bool successfullOperation = false;
            try
            {
                if (!File.Exists(ws.getWorkspaceFullPath()))
                {
                    CreateNewConfigFile(ws.getWorkspaceFullPath());
                }else
                {
                    UpdateConfigFile(ws.getWorkspaceFullPath());
                    
                }
                successfullOperation = true;
            }
            finally
            {
                //
            }
            return successfullOperation;
        }
        /*
         *         private void LoadStroke(string filePath, InkCanvas canvas)
        {
            FileStream fs = null;
            try
            {
                //filePath = @"E:\Proyectos\MeetingBoard\StrokesFolder\stroke.isf" 
                Debug.WriteLine(filePath);
                if(File.Exists(filePath))
                {
                    fs = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite);
                    if (canvas.Strokes.Count > 0)
                    {
                        StrokeCollection strokes = new StrokeCollection(fs);
                        canvas.Strokes = strokes;
                    }
                }
                else
                {

                }
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                }
            }
        }
         */
        private StrokeCollection UpdateStrokeInWorkspaceModel(string filePath, string canvasName)
        {
            FileStream fs = null;
            StrokeCollection strokes = null;
            try
            {
                //filePath = @"E:\Proyectos\MeetingBoard\StrokesFolder\stroke.isf" 
                Debug.WriteLine(filePath);
                if(File.Exists(filePath)) //If file exists it will have strokes
                {
                    fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                    strokes = new StrokeCollection(fs);                  
                }
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                }
            }
            return strokes;
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            SetUpMainGrid(1);
        }

        /*
         Feedback required: This method does not update the tab's time with the
         data from the configuration file. 

         Design Question:
         Would it be actually worthy the the users could see how much time
         has been spen on each tab when they re-open a meeting-board from
         a previous session?
        */
        private bool LoadConfigFile()
        {
            bool successfullOperation = false;
            try
            {
                //@"C:\Users\Public\TestFolder\WriteLines2.txt" //OLD
                string[] lines = System.IO.File.ReadAllLines(ws.getWorkspaceFullPath());

                // Display the file contents by using a foreach loop.
                Debug.WriteLine("Config file content:");
                foreach (string line in lines)
                {
                    //TO DO: Update initial timing 
                    Debug.WriteLine("\t" + line);
                }
                successfullOperation = true;
                /*
                 	workspace2_Goals:00:00:01
	                workspace2_Essence:00:00:00
	                workspace2_Constraints:00:00:00
	                workspace2_Alternatives:00:00:00
	                workspace2_Assumptions:00:00:00
	                workspace2_ImportantDecisions:00:00:03
	                workspace2_ActionItems:00:00:00
                 */
            }
            catch
            {

            }
            return successfullOperation;
        }

        private bool LoadStrokesToWorkspace()
        {
            bool successfullOperation = false;
            try
            {
                //Update Strokes from files to model
                StrokeCollection strokes = null;
                foreach (KeyValuePair<string, InkCanvas> entry in ws.getWorkspaceCanvases())
                {
                    Debug.WriteLine(entry.Key);
                    strokes = UpdateStrokeInWorkspaceModel(ws.getWorkspacePath() + "\\" + entry.Key + ".isf", entry.Key);
                    Debug.WriteLine(ws.getWorkspacePath() + "\\" + entry.Key + ".isf");
                    if (strokes != null)
                    {
                        entry.Value.Strokes = strokes;
                    }
                }
                successfullOperation = true;
            }
            finally
            {

            }
            return successfullOperation;
            //Strokes from model to view
            /*foreach (KeyValuePair<string, InkCanvas> entry in ws.getWorkspaceCanvases())
            {
                Debug.WriteLine(entry.Key);
                
                LoadStroke(ws.getWorkspacePath() + "\\" + entry.Key+ ".isf", entry.Value);
                
            }*/
            //GoalsCanvas.Strokes = ws.getWorkspaceCanvases()[ws.getWorkspaceName() + "_Goals"].Strokes; //TEST

            /*LoadStroke(ws.getWorkspacePath() +"\\"+ ws.getWorkspaceName() + "_Goals.isf", GoalsCanvas);
            LoadStroke(ws.getWorkspacePath() + "\\" + ws.getWorkspaceName() + "_Essence.isf", EssenceCanvas);
            LoadStroke(ws.getWorkspacePath() + "\\" + ws.getWorkspaceName() + "_Constraints.isf", ConstraintsCanvas);
            LoadStroke(ws.getWorkspacePath() + "\\" + ws.getWorkspaceName() + "_Alternatives.isf", AlternativesCanvas);
            LoadStroke(ws.getWorkspacePath() + "\\" + ws.getWorkspaceName() + "_Assumptions.isf", AssumptionsCanvas);
            LoadStroke(ws.getWorkspacePath() + "\\" + ws.getWorkspaceName() + "_ImportantDecisions.isf", ImpDecCanvas);
            LoadStroke(ws.getWorkspacePath() + "\\" + ws.getWorkspaceName() + "_ActionItemss.isf", ActionItemsCanvas);*/

        }

      


        private bool OpenWorkspace()
        {
            bool successfullOperation = false;
            string fullFilePath = "";
            string workspaceName = ""; //TO REFACTOR: Change for default workspace constant
            string filePath = ""; //TO REFACTOR: Change for default path constant

            try
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.InitialDirectory = "c:\\"; //TO DO: Change for last open folder
                    openFileDialog.Filter = "workspace files (*.xml)|*.xml|stroke files (*.isf)|*.isf|All files (*.*)|*.*";
                    openFileDialog.FilterIndex = 1;
                    openFileDialog.Title = "Open Workspace";
                    openFileDialog.RestoreDirectory = true;

                    if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        fullFilePath = openFileDialog.FileName; //ConfigFile
                        Debug.WriteLine(fullFilePath);

                        if (fullFilePath != "")
                        {
                            workspaceName = System.IO.Path.GetFileNameWithoutExtension(fullFilePath); // name of the file
                            filePath = System.IO.Directory.GetParent(fullFilePath).ToString(); // path without name

                            //SetUp workspace data
                            ws.ResetWorkspace();
                            ws.setWorkspaceFullPath(fullFilePath); //For ConfigFile
                            ws.setWorkspaceName(workspaceName);
                            ws.setWorkspacePath(filePath);
                        }

                        //Load files
                        if (ws.getWorkspaceFullPath() != "") // if there is a valid path
                        {
                            SaveToWorkspaceModel(); // dict of sketches (getting info)
                            //TO DO: Read canvas timing
                            ClearAllCanvas();
                            successfullOperation = (LoadConfigFile() && LoadStrokesToWorkspace());
                        }
                    }else
                    {
                        //TO DO: Recover previous workspace model
                    }
                }
            }
            catch
            {
                //TO DO
            }
            finally
            {
                //TO DO
            }
            return successfullOperation;

        }
        private void Open_Click(object sender, RoutedEventArgs e)
        {
            SetUpMainGrid(2);

            //Open one stroke
            /*string fileName = null;
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "isf files (*.isf)|*.isf|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    fileName = openFileDialog.FileName;
                    Debug.WriteLine(fileName);
                }
            }

            if (fileName != null)
            {
                LoadStroke(fileName, GoalsCanvas);
            }*/
        }

        private bool SaveWorkspace()
        {
            bool successfullOperation = false;
            try
            {
                if (ws.getWorkspaceFullPath() == "") //If empty
                {
                    string fullFilePath = "";
                    string workspaceName = ""; //TO REFACTOR: Change for default workspace constant
                    string filePath = ""; //TO REFACTOR: Change for default path constant
                    using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                    {
                        saveFileDialog.Filter = "workspace files (*.xml)|*.xml|stroke files (*.isf)|*.isf|All files (*.*)|*.*";
                        saveFileDialog.FilterIndex = 1;
                        saveFileDialog.Title = "Save Workspace";
                        saveFileDialog.RestoreDirectory = true;
                        saveFileDialog.ShowDialog();

                        fullFilePath = saveFileDialog.FileName;
                        if (fullFilePath != "")
                        {
                            workspaceName = System.IO.Path.GetFileNameWithoutExtension(fullFilePath);
                            filePath = System.IO.Directory.GetParent(fullFilePath).ToString();

                            //SetUp workspace data
                            ws.setWorkspaceFullPath(fullFilePath); //For ConfigFile
                            ws.setWorkspaceName(workspaceName);
                            ws.setWorkspacePath(filePath);

                            SaveToWorkspaceModel();
                        }
                    }
                    
                }   
                if(ws.getWorkspaceFullPath()!="")
                {
                    SaveToWorkspaceModel();
                    successfullOperation = (SaveWorkspaceConfig() && SaveWorkspaceStrokes());
                }                       
            }
            catch
            {
                //TO DO
            }
            finally
            {
                //TO DO
            }
            return successfullOperation;
        }
        private void SaveAll_Click(object sender, RoutedEventArgs e)
        {
            SetUpMainGrid(4);
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            //TO DO: Save active canvas
            /*
             * -If the workspace is saved, saves active tab
             * -If not, saves all the workspace 
             */
            SetUpMainGrid(3);
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            SetUpMainGrid(5); //TO REFACTOR: Change number for a constant
        }

        private void GoalsCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (lblGoals.Visibility != Visibility.Hidden)
            {
                lblGoals.Visibility = Visibility.Hidden;
            
            }

            if(goal_count == 0 && isNew)
            {
                GoalsCanvas.Strokes = new StrokeCollection();
            }
            goal_count += 1;
            
        }

        private void EssenceCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (lblEssence.Visibility != Visibility.Hidden)
            {
                lblEssence.Visibility = Visibility.Hidden;
            }

            if (essence_count == 0 && isNew)
            {
               EssenceCanvas.Strokes = new StrokeCollection();
            }
            essence_count += 1;

        }

        private void ConstraintsCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (lblConstraints.Visibility != Visibility.Hidden)
            {
                lblConstraints.Visibility = Visibility.Hidden;
            }
            if(constraints_count == 0 && isNew)
            {
                ConstraintsCanvas.Strokes = new StrokeCollection();
            }
            constraints_count += 1;
        }

        private void AssumptionsCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (lblAssumptions.Visibility != Visibility.Hidden)
            {
                lblAssumptions.Visibility = Visibility.Hidden;
            }
            if(assumptions_count == 0 && isNew)
            {
                AssumptionsCanvas.Strokes = new StrokeCollection();
            }
            assumptions_count += 1;
            
        }

        private void AlternativesCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (lblAlternatives.Visibility != Visibility.Hidden)
            {
                lblAlternatives.Visibility = Visibility.Hidden;
      
            }
            if (alternatives_count == 0 && isNew)
            {
                int remove_index = 34;

                while(AlternativesCanvas.Strokes.Count > remove_index)
                {
                    AlternativesCanvas.Strokes.RemoveAt(AlternativesCanvas.Strokes.Count - 1);
                }

               
            }
            alternatives_count += 1;
          
        }

        private void ImpDecCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (lblImpDec.Visibility != Visibility.Hidden)
            {
                lblImpDec.Visibility = Visibility.Hidden;
            }
            if(impDec_count == 0 && isNew)
            {
                ImpDecCanvas.Strokes = new StrokeCollection();
            }
            impDec_count += 1;
        }

        private void cmdDraw_Click(object sender, RoutedEventArgs e)
        {
            _activeCanvas.EditingMode = InkCanvasEditingMode.Ink;
            _activeCanvas.DefaultDrawingAttributes = _penSettings;
           // string filepath= "..\templates\first_temp_Goals.isf";
            




        }

        private void cmdErase_Click(object sender, RoutedEventArgs e)
        {
            _activeCanvas.EditingMode = InkCanvasEditingMode.EraseByPoint;
            Debug.WriteLine("Active canvas is:" + _activeCanvas.Name);
        }

        private void cmdEraseStroke_Click(object sender, RoutedEventArgs e)
        {
            _activeCanvas.EditingMode = InkCanvasEditingMode.EraseByStroke;
            Debug.WriteLine("Active canvas is:" + _activeCanvas.Name);
        }

        private void cmdSelect_Click(object sender, RoutedEventArgs e)
        {
            _activeCanvas.EditingMode = InkCanvasEditingMode.Select;
            Debug.WriteLine("Active canvas is:" + _activeCanvas.Name);
        }

        private void cmdClearAll_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = System.Windows.MessageBox.Show("Are you sure you want to clear the canvas? \n This action cannot be undone.", "Confirm Action", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
                _activeCanvas.Strokes.Clear();
            Debug.WriteLine("Active canvas is:" + _activeCanvas.Name);
        }

        private void cmdUndo_Click(object sender, RoutedEventArgs e)
        {
            //TO DO
            Debug.WriteLine("Active canvas is:" + _activeCanvas.Name);
            all_strokes = _activeCanvas.Strokes;
            int length_of_strokes = all_strokes.Count;

            if (length_of_strokes > 0)
            {
                Debug.WriteLine(all_strokes + " " + length_of_strokes);
                Debug.WriteLine(all_strokes[length_of_strokes - 1]);
                redo.Insert(redo.Count, all_strokes[length_of_strokes - 1]);
                all_strokes.RemoveAt(length_of_strokes - 1);
        
            }

          


            // https://docs.microsoft.com/en-us/dotnet/api/system.windows.controls.inkcanvas.strokes?view=netframework-4.7.2
            //https://edi.wang/post/2017/7/25/uwp-ink-undo-redo
        }

        private void cmdRedo_Click(object sender, RoutedEventArgs e)
        {
            //TO DO
            if(redo.Count > 0)
            {
                Debug.WriteLine("Active canvas is:" + _activeCanvas.Name);
                Debug.WriteLine(all_strokes);
                all_strokes.Insert(all_strokes.Count, redo[redo.Count - 1]);
                redo.RemoveAt(redo.Count - 1);
            }
            


        }

        private void cmdToolbox_Checked(object sender, RoutedEventArgs e)
        {
            toolbox.Owner = this;
            toolbox.ActiveCanvas = _activeCanvas;
            toolbox.Show();
        }

        private void cmdToolbox_Unchecked(object sender, RoutedEventArgs e)
        {
            toolbox.Hide();
        }

        //TEST
        /*private void cmdHandCursor_Click(object sender, RoutedEventArgs e)
        {
            if (this.Cursor != System.Windows.Input.Cursors.Wait)
            {
                _activeCanvas.EditingMode = InkCanvasEditingMode.Select;
                _activeCanvas.UseCustomCursor = true;
                Mouse.OverrideCursor = System.Windows.Input.Cursors.Hand;
            }
        }

        private void cmdArrowCursor_Click(object sender, RoutedEventArgs e)
        {
            if (this.Cursor != System.Windows.Input.Cursors.Wait)
            {
                GoalsCanvas.UseCustomCursor = true;
                Mouse.OverrideCursor = System.Windows.Input.Cursors.Arrow;
            }
                
        }*/

        private void cmdZoomIn_Click(object sender, RoutedEventArgs e)
        {                     
            st.ScaleX *= ScaleRate;
            st.ScaleY *= ScaleRate;
            _activeCanvas.RenderTransform = st;
            _activeBorder.RenderTransform = st;

            int expand_scale = 30;

            double current_height = _activeCanvas.ActualHeight;
            double update_height = current_height + expand_scale;

            double current_width = _activeCanvas.ActualWidth;
            double update_width = current_width + expand_scale;


          
            _activeCanvas.Height = update_height;
            _activeCanvas.Width = update_width;

          



        }

        private void cmdZoomOut_Click(object sender, RoutedEventArgs e)
        {
         
            st.ScaleX /= ScaleRate;          
            st.ScaleY /= ScaleRate;
            _activeCanvas.RenderTransform = st;
            _activeBorder.RenderTransform = st;


            Debug.WriteLine("zoom out clicked" + _activeCanvas.ActualHeight + " " + _activeCanvas.ActualWidth);
        }

        private void debugging(object sender, RoutedEventArgs e)
        {
            //Debug.WritecLine();
        }
    }
}