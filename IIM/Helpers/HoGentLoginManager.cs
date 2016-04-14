using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace IIM.Helpers
{
    /// <summary>
    /// Use this class to get the identity of a HoGent user
    /// Written by Matthias Kunnen
    /// </summary>
    public static class HoGentLoginManager
    {
        public struct Identity
        {
            public string Faculteit, Naam, Voornaam, Type, Email, Base64Foto;
        }

        public static async Task<Identity?> Login(string userId, string password)
        {
            var sha256 = new SHA256Managed();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            password = ByteArrayToString(bytes);
            
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44300/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.GetAsync($"https://studservice.hogent.be/auth/{userId}/{password}")
                    .ConfigureAwait(continueOnCapturedContext:false);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<Identity>();
                }
            }
            return null;
        }

        public static async Task<bool> CheckPassword(string userId, string password)
        {
            return (await Login(userId, password)).HasValue;
        }

        private static string ByteArrayToString(IReadOnlyCollection<byte> ba)
        {
            var hex = new StringBuilder(ba.Count * 2);
            foreach (var b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }
    }
}