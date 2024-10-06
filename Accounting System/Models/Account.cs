using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
    namespace Accounting_System.Models
{
    public class AccountViewModel
    {
        public int AccountID { get; set; }

        [Required(ErrorMessage = "Date is required.")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Credit must be a positive number.")]
        public decimal Credit { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Debit must be a positive number.")]
        public decimal Debit { get; set; }

        [Required(ErrorMessage = "Balance is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Balance must be a positive number.")]
        public decimal Balance { get; set; }
    }

    public class AccountModel
    {
        // Connection string to SQL Server
        private string connectionString = @"Data Source=DESKTOP-60G623S;Initial Catalog=Accountingsystem;Integrated Security=True";

        // Method to Get All Accounts
        public DataTable GetAllAccounts()
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "SELECT AccountID, Date, Description, Credit, Debit, Balance FROM Account";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return dt;
        }

        // Method to Get Account by AccountID
        public DataTable GetAccountById(int accountId)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "SELECT AccountID, Date, Description, Credit, Debit, Balance FROM Account WHERE AccountID = @AccountID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@AccountID", accountId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            return dt;
        }

        // Method to Add a New Account
        public int AddAccount(DateTime date, string description, decimal credit, decimal debit, decimal balance)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "INSERT INTO Account (Date, Description, Credit, Debit, Balance) VALUES (@Date, @Description, @Credit, @Debit, @Balance)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Date", date);
                cmd.Parameters.AddWithValue("@Description", description);
                cmd.Parameters.AddWithValue("@Credit", credit);
                cmd.Parameters.AddWithValue("@Debit", debit);
                cmd.Parameters.AddWithValue("@Balance", balance);

                return cmd.ExecuteNonQuery(); // Returns the number of rows affected
            }
        }

        // Method to Update an Existing Account
        public int UpdateAccount(int accountId, DateTime date, string description, decimal credit, decimal debit, decimal balance)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "UPDATE Account SET Date = @Date, Description = @Description, Credit = @Credit, Debit = @Debit, Balance = @Balance WHERE AccountID = @AccountID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@AccountID", accountId);
                cmd.Parameters.AddWithValue("@Date", date);
                cmd.Parameters.AddWithValue("@Description", description);
                cmd.Parameters.AddWithValue("@Credit", credit);
                cmd.Parameters.AddWithValue("@Debit", debit);
                cmd.Parameters.AddWithValue("@Balance", balance);

                return cmd.ExecuteNonQuery(); // Returns the number of rows affected
            }
        }

        // Method to Delete an Account by AccountID
        public int DeleteAccount(int accountId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "DELETE FROM Account WHERE AccountID = @AccountID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@AccountID", accountId);

                return cmd.ExecuteNonQuery(); // Returns the number of rows affected
            }
        }
    }
}
