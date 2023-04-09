namespace Core
{
    public interface ICustomerService
    {

    public void AddCustomer(string[] info);


    public Customer AuthenticateCustomer(string email,string password);

}
}   