using WebApi.Models;
using WebApi.data;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Newtonsoft.Json;

namespace WebApi.Controllers;

class User : Controller
{
    // ADD A NEW USER
    public static string newUser([FromBody] modelUser User)
    {

        // ADD A NEW USER IN THE DATABASE

        SqlConnection conexion = new SqlConnection(Conexion.uri);

        string query = $"INSERT INTO users ( user_name, email, token) VALUES ('{User.name}', '{User.email}', '{User.token}')";

        SqlCommand cmd = new SqlCommand(query, conexion);

        try
        {
            conexion.Open();

            SqlCommand usuario = new SqlCommand($"SELECT * FROM users WHERE email = {User.email};", conexion);

            SqlDataReader response = usuario.ExecuteReader();

            if(response.HasRows) {
                return "El email ya existe";
            }


            cmd.ExecuteNonQuery();

            return "AGREGADO CORRECTAMENTE";

        }
        catch (Exception e)
        {
            return "Ha ocurrido un error " + e.Message;
        }
    }
}