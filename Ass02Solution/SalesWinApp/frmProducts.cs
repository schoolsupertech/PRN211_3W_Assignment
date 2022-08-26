using BusinessObject.Models;
using DataAccess.Repository;

namespace SalesWinApp {
    public partial class frmProducts : Form {
        public bool isAdmin { get; set; }
        IProductRepository productRepository = new ProductRepository();
        BindingSource source;


        public frmProducts() {
            InitializeComponent();
        }

        private void frmProductManagements_Load(object sender, EventArgs e) {
            if (isAdmin == false) {
                btnDelete.Enabled = false;
                btnNew.Enabled = false;
                btnFind.Enabled = false;
                btnLoad.Enabled = false;
                btnSearch.Enabled = false;
                txtProductName.Enabled = false;
                txtPrice.Enabled = false;
                txtProductID.Enabled = false;
                txtSearch.Enabled = false;
                txtUnitPrice.Enabled = false;
                txtWeight.Enabled = false;

                dgvMemberList.CellDoubleClick += null;
            }
            else {
                btnDelete.Enabled = false;
                //Register this event to open the frmMemberDetail form that performs updating
                dgvMemberList.CellDoubleClick += dgvMemberList_CellDoubleClick;
            }
        }
        private void dgvMemberList_CellDoubleClick(object sender, DataGridViewCellEventArgs e) {
            frmProductDetails frm = new frmProductDetails {
                Text = "Update product",
                InsertOrUpdate = true,
                ProductInfor = GetProductObject(),
                ProductRepository = productRepository
            };
            if (frm.ShowDialog() == DialogResult.OK) {
                LoadMemberList();
            }
        }
        //Clear text on TextBoxes
        private void ClearText() {
            txtWeight.Text = string.Empty;
            txtUnitPrice.Text = string.Empty;
            txtProductName.Text = string.Empty;
            txtProductID.Text = string.Empty;
        }
        //-----------------------------------------------
        private TblProduct GetProductObject() {
            TblProduct member = null;
            try {
                member = new TblProduct {
                    ProductId = int.Parse(txtProductID.Text),
                    ProductName = txtProductName.Text.ToString(),
                    UnitPrice = decimal.Parse(txtUnitPrice.Text),
                    Weight = txtWeight.Text.ToString()
                };
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message, "Get product");
            }
            return member;
        }

        public void LoadMemberList() {
            var members = productRepository.GetProducts();
            try {
                //The BindingSource component is designed to simplify
                //the process of binding controls to an underlying data source
                source = new BindingSource();

                source.DataSource = members.OrderByDescending(member => member.ProductName);
                txtProductID.DataBindings.Clear();
                txtProductName.DataBindings.Clear();
                txtUnitPrice.DataBindings.Clear();
                txtWeight.DataBindings.Clear();

                txtProductID.DataBindings.Add("Text", source, "ProductId");
                txtProductName.DataBindings.Add("Text", source, "ProductName");
                txtUnitPrice.DataBindings.Add("Text", source, "UnitPrice");
                txtWeight.DataBindings.Add("Text", source, "Weight");

                dgvMemberList.DataSource = null;
                dgvMemberList.DataSource = source;
                if (isAdmin == false) {
                    if (members.Count() == 0) {
                        ClearText();
                        btnDelete.Enabled = false;
                    }
                    else {
                        btnDelete.Enabled = false;
                    }
                }
                else {
                    if (members.Count() == 0) {
                        ClearText();
                        btnDelete.Enabled = false;
                    }
                    else {
                        btnDelete.Enabled = true;
                    }
                }
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message, "Load product list");
            }
        }
        private void btnLoad_Click(object sender, EventArgs e) {
            LoadMemberList();
        }


        private void btnNew_Click(object sender, EventArgs e) {
            frmProductDetails frm = new frmProductDetails {
                Text = "Add product",
                InsertOrUpdate = false,
                ProductRepository = productRepository
            };
            if (frm.ShowDialog() == DialogResult.OK) {
                LoadMemberList();
                //Set focus member inserted
                source.Position = source.Count - 1;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e) {
            try {
                var member = GetProductObject();
                productRepository.DeleteProduct(member.ProductId);
                LoadMemberList();
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message, "Delete a product");
            }
        }

        private void btnClose_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void LoadOneMember() {
            TblProduct member = new TblProduct();
            var members = productRepository.GetProducts();
            try {
                foreach (var i in members) {
                    //The BindingSource omponent is designed to simplify
                    //the process of binding controls to an underlying data source
                    if (i.ProductName.Equals(txtSearch.Text)) {
                        source = new BindingSource();

                        source.DataSource = productRepository.GetProductById(i.ProductId);

                        txtProductID.DataBindings.Clear();
                        txtProductName.DataBindings.Clear();
                        txtUnitPrice.DataBindings.Clear();
                        txtWeight.DataBindings.Clear();

                        txtProductID.DataBindings.Add("Text", source, "ProductId");
                        txtProductName.DataBindings.Add("Text", source, "ProductName");
                        txtUnitPrice.DataBindings.Add("Text", source, "UnitPrice");
                        txtWeight.DataBindings.Add("Text", source, "Weight");


                        dgvMemberList.DataSource = null;
                        dgvMemberList.DataSource = source;
                        break;
                    }
                    else if (i.ProductId.ToString().Equals(txtSearch.Text)) {
                        source = new BindingSource();
                        source.DataSource = productRepository.GetProductById(i.ProductId);
                        txtProductID.DataBindings.Clear();
                        txtProductName.DataBindings.Clear();
                        txtUnitPrice.DataBindings.Clear();
                        txtWeight.DataBindings.Clear();

                        txtProductID.DataBindings.Add("Text", source, "ProductId");
                        txtProductName.DataBindings.Add("Text", source, "ProductName");
                        txtUnitPrice.DataBindings.Add("Text", source, "UnitPrice");
                        txtWeight.DataBindings.Add("Text", source, "Weight");

                        dgvMemberList.DataSource = null;
                        dgvMemberList.DataSource = source;
                    }


                }
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message, "Load product list");
            }
        }
        private void LoadMultiMember() {
            var members = productRepository.GetProducts();
            List<TblProduct> mem = new List<TblProduct>();

            try {
                foreach (var i in members) {
                    //The BindingSource omponent is designed to simplify
                    //the process of binding controls to an underlying data source
                    if (i.ProductName.ToLower().Contains(txtSearch.Text.ToLower())) {
                        mem.Add(i);
                    }
                    else if (i.ProductId.ToString().Equals(txtSearch.Text)) {
                        mem.Add(i);
                    }
                }
                source = new BindingSource();

                source.DataSource = mem;

                txtProductID.DataBindings.Clear();
                txtProductName.DataBindings.Clear();
                txtUnitPrice.DataBindings.Clear();
                txtWeight.DataBindings.Clear();

                txtProductID.DataBindings.Add("Text", source, "ProductId");
                txtProductName.DataBindings.Add("Text", source, "ProductName");
                txtUnitPrice.DataBindings.Add("Text", source, "UnitPrice");
                txtWeight.DataBindings.Add("Text", source, "Weight");


                dgvMemberList.DataSource = null;
                dgvMemberList.DataSource = source;
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message, "Load product list");
            }
        }

        private void btnSearch_Click(object sender, EventArgs e) {
            LoadMultiMember();
        }

        private void FilterMember() {
            decimal price;

            if(!decimal.TryParse(txtPrice.Text, out price)) {
                price = 0;
            }

            TblProduct member = new TblProduct();
            List<TblProduct> filterList = productRepository.GetProductByUnitPrice(price);
            try {
                if (filterList.Count == 0) {
                    MessageBox.Show("No result matched", "Not Found");
                }
                else if (filterList.Count != 0) {
                    source = new BindingSource();
                    source.DataSource = filterList.OrderByDescending(member => member.UnitPrice);
                    txtProductID.DataBindings.Clear();
                    txtProductName.DataBindings.Clear();
                    txtUnitPrice.DataBindings.Clear();
                    txtWeight.DataBindings.Clear();

                    txtProductID.DataBindings.Add("Text", source, "ProductId");
                    txtProductName.DataBindings.Add("Text", source, "ProductName");
                    txtUnitPrice.DataBindings.Add("Text", source, "UnitPrice");
                    txtWeight.DataBindings.Add("Text", source, "Weight");
                    dgvMemberList.DataSource = null;
                    dgvMemberList.DataSource = source;
                }
            }

            catch (Exception ex) {
                MessageBox.Show(ex.Message, "Load product list");
            }
        }

        private void btnFind_Click(object sender, EventArgs e) {
            FilterMember();
        }
    }
}
