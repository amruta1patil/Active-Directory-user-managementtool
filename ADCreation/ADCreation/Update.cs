using System;
using System.DirectoryServices;
using System.Windows.Forms;

namespace ADCreation
{
    public partial class Update : Form
    {
        public Update()
        {
            InitializeComponent();
            SetupListView();
            lstUsers.SelectedIndexChanged += lstUsers_SelectedIndexChanged;
            btnUpDate.Click += btnUpDate_Click;
        }

        private void modify_Load(object sender, EventArgs e)
        {
            try
            {
                // Setup LDAP path and DirectoryEntry
                string ldapPath = "LDAP://CN=Users,DC=smartnetsoftware,DC=com";
                DirectoryEntry directoryEntry = new DirectoryEntry(ldapPath, "testadmin", "admin@123");

                // Setup DirectorySearcher
                DirectorySearcher searcher = new DirectorySearcher(directoryEntry)
                {
                    Filter = "(objectClass=user)"
                };

                searcher.PropertiesToLoad.Add("samAccountName");
                searcher.PropertiesToLoad.Add("displayName");

                // Get all users
                SearchResultCollection results = searcher.FindAll();
                foreach (SearchResult result in results)
                {
                    string userName = result.Properties["samAccountName"][0].ToString();
                    string displayName = result.Properties.Contains("displayName") && result.Properties["displayName"].Count > 0
                        ? result.Properties["displayName"][0].ToString()
                        : userName;

                    // Add user to ListView
                    ListViewItem item = new ListViewItem(userName);
                    item.SubItems.Add(displayName);
                    lstUsers.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading user list: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupListView()
        {
            // Clear existing columns and items
            lstUsers.Columns.Clear();
            lstUsers.Items.Clear();

            // Add columns
            lstUsers.Columns.Add("User Name", 150);
            lstUsers.Columns.Add("Display Name", 200);

            // Set view to details
            lstUsers.View = View.Details;
            lstUsers.FullRowSelect = true;
            lstUsers.MultiSelect = false;
        }

        private void lstUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstUsers.SelectedItems.Count > 0)
            {
                try
                {
                    // Get selected user
                    ListViewItem selectedItem = lstUsers.SelectedItems[0];
                    string userName = selectedItem.Text;

                    // Construct LDAP path and get user DirectoryEntry
                    string ldapPath = "LDAP://CN=Users,DC=smartnetsoftware,DC=com";
                    DirectoryEntry directoryEntry = new DirectoryEntry(ldapPath, "testadmin", "admin@123");

                    // Setup DirectorySearcher
                    DirectorySearcher searcher = new DirectorySearcher(directoryEntry)
                    {
                        Filter = $"(&(objectClass=user)(samAccountName={userName}))"
                    };

                    searcher.PropertiesToLoad.Add("samAccountName");
                    searcher.PropertiesToLoad.Add("displayName");
                    searcher.PropertiesToLoad.Add("givenName");
                    searcher.PropertiesToLoad.Add("sn");

                    // Find user
                    SearchResult result = searcher.FindOne();
                    if (result != null)
                    {
                        DirectoryEntry userEntry = result.GetDirectoryEntry();

                        // Populate text fields with user details
                        txtUsername.Text = userEntry.Properties["samAccountName"].Value.ToString();
                        txtDisplayName.Text = userEntry.Properties.Contains("displayName") ?
                            userEntry.Properties["displayName"].Value.ToString() : string.Empty;
                        txtFirstName.Text = userEntry.Properties.Contains("givenName") ?
                            userEntry.Properties["givenName"].Value.ToString() : string.Empty;
                        txtLastName.Text = userEntry.Properties.Contains("sn") ?
                            userEntry.Properties["sn"].Value.ToString() : string.Empty;
                    }
                    else
                    {
                        MessageBox.Show("User not found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading user details: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnUpDate_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstUsers.SelectedItems.Count > 0)
                {
                    // Get selected user
                    ListViewItem selectedItem = lstUsers.SelectedItems[0];
                    string userName = selectedItem.Text;

                    // Construct LDAP path and get user DirectoryEntry
                    string ldapPath = "LDAP://CN=Users,DC=smartnetsoftware,DC=com";
                    DirectoryEntry directoryEntry = new DirectoryEntry(ldapPath, "testadmin", "admin@123");

                    // Setup DirectorySearcher
                    DirectorySearcher searcher = new DirectorySearcher(directoryEntry)
                    {
                        Filter = $"(&(objectClass=user)(samAccountName={userName}))"
                    };

                    searcher.PropertiesToLoad.Add("samAccountName");
                    searcher.PropertiesToLoad.Add("displayName");
                    searcher.PropertiesToLoad.Add("givenName");
                    searcher.PropertiesToLoad.Add("sn");

                    // Find user
                    SearchResult result = searcher.FindOne();
                    if (result != null)
                    {
                        DirectoryEntry userEntry = result.GetDirectoryEntry();

                        // Update user properties
                        userEntry.Properties["displayName"].Value = txtDisplayName.Text;
                        userEntry.Properties["givenName"].Value = txtFirstName.Text;
                        userEntry.Properties["sn"].Value = txtLastName.Text;

                        // Optionally update the password
                        if (!string.IsNullOrEmpty(txtPassword.Text))
                        {
                            userEntry.Invoke("SetPassword", new object[] { txtPassword.Text });
                        }

                        // Commit changes
                        userEntry.CommitChanges();
                        MessageBox.Show("User updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("User not found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Please select a user to update.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating user: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
