using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using VPOS.SQLite;

namespace VPOS
{
    public partial class SettingDialog : Form
    {
        public SettingDialog()
        {
            InitializeComponent();
            LoadSettings();
        }
        private static void GrantAccess(string file)
        {
            bool exists = System.IO.Directory.Exists(file);

            if (exists)
            {
                DirectoryInfo di = System.IO.Directory.CreateDirectory(file);
                Console.WriteLine("The Folder is created Sucessfully");
            }
            else
            {
                Console.WriteLine("The Folder already exists");
            }
            DirectoryInfo dInfo = new DirectoryInfo(file);
            DirectorySecurity dSecurity = dInfo.GetAccessControl();
            dSecurity.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), FileSystemRights.FullControl, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.NoPropagateInherit, AccessControlType.Allow));
            dInfo.SetAccessControl(dSecurity);

        }
        private void LoadSettings()
        {
           // GrantAccess("LocalXMLFile.xml");
            try
            {
                XDocument xmlDoc = XDocument.Load(Connection.XMLFile());
                var servers = from person in xmlDoc.Descendants("Server")
                              select new
                              {
                                  Name = person.Element("Name").Value,
                                  Type = person.Element("Type").Value
                                 
                              };
                foreach (var server in servers)
                {
                    serverTxt.Text = server.Name;
                    typeCbx.Text = server.Type;
                   
                    Helper.ServerName = server.Name;
                    Helper.Type = server.Type;
                   
                }
            }
            catch { }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           // GrantAccess("LocalXMLFile.xml");
            if (typeCbx.Text  =="") {
                typeCbx.BackColor = Color.Red;
                MessageBox.Show("Please select the application type");
                return;
            }

            XElement xml = new XElement("Servers",
           new XElement("Server",
           new XElement("Name", serverTxt.Text),          
              new XElement("Type", typeCbx.Text)
           )
           );

            xml.Save(Connection.XMLFile());
            MessageBox.Show("Information Saved");
            this.DialogResult = DialogResult.OK;
            this.Dispose();
        }
    }
}
