namespace WebApi.Helper
{
    public class Token
    {
        // GENERATE A TOKEN
        public static string getToken()
        {
            char[] alphabet = new char[] {
            'A', 'E', 'I', 'O', 'U', '1',  '2', '3', '4', '5', '6', '7', '8', '9', '0'};

            string token = "";

            for (int i = 1; i < 40; i++)
            {
                int random = new Random().Next(0, alphabet.Length);

                if (i % 5 == 0)
                {
                    token += '-';
                    continue;
                }

                token += alphabet[random];
            }

            return token;
        }

    }
}


