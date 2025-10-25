namespace HVK.Models
{
    public class LoginUserState
    {
        public const int USER_NOT_LOGGED_IN = -1;
        public int UserId { get; set; }
        public Customer.UserTypes UserType { get; set; }

        public LoginUserState()
        {
            UserId = USER_NOT_LOGGED_IN;
            UserType = Customer.UserTypes.Customer;
        }

        public LoginUserState(int userId, Customer.UserTypes userType)
        {
            UserId = userId;
            UserType = userType;
        }
    }
}
