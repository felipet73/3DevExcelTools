using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


using Autodesk.Forge.Client;
using Autodesk.Forge;
using DevComponents.DotNetBar;
//using Newtonsoft.Json.Linq;
//using RestSharp;
using Biblioteca;

using Autodesk.Forge.Model;

using Excel = Microsoft.Office.Interop.Excel;
using Office = Microsoft.Office.Core;
using Microsoft.Office.Tools.Excel;

using System.Net;
using System.Threading;
using EO.WinForm;
using System.IO;
using System.Json;
using Newtonsoft.Json;
using System.Windows;
using Microsoft.AspNet.SignalR.Client;

using System.Net.Http;
using System.Web;

using System.Net.Http.Headers;
using EO.WebBrowser;

namespace ExcelAddIn5
{
    public partial class UserControl1 : UserControl
    {

        private const string URL = "https://sub.domain.com/objects.json";
        private string urlParameters = "?api_key=123";


        public UserControl1()
        {
            EO.WebBrowser.Runtime.AddLicense(
            "3a5rp7PD27FrmaQHEPGs4PP/6KFrqKax2r1GgaSxy5916u34GeCt7Pb26bSG" +
            "prT6AO5p3Nfh5LRw4rrqHut659XO6Lto6u34GeCt7Pb26YxDs7P9FOKe5ff2" +
            "6YxDdePt9BDtrNzCnrWfWZekzRfonNzyBBDInbW1yQKzbam2xvGvcau0weKv" +
            "fLOz/RTinuX39vTjd4SOscufWbPw+g7kp+rp9unMmuX59hefi9jx+h3ks7Oz" +
            "/RTinuX39hC9RoGkscufddjw/Rr2d4SOscufWZekscu7mtvosR/4qdzBs/DO" +
            "Z7rsAxrsnpmkBxDxrODz/+iha6iywc2faLWRm8ufWZfAwAzrpeb7z7iJWZek" +
            "sefuq9vpA/Ttn+ak9QzznrSmyNqxaaa2wd2wW5f3Bg3EseftAxDyeuvBs+I=");
            InitializeComponent();
            webView1.JSExtInvoke += new JSExtInvokeHandler(WebView_JSExtInvoke);

        }

        void WebView_JSExtInvoke(object sender, JSExtInvokeArgs e)
        {
            switch (e.FunctionName)
            {
                case "demoAbout":
                    /*string browserEngine = e.Arguments[0] as string;
                    string url = e.Arguments[1] as string;
                    MessageBox.Show("Browser Engine: " + browserEngine + ", Url:" + url);*/

                    Excel.Workbook libro = Globals.ThisAddIn.Application.ActiveWorkbook;
                    Excel.Worksheet hoja = libro.Worksheets[1];
                    //hoja.Cells[2, 1] = e.Arguments[2];
                    string ID = e.Arguments[0] as string;
                    JsonValue data = JsonObject.Parse(e.Arguments[1] as string);


                    hoja.Cells[2, 1] = (string)ID;

                    for (int X = 0; X < data.Count; X++) {
                        hoja.Cells[3+X, 1] = (string)data[X]["attributeName"];
                        hoja.Cells[3+X, 2] = (string)data[X]["displayName"];
                        hoja.Cells[3+X, 3] = data[X]["displayValue"];
                    }
                    //string name = (string)data["name"];
                    //string version = (string)data["version"];

                    break;
            }
        }

        private void UserControl1_Load(object sender, EventArgs e)
        {
            //webControl1.WebView.LoadUrl("https://www.facebook.com/");        
            //webControl1.WebView.LoadUrl("http://localhost:3001/");
            //"file:///HTML/Viewer.html?URN=dXJuOmFkc2sud2lwcHJvZDpmcy5maWxlOnZmLkpNR2ZnSFh2VFVHNmFRNkNBWTVOcWc/dmVyc2lvbj0x&Token=eyJhbGciOiJSUzI1NiIsImtpZCI6IlU3c0dGRldUTzlBekNhSzBqZURRM2dQZXBURVdWN2VhIn0.eyJzY29wZSI6WyJkYXRhOnJlYWQiLCJkYXRhOndyaXRlIiwiZGF0YTpjcmVhdGUiLCJkYXRhOnNlYXJjaCIsImJ1Y2tldDpjcmVhdGUiLCJidWNrZXQ6cmVhZCIsImJ1Y2tldDp1cGRhdGUiLCJidWNrZXQ6ZGVsZXRlIl0sImNsaWVudF9pZCI6IkxybjZvcUxud3BDQmQ4R1MwTHVpbUd4NVNIT05ZdzRiIiwiYXVkIjoiaHR0cHM6Ly9hdXRvZGVzay5jb20vYXVkL2Fqd3RleHA2MCIsImp0aSI6IkdmRzNaVmxtWDlzWkM4a0lrV2F5UjFrQXJrekdXVHQ3OXdaczlzckoycTZBZ0h1d1hBU3YzanRYcDlFSlJUZ04iLCJleHAiOjE2NDEwODQzNzV9.Oq7XE3bDoG1m1T9ZBUcRSCYZy_FJPv7jwho9uypdIv3aHgM7DVONQLIQ5a3PdZkdFzxImO5C4x_RRJyFCaJwXDsvcgW8jR_mTzNqbNQU1kfIeDrMMT5-bc0Tgt1galK9NMBXrM9eeUsXAcKTF_UY9blnnLY8ef9_8NsRRuVsjSdaZ7UK3H6qx1lWIChCuGqgi3ODwgJuv9fyv3GO0a1ULIDVLTuOwp5NowqsAWXtf7BpB0veEpnaTwPsvLU72hxJV3ar-9QJ8gJI8aZ7F-055asuPVvbTKRtWQIQGwGSOUHwePX16R-elEUP3QvjpwG8P9FUMjPZQryuCP3PmPw6ng"

            //webControl1.WebView.LoadUrl(ViewerURN("urn:adsk.wipprod:fs.file:vf.JMGfgHXvTUG6aQ6CAY5Nqg?version=1"),"");
            //webControl1.WebView.LoadUrl(@"file:///C:/HTML/Viewer.html?URN=dXJuOmFkc2sud2lwcHJvZDpmcy5maWxlOnZmLkpNR2ZnSFh2VFVHNmFRNkNBWTVOcWc/dmVyc2lvbj0x&Token=eyJhbGciOiJSUzI1NiIsImtpZCI6IlU3c0dGRldUTzlBekNhSzBqZURRM2dQZXBURVdWN2VhIn0.eyJzY29wZSI6WyJkYXRhOnJlYWQiLCJkYXRhOndyaXRlIiwiZGF0YTpjcmVhdGUiLCJkYXRhOnNlYXJjaCIsImJ1Y2tldDpjcmVhdGUiLCJidWNrZXQ6cmVhZCIsImJ1Y2tldDp1cGRhdGUiLCJidWNrZXQ6ZGVsZXRlIl0sImNsaWVudF9pZCI6IkxybjZvcUxud3BDQmQ4R1MwTHVpbUd4NVNIT05ZdzRiIiwiYXVkIjoiaHR0cHM6Ly9hdXRvZGVzay5jb20vYXVkL2Fqd3RleHA2MCIsImp0aSI6IkdmRzNaVmxtWDlzWkM4a0lrV2F5UjFrQXJrekdXVHQ3OXdaczlzckoycTZBZ0h1d1hBU3YzanRYcDlFSlJUZ04iLCJleHAiOjE2NDEwODQzNzV9.Oq7XE3bDoG1m1T9ZBUcRSCYZy_FJPv7jwho9uypdIv3aHgM7DVONQLIQ5a3PdZkdFzxImO5C4x_RRJyFCaJwXDsvcgW8jR_mTzNqbNQU1kfIeDrMMT5-bc0Tgt1galK9NMBXrM9eeUsXAcKTF_UY9blnnLY8ef9_8NsRRuVsjSdaZ7UK3H6qx1lWIChCuGqgi3ODwgJuv9fyv3GO0a1ULIDVLTuOwp5NowqsAWXtf7BpB0veEpnaTwPsvLU72hxJV3ar-9QJ8gJI8aZ7F-055asuPVvbTKRtWQIQGwGSOUHwePX16R-elEUP3QvjpwG8P9FUMjPZQryuCP3PmPw6ng");
            //dXJuOmFkc2sud2lwcHJvZDpmcy5maWxlOnZmLmlPbG9fOEJSU2JDODh6ZmxaaUpxVXc / dmVyc2lvbj0
            //webControl1.WebView.LoadUrl(@"file:///c:/HTML2/Viewer.html?URN=dXJuOmFkc2sud2lwcHJvZDpmcy5maWxlOnZmLmJUQlZFNDl1VFlDcm8tT3gxRVA1bkE/dmVyc2lvbj0x&Token=eyJhbGciOiJSUzI1NiIsImtpZCI6IlU3c0dGRldUTzlBekNhSzBqZURRM2dQZXBURVdWN2VhIn0.eyJzY29wZSI6WyJkYXRhOnJlYWQiLCJkYXRhOndyaXRlIiwiZGF0YTpjcmVhdGUiLCJkYXRhOnNlYXJjaCIsImJ1Y2tldDpjcmVhdGUiLCJidWNrZXQ6cmVhZCIsImJ1Y2tldDp1cGRhdGUiLCJidWNrZXQ6ZGVsZXRlIl0sImNsaWVudF9pZCI6IkxybjZvcUxud3BDQmQ4R1MwTHVpbUd4NVNIT05ZdzRiIiwiYXVkIjoiaHR0cHM6Ly9hdXRvZGVzay5jb20vYXVkL2Fqd3RleHA2MCIsImp0aSI6InVkT1dSRFV3TGZGSXl5VUFHbWhRUkpaZHlxWmZvSXJnUlFFcmJFOHRqZ1A3N1h6UmRSZFU0ZjVramw2NkpnWlIiLCJleHAiOjE2NDMyOTc2Nzh9.Uj1PnIL-LounXAnHM_LnGJns9EqBQP6nWC7LJ0O5CEt7Xal_p6eG79CIPdBQvgroESby27OV10ZUHP2PNw5NyQscc4HhN8Edc040LOMKcacyqP9HX1HQ2_k66JvD4FMNRw4QOWoQtH_ToytbHwBy8awJ7SaUbvOkOtnje3qRzjgNB-B2FCrKLX9J_AFaEHui6yPzXpz9ouWo8bM-oFQfVZq-cFQMD48_DhAhXEWV30af6sg3KLc-4QKHrAta2_GMtlWfASjps57N-3Fn0FL5hXpHraHbKv5lvZuyAI1QGLM-rcCsrd1Fs6jiSNBk8kT-rQrGW0FWWPVrowzcoE-iCA");
            webControl1.WebView.LoadUrl(@"file:///c:/HTML2/Viewer.html?URN=dXJuOmFkc2sud2lwcHJvZDpmcy5maWxlOnZmLm1nRmtOWmdmUTJ5dDBmLUxPeVlDUlE/dmVyc2lvbj0x&Token=eyJhbGciOiJSUzI1NiIsImtpZCI6IjY0RE9XMnJoOE9tbjNpdk1NU0xlNGQ2VHEwUV9SUzI1NiIsInBpLmF0bSI6Ijd6M2gifQ.eyJzY29wZSI6WyJkYXRhOnJlYWQiLCJkYXRhOndyaXRlIiwiZGF0YTpjcmVhdGUiLCJkYXRhOnNlYXJjaCIsImJ1Y2tldDpjcmVhdGUiLCJidWNrZXQ6cmVhZCIsImJ1Y2tldDp1cGRhdGUiLCJidWNrZXQ6ZGVsZXRlIl0sImNsaWVudF9pZCI6IkxybjZvcUxud3BDQmQ4R1MwTHVpbUd4NVNIT05ZdzRiIiwiYXVkIjoiaHR0cHM6Ly9hdXRvZGVzay5jb20vYXVkL2Fqd3RleHA2MCIsImp0aSI6InNQZjZ2akVUU0xwbjNEUXZUVXlSdlQ5ZHJIODM3eU12emRXOE9xOUhFbUlSUGd3RHRJeTdJQzZXWGxoeWs5RlUiLCJleHAiOjE3MDg5ODM0MzB9.PDgNllXExpNUo67eH1g1TI3KfIJUf4ZpjF-brAqrMDG6sdghurjdlb_5g8cDBvgcFRTnog4oFphwat-nDysn1SNNX_-_hacKksZW-qnqSEFUJs99b-LJZp_bRg7E8WLSIbdaM2Kn-8XkreN8BWGcVhwWopeeZu3mNEveHR_nwKZMzTVNCgQQ-tD4yEFtYuR2t70uZge8GzneXdXTA7fK-exkGTkNtJCk9rQKTJnfweWbrKlG_M7tyvY8xadS3p0OH1Kuye8ZlRASrVYZ6gQVotFmPwZPwcO7Mh-idn3jDCn8_hUM7Pr5ygp0891xrLwILGaNzZJK1VN-leboF3SBYg");
            EO.WebBrowser.Runtime.AddLicense(
            "3a5rp7PD27FrmaQHEPGs4PP/6KFrqKax2r1GgaSxy5916u34GeCt7Pb26bSG" +
            "prT6AO5p3Nfh5LRw4rrqHut659XO6Lto6u34GeCt7Pb26YxDs7P9FOKe5ff2" +
            "6YxDdePt9BDtrNzCnrWfWZekzRfonNzyBBDInbW1yQKzbam2xvGvcau0weKv" +
            "fLOz/RTinuX39vTjd4SOscufWbPw+g7kp+rp9unMmuX59hefi9jx+h3ks7Oz" +
            "/RTinuX39hC9RoGkscufddjw/Rr2d4SOscufWZekscu7mtvosR/4qdzBs/DO" +
            "Z7rsAxrsnpmkBxDxrODz/+iha6iywc2faLWRm8ufWZfAwAzrpeb7z7iJWZek" +
            "sefuq9vpA/Ttn+ak9QzznrSmyNqxaaa2wd2wW5f3Bg3EseftAxDyeuvBs+I=");
        }

       
        private void btnAuthenticate_Click(object sender, EventArgs e)
        {
            
        }

        private void tabFormControl1_Click(object sender, EventArgs e)
        {

        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            //AccessToken ELEMENTO= new AccessToken();
            //txtAccessToken.Text= ELEMENTO.Token(txtClientId.Text, txtClientSecret.Text).access_token;

            //Globals.ThisAddIn.Application.ThisWorkbook.ActiveSheet.
            /*Excel.Workbook libro = Globals.ThisAddIn.Application.ActiveWorkbook;
            Excel.Worksheet hoja = libro.Worksheets[1];
            hoja.Cells[1, 1] = "  HOLA MUNDO  ";*/
            Excel.Workbook libro = Globals.ThisAddIn.Application.ActiveWorkbook;
            Excel.Worksheet hoja = libro.Worksheets[1];
            //hoja.Cells[2, 1] = e.Arguments[2];
            string ID = hoja.Cells[2, 1].Text as string;
            


            
            webView1.EvalScript("highlightRevit('" + ID + "');");

        }

        private void superTabControl1_SelectedTabChanged(object sender, SuperTabStripSelectedTabChangedEventArgs e)
        {

        }


        string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            var ddd = Convert.ToBase64String(plainTextBytes);
            return Convert.ToBase64String(plainTextBytes);
        }

        string ViewerURN(string urn, string viewableId)
        {
            string respuesta = string.Empty;
            var curiosidad = Base64Encode(urn);
            if (String.IsNullOrEmpty(viewableId))//vista 3D               
                                                 //respuesta = string.Format("file:///HTML/Viewer.html?URN={0}&Token={1}", Base64Encode(urn), txtAccessToken.Text);
                respuesta = string.Format(@"C:\HTML1\Viewer.html?URN={0}&Token={1}", Base64Encode(urn), txtAccessToken.Text);

            else
                // respuesta = string.Format("file:///HTML/Viewer.html?URN={0}&Token={1}&ViewableId={2}", Base64Encode(urn), txtAccessToken.Text, viewableId);
                //respuesta = string.Format(@"C:\HTML1\Viewer.html?URN={0}&Token={1}&ViewableId={2}", Base64Encode(urn), txtAccessToken.Text, viewableId);
                respuesta = string.Format(@"C:\HTML1\Viewer.html?URN={0}&Token={1}&ViewableId={2}", Base64Encode(urn), txtAccessToken.Text, viewableId);
            return respuesta;
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            webView1.EvalScript("handleScriptLoad2();");
        }

        private void groupPanel2_Click(object sender, EventArgs e)
        {

        }

        private void groupPanel1_Click(object sender, EventArgs e)
        {

        }

        private void superTabControlPanel1_Click(object sender, EventArgs e)
        {

        }
    }
}
