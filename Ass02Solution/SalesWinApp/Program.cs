namespace SalesWinApp; 
internal static class Program {
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main() {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        Application.SetHighDpiMode(HighDpiMode.SystemAware);
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        ApplicationConfiguration.Initialize();
        frmLogin loginForm = new frmLogin();
        Application.Run(loginForm);
        if (loginForm.UserSuccessfullyAuthenticated) {
            MessageBox.Show("User successfully authenticated", "Login Success");
            if (loginForm.isAdmin is true) {
                Application.Run(new frmMain() {
                    isAdmin = true
                });
            }
            else {
                Application.Run(new frmMain() {
                    isAdmin = false,
                    id = loginForm.id
                });
            }
        }
    }
}