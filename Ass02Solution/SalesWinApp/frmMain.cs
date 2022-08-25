
namespace SalesWinApp; 
public partial class frmMain : Form {
    public bool isAdmin { get; set; }
    public int id { get; set; }

    public frmMain() {
        InitializeComponent();
    }

    private void closeToolStripMenuItem_Click(object sender, EventArgs e) => Application.Exit();

    private void btnMember_Click(object sender, EventArgs e) {
        if(!CheckExistForm("frmMembers")) {
            frmMembers member = new frmMembers() {
                isAdmin = this.isAdmin,
                id = this.id,
            };
            member.MdiParent = this;
            member.Show();
        }
        else {
            ActiveChildForm("frmMembers");
        }
    }

    private void btnProduct_Click(object sender, EventArgs e) {
        if(!CheckExistForm("frmProducts")) {
            frmProducts product = new frmProducts() {
                //isAdmin = this.isAdmin
            };
            product.MdiParent = this;
            product.Show();
        }
        else {
            ActiveChildForm("frmProducts");
        }
    }

    private void btnOrder_Click(object sender, EventArgs e) {
        if (!CheckExistForm("frmOrders")) {
            frmOrders order = new frmOrders() {
                //isAdmin = this.isAdmin,
                //id = this.id
            };
            order.MdiParent = this;
            order.Show();
        }
        else {
            ActiveChildForm("frmOrders");
        }
    }

    private bool CheckExistForm(string name) {
        bool flag = false;

        foreach(Form frm in this.MdiChildren) {
            if(frm.Name == name) {
                flag = true;
                break;
            }
        }

        return flag;
    }

    private void ActiveChildForm(string name) {
        foreach(Form frm in this.MdiChildren) {
            if(frm.Name.Equals(name)) {
                frm.Activate();
                break;
            }
        }
    }
}
