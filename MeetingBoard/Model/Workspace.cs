using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MeetingBoard.Model
{
    class Workspace
    {
        private string workspaceName; //save to file
        private string workspacePath;
        private string workspaceFullPath;
        private Dictionary<String, InkCanvas> workspaceCanvases; //save to a file and independent strokes
        private Dictionary<String, String> workspaceCanvasesTime; 

        public Workspace() //TO DO: Init workspace name and path with the workspace to open, or a default value in the case of creation
        {
            workspaceFullPath = "";
            workspaceName = "";
            workspacePath = "";         
            workspaceCanvases = new Dictionary<String, InkCanvas>();
            workspaceCanvasesTime = new Dictionary<String, String>();
        }

        //Private: Clean dictionaries
        public void ResetWorkspace()
        {
            workspaceFullPath = "";
            workspaceName = "";
            workspacePath = "";
            workspaceCanvases.Clear();
            workspaceCanvasesTime.Clear();
        }
        
        //Public: Add Workspace Canvases 
        public void AddCanvas(string canvasName, InkCanvas canvas, string time)
        {
            if (workspaceCanvases.ContainsKey(canvasName) == false)
            {
                workspaceCanvases.Add(canvasName, canvas);
                workspaceCanvasesTime.Add(canvasName, time);
            }else
            {
                workspaceCanvases[canvasName] = canvas; 
                workspaceCanvasesTime[canvasName] = time;
            }
        }

        //CanvasName, Canvas object
        public Dictionary<String, InkCanvas> getWorkspaceCanvases()
        {
            return workspaceCanvases;
        }

        public Dictionary<String, String> getWorkspaceCanvasesTime()
        {
            return workspaceCanvasesTime;
        }

        public void setWorkspaceFullPath(string wsFullPath)
        {
            workspaceFullPath = wsFullPath;
        }
        public string getWorkspaceFullPath()
        {
            return workspaceFullPath;
        }

        public void setWorkspaceName(string wsName)
        {
            workspaceName = wsName;
        }
        public string getWorkspaceName()
        {
            return workspaceName;
        }

        public void setWorkspacePath(string wsPath)
        {
            workspacePath = wsPath;
        }
        public string getWorkspacePath()
        {
            return workspacePath;
        }
    }
}
