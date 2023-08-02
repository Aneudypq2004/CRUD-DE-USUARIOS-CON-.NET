using WebApi.Models;
using WebApi.data;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using BCrypt.Net;

namespace WebApi.Controllers;

class User : Controller
{
    // ADD A NEW USER
    public static string newUser([FromBody] modelUser User)
    {

        // HASH PASSOWRD

        User.password = BCrypt.Net.BCrypt.HashPassword(User.password);

        // ADD A NEW USER IN THE DATABASE

        using (SqlConnection conexion = new SqlConnection(Conexion.uri))
        {

            string query = $"INSERT INTO users ( user_name, email, token,  u_password) VALUES ('{User.name}', '{User.email}', '{User.token}', '{User.password}')";

            using (SqlCommand cmd = new SqlCommand(query, conexion))
            {
                using (SqlCommand usuario = new SqlCommand($"SELECT * FROM users WHERE email = '{User.email}';", conexion))
                {

                    try
                    {
                        conexion.Open();

                        // VERIFY, IF WE ALREADY HAVE THAT USER

                        SqlDataReader response = usuario.ExecuteReader();

                        if (response.Read())
                        {
                            return "EL USUARIO YA EXISTE";
                        }

                        // IF NOT EXITS, IT WILL ADD

                        cmd.ExecuteNonQuery();

                        return "AGREGADO CORRECTAMENTE";

                    }
                    catch (Exception e)
                    {
                        return "Ha ocurrido un error " + e.Message + e.Source;
                    }
                }
            }
        }

    }

    // METHOD TO VERIFY THE USER
    public static string verifyUser()
    {

        return "";

    }
}