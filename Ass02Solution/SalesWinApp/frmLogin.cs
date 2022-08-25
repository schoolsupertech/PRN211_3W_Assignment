using DataAccess.Repository;
using Nancy.Json;

namespace SalesWinApp; 
public partial class frmLogin : Form {
    private MemberRepository memberRepository = new MemberRepository();
    public bool UserSuccessfullyAuthenticated { get; private set; }
    public bool isAdmin { get; private set; }
    public int id { get; private set; }

    public frmLogin() {
        InitializeComponent();
    }

    private void getAdminAccount(ref string? email, ref string? password) {
        string json = string.Empty;

        try {
            // read json string from file
            using (StreamReader reader = new StreamReader(@"..\..\..\appsettings.json")) {
                json = reader.ReadToEnd();
            }

            JavaScriptSerializer jss = new JavaScriptSerializer();

            // Convert json string to dynamic type
            var obj = jss.Deserialize<dynamic>(json);

            // Get contents
            email = obj["AdminAccount"]["Email"];
            password = obj["AdminAccount"]["Password"];
        }
        catch (Exception ex) {
            MessageBox.Show(ex.Message);
        }
    }

    private bool CheckValidAccount() {
        string email = txtEmail.Text;
        string pass = txtPassword.Text;

        if(email is null || email.Equals("")) {
            MessageBox.Show("Please enter email", "Error");
            txtEmail.Focus();
            return false;
        }
        if(pass is null || pass.Equals("")) {
            MessageBox.Show("Please enter password", "Error");
            txtPassword.Focus();
            return false;
        }

        return true;
    }

    private void btnLog_Click(object sender, EventArgs e) {
        string? Email = null;
        string? Password = null;
        
        // Get contents
        getAdminAccount(ref Email, ref Password);
        UserSuccessfullyAuthenticated = false;

        if(CheckValidAccount()) {
            if (Email.Equals(txtEmail.Text) && Password.Equals(txtPassword.Text)) {
                UserSuccessfullyAuthenticated = true;
                isAdmin = true;
                Close();
            }
            else {
                var members = memberRepository.GetMembers();
                foreach(var member in members) {
                    if(member.Email.Equals(txtEmail.Text) && member.Password.Equals(txtPassword.Text)) {
                        UserSuccessfullyAuthenticated = true;
                        isAdmin = false;
                        id = member.MemberId;
                        Close();
                    }
                }
            }
            if(!UserSuccessfullyAuthenticated) {
                MessageBox.Show("Oops!! Wrong email or password, please try again", "Login Failed");
            }
        }

    }

    private void btnCancel_Click(object sender, EventArgs e) => this.Close();

    protected override bool ProcessCmdKey(ref Message msg, Keys keyData) {
        if (keyData == (Keys.Alt | Keys.P)) {
            txtPassword.SelectAll();
            txtPassword.Focus();
            return true;
        }
        if (keyData == (Keys.Alt | Keys.U)) {
            txtEmail.SelectAll();
            txtEmail.Focus();
            return true;
        }

        return base.ProcessCmdKey(ref msg, keyData);
    }
}