using System;
using System.Threading.Tasks;

namespace UserConnection
{
    //Es posible que desde el terminal, dentro del proyecto donde estan los .cs haya que instalar
    //dotnet add package Microsoft.AspNet.WebApi.Client --version 5.2.7
    class Program
    {
        //La API tiene que estar corriendo para que funcione
        //El Main debe ser Asyncrono para poder ejecutar las peticiones
        static async Task Main(string[] args)
        {
            /**
             * CIFRADO:
             *  El string password no lo podemos almacenar asi sino que directamente
             *  llamaremos al metodo Encrypt.GetSHA256 que es una encriptacion 
             *  unidireccional (no se puede desencriptar) por lo que la contrasenya
             *  ya la subiremos encriptada a MongoDB, para comprobar si la contrasenya
             *  es corrrecta deberemos comparar ambas encriptadas.
             */
            User usuarioPruebas = new User();
            usuarioPruebas.passw = Encrypt.GetSHA256("contrasenya");
            usuarioPruebas._id = "61c87e39ac57ba155a8e5ebb";
            usuarioPruebas.name = "Intermodular";
            usuarioPruebas.email = "inter@inter.com";
            usuarioPruebas.id_client = "Ecosistema1";

            //Comprobar contrasenya de un usuario
            Boolean correct = await User.checkPass(usuarioPruebas._id, usuarioPruebas.passw);
            if (correct) Console.WriteLine("Pass OK");
            else Console.WriteLine("Pass WRONG");

            //Comprobar si usuario existe en MongoDB
            Boolean exists = await User.checkUser(usuarioPruebas._id);
            if (exists) Console.WriteLine("User Exists");
            else Console.WriteLine("No user");

            //GET obtener usuario por id
            await User.getUserById(usuarioPruebas._id);
            Console.WriteLine($"CURRENT USER: {User.currentUser.name}\t{User.currentUser.email}\t{User.currentUser._id}");

            //POST crear nuevo usuario con id auto
            await User.createUser(usuarioPruebas);

            //PUT actualizar name y email por id
            await User.updateUser(usuarioPruebas._id, usuarioPruebas);

            //DELETE borra un usuario por id
            await User.deleteUser("61c4dc03960c0f52005ddf36");

            //GET obtener todos los usuarios
            await User.getAllUsers();
            foreach (User user in User.allUsers) Console.WriteLine($"\n\n{user._id}\t{user.name}\t{user.email}\t{user.passw}\t{user.id_client}");
        }
    }
}
