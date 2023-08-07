using WebApi.Models;
using WebApi.data;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using BCrypt.Net;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    // ADD A NEW USER
    [HttpPost]
    public ActionResult newUser([FromBody] modelUser User)
    {
        // HASH PASSOWRD

        User.password = BCrypt.Net.BCrypt.HashPassword(User.password);

        // ADD A NEW USER IN THE DATABASE

        using (SqlConnection conexion = new SqlConnection(Conexion.uri))
        {

            string query = $"INSERT INTO users ( user_name, email, token,  u_password) VALUES (@user_name, @email, @token, @userPassword)";


            using (SqlCommand cmd = new SqlCommand(query, conexion))
            {
                // PARAMETERS

                cmd.Parameters.AddWithValue("@user_name", User.name);
                cmd.Parameters.AddWithValue("@email", User.email);
                cmd.Parameters.AddWithValue("@token", User.token);
                cmd.Parameters.AddWithValue("@userPassword", User.password);

                using (SqlCommand usuario = new SqlCommand($"SELECT * FROM users WHERE email = @email;", conexion))
                {
                    // PARAMETERS

                    usuario.Parameters.AddWithValue("@email", User.email);

                    try
                    {
                        conexion.Open();

                        // VERIFY, IF WE ALREADY HAVE THAT USER~

                        using (SqlDataReader response = usuario.ExecuteReader())
                        {

                            if (response.Read())

                            {
                                return BadRequest("EL USUARIO YA EXISTE");
                            }

                        }
                        // IF NOT EXITS, IT WILL ADD

                        cmd.ExecuteNonQuery();

                        return Ok("AGREGADO CORRECTAMENTE");
                    }
                    catch (Exception e)
                    {
                        return BadRequest("Ha ocurrido un error" + e.Message);
                    }
                }
            }
        }

    }

    // METHOD TO VERIFY THE USER
    [HttpGet("{token}")]
    public ActionResult VerificarToken([FromRoute] string token)
    {      
        using (SqlConnection conexion = new SqlConnection(Conexion.uri))
        {
            string query = "UPDATE users SET confirmed = 1, token = null WHERE token = @Token";

            try
            {
                conexion.Open();

                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {

                    cmd.Parameters.AddWithValue("@Token", token);

                    int response = cmd.ExecuteNonQuery();

                    // if, there are not any change

                    if(response == 0)
                    {
                        return BadRequest("EL TOKEN NO ES VALIDO");
                    }

                    return Ok("CONFIRMADO CORRECTAMENTE " + response);
                }           

            }
            catch (Exception e)
            {
                return BadRequest("Ha ocurrido un error" + e.Message);
            }
        }      
    }
}