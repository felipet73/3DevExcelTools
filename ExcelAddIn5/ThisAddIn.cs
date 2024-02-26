using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Excel = Microsoft.Office.Interop.Excel;
using Office = Microsoft.Office.Core;
using Microsoft.Office.Tools.Excel;

namespace ExcelAddIn5
{
    public partial class ThisAddIn
    {

        private UserControl1 myUserControl1;
        public Microsoft.Office.Tools.CustomTaskPane myCustomTaskPane;

        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            //UserControl1 hello = new UserControl1();
            //this.Application.ThisWorkbook.ActionsPane.Controls.Add(hello);



        }
        public void activar()
        {
            myUserControl1 = new UserControl1();
            myCustomTaskPane = this.CustomTaskPanes.Add(myUserControl1,
                "New Task Pane");

            myCustomTaskPane.DockPosition =
                Office.MsoCTPDockPosition.msoCTPDockPositionFloating;
            myCustomTaskPane.Height = 500;
            myCustomTaskPane.Width = 500;

            myCustomTaskPane.DockPosition =
                Office.MsoCTPDockPosition.msoCTPDockPositionRight;
            myCustomTaskPane.Width = 300;

            myCustomTaskPane.Visible = true;
            myCustomTaskPane.DockPositionChanged +=
                new EventHandler(myCustomTaskPane_DockPositionChanged);

        }
        private void myCustomTaskPane_DockPositionChanged(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
        }

        #region Código generado por VSTO

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
            //this.Application.DisplayDocumentActionTaskPane


        }

        #endregion
    }
}
