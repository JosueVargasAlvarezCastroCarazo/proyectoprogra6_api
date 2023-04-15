using proyectoprogra6_api.Models;

namespace proyectoprogra6_api.ModelsDTOs
{
    public class UserDTO
    {

        public UserDTO()
        {
            
        }

        public UserDTO(int userId, string name, string phoneNumber, string address, string loginPassword, bool? isAdmin, string identification, bool? active, string? email)
        {
            UserId = userId;
            Name = name;
            PhoneNumber = phoneNumber;
            Address = address;
            LoginPassword = loginPassword;
            IsAdmin = isAdmin;
            Identification = identification;
            Active = active;
            Email = email;
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
            Email = user.Email;
        }

        //return the original model
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
            model.Email = Email;
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
        public string? Email { get; set; }

    }
}
