namespace Core
{
    public interface IDALCustomer
    {


        public void InsertCustomer(string first_name, string last_name, string address, string email, string password);

        public Dictionary<string, object> GetCustomerByEmail(string email);
    }
}