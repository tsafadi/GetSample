using Microsoft.AspNetCore.WebUtilities;
using SampleGet.DTOs;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SampleGet
{
    public class Helper
    {
        /// <summary>
        /// Authorize user against an Api endpoint.
        /// On successfull authorization, the token and expiration date will be mapped instide the AuthoridationDTO object.
        /// On failure, the returned object will be null.
        /// </summary>
        /// <param name="username">The username of the user</param>
        /// <param name="password">The password of the user</param>
        /// <returns>Authorization DTO containing the token</returns>
        public async Task<AuthorizationDTO> GetToken(string username, string password)
        {
            AuthorizationDTO authorizationDTO;
            using (var client = new HttpClient())
            {
                // query string parameters
                var query = new Dictionary<string, string>();
                query.Add("Username", username);
                query.Add("Password", password);

                // send
                var authorizationResponse = await client.GetAsync(QueryHelpers.AddQueryString($"{Constants.WEB_URL}/{Constants.AUTHORIZATION_ENDPOINT}", query));
                // check if valid
                if (!authorizationResponse.IsSuccessStatusCode)
                    return null;
                else
                {
                    // read response as string
                    var contents = await authorizationResponse.Content.ReadAsStringAsync();
                    // Deserialize token
                    authorizationDTO = JsonSerializer.Deserialize<AuthorizationDTO>(contents);

                    return authorizationDTO;
                }
            }
        }

        /// <summary>
        /// Get Dokumente from an API endpoint
        /// </summary>
        /// <param name="authorizationDTO">Authorization DTO containing the token</param>
        /// <returns>List of Dokumente</returns>
        public async Task<IEnumerable<DokumentDTO>> GetDokumente(AuthorizationDTO authorizationDTO)
        {
            // construct URL
            var url = $"{Constants.WEB_URL}/{Constants.GET_DOKUMENTE_ENDPOINT}";

            // add parameters to dictonary
            var query = new Dictionary<string, string>();
            query.Add("AuftragId", "1");

            // construct request
            var req = new HttpRequestMessage(HttpMethod.Get, url) { Content = new FormUrlEncodedContent(query) };

            using (HttpClient client = new HttpClient())
            {
                // Append token
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authorizationDTO.token);
                // send
                var res = await client.SendAsync(req);
                if (!res.IsSuccessStatusCode)
                    return null;
                else
                {
                    // read response as string
                    var contents = await res.Content.ReadAsStringAsync();
                    // Deserialize token
                    IEnumerable<DokumentDTO> dokumentDTOs = JsonSerializer.Deserialize<IEnumerable<DokumentDTO>>(contents);

                    return dokumentDTOs;
                }
            }
        }
    }
}
