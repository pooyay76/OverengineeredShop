using Auth.Api.Utility;

namespace Auth.Api.Models
{
    public class User
    {
        public Guid Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string PhoneNumber { get; private set; }



        public bool IsPhoneNumberConfirmed { get; private set; }


        public string SmsCode { get; private set; }




        //password is saved as hashed
        public string Password { get; private set; }


        //Many to many with roles
        public List<Role> Roles { get; private set; }
        public List<UserRole> UserRoles { get; private set; }


        //the operation for creating a user, hence it should contain some business logic 
        public User(string firstName, string lastName, string phoneNumber, string password)
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                throw new Exception();
            }
            FirstName = firstName;


            if (string.IsNullOrWhiteSpace(lastName))
            {

                throw new Exception();
            }
            LastName = lastName;




            if (string.IsNullOrWhiteSpace(password))
            {

                throw new Exception();
            }
            Password = password;


            //we should also add regex validation here to ensure that the pn is in correct format
            if (string.IsNullOrWhiteSpace(phoneNumber))
            {

                throw new Exception();
            }


            PhoneNumber = phoneNumber;



            SmsCode = "";
            IsPhoneNumberConfirmed = false;
        }


        public void SetSmsCode()
        {

            //after 2 mins this will be turned into an empty string in DB by a background service
            SmsCode = RandomGenerator.GenerateRandomText(6);
            SmsSender.SendSmsCode(PhoneNumber, SmsCode);
        }

        public bool VerifySmsCode(string code)
        {
            //could also throw here
            if (SmsCode == "")
                return false;

            if (code == SmsCode)
            {
                return true;
            }
            else
                return false;

        }
        public void ChangePassword(string newPassword)
        {
            Password = newPassword;
        }
        public void EditPersonalInfo(string fName, string lName, string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(fName) == false && fName.Equals(FirstName, StringComparison.OrdinalIgnoreCase) == false)
            {
                FirstName = fName;

            }

            if (string.IsNullOrWhiteSpace(lName) == false && lName.Equals(LastName, StringComparison.OrdinalIgnoreCase) == false)
            {
                LastName = lName;

            }

            //also add regex check here
            if (string.IsNullOrWhiteSpace(phoneNumber) == false && phoneNumber != PhoneNumber)
            {
                PhoneNumber = phoneNumber;
            }

        }

    }
}
