using SampleGet.DTOs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SampleGet
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Initialize Helper
            Helper helper = new Helper();

            Console.WriteLine("Please authenticate!");
            // Benutzername
            Console.Write("Username: ");
            string username = Console.ReadLine();
            // Passwort
            Console.Write("Password: ");
            string password = Console.ReadLine();

            // Get token
            Console.WriteLine("\nRetrieving token...");
            AuthorizationDTO authorizationDTO = await helper.GetToken(username, password);
            Console.WriteLine("Token received!\n");
            if (authorizationDTO == null)
            {
                Console.Write("Authorization failed!");
                return;
            }

            // Get Dokumente
            IEnumerable<DokumentDTO> dokumentDTOs = await helper.GetDokumente(authorizationDTO);
        }
    }
}
