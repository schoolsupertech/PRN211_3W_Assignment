using BusinessObject.Models;
using DataAccess.Repository;

namespace SalesWinApp; 
public partial class frmMemberDetails : Form {
    public IMemberRepository MemberRepository { get; set; }
    public bool InsertOrUpdate { get; set; }
    public TblMember MemberInfo { get; set; }

    public frmMemberDetails() {
        InitializeComponent();
    }

    private void frmMemberDetails_Load(object sender, EventArgs e) {
        cboCity.SelectedIndex = 0;
        txtMemberID.Enabled = !InsertOrUpdate;
        if(InsertOrUpdate) {
            // Show member to persorm updating
            txtMemberID.Text = MemberInfo.MemberId.ToString();
            txtMemberName.Text = MemberInfo.CompanyName;
            txtEmail.Text = MemberInfo.Email;
            txtPassword.Text = MemberInfo.Password;
            cboCity.Text = MemberInfo.City;
            cboCountry.Text = MemberInfo.Country;
        }
    }

    private void btnSave_Click(object sender, EventArgs e) {
        try {
            bool flag = false;
            var list = MemberRepository.GetMembers();

            foreach(var i in list) {
                if(txtEmail.Text.Equals(i.Email) && !txtMemberID.Text.Equals(i.MemberId.ToString())) {
                    MessageBox.Show("This email exists. Please check again!", !InsertOrUpdate? "Add a new member" : "Update a member");
                    flag = true;
                }
                if (System.Text.RegularExpressions.Regex.IsMatch(txtEmail.Text, @"^(?!\s*$).+")
                    && System.Text.RegularExpressions.Regex.IsMatch(txtMemberID.Text, @"^(?!\s*$).+")
                    && System.Text.RegularExpressions.Regex.IsMatch(txtMemberName.Text, @"^(?!\s*$).+")
                    && System.Text.RegularExpressions.Regex.IsMatch(cboCity.Text, @"^(?!\s*$).+")
                    && System.Text.RegularExpressions.Regex.IsMatch(cboCountry.Text, @"^(?!\s*$).+")
                    && System.Text.RegularExpressions.Regex.IsMatch(txtPassword.Text, @"^(?!\s*$).+") && flag == false) {
                    var member = new TblMember {
                        MemberId = int.Parse(txtMemberID.Text),
                        CompanyName = txtMemberName.Text,
                        Email = txtEmail.Text,
                        Password = txtPassword.Text,
                        City = cboCity.Text,
                        Country = cboCountry.Text
                    };
                    if(!InsertOrUpdate) {
                        MemberRepository.InsertMember(member);
                    }
                    else {
                        MemberRepository.UpdateMember(member);
                    }
                }
                else {
                    MessageBox.Show("Please double check every fields must bot be null, empty or spaces only!", !InsertOrUpdate ? "Add a new member" : "Update a member");
                }
            }
        }
        catch(Exception ex) {
            MessageBox.Show(ex.Message);
        }
    }

    private void btnCancel_Click(object sender, EventArgs e) {
        Close();
    }
}
