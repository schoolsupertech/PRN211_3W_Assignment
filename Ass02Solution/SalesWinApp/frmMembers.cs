using BusinessObject.Models;
using DataAccess.Repository;

namespace SalesWinApp {
    public partial class frmMembers : Form {
        public bool isAdmin { get; set; }
        public int id { get; set; }

        private IMemberRepository memberRepository;
        private BindingSource source;

        public frmMembers() {
            memberRepository = new MemberRepository();
            InitializeComponent();
        }

        private void frmMembers_Load(object sender, EventArgs e) {
            if(!isAdmin) {
                btnDelete.Enabled = false;
                btnNew.Enabled = false;

                txtEmail.Enabled = false;
                txtMemberID.Enabled = false;
                txtPassword.Enabled = false;
                cboCity.Enabled = false;
                cboCountry.Enabled = false;
                cboSearchCity.Enabled = false;
                cboSearchCountry.Enabled = false;
                btnFind.Enabled = false;
                txtSearch.Visible = false;
                btnSearch.Enabled = false;
            }
            else {
                // Register this event to open the frmMemberDetail from that performs updating
                dgvMemberList.CellDoubleClick += dgvMemberList_CellDoubleClick;
            }
            LoadMemberList();
        }

        private void dgvMemberList_CellDoubleClick(object sender, DataGridViewCellEventArgs e) {
            frmMemberDetails memberDetails = new frmMemberDetails {
                Text = "Update member",
                InsertOrUpdate = true,
                MemberInfo = GetMemberObject(),
                MemberRepository = memberRepository
            };
            if(memberDetails.ShowDialog() == DialogResult.OK) {
                LoadMemberList();
                // Set focus member updated
                source.Position = source.Count - 1;
            }
        }

        private void ClearText() {
            txtMemberID.Text = String.Empty;
            txtMemberName.Text = String.Empty;
            txtEmail.Text = String.Empty;
            txtPassword.Text = String.Empty;
            cboCity.Text = String.Empty;
            cboCountry.Text = String.Empty;
        }

        private TblMember GetMemberObject() {
            TblMember member = null;

            try {
                member = new TblMember() {
                    MemberId = int.Parse(txtMemberID.Text),
                    CompanyName = txtMemberName.Text,
                    Email = txtEmail.Text,
                    Password = txtPassword.Text,
                    Country = cboCountry.Text,
                    City = cboCity.Text
                };
            }
            catch(Exception ex) {
                MessageBox.Show(ex.Message);
            }

            return member;
        }

        public void LoadMemberList() {
            var members = memberRepository.GetMembers();

            try {
                source = new BindingSource();
                if(!isAdmin) {
                    source.DataSource = memberRepository.GetMemberById(id);
                    if(members.Count() == 0) {
                        ClearText();
                        btnDelete.Enabled = false;
                    }
                    else {
                        btnDelete.Enabled = false;
                    }
                }
                else {
                    source.DataSource = members.OrderBy(member => member.Email);
                    if (members.Count() == 0) {
                        ClearText();
                        btnDelete.Enabled = false;
                    }
                    else {
                        btnDelete.Enabled = true;
                    }
                }

                /*
                 * The BindingSource component is designed to simplify the process
                 * of binding controls to an underlying data source
                 */
                txtMemberID.DataBindings.Clear();
                txtEmail.DataBindings.Clear();
                txtMemberName.DataBindings.Clear();
                txtPassword.DataBindings.Clear();
                cboCity.DataBindings.Clear();
                cboCountry.DataBindings.Clear();

                txtMemberID.DataBindings.Add("Text", source, "MemberId");
                txtEmail.DataBindings.Add("Text", source, "Email");
                txtMemberName.DataBindings.Add("Text", source, "CompanyName");
                txtPassword.DataBindings.Add("Text", source, "Password");
                cboCity.DataBindings.Add("Text", source, "City");
                cboCountry.DataBindings.Add("Text", source, "Country");

                dgvMemberList.DataSource = null;
                dgvMemberList.DataSource = source;
            }
            catch(Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnNew_Click(object sender, EventArgs e) {
            frmMemberDetails memberDetails = new frmMemberDetails {
                Text = "Add member",
                InsertOrUpdate = false,
                MemberRepository = memberRepository,
            };
            if(memberDetails.ShowDialog() == DialogResult.OK) {
                LoadMemberList();
                // Set focus member inserted
                source.Position = source.Count - 1;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e) {
            try {
                var member = GetMemberObject();
                memberRepository.DeleteMember(member.MemberId);
                LoadMemberList();
            }
            catch(Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnFind_Click(object sender, EventArgs e) {
            LoadMemberByCityAndCountry();
        }

        private void btnSearch_Click(object sender, EventArgs e) {
            LoadMultiMember();
        }

        private void LoadOneMember() {
            var members = memberRepository.GetMembers();
            
            try {
                foreach(var mem in members) {
                    if(mem.CompanyName.Contains(txtSearch.Text)) {
                        source = new BindingSource();
                        source.DataSource = memberRepository.GetMemberById(mem.MemberId);

                        txtMemberID.DataBindings.Clear();
                        txtEmail.DataBindings.Clear();
                        txtMemberName.DataBindings.Clear();
                        txtPassword.DataBindings.Clear();
                        cboCity.DataBindings.Clear();
                        cboCountry.DataBindings.Clear();

                        txtMemberID.DataBindings.Add("Text", source, "MemberId");
                        txtEmail.DataBindings.Add("Text", source, "Email");
                        txtMemberName.DataBindings.Add("Text", source, "CompanyName");
                        txtPassword.DataBindings.Add("Text", source, "Password");
                        cboCity.DataBindings.Add("Text", source, "City");
                        cboCountry.DataBindings.Add("Text", source, "Country");

                        dgvMemberList.DataSource = null;
                        dgvMemberList.DataSource = source;
                        break;
                    }
                    else if(mem.MemberId.ToString().Equals(txtSearch.Text)) {
                        source = new BindingSource();
                        source.DataSource = memberRepository.GetMemberById(mem.MemberId);

                        txtMemberID.DataBindings.Clear();
                        txtEmail.DataBindings.Clear();
                        txtMemberName.DataBindings.Clear();
                        txtPassword.DataBindings.Clear();
                        cboCity.DataBindings.Clear();
                        cboCountry.DataBindings.Clear();

                        txtMemberID.DataBindings.Add("Text", source, "MemberId");
                        txtEmail.DataBindings.Add("Text", source, "Email");
                        txtMemberName.DataBindings.Add("Text", source, "CompanyName");
                        txtPassword.DataBindings.Add("Text", source, "Password");
                        cboCity.DataBindings.Add("Text", source, "City");
                        cboCountry.DataBindings.Add("Text", source, "Country");
                        
                        dgvMemberList.DataSource = null;
                        dgvMemberList.DataSource = source;
                        break;
                    }
                }
            }
            catch(Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadMultiMember() {
            var members = memberRepository.GetMembers();
            List<TblMember> mems = new List<TblMember>();

            try {
                foreach(var member in members) {
                    if(member.CompanyName.ToLower().Contains(txtSearch.Text.ToLower())) {
                        mems.Add(member);
                    }
                    else if(member.MemberId.ToString().Equals(txtSearch.Text)) {
                        mems.Add(member);
                    }
                }
                source = new BindingSource();
                source.DataSource = mems;

                txtMemberID.DataBindings.Clear();
                txtEmail.DataBindings.Clear();
                txtMemberName.DataBindings.Clear();
                txtPassword.DataBindings.Clear();
                cboCity.DataBindings.Clear();
                cboCountry.DataBindings.Clear();

                txtMemberID.DataBindings.Add("Text", source, "MemberId");
                txtEmail.DataBindings.Add("Text", source, "Email");
                txtMemberName.DataBindings.Add("Text", source, "CompanyName");
                txtPassword.DataBindings.Add("Text", source, "Password");
                cboCity.DataBindings.Add("Text", source, "City");
                cboCountry.DataBindings.Add("Text", source, "Country");

                dgvMemberList.DataSource = null;
                dgvMemberList.DataSource = source;

            }
            catch(Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadMemberByCityAndCountry() {
            TblMember member = new TblMember();
            List<TblMember> filterList = memberRepository.GetMemberByCityAndCountry(cboSearchCity.Text, cboSearchCountry.Text);

            try {
                if(filterList.Count == 0) {
                    MessageBox.Show("No member matched", "No result");
                }
                else {
                    source = new BindingSource();
                    source.DataSource = filterList.OrderByDescending(member => member.CompanyName);

                    txtMemberID.DataBindings.Clear();
                    txtEmail.DataBindings.Clear();
                    txtMemberName.DataBindings.Clear();
                    txtPassword.DataBindings.Clear();
                    cboCity.DataBindings.Clear();
                    cboCountry.DataBindings.Clear();

                    txtMemberID.DataBindings.Add("Text", source, "MemberId");
                    txtEmail.DataBindings.Add("Text", source, "Email");
                    txtMemberName.DataBindings.Add("Text", source, "CompanyName");
                    txtPassword.DataBindings.Add("Text", source, "Password");
                    cboCity.DataBindings.Add("Text", source, "City");
                    cboCountry.DataBindings.Add("Text", source, "Country");

                    dgvMemberList.DataSource = null;
                    dgvMemberList.DataSource = source;
                }
            }
            catch(Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnClose_Click(object sender, EventArgs e) {
            Close();
        }

        private void btnReload_Click(object sender, EventArgs e) {
            LoadMemberList();
        }
    }
}
