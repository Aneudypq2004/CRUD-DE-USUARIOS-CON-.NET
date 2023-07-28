namespace WebApi.Models
{

    class modelUser
    {
        public modelUser(string name, string email)
        {
            this.name = name;

            this.email = email;

            this.token = getToken();
        }
        public string name { get; set; }
        public string email { get; set; }
        public string token { get; set; }

        // GENERATE A TOKEN
        public static string getToken()
        {

            DateTime timeNow = DateTime.Now;

            char[] alphabet = new char[] {
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm',
            'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '1',
           '2', '3', '4', '5', '6', '7', '8', '9'};

            string token = "";

            for (int i = 0; i < 50; i++)
            {
                int random = new Random().Next(0, alphabet.Length);

                token += alphabet[random];
            }

            return token;
        }
    }
}