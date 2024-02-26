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
using Newtonsoft.Json.Linq;
using RestSharp;


using Autodesk.Forge.Model;


using System.Net;
using System.Threading;
using EO.WinForm;
using System.IO;
using System.Json;
using Newtonsoft.Json;
using System.Windows;
using Microsoft.AspNet.SignalR.Client;

namespace S10Cuantificacion
{
    public partial class FrmBim360 : Form
    {
        HubConnection connection = null;
        IHubProxy _s10ERPHubProxy = null;



        public string ModeloCargado = "";
        public int EnEspera = 0;
        public string ModeloXCargar = "";

        public string SignalToken = "";

        public string TokenAct="";
        public string UrnCargado = "";
        internal static string tokk = string.Empty;


        public string Token;

        //private static string FORGE_CLIENT_ID;
        //private static string FORGE_CLIENT_SECRET;
        //private static string FORGE_CALLBACK;//= "http://localhost:3000/api/forge/callback/oauth";
        private static Scope[] _scope = new Scope[] { Scope.DataRead, Scope.DataWrite, Scope.AccountRead, Scope.ViewablesRead };
        private static ThreeLeggedApi _threeLeggedApi = new ThreeLeggedApi();
        internal delegate void NewBearerDelegate(dynamic bearer);
        public FrmBim360()
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
        }

        private void FrmBim360_Load(object sender, EventArgs e)
        {
            if (TokenAct == "")
            {
                var signalServer = @"http://200.48.100.203:5030/";
                connection = new HubConnection(signalServer);
                _s10ERPHubProxy = connection.CreateHubProxy("S10ERPHub");
                connection.Headers.Add("AuthType", "1"); //CLIENTE
                connection.Headers.Add("Token", Token);
                connection.Headers.Add("ModuleId", "11");
                ConnectWithRetry();
            }

            if (TokenAct == "") autentificar(); else txtAccessToken.Text = TokenAct;
            timer1.Enabled = true;


           /* double height = SystemParameters.FullPrimaryScreenHeight;
            double width = SystemParameters.FullPrimaryScreenWidth;
            double resolution = height * width;



            this.Height = (int)(height * 0.7);
            this.Width = (int)(width * 0.7);*/
            groupPanel2.Width = 328;
            expandablePanel1.Expanded = true;
            advTree1.Height = (int)(this.Height * 0.30);
            advTree2.Height = (int)(this.Height * 0.30);
            advTree6.Height = (int)(this.Height * 0.20);








            

        }

        //public ChromiumWebBrowser browser;

        /*public void InitBrowser()
        {
            var settings = new CefSettings();
            CefSharpSettings.LegacyJavascriptBindingEnabled = true;
            settings.LogSeverity = LogSeverity.Error;
            Cef.Initialize(settings);
            browser = new ChromiumWebBrowser("file:///HTML/Viewer.html"); // CefSharp needs a initial page...

            browser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                | System.Windows.Forms.AnchorStyles.Left)
                | System.Windows.Forms.AnchorStyles.Right)));
            browser.MinimumSize = new System.Drawing.Size(20, 20);
            browser.Name = "webBrowser1";
            browser.TabIndex = 1;
            browser.Dock = DockStyle.Fill;
            panel1.Controls.Add(browser);
        }*/

        private System.Windows.Forms.Timer _tokenTimer = new System.Windows.Forms.Timer();
        private System.Windows.Forms.Timer _translationTimer = new System.Windows.Forms.Timer();
        private DateTime _expiresAt;


        public async void cargar_datos()
        {

            /*if (string.IsNullOrWhiteSpace(txtClientId.Text) || string.IsNullOrWhiteSpace(txtClientSecret.Text)) return;
            Scope[] scope = new Scope[] { Scope.DataRead, Scope.DataWrite };
            RestClient client = new RestClient("https://developer.api.autodesk.com");
            RestRequest request = new RestRequest("/authentication/v1/authenticate", RestSharp.Method.POST);
            request.AddParameter("client_id", txtClientId.Text);
            request.AddParameter("client_secret", txtClientSecret.Text);
            request.AddParameter("grant_type", "client_credentials");
            request.AddParameter("scope", scope, ParameterType.UrlSegment);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            IRestResponse response = await client.ExecuteTaskAsync(request);
            dynamic responseDynamic = JObject.Parse(response.Content);
            txtAccessToken.Text = responseDynamic.access_token;
            try
            {
                _expiresAt = DateTime.Now.AddSeconds((double)(responseDynamic.expires_in));
                // keep track on time
                _tokenTimer.Tick += new EventHandler(tickTokenTimer);
                _tokenTimer.Interval = 1000;
                _tokenTimer.Enabled = true;
            }
            catch
            {
            }*/


            /*var adap = new OleDbDataAdapter(sql, conex);
            var dt = new DataTable();
            adap.Fill(dt);*/
            //advTree1.BeginUpdate();
            //advTree1.Nodes.Clear();

            //BucketsApi bucketApi = new BucketsApi();
            //bucketApi.Configuration.AccessToken = txtAccessToken.Text;
            Configuration.Default.AccessToken = txtAccessToken.Text;


            //HubsApi hubsApi = new HubsApi();

            //hubsApi.Configuration.AccessToken = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(txtAccessToken.Text));
            //hubsApi.Configuration.AccessToken = txtAccessToken.Text;
            //hubsApi.Configuration.AddApiKey("AccessToken", txtAccessToken.Text);

            //hubsApi.Configuration.Username = txtClientId.Text;
            //hubsApi.Configuration.Password = txtClientSecret.Text;

            //string lastBucket = null;
            //DynamicJsonResponse hubsJson = hubsApi.GetHubs();
            //DynamicDictionary hubs = (DynamicDictionary)hubsJson.Dictionary["data"];
            //DynamicJsonResponse resp = api.GetHubs();


            //foreach (var bucket in hubsJson)
            /*for (int i = 0; i < hubs.Dictionary.Count; i++)
            {
                string hub = hubs.Dictionary.ElementAt(i).ToString();

                var node = new DevComponents.AdvTree.Node();
                //node.Tag = (hub.Id.ToString());
                //node.Text = (hub.Attributes.Name);
                node.Tag = (hub);
                node.Text = (hub);
                //node.Text = (bucket.BucketText);
                node.Image = ImageList1.Images[0];
                node.Cells.Add(new DevComponents.AdvTree.Cell());
                node.Cells.Add(new DevComponents.AdvTree.Cell());
                advTree1.Nodes.Add(node);
                // We will load drive content on demand
                node.ExpandVisibility = DevComponents.AdvTree.eNodeExpandVisibility.Visible;
                node.Expanded = true;



                //TreeNode nodeBucket = new TreeNode(bucket.BucketKey);
                //nodeBucket.Tag = bucket.BucketKey;
                //treeBuckets.Nodes.Add(nodeBucket);
                //lastBucket = bucket.Id; // after the loop, this will contain the last bucketKey
            }*/

            //Hubs allHubs = JsonConvert.DeserializeObject<Hubs>(hubsJson.ToString());


            /*string lastBucket = null;
            Hubs buckets = null;

            do
            {
                //buckets = (await bucketApi.GetBucketsAsync("EMEA", 100, lastBucket)).ToObject<Buckets>();
                
                buckets = (await hubsApi.GetHubsAsync(null)).ToObject<Hubs>();
                foreach (var bucket in buckets.Data)
                {
                    var node = new DevComponents.AdvTree.Node();
                    node.Tag = (bucket.Id.ToString());
                    node.Text = (bucket.Attributes.Name);
                    //node.Text = (bucket.BucketText);
                    node.Image = ImageList1.Images[0];
                    node.Cells.Add(new DevComponents.AdvTree.Cell());
                    node.Cells.Add(new DevComponents.AdvTree.Cell());
                    advTree1.Nodes.Add(node);
                    // We will load drive content on demand
                    node.ExpandVisibility = DevComponents.AdvTree.eNodeExpandVisibility.Visible;
                    node.Expanded = true;



                    //TreeNode nodeBucket = new TreeNode(bucket.BucketKey);
                    //nodeBucket.Tag = bucket.BucketKey;
                    //treeBuckets.Nodes.Add(nodeBucket);
                    lastBucket = bucket.Id; // after the loop, this will contain the last bucketKey
                }
            } while (buckets.Data.Count > 0);
            advTree1.EndUpdate();


            // for each bucket, show the objects
            foreach (DevComponents.AdvTree.Node n in advTree1.Nodes)
                if (n != null) // async?
                    await ShowBucketObjects(n);*/


            //Task<IList<DevComponents.AdvTree.Node>>  lista =  (Task<IList<DevComponents.AdvTree.Node>>) GetTreeNodeAsync("#");
            GetHubsAsync();




        }



        /* private async Task<IList<DevComponents.AdvTree.Node>> GetHubsAsync()
         {
             IList<DevComponents.AdvTree.Node> nodes = new List<DevComponents.AdvTree.Node>();

             HubsApi hubsApi = new HubsApi();
             hubsApi.Configuration.AccessToken = txtAccessToken.Text;

             var hubs = await hubsApi.GetHubsAsync();
             string urn = string.Empty;
             foreach (KeyValuePair<string, dynamic> hubInfo in new DynamicDictionaryItems(hubs.data))
             {
                 string nodeType = "hubs";
                 switch ((string)hubInfo.Value.attributes.extension.type)
                 {
                     case "hubs:autodesk.core:Hub":
                         nodeType = "hubs";
                         break;
                     case "hubs:autodesk.a360:PersonalHub":
                         nodeType = "personalhub";
                         break;
                     case "hubs:autodesk.bim360:Account":
                         nodeType = "bim360hubs";
                         break;
                 }
                 //DevComponents.AdvTree.Node hubNode = new DevComponents.AdvTree.Node(hubInfo.Value.links.self.href, (nodeType == "bim360hubs" ? "BIM 360 Projects" : hubInfo.Value.attributes.name), nodeType, true);
                 DevComponents.AdvTree.Node hubNode = new DevComponents.AdvTree.Node();
                 hubNode.Tag = (hubInfo.Value.links.self.href);
                 hubNode.Text = ((nodeType == "bim360hubs" ? "BIM 360 Projects" : hubInfo.Value.attributes.name));
                 //node.Text = (bucket.BucketText);
                 hubNode.Image = ImageList1.Images[0];


                 nodes.Add(hubNode);
             }

             return nodes;
         }

         */

        private async Task ShowBucketObjects(DevComponents.AdvTree.Node nodeBucket)
        {
            nodeBucket.Nodes.Clear();

            ObjectsApi objects = new ObjectsApi();
            objects.Configuration.AccessToken = txtAccessToken.Text;

            // show objects on the given TreeNode
            BucketObjects objectsList = (await objects.GetObjectsAsync((string)nodeBucket.Tag)).ToObject<BucketObjects>();
            foreach (var objInfo in objectsList.Items)
            {
                DevComponents.AdvTree.Node nodeObject = new DevComponents.AdvTree.Node();
                nodeObject.Tag = objInfo.ObjectKey;
                nodeObject.Text = objInfo.ObjectKey;

                //nodeObject.Tag = ((string)objInfo.ObjectId).Base64Encode();
                nodeBucket.Nodes.Add(nodeObject);
            }
        }







        /*public async void GetTreeNodeAsync(string id)
        //public IList<DevComponents.AdvTree.Node> GetTreeNodeAsync(string id)
        {
            IList<DevComponents.AdvTree.Node> nodes = new List<DevComponents.AdvTree.Node>();
            //Credentials = await Credentials.FromSessionAsync(base.Request.Cookies, Response.Cookies);
            //if (Credentials == null) { return null; }

            //IList<jsTreeNode> nodes = new List<jsTreeNode>();
            if (id == "#") // root
                // Retornamos todos los hub
                return await GetHubsAsync();
            // Retornamos un hub
            //  return await GetHubAsyncId();
            else
            {
                string[] idParams = id.Split('/');
                string resource = idParams[idParams.Length - 2];
                switch (resource)
                {
                    case "hubs": // hubs node selected/expanded, show projects
                        return await GetProjectsAsync(id);
                    case "projects": // projects node selected/expanded, show root folder contents
                        return await GetProjectContents(id);
                    case "folders": // folders node selected/expanded, show folder contents
                        return await GetFolderContents(id);
                    case "items":
                        return await GetItemVersions(id);
                }
            }

            return nodes;
        }*/

        private async void GetHubsAsync()
        {
            IList<DevComponents.AdvTree.Node> nodes = new List<DevComponents.AdvTree.Node>();

            // the API SDK
            HubsApi hubsApi = new HubsApi();
            hubsApi.Configuration.AccessToken = txtAccessToken.Text;
            //Definicion de filtros
            List<string> filtersId = new List<string>() { "a.cGVyc29uYWw6dWUyY2Y1Yjg4" };
            List<string> filtersName = new List<string>() { "Felipe" };
            List<string> filtersType = new List<string>() { "hubs:autodesk.bim360:Account" };
            //var hubs = await hubsApi.GetHubsAsync(null, filtersName, filtersType);
            try
            {
                var hubs = await hubsApi.GetHubsAsync();
foreach (KeyValuePair<string, dynamic> hubInfo in new DynamicDictionaryItems(hubs.data))
            {
                // check the type of the hub to show an icon
                string nodeType = "hubs";
                switch ((string)hubInfo.Value.attributes.extension.type)
                {
                    case "hubs:autodesk.core:Hub":
                        nodeType = "hubs"; // if showing only BIM 360, mark this as 'unsupported'
                        break;
                    case "hubs:autodesk.a360:PersonalHub":
                        nodeType = "personalHub"; // if showing only BIM 360, mark this as 'unsupported'
                        break;
                    case "hubs:autodesk.bim360:Account":
                        nodeType = "bim360Hubs";
                        break;
                }

                // create a treenode with the values
                //DevComponents.AdvTree.Node hubNode = new DevComponents.AdvTree.Node(hubInfo.Value.links.self.href, hubInfo.Value.attributes.name, nodeType, !(nodeType == "unsupported"));
                advTree1.BeginUpdate();
                DevComponents.AdvTree.Node hubNode = new DevComponents.AdvTree.Node();
                hubNode.Tag = (hubInfo.Value.links.self.href);
                hubNode.Text = (hubInfo.Value.attributes.name);
                hubNode.Image = ImageList1.Images[0];
                hubNode.Cells.Add(new DevComponents.AdvTree.Cell());
                hubNode.Cells.Add(new DevComponents.AdvTree.Cell());
                //nodes.Add(hubNode);
                advTree1.Nodes.Add(hubNode);
                advTree1.EndUpdate();
            }
            }
            catch { }

            

            //return nodes;
        }


        private async void GetProjectsAsync(string href, DevComponents.AdvTree.Node padre)
        {
            //IList<DevComponents.AdvTree.Node> nodes = new List<DevComponents.AdvTree.Node>();

            // the API SDK
            ProjectsApi projectsApi = new ProjectsApi();
            projectsApi.Configuration.AccessToken = txtAccessToken.Text;

            // extract the hubId from the href
            string[] idParams = href.Split('/');
            string hubId = idParams[idParams.Length - 1];

            bool continuar = true;// condición pa continuar en el bucle
            int number = 0;//iniciamos el contador
            int limit = 2;//limite de proyectos por página

            //while (continuar)
            //{
            try
            {
                var projects = await projectsApi.GetHubProjectsAsync(hubId, null, null);
                number++;//incrementamos el contador
                         //creamos el DynamicDictionaryItems para poder contar items
                DynamicDictionaryItems mDynamicDictionaryItems = new DynamicDictionaryItems(projects.data);
                if (mDynamicDictionaryItems.Count() < limit)
                {
                    continuar = false;//terminamos el bucle actual, y salimos del while
                }
                foreach (KeyValuePair<string, dynamic> projectInfo in mDynamicDictionaryItems)
                {
                    string mName = projectInfo.Value.attributes.name;
                    //FILTRO LOS PROYECTOS QUE COMIENZAN CON VIVIENDA
                    //if (!mName.StartsWith("Vivienda"))
                    //continue;
                    // check the type of the project to show an icon
                    string nodeType = "projects";
                    switch ((string)projectInfo.Value.attributes.extension.type)
                    {
                        /*case "hubs:autodesk.core:Hub":
                            nodeType = "hubs";
                            break;*/
                        case "projects:autodesk.core:Project":
                            nodeType = "a360projects";
                            break;
                        case "projects:autodesk.bim360:Project":
                            nodeType = "bim360projects";
                            break;
                    }
                    // createcreate a treenode with the values
                    var projectNode = new DevComponents.AdvTree.Node();
                    //DevComponents.AdvTree.Node projectNode = new DevComponents.AdvTree.Node(projectInfo.Value.links.self.href, projectInfo.Value.attributes.name, nodeType, true);                   
                    projectNode.Tag = (projectInfo.Value.links.self.href);
                    projectNode.Text = (projectInfo.Value.attributes.name);
                    projectNode.Image = ImageList1.Images[0];
                    projectNode.Cells.Add(new DevComponents.AdvTree.Cell(nodeType));
                    projectNode.Cells.Add(new DevComponents.AdvTree.Cell());

                    padre.Nodes.Add(projectNode);
                }

            }
            catch { 
            
            }
            
            //}

        }

        private async void GetProjectContents(string href, DevComponents.AdvTree.Node padre)
        {
            IList<DevComponents.AdvTree.Node> nodes = new List<DevComponents.AdvTree.Node>();

            // the API SDK
            ProjectsApi projectApi = new ProjectsApi();
            projectApi.Configuration.AccessToken = txtAccessToken.Text;

            // extract the hubId & projectId from the href
            string[] idParams = href.Split('/');
            string hubId = idParams[idParams.Length - 3];
            string projectId = idParams[idParams.Length - 1];
            try
            {
                var folders = await projectApi.GetProjectTopFoldersAsync(hubId, projectId);
                foreach (KeyValuePair<string, dynamic> folder in new DynamicDictionaryItems(folders.data))
                {
                    if (folder.Value.attributes.name == "Project Files")
                    {

                        var tNode = new DevComponents.AdvTree.Node();
                        tNode.Tag = (folder.Value.links.self.href);
                        tNode.Text = (folder.Value.attributes.name);
                        tNode.Image = ImageList1.Images[0];
                        tNode.Cells.Add(new DevComponents.AdvTree.Cell("folders"));
                        tNode.Cells.Add(new DevComponents.AdvTree.Cell());

                        padre.Nodes.Add(tNode);
                    }

                    //nodes.Add(new DevComponents.AdvTree.Node(folder.Value.links.self.href, folder.Value.attributes.displayName, "folders", true));
                }

            }
            catch { 
            
            }
            //return nodes;
        }

        private async void GetFolderContents(string href, DevComponents.AdvTree.Node padre)
        {
            IList<DevComponents.AdvTree.Node> nodes = new List<DevComponents.AdvTree.Node>();

            // the API SDK
            FoldersApi folderApi = new FoldersApi();
            folderApi.Configuration.AccessToken = txtAccessToken.Text;

            // extract the projectId & folderId from the href
            string[] idParams = href.Split('/');
            string folderId = idParams[idParams.Length - 1];
            string projectId = idParams[idParams.Length - 3];

            // check if folder specifies visible types
            JArray visibleTypes = null;
            try
            {
                dynamic folder = (await folderApi.GetFolderAsync(projectId, folderId)).ToJson();
                if (folder.data.attributes != null && folder.data.attributes.extension != null && folder.data.attributes.extension.data != null && !(folder.data.attributes.extension.data is JArray) && folder.data.attributes.extension.data.visibleTypes != null)
                {
                    visibleTypes = folder.data.attributes.extension.data.visibleTypes;
                    visibleTypes.Add("items:autodesk.bim360:C4RModel"); // C4R models are not returned on visibleTypes, therefore add them here
                }
                List<string> includeHidden = new List<string>() { "false" }; /*{ "false", "true"}*/

                var folderContents = await folderApi.GetFolderContentsAsync(projectId, folderId, null, null, null, null, null);
                // the GET Folder Contents has 2 main properties: data & included (not always available)
                var folderData = new DynamicDictionaryItems(folderContents.data);
                var folderIncluded = (folderContents.Dictionary.ContainsKey("included") ? new DynamicDictionaryItems(folderContents.included) : null);

                // let's start iterating the FOLDER DATA
                foreach (KeyValuePair<string, dynamic> folderContentItem in folderData)
                {
                    // do we need to skip some items? based on the visibleTypes of this folder
                    string extension = folderContentItem.Value.attributes.extension.type;
                    if (extension.IndexOf("Folder") /*any folder*/ == -1 && visibleTypes != null && !visibleTypes.ToString().Contains(extension)) continue;
                    //linea añadida para no distinguir entre las dos topFolders
                    // nodes.Add(new jsTreeNode(folderContentItem.Value.links.self.href, folderContentItem.Value.attributes.displayName, (string)folderContentItem.Value.type, true));

                    // if the type is items:autodesk.bim360:Document we need some manipulation...
                    if (extension.Equals("items:autodesk.bim360:Document"))
                    {
                        // as this is a DOCUMENT, lets interate the FOLDER INCLUDED to get the name (known issue)
                        foreach (KeyValuePair<string, dynamic> includedItem in folderIncluded)
                        {
                            // check if the id match...
                            if (includedItem.Value.relationships.item.data.id.IndexOf(folderContentItem.Value.id) != -1)
                            {
                                // found it! now we need to go back on the FOLDER DATA to get the respective FILE for this DOCUMENT
                                foreach (KeyValuePair<string, dynamic> folderContentItem1 in folderData)
                                {
                                    if (folderContentItem1.Value.attributes.extension.type.IndexOf("File") == -1) continue; // skip if type is NOT File

                                    // check if the sourceFileName match...
                                    if (folderContentItem1.Value.attributes.extension.data.sourceFileName == includedItem.Value.attributes.extension.data.sourceFileName)
                                    {
                                        // ready!

                                        // let's return for the jsTree with a special id:
                                        // itemUrn|versionUrn|viewableId
                                        // itemUrn: used as target_urn to get document issues
                                        // versionUrn: used to launch the Viewer
                                        // viewableId: which viewable should be loaded on the Viewer
                                        // this information will be extracted when the user click on the tree node, see ForgeTree.js:136 (activate_node.jstree event handler)
                                        string treeId = string.Format("{0}|{1}|{2}|{3}",
                                            folderContentItem.Value.id, // item urn
                                            Base64Encode(folderContentItem1.Value.relationships.tip.data.id), // version urn
                                            includedItem.Value.attributes.extension.data.viewableId, // viewableID
                                            includedItem.Value.attributes.versionNumber //número de versión en documentos de la topFolder Planos
                                        );

                                        var tNode = new DevComponents.AdvTree.Node();
                                        tNode.Tag = (treeId);
                                        tNode.Text = (WebUtility.UrlDecode(includedItem.Value.attributes.name));
                                        tNode.Image = ImageList1.Images[1];
                                        tNode.Cells.Add(new DevComponents.AdvTree.Cell());
                                        tNode.Cells.Add(new DevComponents.AdvTree.Cell());

                                        padre.Nodes.Add(tNode);
                                        //nodes.Add(new DevComponents.AdvTree.Node(treeId, WebUtility.UrlDecode(includedItem.Value.attributes.name), "bim360documents", false));
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if ((string)folderContentItem.Value.type == "folders")
                        {
                            var tNode = new DevComponents.AdvTree.Node();
                            tNode.Tag = (folderContentItem.Value.links.self.href);
                            tNode.Text = (folderContentItem.Value.attributes.displayName);
                            tNode.Image = ImageList1.Images[1];
                            tNode.Cells.Add(new DevComponents.AdvTree.Cell((string)folderContentItem.Value.type));
                            tNode.Cells.Add(new DevComponents.AdvTree.Cell());
                            padre.Nodes.Add(tNode);
                        }
                        else
                        {
                            var tNode = new DevComponents.AdvTree.Node();
                            tNode.Tag = (folderContentItem.Value.links.self.href);
                            tNode.Text = (folderContentItem.Value.attributes.displayName);
                            tNode.Image = ImageList1.Images[4];
                            tNode.Cells.Add(new DevComponents.AdvTree.Cell((string)folderContentItem.Value.type));
                            tNode.Cells.Add(new DevComponents.AdvTree.Cell());
                            advTree2.Nodes.Add(tNode);

                        }

                        // non-Plans folder items
                        //nodes.Add(new DevComponents.AdvTree.Node(folderContentItem.Value.links.self.href, folderContentItem.Value.attributes.displayName, (string)folderContentItem.Value.type, true));
                    }
                }


            }
            catch { }


            //return nodes;
        }






        /*    {
        "jsonapi": {"version": "1.0"},
        "links": {
            "self": {
                "href": "https://developer.api.autodesk.com/data/v1/projects/b.123c8e2a-f6f0-4dfe-a97f-be7afb2b88c5/items/urn:adsk.wipprod:dm.lineage:Ue0YFuWHQ1qzAnWAoSCrHw"
            }
        },


        "data": {
            "type": "items",
            "id": "urn:adsk.wipprod:dm.lineage:Ue0YFuWHQ1qzAnWAoSCrHw",
            "attributes": {
                "displayName": "Archivo1.rvt",
                "createTime": "2021-07-20T16:02:38.0000000Z",
                "createUserId": "",
                "createUserName": "",
                "lastModifiedTime": "2021-07-20T16:02:38.0000000Z",
                "lastModifiedUserId": "",
                "lastModifiedUserName": "",
                "hidden": false,
                "reserved": false,
                "extension": {
                    "type": "items:autodesk.bim360:File",
                    "version": "1.0",
                    "schema": {
                        "href": "https://developer.api.autodesk.com/schema/v1/versions/items:autodesk.bim360:File-1.0"
                    },
                    "data": {
                        "sourceFileName": "Archivo1.rvt"
                    }
                }
            },
            "links": {
                "self": {
                    "href": "https://developer.api.autodesk.com/data/v1/projects/b.123c8e2a-f6f0-4dfe-a97f-be7afb2b88c5/items/urn:adsk.wipprod:dm.lineage:Ue0YFuWHQ1qzAnWAoSCrHw"
                },
                "webView": {
                    "href": "https://acc.autodesk.com/docs/files/projects/123c8e2a-f6f0-4dfe-a97f-be7afb2b88c5?folderUrn=urn%3Aadsk.wipprod%3Afs.folder%3Aco.aYsQFprRRzKIda1_wh0TMQ&entityId=urn%3Aadsk.wipprod%3Adm.lineage%3AUe0YFuWHQ1qzAnWAoSCrHw"
                }
            },
            "relationships": {
                "tip": {
                    "data": {
                        "type": "versions",
                        "id": "urn:adsk.wipprod:fs.file:vf.Ue0YFuWHQ1qzAnWAoSCrHw?version=1"
                    },
                    "links": {
                        "related": {
                            "href": "https://developer.api.autodesk.com/data/v1/projects/b.123c8e2a-f6f0-4dfe-a97f-be7afb2b88c5/items/urn:adsk.wipprod:dm.lineage:Ue0YFuWHQ1qzAnWAoSCrHw/tip"
                        }
                    }
                },
                "versions": {
                    "links": {
                        "related": {
                            "href": "https://developer.api.autodesk.com/data/v1/projects/b.123c8e2a-f6f0-4dfe-a97f-be7afb2b88c5/items/urn:adsk.wipprod:dm.lineage:Ue0YFuWHQ1qzAnWAoSCrHw/versions"
                        }
                    }
                },
                "parent": {
                    "data": {
                        "type": "folders",
                        "id": "urn:adsk.wipprod:fs.folder:co.aYsQFprRRzKIda1_wh0TMQ"
                    },
                    "links": {
                        "related": {
                            "href": "https://developer.api.autodesk.com/data/v1/projects/b.123c8e2a-f6f0-4dfe-a97f-be7afb2b88c5/items/urn:adsk.wipprod:dm.lineage:Ue0YFuWHQ1qzAnWAoSCrHw/parent"
                        }
                    }
                },
                "refs": {
                    "links": {
                        "self": {
                            "href": "https://developer.api.autodesk.com/data/v1/projects/b.123c8e2a-f6f0-4dfe-a97f-be7afb2b88c5/items/urn:adsk.wipprod:dm.lineage:Ue0YFuWHQ1qzAnWAoSCrHw/relationships/refs"
                        },
                        "related": {
                            "href": "https://developer.api.autodesk.com/data/v1/projects/b.123c8e2a-f6f0-4dfe-a97f-be7afb2b88c5/items/urn:adsk.wipprod:dm.lineage:Ue0YFuWHQ1qzAnWAoSCrHw/refs"
                        }
                    }
                },
                "links": {
                    "links": {
                        "self": {
                            "href": "https://developer.api.autodesk.com/data/v1/projects/b.123c8e2a-f6f0-4dfe-a97f-be7afb2b88c5/items/urn:adsk.wipprod:dm.lineage:Ue0YFuWHQ1qzAnWAoSCrHw/relationships/links"
                        }
                    }
                }
            }
        },
        "meta": {
            "bim360DmCommandId": "19d63185-b0b9-46e4-ae60-fbb413f08ab6"
        },
        "included": [
            {
                "type": "versions",
                "id": "urn:adsk.wipprod:fs.file:vf.Ue0YFuWHQ1qzAnWAoSCrHw?version=1",
                "attributes": {
                    "name": "Archivo1.rvt",
                    "displayName": "Archivo1.rvt",
                    "createTime": "2021-07-20T16:02:38.0000000Z",
                    "createUserId": "",
                    "createUserName": "",
                    "lastModifiedTime": "2021-07-20T16:02:38.0000000Z",
                    "lastModifiedUserId": "",
                    "lastModifiedUserName": "",
                    "versionNumber": 1,
                    "storageSize": 19128320,
                    "fileType": "rvt",
                    "extension": {
                        "type": "versions:autodesk.bim360:File",
                        "version": "1.0",
                        "schema": {
                            "href": "https://developer.api.autodesk.com/schema/v1/versions/versions:autodesk.bim360:File-1.0"
                        },
                        "data": {
                            "processState": "NEEDS_PROCESSING",
                            "sourceFileName": "Archivo1.rvt"
                        }
                    }
                },
                "links": {
                    "self": {
                        "href": "https://developer.api.autodesk.com/data/v1/projects/b.123c8e2a-f6f0-4dfe-a97f-be7afb2b88c5/versions/urn:adsk.wipprod:fs.file:vf.Ue0YFuWHQ1qzAnWAoSCrHw%3Fversion=1"
                    },
                    "webView": {
                        "href": "https://acc.autodesk.com/docs/files/projects/123c8e2a-f6f0-4dfe-a97f-be7afb2b88c5?folderUrn=urn%3Aadsk.wipprod%3Afs.folder%3Aco.aYsQFprRRzKIda1_wh0TMQ&entityId=urn%3Aadsk.wipprod%3Afs.file%3Avf.Ue0YFuWHQ1qzAnWAoSCrHw%3Fversion%3D1"
                    }
                },
                "relationships": {
                    "item": {
                        "data": {
                            "type": "items",
                                "id": "urn:adsk.wipprod:dm.lineage:Ue0YFuWHQ1qzAnWAoSCrHw"
                                },
                        "links": {
                      "related": {
                              "href": "https://developer.api.autodesk.com/data/v1/projects/b.123c8e2a-f6f0-4dfe-a97f-be7afb2b88c5/versions/urn:adsk.wipprod:fs.file:vf.Ue0YFuWHQ1qzAnWAoSCrHw%3Fversion=1/item"
                            }
                    }
                },
                    "links": {
            "links": {
                "self": {
                    "href": "https://developer.api.autodesk.com/data/v1/projects/b.123c8e2a-f6f0-4dfe-a97f-be7afb2b88c5/versions/urn:adsk.wipprod:fs.file:vf.Ue0YFuWHQ1qzAnWAoSCrHw%3Fversion=1/relationships/links"
                            }
                }
             },
                    "refs": {
            "links": {
                "self": {
                    "href": "https://developer.api.autodesk.com/data/v1/projects/b.123c8e2a-f6f0-4dfe-a97f-be7afb2b88c5/versions/urn:adsk.wipprod:fs.file:vf.Ue0YFuWHQ1qzAnWAoSCrHw%3Fversion=1/relationships/refs"
                            },
                            "related": {
                    "href": "https://developer.api.autodesk.com/data/v1/projects/b.123c8e2a-f6f0-4dfe-a97f-be7afb2b88c5/versions/urn:adsk.wipprod:fs.file:vf.Ue0YFuWHQ1qzAnWAoSCrHw%3Fversion=1/refs"
                            }
                }
            },
                    "downloadFormats": {
            "links": {
                "related": {
                    "href": "https://developer.api.autodesk.com/data/v1/projects/b.123c8e2a-f6f0-4dfe-a97f-be7afb2b88c5/versions/urn:adsk.wipprod:fs.file:vf.Ue0YFuWHQ1qzAnWAoSCrHw%3Fversion=1/downloadFormats"
                            }
                }
            },
                    "derivatives": {
            "data": {
                "type": "derivatives",
                            "id": "dXJuOmFkc2sud2lwcHJvZDpmcy5maWxlOnZmLlVlMFlGdVdIUTFxekFuV0FvU0NySHc_dmVyc2lvbj0x"
                        },
                        "meta": {
                "link": {
                    "href": "https://developer.api.autodesk.com/modelderivative/v2/designdata/dXJuOmFkc2sud2lwcHJvZDpmcy5maWxlOnZmLlVlMFlGdVdIUTFxekFuV0FvU0NySHc_dmVyc2lvbj0x/manifest?scopes=b360project.123c8e2a-f6f0-4dfe-a97f-be7afb2b88c5,O2tenant.17818482"
                            }
            }
        },
                    "thumbnails": {
            "data": {
                "type": "thumbnails",
                            "id": "dXJuOmFkc2sud2lwcHJvZDpmcy5maWxlOnZmLlVlMFlGdVdIUTFxekFuV0FvU0NySHc_dmVyc2lvbj0x"
                        },
                        "meta": {
                "link": {
                    "href": "https://developer.api.autodesk.com/modelderivative/v2/designdata/dXJuOmFkc2sud2lwcHJvZDpmcy5maWxlOnZmLlVlMFlGdVdIUTFxekFuV0FvU0NySHc_dmVyc2lvbj0x/thumbnail?scopes=b360project.123c8e2a-f6f0-4dfe-a97f-be7afb2b88c5,O2tenant.17818482"
                            }
            }
        },
                    "storage": {
            "data": {
                "type": "objects",
                            "id": "urn:adsk.objects:os.object:wip.dm.prod/f261c200-893c-4023-98b9-60591b446218.rvt"
                        },
                        "meta": {
                "link": {
                    "href": "https://developer.api.autodesk.com/oss/v2/buckets/wip.dm.prod/objects/f261c200-893c-4023-98b9-60591b446218.rvt?scopes=b360project.123c8e2a-f6f0-4dfe-a97f-be7afb2b88c5,O2tenant.17818482"
                            }
            }
        }
    }
            }
        ]
    }*/



        public class ObjExtensionData { public string sourceFileName { get; set; } }
        public class ObjSchema { public string href { get; set; } }

        public class ObjExtension
        {
            public string type { get; set; }
            public string version { get; set; }
            public ObjSchema schema { get; set; }
            public ObjExtensionData data { get; set; }
        }

        public class ObjAttributes
        {
            public string displayName { get; set; }
            public string createTime { get; set; }
            public string createUserId { get; set; }

            public string createUserName { get; set; }
            public string lastModifiedTime { get; set; }
            public string lastModifiedUserId { get; set; }
            public string lastModifiedUserName { get; set; }
            public bool hidden { get; set; }
            public bool reserved { get; set; }
            public ObjExtension extension { get; set; }

        }


        public class ObjTipLinks
        {
            public ObjSelf related { get; set; }
        }

        public class ObjTipData
        {
            public string type { get; set; }
            public string id { get; set; } //aqui tengo el urnAddin
        }

        public class ObjTip
        {
            public ObjTipData data { get; set; }
            public ObjTipLinks links { get; set; }
        }


        public class ObjVersions { public ObjTipLinks links { get; set; } }

        public class ObjParent
        {
            public ObjTipData data { get; set; }
            public ObjTipLinks links { get; set; }
        }

        public class ObjRefs
        {
            public ObjSelf self { get; set; }
            public ObjSelf related { get; set; }
        }


        

        public class ObjRelationships
        {
            public ObjTip tip { get; set; }
            public ObjVersions versions { get; set; }
            public ObjParent parent { get; set; }
            public ObjRefs refs { get; set; }
            public ObjLinks links { get; set; }

        }


        //clase principal de data
        public class ObjData { 
            public string type { get; set; }
            public string id { get; set; }
            public ObjAttributes attributes { get; set; }
            public ObjLinks1 links { get; set; }

            public ObjRelationships relationships { get; set; }
            

        }

        public class ObjMeta { public string bim360DmCommandId { get; set; } }

        public class ObjSelf { public string href { get; set; } }
        public class ObjLinks { public ObjSelf self { get; set; } }
        public class ObjLinks1 { public ObjSelf self { get; set; } public ObjSelf webView { get; set; } }
        
        
        
        
        
        
        public class LJsonApiObj
        {
            public LJsonVersion jsonapi { get; set; }
            public ObjLinks links { get; set; }
            public ObjData data { get; set; }
            public ObjMeta meta { get; set; }
            public ObjIncluded[] included { get; set; }

        }



        public class ObjIncluded
        {
            public string type { get; set; }
            public string id { get; set; }
            public ObjAttributes1 attributes { get; set; }
            public ObjLinks1 links { get; set; }

            public ObjRelationships1 relationships { get; set; }

        }





        public class ObjAttributes1
        {
            public string name { get; set; }
            public string displayName { get; set; }
            public string createTime { get; set; }
            public string createUserId { get; set; }
            public string createUserName { get; set; }
            public string lastModifiedTime { get; set; }
            public string lastModifiedUserId { get; set; }
            public string lastModifiedUserName { get; set; }
            public int versionNumber { get; set; }
            public int storageSize { get; set; }
            public string fileType { get; set; }
            public ObjExtension extension { get; set; } // chequear si da problemas
          }


        public class ObjRelationships1
        {
            public ObjData1 item { get; set; }
            public ObjLinks links { get; set; }
            public ObjRefs refs { get; set; }
            public ObjdownloadFormats downloadFormats { get; set; }

            public ObjDerivatives derivatives { get; set; }

        }

        public class ObjDerivatives
        {
            public ObjData data { get; set; }
            public ObjMeta meta { get; set; }

        }

        public class ObjdownloadFormats
        {
            public ObjLinks links { get; set; }
        }

        public class ObjData1
        {
            public ObjData2 data { get; set; }
            public ObjLinks links { get; set; }
        }


        public class ObjData2
        {
            public string type { get; set; }
            public string id { get; set; }
        }


        private const int UPLOAD_CHUNK_SIZE = 2; // Mb


        /*class tipoVersion
         {
          jsonapi: { "version": "1.0" },
          data: {
         "type": "objects",
         "attributes": {
           "name": "Archivo1.rvt"
         },
         "relationships": {
           "target": {
             "data": { "type": "folders", "id": "urn:adsk.wipprod:fs.folder:co.aYsQFprRRzKIda1_wh0TMQ" }
           }
         }
       }
         }*/


        //class RCrear { jsonapi = new { version = "1.0" }, data = new { type = "objects", attributes = new { name = "Archivo2.rvt" }, relationships = new { target = new { data = new { type = "folders", id = "urn:adsk.wipprod:fs.folder:co.aYsQFprRRzKIda1_wh0TMQ" } } } } }

        //"{\"jsonapi\":{\"version\":\"1.0\"},\"data\":{\"type\":\"objects\",\"id\":\"urn:adsk.objects:os.object:wip.dm.prod/f9f4ca31-6d9b-4045-8427-f35e9b134ac9.rvt\",\"relationships\":{\"target\":{\"data\":{\"type\":\"folders\",\"id\":\"urn:adsk.wipprod:fs.folder:co.aYsQFprRRzKIda1_wh0TMQ\"},\"links\":{\"related\":{\"href\":\"https://developer.api.autodesk.com/data/v1/projects/b.123c8e2a-f6f0-4dfe-a97f-be7afb2b88c5/folders/urn:adsk.wipprod:fs.folder:co.aYsQFprRRzKIda1_wh0TMQ\"}}}}}}"
        public class LJsonVersion
        {
            public string version { get; set; }
        }

        public class LJsonattributes
        {
            public string name { get; set; }
        }

        public class LJsonTargetData
        {
            public string type { get; set; }
            public string id { get; set; }
        }

        public class LJsonRelated
        {
            public string href { get; set; }
        }


        public class LJsonTargetLinks
        {
            public LJsonTargetLinks related { get; set; }
        }


        public class LJsonTarget
        {
            public LJsonTargetData data { get; set; }
            public LJsonTargetLinks links { get; set; }
        }

        public class LJsonrelationships
        {
            public LJsonTarget target { get; set; }
        }

        public class LJsonData
        {
            public string type { get; set; }
            public string id { get; set; }
            //public LJsonattributes attributes { get; set; }
            public LJsonrelationships relationships { get; set; }
        }


        public class LJsonObj
        {
            public LJsonVersion jsonapi { get; set; }
            public LJsonData data { get; set; }
            
        }
        


        private async void SubirArchivo(string fileName) //{
        {
            //1 obtenemos informacion del proyecto y carpeta
            //ItemsApi itemApi = new ItemsApi();

            //string fileName = "Archivo4.rvt";

            //string fileName = labelItem1.Text + ".rvt";

            //4 Creamos el objeto
            //var body = new { jsonapi = new { version = "1.0" }, data = new { type = "objects", attributes = new { name = "Archivo2.rvt" }, relationships = new { target = new { data = new { type = "folders", id = "urn:adsk.wipprod:fs.folder:co.aYsQFprRRzKIda1_wh0TMQ" } } } } };
            string body1 = @"{""jsonapi"":{""version"":""1.0""},""data"":{""type"":""objects"",""attributes"":{""name"":""" + fileName.ToString() + @"""},""relationships"":{""target"":{""data"":{""type"":""folders"",""id"":""urn:adsk.wipprod:fs.folder:co.aYsQFprRRzKIda1_wh0TMQ""}}}}}";
            RestClient client = new RestClient("https://developer.api.autodesk.com");
            RestRequest request = new RestRequest("/data/v1/projects/b.123c8e2a-f6f0-4dfe-a97f-be7afb2b88c5/storage", RestSharp.Method.POST);
            request.AddParameter("application/json; charset=utf-8", body1, ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/vnd.api+json");
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Authorization", "Bearer " + TokenAct);
            IRestResponse response = await client.ExecuteAsync(request);
            dynamic responseDynamic = JObject.Parse(response.Content);

            string cadena = response.Content.ToString();
            //JsonValue json = JsonValue.Parse(cadena);
            LJsonObj responseSR = JsonConvert.DeserializeObject<LJsonObj>(cadena);

            //"{\"jsonapi\":{\"version\":\"1.0\"},\"data\":{\"type\":\"objects\",\"id\":\"urn:adsk.objects:os.object:wip.dm.prod/f9f4ca31-6d9b-4045-8427-f35e9b134ac9.rvt\",\"relationships\":{\"target\":{\"data\":{\"type\":\"folders\",\"id\":\"urn:adsk.wipprod:fs.folder:co.aYsQFprRRzKIda1_wh0TMQ\"},\"links\":{\"related\":{\"href\":\"https://developer.api.autodesk.com/data/v1/projects/b.123c8e2a-f6f0-4dfe-a97f-be7afb2b88c5/folders/urn:adsk.wipprod:fs.folder:co.aYsQFprRRzKIda1_wh0TMQ\"}}}}}}"
            //MessageBox.Show(responseSR.data.id.ToString(), "");

            if (responseSR.data is null) {
                System.Windows.Forms.MessageBox.Show("No se cuenta con token de acceso", "");
                return;
            }

            string[] subs = responseSR.data.id.ToString().Split('/');
                string objectKey = Path.GetFileName(subs[1]);
                dynamic miobj = "";
                var a = 5;

                string bucketKey = "wip.dm.prod";
                string itemId = responseSR.data.id;





                var resp = resumableUploadFile(bucketKey, objectKey, TxtModelo.Text);


                var storageId = (responseSR.data.id);

                // 4 CREAMOS LA VERSION

                //string body2 = @"{""jsonapi"":{""version"":""1.0""},""data"":{""type"":""objects"",""attributes"":{""name"":""" + fileName.ToString() + @"""},""relationships"":{""target"":{""data"":{""type"":""folders"",""id"":""urn:adsk.wipprod:fs.folder:co.aYsQFprRRzKIda1_wh0TMQ""}}}}}";
                string body2 = @"{""jsonapi"":{""version"":""1.0""},""data"":{""type"":""items"",""attributes"":{""displayName"":""" + fileName.ToString() + @""",""extension"":{""type"":""items:autodesk.bim360:File"",""version"":""1.0""}},""relationships"":{""tip"":{""data"":{""type"":""versions"",""id"":""1""}},""parent"":{""data"":{""type"":""folders"",""id"":""urn:adsk.wipprod:fs.folder:co.aYsQFprRRzKIda1_wh0TMQ""}}}},""included"":[{""type"":""versions"",""id"":""1"",""attributes"":{""name"":""" + fileName.ToString() + @""",""extension"":{""type"":""versions:autodesk.bim360:File"",""version"":""1.0""}},""relationships"":{""storage"":{""data"":{""type"":""objects"",""id"":""" + storageId.ToString() + @"""}}}}]}";
                RestRequest request1 = new RestRequest("/data/v1/projects/b.123c8e2a-f6f0-4dfe-a97f-be7afb2b88c5/items", RestSharp.Method.POST);
                request1.AddParameter("application/json; charset=utf-8", body2, ParameterType.RequestBody);
                request1.AddHeader("Content-Type", "application/vnd.api+json");
                request1.AddHeader("Accept", "application/json");
                request1.AddHeader("Authorization", "Bearer " + TokenAct);
                IRestResponse response1 = await client.ExecuteAsync(request1);
                dynamic responseDynamic1 = JObject.Parse(response1.Content);

                string cadena1 = response1.Content.ToString();


                LJsonApiObj responseSR1 = JsonConvert.DeserializeObject<LJsonApiObj>(cadena1);
                

                if (responseSR1.included is null)
                {

                    //ya esta cargado
                }
                else
                {
                    TxtUrnAddIn.Text = responseSR1.included[0].id;
                    TxtUrnWeb.Text = responseSR1.included[0].relationships.derivatives.data.id.Replace("_", "/");
                    UrnCargado = TxtUrnAddIn.Text;
                    string CodigoUni = Convert.ToBase64String(Guid.NewGuid().ToByteArray());

                    /*var basdat = new ConexionBD();
                    basdat.Conexion();
                    basdat.GuardarPlano(CodigoUni, labelItem1.Text, TxtModelo.Text, TxtUrnAddIn.Text, TxtUrnWeb.Text, labelItem2.Text);
                    GuardarPlano(CodigoUni, labelItem1.Text, TxtModelo.Text, TxtUrnAddIn.Text, TxtUrnWeb.Text);
                    basdat.Conexion();*/


                
                String cadena88 = TxtUrnAddIn.Text;
                ModeloCargado = TxtUrnAddIn.Text;
                //MessageBox.Show(cadena, "Ejemplo Mensaje Aceptar");
                string urn = ViewerURN(cadena88, "");
                webControl1.WebView.LoadUrl(urn);
                EO.WebBrowser.Runtime.AddLicense(
                "3a5rp7PD27FrmaQHEPGs4PP/6KFrqKax2r1GgaSxy5916u34GeCt7Pb26bSG" +
                "prT6AO5p3Nfh5LRw4rrqHut659XO6Lto6u34GeCt7Pb26YxDs7P9FOKe5ff2" +
                "6YxDdePt9BDtrNzCnrWfWZekzRfonNzyBBDInbW1yQKzbam2xvGvcau0weKv" +
                "fLOz/RTinuX39vTjd4SOscufWbPw+g7kp+rp9unMmuX59hefi9jx+h3ks7Oz" +
                "/RTinuX39hC9RoGkscufddjw/Rr2d4SOscufWZekscu7mtvosR/4qdzBs/DO" +
                "Z7rsAxrsnpmkBxDxrODz/+iha6iywc2faLWRm8ufWZfAwAzrpeb7z7iJWZek" +
                "sefuq9vpA/Ttn+ak9QzznrSmyNqxaaa2wd2wW5f3Bg3EseftAxDyeuvBs+I=");

                timer12.Enabled = true;



            }
            
  
  


            



            //string cadena3 = "";

            
            




            //ItemsApi itemApi = new ItemsApi();
            //itemApi.Configuration.AccessToken = TokenAct;
            //dynamic item = await itemApi.GetItemAsync(TxtIdProyecto.Text, itemId);



            /*itemId = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
            itemId = Microsoft.VisualBasic.Strings.Mid(itemId,1,22);
            itemId = "urn:adsk.wipprod:dm.lineage:" + itemId;
            itemId = "urn:adsk.wipprod:dm.lineage:Ue0YFuWHQ1qzAnWAoSCrHw";

            VersionsApi versionsApis = new VersionsApi();
            //versionsApis.Configuration.AccessToken = credentials.TokenInternal;
            versionsApis.Configuration.AccessToken = TokenAct;
            CreateVersion newVersionData = new CreateVersion
            (
                new JsonApiVersionJsonapi(JsonApiVersionJsonapi.VersionEnum._0),
                new CreateVersionData
                (
                    CreateVersionData.TypeEnum.Versions,
                    new CreateStorageDataAttributes
                    (
                    fileName,
                    new BaseAttributesExtensionObject
                    (
                        "versions:autodesk.bim360:File",
                        "1.0",
                        new JsonApiLink(string.Empty),
                        null
                    )
                    ),
                    new CreateVersionDataRelationships
                    (
                    new CreateVersionDataRelationshipsItem
                    (
                    new CreateVersionDataRelationshipsItemData
                    (
                    CreateVersionDataRelationshipsItemData.TypeEnum.Items,
                    itemId
                    )
                    ),
                    new CreateItemRelationshipsStorage
                    (
                        new CreateItemRelationshipsStorageData
                        (
                        CreateItemRelationshipsStorageData.TypeEnum.Objects,
                        storageId
                        )
                    )
                    )
                )
            );
            dynamic newVersion = await versionsApis.PostVersionAsync(TxtIdProyecto.Text, newVersionData);*/



        }

        private async void subir2(string bucketKey, string objectName, string filePath, string objectKey)
        {

            //string bucketKey = treeBuckets.SelectedNode.Text;

            // ask user to select file
           

            ObjectsApi objects = new ObjectsApi();

            objects.Configuration.AccessToken = txtAccessToken.Text;

            // get file size
            long fileSize = (new FileInfo(filePath)).Length;

            // show progress bar for upload
            /*progressBar.DisplayStyle = ProgressBarDisplayText.CustomText;
            progressBar.Show();
            progressBar.Value = 0;
            progressBar.Minimum = 0;
            progressBar.CustomText = "Preparing to upload file...";*/

            // decide if upload direct or resumable (by chunks)
            if (fileSize > UPLOAD_CHUNK_SIZE * 1024 * 1024) // upload in chunks
            {
                long chunkSize = 2 * 1024 * 1024; // 2 Mb
                long numberOfChunks = (long)Math.Round((double)(fileSize / chunkSize)) + 1;

                //progressBar.Maximum = (int)numberOfChunks;

                long start = 0;
                chunkSize = (numberOfChunks > 1 ? chunkSize : fileSize);
                long end = chunkSize;
                string sessionId = Guid.NewGuid().ToString();

                // upload one chunk at a time
                using (BinaryReader reader = new BinaryReader(new FileStream(filePath, FileMode.Open)))
                {
                    for (int chunkIndex = 0; chunkIndex < numberOfChunks; chunkIndex++)
                    {
                        string range = string.Format("bytes {0}-{1}/{2}", start, end, fileSize);

                        long numberOfBytes = chunkSize + 1;
                        byte[] fileBytes = new byte[numberOfBytes];
                        MemoryStream memoryStream = new MemoryStream(fileBytes);
                        reader.BaseStream.Seek((int)start, SeekOrigin.Begin);
                        int count = reader.Read(fileBytes, 0, (int)numberOfBytes);
                        memoryStream.Write(fileBytes, 0, (int)numberOfBytes);
                        memoryStream.Position = 0;

                        dynamic chunkUploadResponse = await objects.UploadChunkAsync(bucketKey, objectKey, (int)numberOfBytes, range, sessionId, memoryStream);

                        start = end + 1;
                        chunkSize = ((start + chunkSize > fileSize) ? fileSize - start - 1 : chunkSize);
                        end = start + chunkSize;

                        //progressBar.CustomText = string.Format("{0} Mb uploaded...", (chunkIndex * chunkSize) / 1024 / 1024);
                        //progressBar.Value = chunkIndex;
                    }
                }
            }
            else // upload in a single call
            {
                using (StreamReader streamReader = new StreamReader(filePath))
                {
                   // progressBar.Value = 50; // random...
                    //progressBar.Maximum = 100;
                    dynamic uploadedObj = await objects.UploadObjectAsync(bucketKey,
                           objectKey, (int)streamReader.BaseStream.Length, streamReader.BaseStream,
                           "application/octet-stream");
                }

            }

        }

        //private static dynamic resumableUploadFile(string bucketKey, string objectName, string FILE_PATH)
        private dynamic resumableUploadFile(string bucketKey, string objectName, string FILE_PATH)
        {
            Console.WriteLine("*****begin uploading large file");
            //string FILE_PATH = @"C:\Planos\edificio colinas\EdificioColinas.rvt";
            string path = FILE_PATH;
            if (!File.Exists(path))
                path = @"..\..\..\" + FILE_PATH;


            
            groupPanel6.Visible=true;
            groupPanel6.Refresh();

            progressBar1.Value = 0;
            progressBar1.Minimum = 0;
            


            ObjectsApi objects = new ObjectsApi();

            //total size of file        
            long fileSize = new System.IO.FileInfo(path).Length;
            //size of piece, say 2M    
            long chunkSize = 2 * 1024 * 1024;
            //pieces count
            long nbChunks = (long)Math.Round(0.5 + (double)fileSize / (double)chunkSize);
            //record a global response for next function. 
            ApiResponse<dynamic> finalRes = null;
            try
            {
                using (FileStream streamReader = new FileStream(path, FileMode.Open))
                {
                    //unique id of this session
                    //string sessionId = RandomString(12);

                    progressBar1.Maximum = (int)nbChunks;

                    string sessionId = Guid.NewGuid().ToString();
                    for (int i = 0; i < nbChunks; i++)
                    {
                        //start binary position of one certain piece 
                        long start = i * chunkSize;
                        //end binary position of one certain piece 
                        //if the size of last piece is bigger than  total size of the file, end binary 
                        // position will be the end binary position of the file 
                        long end = Math.Min(fileSize, (i + 1) * chunkSize) - 1;

                        //tell Forge about the info of this piece
                        string range = "bytes " + start + "-" + end + "/" + fileSize;
                        // length of this piece
                        long length = end - start + 1;

                        //read the file stream of this piece
                        byte[] buffer = new byte[length];
                        MemoryStream memoryStream = new MemoryStream(buffer);

                        int nb = streamReader.Read(buffer, 0, (int)length);
                        memoryStream.Write(buffer, 0, nb);
                        memoryStream.Position = 0;

                        //upload the piece to Forge bucket
                        ApiResponse<dynamic> response = objects.UploadChunkWithHttpInfo(bucketKey,
                                objectName, (int)length, range, sessionId, memoryStream,
                                "application/octet-stream");

                        finalRes = response;
                        progressBar1.Value = i;
                        progressBar1.Refresh();
                        if (response.StatusCode == 202)
                        {
                            Console.WriteLine("one certain piece has been uploaded");
                            continue;
                        }
                        else if (response.StatusCode == 200)
                        {
                            Console.WriteLine("the last piece has been uploaded");
                        }
                        else
                        {
                            //any error
                            Console.WriteLine(response.StatusCode);
                            break;

                        }
                    }

                }
            }
            catch {
                System.Windows.Forms.MessageBox.Show("No se tiene acceso al archivo, esta siendo ocupado en otro proceso", "Error");
            }
            
            progressBar1.Refresh();
            groupPanel6.Visible = false;
            groupPanel6.Refresh();

            return (finalRes);
        }




        private async void GetItemVersions(string href)
        {
            IList<DevComponents.AdvTree.Node> nodes = new List<DevComponents.AdvTree.Node>();

            // the API SDK
            ItemsApi itemApi = new ItemsApi();
            itemApi.Configuration.AccessToken = txtAccessToken.Text;
            
            // extract the projectId & itemId from the href
            string[] idParams = href.Split('/');
            string itemId = idParams[idParams.Length - 1];
            string projectId = idParams[idParams.Length - 3];
            try
            {
                var versions = await itemApi.GetItemVersionsAsync(projectId, itemId, null, null, null/*, new List<int?>() { 1, 2 }*/);
                foreach (KeyValuePair<string, dynamic> version in new DynamicDictionaryItems(versions.data))
                {
                    DateTime versionDate = version.Value.attributes.lastModifiedTime;
                    
                    string verNum = "";
                    /*try
                    {
                        verNum = version.Value.id.Split("=")[1];
                    }
                    catch {
                        verNum = "";
                    }*/
                    string userName = version.Value.attributes.lastModifiedUserName;
                    verNum = (advTree6.Nodes.Count + 1).ToString();

                    string urn = string.Empty;
                    try { urn = (string)version.Value.relationships.derivatives.data.id; }
                    catch { urn = Base64Encode(version.Value.id); } // some BIM 360 versions don't have viewable



                    var tNode = new DevComponents.AdvTree.Node();
                    tNode.Tag = (urn);
                    tNode.Text = string.Format("Versión: {0}: {1}", verNum, versionDate.ToString("dd/MM/yy HH:mm:ss"));



                    //tNode.Text = "Versión: " + verNum.ToString();
                    //nodes.Add(tNode);

                    /*DevComponents.AdvTree.Node node = new DevComponents.AdvTree.Node(
                        urn,
                        string.Format("Versión: {0}: {1}", verNum, versionDate.ToString("dd/MM/yy HH:mm:ss")/*, userName*///),
                                                                                                                          //"versions",
                                                                                                                          //false);



                    advTree6.Nodes.Add(tNode);
                    // break;
                }

            }
            catch { }
            

            // return nodes;
        }


        private async void autentificar()
        {
            if (string.IsNullOrWhiteSpace(txtClientId.Text) || string.IsNullOrWhiteSpace(txtClientSecret.Text)) return;
            Scope[] scope = new Scope[] { Scope.DataRead, Scope.DataWrite };
            //LLAMADA A LA API. DOCUMENTACION AUTODESK
            //https://forge.autodesk.com/en/docs/oauth/v1/reference/http/authenticate-POST/
            // get the access token
            //https://developer.api.autodesk.com/authentication/v1/authorize
            RestClient client = new RestClient("https://developer.api.autodesk.com");
            RestRequest request = new RestRequest("/authentication/v1/authenticate", RestSharp.Method.POST);
            //RestRequest request = new RestRequest("/authentication/v1/authorize", RestSharp.Method.POST);
            request.AddParameter("client_id", txtClientId.Text);
            request.AddParameter("client_secret", txtClientSecret.Text);
            request.AddParameter("grant_type", "client_credentials");
            //request.AddParameter("refresh_token", "client_credentials");
            //request.AddParameter("scope", "data:read data:write data:create data:search bucket:create bucket:read bucket:update bucket:delete", ParameterType.UrlSegment);
            request.AddParameter("scope", "data:read data:write data:create data:search bucket:create bucket:read bucket:update bucket:delete");
            //request.AddParameter("scope", { Scope.DataRead, Scope.DataWrite }, ParameterType.QueryStringWithoutEncode);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            IRestResponse response = await client.ExecuteTaskAsync(request);
            //TextBoxX1.Text = response.Content.ToString();
            dynamic responseDynamic = JObject.Parse(response.Content);
            txtAccessToken.Text = responseDynamic.access_token;
            TokenAct = txtAccessToken.Text;
            advTree1.Nodes.Clear();
            try
            {
                _expiresAt = DateTime.Now.AddSeconds((double)(responseDynamic.expires_in));
                // keep track on time
                _tokenTimer.Tick += new EventHandler(tickTokenTimer);
                _tokenTimer.Interval = 1000;
                _tokenTimer.Enabled = true;
            }
            catch
            {
            }
        }




        private async void btnAuthenticate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtClientId.Text) || string.IsNullOrWhiteSpace(txtClientSecret.Text)) return;
            Scope[] scope = new Scope[] { Scope.DataRead, Scope.DataWrite };
            //LLAMADA A LA API. DOCUMENTACION AUTODESK
            //https://forge.autodesk.com/en/docs/oauth/v1/reference/http/authenticate-POST/
            // get the access token
            //https://developer.api.autodesk.com/authentication/v1/authorize
            RestClient client = new RestClient("https://developer.api.autodesk.com");
            RestRequest request = new RestRequest("/authentication/v1/authenticate", RestSharp.Method.POST);
            //RestRequest request = new RestRequest("/authentication/v1/authorize", RestSharp.Method.POST);
            request.AddParameter("client_id", txtClientId.Text);
            request.AddParameter("client_secret", txtClientSecret.Text);
            request.AddParameter("grant_type", "client_credentials");
            //request.AddParameter("refresh_token", "client_credentials");
            //request.AddParameter("scope", "data:read data:write data:create data:search bucket:create bucket:read bucket:update bucket:delete", ParameterType.UrlSegment);
            request.AddParameter("scope", "data:read data:write data:create data:search bucket:create bucket:read bucket:update bucket:delete");
            //request.AddParameter("scope", { Scope.DataRead, Scope.DataWrite }, ParameterType.QueryStringWithoutEncode);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");

            IRestResponse response = await client.ExecuteTaskAsync(request);
            dynamic responseDynamic = JObject.Parse(response.Content);
            txtAccessToken.Text = responseDynamic.access_token;


            /*GetAppSetting("FORGE_CLIENT_ID"), GetAppSetting("FORGE_CLIENT_SECRET"),
              "refresh_token", RefreshToken, new Scope[] { Scope.DataRead, Scope.DataCreate, Scope.DataWrite, Scope.ViewablesRead });*/



            /*Configuration.Default.AccessToken = txtAccessToken.Text;
            Configuration.Default
            HubsApi hubsApi = new HubsApi();
            //hubsApi.Configuration.AccessToken = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(txtAccessToken.Text));
            hubsApi.Configuration.AccessToken = txtAccessToken.Text;

            
            dynamic hubsJson = hubsApi.GetHubs();*/

            advTree1.Nodes.Clear();

            //BucketsApi bucketApi = new BucketsApi();
            //bucketApi.Configuration.AccessToken = txtAccessToken.Text;

            // control GetBucket pagination
            /*string lastBucket = null;

            Buckets buckets = null;
            do
            {
                buckets = (await bucketApi.GetBucketsAsync("EMEA", 100, lastBucket)).ToObject<Buckets>();
                foreach (var bucket in buckets.Items)
                {
                    DevComponents.AdvTree.Node nodeBucket = new DevComponents.AdvTree.Node();
                    nodeBucket.Tag = bucket.BucketKey;
                    nodeBucket.Text = bucket.BucketKey;
                    advTree1.Nodes.Add(nodeBucket);
                    lastBucket = bucket.BucketKey; // after the loop, this will contain the last bucketKey
                }
            } while (buckets.Items.Count > 0);

            // for each bucket, show the objects
            foreach (DevComponents.AdvTree.Node n in advTree1.Nodes)
                if (n != null) // async?
                    await ShowBucketObjects(n);

            */

            try
            {
                _expiresAt = DateTime.Now.AddSeconds((double)(responseDynamic.expires_in));
                // keep track on time
                _tokenTimer.Tick += new EventHandler(tickTokenTimer);
                _tokenTimer.Interval = 1000;
                _tokenTimer.Enabled = true;
            }
            catch
            {
            }

        }
        void tickTokenTimer(object sender, EventArgs e)
        {
            // update the time left on the access token
            double secondsLeft = (_expiresAt - DateTime.Now).TotalSeconds;
            txtTimeout.Text = secondsLeft.ToString("0");
            txtTimeout.BackColor = (secondsLeft < 60 ? System.Drawing.Color.Red : System.Drawing.SystemColors.Control);
            if (textosaca != "")
            {
                TxtModelo.Text = textosaca;
                textosaca = "";
            }
        }
        private void BtnRefreshToken_Click(object sender, EventArgs e)
        {
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // required by CefSharp Browser
            //Cef.Shutdown();
        }
        private void btnShowDevTools_Click(object sender, EventArgs e)
        {
            //browser.ShowDevTools();
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
            respuesta = string.Format(@"C:\HTML\Viewer.html?URN={0}&Token={1}", Base64Encode(urn), txtAccessToken.Text);

            else
                // respuesta = string.Format("file:///HTML/Viewer.html?URN={0}&Token={1}&ViewableId={2}", Base64Encode(urn), txtAccessToken.Text, viewableId);
                respuesta = string.Format(@"C:\HTML\Viewer.html?URN={0}&Token={1}&ViewableId={2}", Base64Encode(urn), txtAccessToken.Text, viewableId);
            return respuesta;
        }

        private void ver4Muros_Click(object sender, EventArgs e)
        {
            // ids opbtenidos de cada linea de medicio.
            //en la actualidad se almacena in id de tipo int.
            //debe cambiarse en MenfisBIm a id de tipo string obtenido de GUID
            string[] cadena = new string[] { "f2f023ce-0715-41ca-91d2-c7f799edc7a2-000227b3," +
                "f2f023ce-0715-41ca-91d2-c7f799edc7a2-000227b2," +
                "f2f023ce-0715-41ca-91d2-c7f799edc7a2-000227b0," +
                "f2f023ce-0715-41ca-91d2-c7f799edc7a2-000227b1" };//4 muros
            //browser.ExecuteScriptAsync("highlightRevit", cadena);
        }
        private void ver1Ventana_Click(object sender, EventArgs e)
        {
            // ids opbtenidos de cada linea de medicio.
            //en la actualidad se almacena in id de tipo int.
            //debe cambiarse en MenfisBIm a id de tipo string obtenido de GUID
            //string[] cadena = new string[] { "08aa1076-cff6-462c-8d1a-b660405acac6-000603db"};//4 muros
            //5210da58 - 95ae - 40fd - af36 - 4ec8ebc2a9b2 - 0006f45b
            //string[] cadena = new string[] { "9c9538fd-af40-4b3d-bd89-f8e4acac1fd8-000525ae" };//4 muros
            string[] cadena = new string[] { "5210da58-95ae-40fd-af36-4ec8ebc2a9b2-0006f45b,9c9538fd-af40-4b3d-bd89-f8e4acac1fd8-000525ae" };//4 muros
            //browser.ExecuteScriptAsync("highlightRevit", cadena);
        }

        public static String textosaca = "";

        #region browser to c#
        public class CallbackObjectForJs
        {
            public void showMessage(List<Object> msg)
            {
                //MessageBox.Show(String.Join(",",msg));
                //TextBox nnn = new TextBox();
                
                
                //BIM360_Menfis.textosaca = String.Join(",", msg);
                
                
                
                //FrmPresupuestos Fr1 = new FrmPresupuestos();
                //Fr1.Show();
                //obj.TextBoxX1.Text = msg.ToString();
                //TextBoxX1.Text=msg.ToString();
            }
        }
        private CallbackObjectForJs _callBackObjectForJs;


        #endregion

        private void BIM360_Menfis_Load(object sender, EventArgs e)
        {
            //styleManager1.ManagerStyle = eStyle.Office2007Blue;


            




            autentificar();
            //Thread.Sleep(10000);
            //cargar_datos();
        }

        private async void buttonX1_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtClientId.Text) || string.IsNullOrWhiteSpace(txtClientSecret.Text)) return;

            // get the access token
            TwoLeggedApi oAuth = new TwoLeggedApi();
            Bearer token = (await oAuth.AuthenticateAsync(
              txtClientId.Text,
              txtClientSecret.Text,
              oAuthConstants.CLIENT_CREDENTIALS,
              new Scope[] { Scope.BucketRead, Scope.BucketCreate, Scope.DataRead, Scope.DataWrite })).ToObject<Bearer>();
            txtAccessToken.Text = token.AccessToken;
            _expiresAt = DateTime.Now.AddSeconds(token.ExpiresIn.Value);

            // keep track on time
            _tokenTimer.Tick += new EventHandler(tickTokenTimer);
            _tokenTimer.Interval = 1000;
            _tokenTimer.Enabled = true;



        }

        private async void buttonX2_Click(object sender, EventArgs e)
        {
            /* if (string.IsNullOrWhiteSpace(txtClientId.Text) || string.IsNullOrWhiteSpace(txtClientSecret.Text)) return;

             // get the access token
             //TwoLeggedApi oAuth = new TwoLeggedApi();
             ThreeLeggedApi oAuth = new ThreeLeggedApi();

             Bearer token = (await oAuth.AuthenticateAsync(
               txtClientId.Text,
               txtClientSecret.Text,
               oAuthConstants.CLIENT_CREDENTIALS,
               new Scope[] { Scope.BucketRead, Scope.BucketCreate, Scope.DataRead, Scope.DataWrite })).ToObject<Bearer>();
             txtAccessToken.Text = token.AccessToken;
             _expiresAt = DateTime.Now.AddSeconds(token.ExpiresIn.Value);

             // keep track on time
             _tokenTimer.Tick += new EventHandler(tickTokenTimer);
             _tokenTimer.Interval = 1000;
             _tokenTimer.Enabled = true;
            */
        }

        private void advTree1_Click(object sender, EventArgs e)
        {

        }

        private void advTree1_DoubleClick(object sender, EventArgs e)
        {


        }

        private void advTree1_NodeDoubleClick(object sender, DevComponents.AdvTree.TreeNodeMouseEventArgs e)
        {

            /* foreach (DevComponents.AdvTree.Node nd in advTree1.Nodes)
             {
                 //Task<IList<DevComponents.AdvTree.Node>> lista1 = (Task<IList<DevComponents.AdvTree.Node>>) GetTreeNodeAsync(nd.Tag.ToString());
                 MessageBox.Show(nd.Tag.ToString(), "Ejemplo Mensaje Aceptar");
             }*/
        }

        private void advTree1_NodeClick(object sender, DevComponents.AdvTree.TreeNodeMouseEventArgs e)
        {
            DevComponents.AdvTree.Node nodoactual = advTree1.SelectedNode;
            if (nodoactual.Parent == null && nodoactual.Nodes.Count == 0)
            {
                GetProjectsAsync(nodoactual.Tag.ToString(), nodoactual);
            }


            advTree2.Nodes.Clear();
            if (nodoactual.Parent == null) return;
            if (nodoactual.Parent != null && nodoactual.Nodes.Count == 0 && nodoactual.Parent.Parent == null)
            {
                GetProjectContents(nodoactual.Tag.ToString(), nodoactual);
                //GetProjectsAsync(nodoactual.Tag.ToString(), nodoactual);
            }

            if (nodoactual.Parent != null && nodoactual.Nodes.Count == 0 && nodoactual.Parent.Parent != null)
            {
                //GetProjectContents(nodoactual.Tag.ToString(), nodoactual);
                GetFolderContents(nodoactual.Tag.ToString(), nodoactual);
            }



        }

        private void advTree2_NodeClick(object sender, DevComponents.AdvTree.TreeNodeMouseEventArgs e)
        {
            advTree6.Nodes.Clear();
            DevComponents.AdvTree.Node nodoactual = advTree2.SelectedNode;
            GetItemVersions(nodoactual.Tag.ToString());
            //MessageBox.Show(nodoactual.Cells[1].Text, "Ejemplo Mensaje Aceptar");
        }

        private void advTree2_Click(object sender, EventArgs e)
        {

        }

        private void advTree6_NodeClick(object sender, DevComponents.AdvTree.TreeNodeMouseEventArgs e)
        {
            DevComponents.AdvTree.Node nodoactual = advTree2.SelectedNode;
            int numActual = advTree6.SelectedNode.Index + 1;
            //string phrase = "The quick brown fox jumps over the lazy dog.";
            string[] words = nodoactual.TagString.Split(':');
            String cadena = "urn:adsk.wipprod:fs.file:vf." + words[4] + "?version=" + numActual.ToString().Trim();
            //MessageBox.Show(cadena, "Ejemplo Mensaje Aceptar");
            string urn = ViewerURN(cadena, "");
            //browser.Load(urn);
            TxtModelo.Text = cadena;
            //TextBoxX1.Text = urn;


        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            //urn:adsk.wipprod:fs.file:vf.Mfz0TALLQqyzhi9MNKIkIg?version=1
            //urn:adsk.wipprod:fs.file:vf.KpDEbOxjTVKNiO7OcKRw5A?version=1
            //urn:adsk.wipprod:fs.file:vf.npRiaqIwQDe8d19EAWfwHg?version=1
            //npRiaqIwQDe8d19EAWfwHg
            //string urn = ViewerURN(textBoxUrn.Text, "");
            //browser.Load(urn);
            cargar_datos();

        }

        private void advTree1_MouseClick(object sender, MouseEventArgs e)
        {
            /*if (advTree1.SelectedNode == null) return;
            if (e.Button == MouseButtons.Right)
            {
                Point pt = new Point();
                pt = Control.MousePosition;
                if (advTree1.SelectedNode.Parent == null)
                {
                    ButtonItem8.Text = "Crear Carpeta en Raiz ";
                }
                else
                {
                    ButtonItem8.Text = "Crear Carpeta en " + advTree1.SelectedNode.Parent.Text;
                }
                ButtonItem17.Text = "Crear SubCarpeta dentro de " + advTree1.SelectedNode.Text;
                ButtonItem18.Text = "Cambiar Nombre a " + advTree1.SelectedNode.Text;
                ButtonItem19.Text = "Eliminar " + advTree1.SelectedNode.Text;
                BtCatalogo.Popup(pt);
            }*/
        }

        private void buttonX4_Click(object sender, EventArgs e)
        {
            //FrmPresupuestos Fr1 = new FrmPresupuestos();
            //Fr1.Show();

        }

        private void buttonX5_Click(object sender, EventArgs e)
        {
            if (TxtModelo.Text == "") return;
            string[] cadena = new string[] { "" + TxtModelo.Text + "" };//4 muros
            //browser.ExecuteScriptAsync("highlightRevit", cadena);

        }

        private void advTree6_Click(object sender, EventArgs e)
        {

        }

        private void buttonX3_Click_1(object sender, EventArgs e)
        {
            //cargar_datos();
        }

        private void advTree1_NodeClick_1(object sender, DevComponents.AdvTree.TreeNodeMouseEventArgs e)
        {
            DevComponents.AdvTree.Node nodoactual = advTree1.SelectedNode;
            if (nodoactual.Parent == null && nodoactual.Nodes.Count == 0)
            {
                GetProjectsAsync(nodoactual.Tag.ToString(), nodoactual);
                nodoactual.Expanded = true;
            }


            advTree2.Nodes.Clear();
            if (nodoactual.Parent == null) return;
            if (nodoactual.Parent != null && nodoactual.Nodes.Count == 0 && nodoactual.Parent.Parent == null)
            {
                GetProjectContents(nodoactual.Tag.ToString(), nodoactual);
                nodoactual.Expanded = true;
                //GetProjectsAsync(nodoactual.Tag.ToString(), nodoactual);
            }

            if (nodoactual.Parent != null && /*nodoactual.Nodes.Count == 0 && */nodoactual.Parent.Parent != null)
            {
                //GetProjectContents(nodoactual.Tag.ToString(), nodoactual);
                nodoactual.Nodes.Clear();
                GetFolderContents(nodoactual.Tag.ToString(), nodoactual);
                nodoactual.Expanded = true;
                //SubirArchivo(nodoactual.Tag.ToString());
            }

        }

        private void advTree2_NodeClick_1(object sender, DevComponents.AdvTree.TreeNodeMouseEventArgs e)
        {
            advTree6.Nodes.Clear();
            DevComponents.AdvTree.Node nodoactual = advTree2.SelectedNode;
            GetItemVersions(nodoactual.Tag.ToString());

            //SubirArchivo(nodoactual.Tag.ToString());

            //MessageBox.Show(nodoactual.Cells[1].Text, "Ejemplo Mensaje Aceptar");
        }

        private void advTree6_NodeClick_1(object sender, DevComponents.AdvTree.TreeNodeMouseEventArgs e)
        {
            DevComponents.AdvTree.Node nodoactual = advTree2.SelectedNode;
            int numActual = advTree6.SelectedNode.Index + 1;
            //string phrase = "The quick brown fox jumps over the lazy dog.";
            if (nodoactual is null) return;
            string[] words = nodoactual.TagString.Split(':');
            String cadena = "urn:adsk.wipprod:fs.file:vf." + words[4] + "?version=" + numActual.ToString().Trim();
            //MessageBox.Show(cadena, "Ejemplo Mensaje Aceptar");
            string urn = ViewerURN(cadena, "");
            //browser.Load(urn);

            //string urn = ViewerURN("urn:adsk.wipprod:fs.file:vf.XxTLQ7HyQJysdVLpg96WmA?version=1", "");
            //bRowser.Load(urn);
            //webControl2.WebView = new EO.WinForm.WebControl().WebView;
            
            //webControl2.WebView.LoadUrl("google.com");
            webControl1.WebView.LoadUrl(urn);
            EO.WebBrowser.Runtime.AddLicense(
            "3a5rp7PD27FrmaQHEPGs4PP/6KFrqKax2r1GgaSxy5916u34GeCt7Pb26bSG" +
            "prT6AO5p3Nfh5LRw4rrqHut659XO6Lto6u34GeCt7Pb26YxDs7P9FOKe5ff2" +
            "6YxDdePt9BDtrNzCnrWfWZekzRfonNzyBBDInbW1yQKzbam2xvGvcau0weKv" +
            "fLOz/RTinuX39vTjd4SOscufWbPw+g7kp+rp9unMmuX59hefi9jx+h3ks7Oz" +
            "/RTinuX39hC9RoGkscufddjw/Rr2d4SOscufWZekscu7mtvosR/4qdzBs/DO" +
            "Z7rsAxrsnpmkBxDxrODz/+iha6iywc2faLWRm8ufWZfAwAzrpeb7z7iJWZek" +
            "sefuq9vpA/Ttn+ak9QzznrSmyNqxaaa2wd2wW5f3Bg3EseftAxDyeuvBs+I=");

            TxtUrnAddIn.Text = cadena;
            TxtUrnWeb.Text = Base64Encode(cadena);
        }

        private void ver1Ventana_Click_1(object sender, EventArgs e)
        {
            //webControl2.WebView.LoadUrl("");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            cargar_datos();

            System.Data.DataTable datosPl = new System.Data.DataTable();
            /*ConexionBD bdatos = new ConexionBD();
            datosPl = bdatos.LPlanos(labelItem1.Text);

            if (datosPl.Rows.Count == 0)
            {
                TxtUrnAddIn.Text = "";
                TxtUrnWeb.Text = "";
            }
            else
            {
                TxtUrnAddIn.Text = datosPl.Rows[0]["UrnAddIn"].ToString().Trim();
                TxtUrnWeb.Text = datosPl.Rows[0]["UrnWeb"].ToString().Trim();
                String cadena = TxtUrnAddIn.Text;
                //MessageBox.Show(cadena, "Ejemplo Mensaje Aceptar");
                string urn = ViewerURN(cadena, "");
                webControl1.WebView.LoadUrl(urn);
                EO.WebBrowser.Runtime.AddLicense(
                "3a5rp7PD27FrmaQHEPGs4PP/6KFrqKax2r1GgaSxy5916u34GeCt7Pb26bSG" +
                "prT6AO5p3Nfh5LRw4rrqHut659XO6Lto6u34GeCt7Pb26YxDs7P9FOKe5ff2" +
                "6YxDdePt9BDtrNzCnrWfWZekzRfonNzyBBDInbW1yQKzbam2xvGvcau0weKv" +
                "fLOz/RTinuX39vTjd4SOscufWbPw+g7kp+rp9unMmuX59hefi9jx+h3ks7Oz" +
                "/RTinuX39hC9RoGkscufddjw/Rr2d4SOscufWZekscu7mtvosR/4qdzBs/DO" +
                "Z7rsAxrsnpmkBxDxrODz/+iha6iywc2faLWRm8ufWZfAwAzrpeb7z7iJWZek" +
                "sefuq9vpA/Ttn+ak9QzznrSmyNqxaaa2wd2wW5f3Bg3EseftAxDyeuvBs+I=");

                timer12.Enabled = true;
            }*/

        }

        private void ButtonItem105_Click(object sender, EventArgs e)
        {

            System.Data.DataTable datosPl = new System.Data.DataTable();
            /*ConexionBD bdatos = new ConexionBD();
            datosPl = bdatos.LPlanos(labelItem1.Text);

            if (datosPl.Rows.Count == 0)
            {
                SubirArchivo(labelItem1.Text + ".rvt");
                
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Este modelo ya se encuentra asociado","Error");
            }*/


            
        }

        private void ButtonItem51_Click(object sender, EventArgs e)
        {
            //textBoxX6.Text = Base64Encode(textBoxX5.Text);
            //textBoxX6.Text = (Guid.Parse(textBoxX5.Text).ToByteArray()).Base64Encode();
            //textBoxX6.Text = Convert.ToBase64String(Guid.Parse(textBoxX5.Text).ToByteArray());
        }

        private void buttonItem2_Click(object sender, EventArgs e)
        {

            if (TxtUrnAddIn.Text == "") {
                System.Windows.Forms.MessageBox.Show("Debe seleccionar un modelo web", "Error");
                return;
            }

            //verificar si ya esta asociado
            System.Data.DataTable datosPl = new System.Data.DataTable();
            /*ConexionBD bdatos = new ConexionBD();
            datosPl = bdatos.LPlanos(labelItem1.Text);

            if (datosPl.Rows.Count == 0)
            {
                string CodigoUni = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                var basdat = new ConexionBD();
                basdat.Conexion();
                basdat.GuardarPlano(CodigoUni, labelItem1.Text, TxtModelo.Text, TxtUrnAddIn.Text, TxtUrnWeb.Text, labelItem2.Text);
                GuardarPlano(CodigoUni, labelItem1.Text, TxtModelo.Text, TxtUrnAddIn.Text, TxtUrnWeb.Text);
                basdat.Conexion();
                UrnCargado = TxtUrnAddIn.Text;
            }
            else
            {
                //System.Windows.Forms.MessageBox.Show("Este modelo ya esta asociado", "Error");

                if (System.Windows.Forms.MessageBox.Show("Este modelo ya esta asociado, desea Reemplazarlo ?", "Advertencia", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    /*bdatos.EliminarConfCalculo(AdvTree7.SelectedNode.Tag.ToString());
                    bdatos.EliminarConfCalculoDetalle2(AdvTree7.SelectedNode.Tag.ToString());
                    AdvTree7.SelectedNode.Visible = false;*/
                    //System.Windows.Forms.MessageBox.Show("Este el el modelo " + datosPl.Rows[0]["CodPlano"].ToString().Trim(), "Error");

                    //string CodigoUni = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                    /*var basdat = new ConexionBD();
                    basdat.Conexion();
                    basdat.GuardarPlano(datosPl.Rows[0]["CodPlano"].ToString().Trim(), labelItem1.Text, TxtModelo.Text, TxtUrnAddIn.Text, TxtUrnWeb.Text, labelItem2.Text);
                    GuardarPlano(datosPl.Rows[0]["CodPlano"].ToString().Trim(), labelItem1.Text, TxtModelo.Text, TxtUrnAddIn.Text, TxtUrnWeb.Text);
                    basdat.Conexion();
                    UrnCargado = TxtUrnAddIn.Text;

                }



            }*/



        }

        private void buttonItem1_Click(object sender, EventArgs e)
        {
            OpenFileDialog formSelectFile = new OpenFileDialog();
            formSelectFile.Multiselect = false;
            if (formSelectFile.ShowDialog() != DialogResult.OK) return;

            //VERIFICAR EXTENSION DE ARCHIVOS

            TxtModelo.Text = formSelectFile.FileName;
            labelItem1.Text = Path.GetFileName(TxtModelo.Text);
            labelItem1.Refresh();
            metroShell1.Refresh();

            TxtUrnAddIn.Text = "";
            TxtUrnWeb.Text = "";


            SubirArchivo(labelItem1.Text);


        }

        private void FrmBim360_Resize(object sender, EventArgs e)
        {
           /* double height = SystemParameters.FullPrimaryScreenHeight;
            double width = SystemParameters.FullPrimaryScreenWidth;
            double resolution = height * width;*/



            //this.Height = (int)(height * 0.7);
            //this.Width = (int)(width * 0.7);
            //groupPanel2.Width = 328;
            //expandablePanel1.Expanded = true;
            advTree1.Height = (int)(this.Height * 0.30);
            advTree2.Height = (int)(this.Height * 0.30);
            advTree6.Height = (int)(this.Height * 0.20);
        }

        private void expandablePanel1_ExpandedChanged(object sender, ExpandedChangeEventArgs e)
        {
            if (expandablePanel1.Expanded == true)
            {
                groupPanel2.Width = 328;
            }
            else { 
                groupPanel2.Width = 40;
            }



        }

        private void timer12_Tick(object sender, EventArgs e)
        {
            timer12.Enabled = false;
            string datoError = "0";
            //string datoError = (string)webControl1.WebView.EvalScript("RetornaErrorCode();");
            try
            {
                datoError = (string)webControl1.WebView.EvalScript(@"CodError");
                EO.WebBrowser.Runtime.AddLicense(
                "3a5rp7PD27FrmaQHEPGs4PP/6KFrqKax2r1GgaSxy5916u34GeCt7Pb26bSG" +
                "prT6AO5p3Nfh5LRw4rrqHut659XO6Lto6u34GeCt7Pb26YxDs7P9FOKe5ff2" +
                "6YxDdePt9BDtrNzCnrWfWZekzRfonNzyBBDInbW1yQKzbam2xvGvcau0weKv" +
                "fLOz/RTinuX39vTjd4SOscufWbPw+g7kp+rp9unMmuX59hefi9jx+h3ks7Oz" +
                "/RTinuX39hC9RoGkscufddjw/Rr2d4SOscufWZekscu7mtvosR/4qdzBs/DO" +
                "Z7rsAxrsnpmkBxDxrODz/+iha6iywc2faLWRm8ufWZfAwAzrpeb7z7iJWZek" +
                "sefuq9vpA/Ttn+ak9QzznrSmyNqxaaa2wd2wW5f3Bg3EseftAxDyeuvBs+I=");

            }
            catch { 
            
            }
            
            //System.Windows.Forms.MessageBox.Show(datoError.ToString(), "");
            if (datoError.Trim() != "0")
            {
                //activar proceso de espera de carga
                ModeloXCargar = ModeloCargado;
                ModeloCargado = "urn:adsk.wipprod:fs.file:vf.kJrrf5s8T1KntOTN3n4zrw?version=1";
                //VERIFICAR SI EL MODELO ESTA EN PROCESO DE CARGA
                string urn = ViewerURN(ModeloCargado, "");
                //bRowser.Load(urn);
                webControl1.WebView.LoadUrl(urn);
                EO.WebBrowser.Runtime.AddLicense(
                "3a5rp7PD27FrmaQHEPGs4PP/6KFrqKax2r1GgaSxy5916u34GeCt7Pb26bSG" +
                "prT6AO5p3Nfh5LRw4rrqHut659XO6Lto6u34GeCt7Pb26YxDs7P9FOKe5ff2" +
                "6YxDdePt9BDtrNzCnrWfWZekzRfonNzyBBDInbW1yQKzbam2xvGvcau0weKv" +
                "fLOz/RTinuX39vTjd4SOscufWbPw+g7kp+rp9unMmuX59hefi9jx+h3ks7Oz" +
                "/RTinuX39hC9RoGkscufddjw/Rr2d4SOscufWZekscu7mtvosR/4qdzBs/DO" +
                "Z7rsAxrsnpmkBxDxrODz/+iha6iywc2faLWRm8ufWZfAwAzrpeb7z7iJWZek" +
                "sefuq9vpA/Ttn+ak9QzznrSmyNqxaaa2wd2wW5f3Bg3EseftAxDyeuvBs+I=");
                //timer12.Enabled = true;
                EnEspera = 1;
                timerEspera.Enabled = true;
            }
        }

        private void timerEspera_Tick(object sender, EventArgs e)
        {
            timerEspera.Enabled = false;
            //string datoError = (string)webControl1.WebView.EvalScript(@"CodError");
            //activar proceso de espera de carga
            ModeloCargado = ModeloXCargar;
            //ModeloCargado = "urn:adsk.wipprod:fs.file:vf.kJrrf5s8T1KntOTN3n4zrw?version=1";
            string urn = ViewerURN(ModeloCargado, "");
            //bRowser.Load(urn);
            try
            {
                webControl1.WebView.LoadUrl(urn);
                EO.WebBrowser.Runtime.AddLicense(
                "3a5rp7PD27FrmaQHEPGs4PP/6KFrqKax2r1GgaSxy5916u34GeCt7Pb26bSG" +
                "prT6AO5p3Nfh5LRw4rrqHut659XO6Lto6u34GeCt7Pb26YxDs7P9FOKe5ff2" +
                "6YxDdePt9BDtrNzCnrWfWZekzRfonNzyBBDInbW1yQKzbam2xvGvcau0weKv" +
                "fLOz/RTinuX39vTjd4SOscufWbPw+g7kp+rp9unMmuX59hefi9jx+h3ks7Oz" +
                "/RTinuX39hC9RoGkscufddjw/Rr2d4SOscufWZekscu7mtvosR/4qdzBs/DO" +
                "Z7rsAxrsnpmkBxDxrODz/+iha6iywc2faLWRm8ufWZfAwAzrpeb7z7iJWZek" +
                "sefuq9vpA/Ttn+ak9QzznrSmyNqxaaa2wd2wW5f3Bg3EseftAxDyeuvBs+I=");

            }
            catch { 
            }


            //textBoxX2.Text = urn;
            //timer12.Enabled = true;
            EnEspera = 0;
            ModeloXCargar = "";
            timer12.Enabled = true;
        }



        private async void GuardarPlano(string CodPlano, string NombreArchivoRvt, string RutaArchivoRvt, string UrnAddIn, string UrnWeb)
        {

            /*var basdat = new ConexionBD();
            basdat.Conexion();
            basdat.GuardarAsociado(Presupuesto_actual, SubPresupuesto_actual, Item_actual, CodAsociado, Categoria, Familia, Tipo, campoFiltro, valorFiltro);
            basdat.Conexion();*/



            RestClient client = new RestClient("http://200.48.100.203:5030/api");
            RestRequest request = new RestRequest("/S10ERP/RequestS10ERPData", RestSharp.Method.POST);
            request.AddParameter("HasOutputParam", "false");
            //request.AddParameter("ObjectName", "dbo.S10_01_SubpresupuestoDetallePlano_Actualizar '" + Presupuesto_actual + "','" + SubPresupuesto_actual + "','" + Item_actual + "','" + CodPlano + "','" + NombreArchivoRvt + "','" + UrnAddIn + "','" + UrnWeb + "','" + EmailUsuario + "'");
            request.AddParameter("ObjectName", "dbo.S10_01_SubpresupuestoDetallePlano_Actualizar '" + CodPlano + "','" + NombreArchivoRvt + "','" + RutaArchivoRvt + "','" + UrnAddIn + "','" + UrnWeb + "','" + labelItem2.Text + "'");
            request.AddParameter("RequestId", "ActPlano");
            request.AddParameter("SignalRConnectionID", SignalToken);
            request.AddParameter("SecurityUserId", "1148");
            request.AddHeader("Token", Token);
            request.AddHeader("ModuleID", "11");
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            IRestResponse response = await client.ExecuteAsync(request);
            dynamic responseDynamic = JObject.Parse(response.Content);
           // textBox1.Text = JObject.Parse(response.Content).ToString();
        }


        /*private async void GuardarPlano(string CodPlano, string NombreArchivoRvt, string UrnAddIn, string UrnWeb)
        {

            /*var basdat = new ConexionBD();
            basdat.Conexion();
            basdat.GuardarAsociado(Presupuesto_actual, SubPresupuesto_actual, Item_actual, CodAsociado, Categoria, Familia, Tipo, campoFiltro, valorFiltro);
            basdat.Conexion();

            RestClient client = new RestClient("http://200.48.100.203:5030/api");
            RestRequest request = new RestRequest("/S10ERP/RequestS10ERPData", RestSharp.Method.POST);
            request.AddParameter("HasOutputParam", "false");
            request.AddParameter("ObjectName", "dbo.S10_01_SubpresupuestoDetallePlano_Actualizar '" + "000" + "','" + "000" + "','" + "000" + "','" + CodPlano + "','" + NombreArchivoRvt + "','" + UrnAddIn + "','" + UrnWeb + "','" + labelItem2.Text + "'");
            request.AddParameter("RequestId", "ActPlano");
            request.AddParameter("SignalRConnectionID", SignalToken);
            request.AddParameter("SecurityUserId", "1148");
            request.AddHeader("Token", Token);
            request.AddHeader("ModuleID", "11");
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            IRestResponse response = await client.ExecuteAsync(request);
            dynamic responseDynamic = JObject.Parse(response.Content);
            //textBox1.Text = JObject.Parse(response.Content).ToString();
        }*/

        public class ResponseExecuteS10ERPData
        {
            public string Data { get; set; }
            public string Key { get; set; }
            public string Name { get; set; }
            public int Part { get; set; }
            public int Count { get; set; }
            public string TargetName { get; set; }
            public string SignalRConnectionID { get; set; }
            public string OutputValue { get; set; }
        }


        public string cadenaObtenida;
        private void ConnectWithRetry()
        {
            connection.Start().ContinueWith(task => {
                if (task.IsFaulted)
                {
                    //log.Error(string.Format("There was an error opening the connection:{0}", task.Exception.GetBaseException()));
                }
                else
                {
                    var idSignal = connection.ConnectionId;
                    //textBox2.Text = idSignal.ToString();
                    SignalToken = idSignal.ToString();
                    //signalstk = idSignal.ToString();
                    #region S10ERPHub
                    System.Windows.Forms.MessageBox.Show(SignalToken, "");

                    _s10ERPHubProxy.On<string>("receiveS10ERPDataResult", (s1) =>
                    {
                        if (!string.IsNullOrEmpty(s1))
                        {
                            ResponseExecuteS10ERPData responseSR = JsonConvert.DeserializeObject<ResponseExecuteS10ERPData>(s1);
                            if (responseSR != null)
                            {
                                switch (responseSR.Name)
                                {
                                    case "Items":
                                        //List<LItems> ListaItems1 = JsonConvert.DeserializeObject<List<LItems>>(responseSR.Data);
                                        //ListaItems.AddRange(ListaItems1);
                                        cadenaObtenida = s1.ToString();
                                        ; break;

                                    default: return;
                                }
                            }
                        }
                    });

                    #endregion

                }

            });


        }




    }
}
