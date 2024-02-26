using Microsoft.Office.Tools.Ribbon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExcelAddIn5
{
    public partial class Ribbon1
    {
        private void Ribbon1_Load(object sender, RibbonUIEventArgs e)
        {

        }

        private void button1_Click(object sender, RibbonControlEventArgs e)
        {
            Form1 fr = new Form1();
            fr.ShowDialog();
        }
        
        private void button2_Click(object sender, RibbonControlEventArgs e)
        {
            //Globals.ThisAddIn.myCustomTaskPane.Visible = true;
            Globals.ThisAddIn.activar();
        }
       
    }
}
