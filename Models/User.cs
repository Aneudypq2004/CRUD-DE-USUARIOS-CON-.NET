namespace WebApi.Models
{
    public class modelUser
    {
        public modelUser(string name, string email, string password)
        {
            this.name = name;

            this.email = email;

            this.password = password;

            this.token = getToken();
        }
        public string name { get; set; }

        public string email { get; set; }

        public string password { get; set; }
        public string token { get; set; }

        // GENERATE A TOKEN
        public static string getToken()
        {

            char[] alphabet = new char[] {
            'a', 'e', 'i', 'o', 'u', '1',  '2', '3', '4', '5', '6', '7', '8', '9', '0'};

            string token = "";

            for (int i = 0; i < 30; i++)
            {
                int random = new Random().Next(0, alphabet.Length);

                if(i % 4 == 0 && i != 0){
                    token += '-';
                }

                token += alphabet[random];
            }

            return token;
        }

        // HASH PASSWORD
        
    }
}