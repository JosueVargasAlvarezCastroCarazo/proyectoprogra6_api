using proyectoprogra6_api.Models;

namespace proyectoprogra6_api.ModelsDTOs
{
    public class UserDTO
    {

        public UserDTO()
        {
            
        }

        public UserDTO(User user)
        {
            UserId = user.UserId;
            Name = user.Name;
            PhoneNumber = user.PhoneNumber;
            Address = user.Address;
            LoginPassword = user.LoginPassword;
            IsAdmin = user.IsAdmin;
            Identification = user.Identification;
            Active = user.Active;
        }

        public User getNativeModel()
        {
            User model = new User();
            model.UserId = UserId;
            model.Name = Name;
            model.PhoneNumber = PhoneNumber;
            model.Address = Address;
            model.LoginPassword = LoginPassword;
            model.IsAdmin = IsAdmin;
            model.Identification = Identification;
            model.Active = Active;
            return model;
        }

        public int UserId { get; set; }
        public string Name { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string LoginPassword { get; set; } = null!;
        public bool? IsAdmin { get; set; }
        public string Identification { get; set; } = null!;
        public bool? Active { get; set; }

    }
}
