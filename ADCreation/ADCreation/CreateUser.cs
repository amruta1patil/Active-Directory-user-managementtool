using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Security.AccessControl;
using System.Security.Principal;
using System;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.IO;
using Newtonsoft.Json;


namespace ADCreation
{
    public partial class CreateUser : Form
    {
        private static string pwdOfNewlyCreatedUser;

        public CreateUser()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void btnCreateUser_Click(object sender, EventArgs e)
        {
           
        }

        private void btnCreateUser_Click_1(object sender, EventArgs e)
        {

            try
            {
                // Load LDAP credentials from JSON file
                var json = File.ReadAllText("Config.json");
                dynamic credentials = JsonConvert.DeserializeObject(json);


                // Extract the LDAP path, username, and password
                string ldapPath = credentials.ldapPath;
                string username = credentials.credentials.username;
                string password = credentials.credentials.password;

                // Create a DirectoryEntry object with the credentials
                DirectoryEntry directoryEntry = new DirectoryEntry(ldapPath, username, password);



                // Active Directory domain and container (OU) where the user will be created
                

              //  string ldapPath = "LDAP://CN=Users,DC=smartnetsoftware,DC=com";



            
               // DirectoryEntry directoryEntry = new DirectoryEntry(ldapPath, "testadmin", "admin@123");


                // Create new user entry
                DirectoryEntry newUser = directoryEntry.Children.Add("CN=" + txtUsername.Text, "user");

                // Set basic user properties
                newUser.Properties["samAccountName"].Value = txtUsername.Text;
                newUser.Properties["userPrincipalName"].Value = txtUsername.Text + "@smartnetsoftware.com";
                //newUser.Properties[""].Value=
                newUser.Properties["displayName"].Value = txtDisplayName.Text;
                newUser.Properties["givenName"].Value = txtFirstName.Text;
                newUser.Properties["sn"].Value = txtLastName.Text;

                // Commit the changes to the AD
                newUser.CommitChanges();

                //// Set user password
                newUser.Invoke("SetPassword", new object[] { txtPassword.Text });

                // Enable the user account
                newUser.Properties["userAccountControl"].Value = 0x200; // Normal account
                newUser.CommitChanges();

                MessageBox.Show("User created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error creating user: " + ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }





        }





























        private void labelDisplayName_Click(object sender, EventArgs e)
        {

        }















        //public static bool CreateUser(string firstName, string lastName, string userLogonName, string employeeID, string emailAddress, string telephone, string address)

        //{

        //    // Creating the PrincipalContext

        //    PrincipalContext principalContext = null;

        //    try

        //    {

        //        //principalContext = new PrincipalContext(ContextType.Domain, "SSPL", "DC=smartnetsoftware,DC=com");
        //        principalContext = new PrincipalContext(ContextType.Domain, "smartnetsoftware", "testadmin", "admin@123");
        //    }

        //    catch (Exception e)

        //    {

        //        MessageBox.Show("Failed to create PrincipalContext. Exception: " + e);

        //        Application.Exit();

        //    }

        //    // Check if user object already exists in the store

        //    UserPrincipal usr = UserPrincipal.FindByIdentity(principalContext, userLogonName);

        //    if (usr != null)

        //    {

        //        MessageBox.Show(userLogonName + " already exists. Please use a different User Logon Name.");

        //        return false;

        //    }

        //    // Create the new UserPrincipal object

        //    UserPrincipal userPrincipal = new UserPrincipal(principalContext);

        //    if (lastName != null && lastName.Length > 0)

        //        userPrincipal.Surname = lastName;

        //    if (firstName != null && firstName.Length > 0)

        //        userPrincipal.GivenName = firstName;

        //    if (employeeID != null && employeeID.Length > 0)

        //        userPrincipal.EmployeeId = employeeID;

        //    if (emailAddress != null && emailAddress.Length > 0)

        //        userPrincipal.EmailAddress = emailAddress;

        //    if (telephone != null && telephone.Length > 0)

        //        userPrincipal.VoiceTelephoneNumber = telephone;

        //    if (userLogonName != null && userLogonName.Length > 0)

        //        userPrincipal.SamAccountName = userLogonName;

        //    pwdOfNewlyCreatedUser = "abcde@@12345!~";

        //    userPrincipal.SetPassword(pwdOfNewlyCreatedUser);

        //    userPrincipal.Enabled = true;

        //    userPrincipal.ExpirePasswordNow();

        //    try

        //    {

        //        userPrincipal.Save();

        //    }

        //    catch (Exception e)

        //    {

        //        MessageBox.Show("Exception creating user object. " + e);

        //        return false;

        //    }

        //    /***************************************************************

        //     *   The below code demonstrates on how you can make a smooth 

        //     *   transition to DirectoryEntry from AccountManagement namespace, 

        //     *   for advanced operations.

        //     ***************************************************************/

        //    if (userPrincipal.GetUnderlyingObjectType() == typeof(DirectoryEntry))

        //    {

        //        DirectoryEntry entry = (DirectoryEntry)userPrincipal.GetUnderlyingObject();

        //        if (address != null && address.Length > 0)

        //            entry.Properties["streetAddress"].Value = address;

        //        try

        //        {

        //            entry.CommitChanges();

        //        }

        //        catch (Exception e)

        //        {

        //            MessageBox.Show("Exception modifying address of the user. " + e);

        //            return false;

        //        }

        //    }

        //    return true;

        //}

    }
}
