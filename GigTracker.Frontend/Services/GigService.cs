using GigTracker.Models;
using System.Net.Http.Json;

namespace GigTracker.Frontend.Services
{
    public class GigService(HttpClient http)
    {
        private readonly HttpClient _http = http;

        public async Task<List<Gig>> GetGigsAsync() =>
            await _http.GetFromJsonAsync<List<Gig>>("Gig") ?? [];

        public async Task<Gig?> GetGigAsync(int id) =>
            await _http.GetFromJsonAsync<Gig>($"Gig/{id}");

        public async Task CreateGigAsync(Gig gig) =>
            await _http.PostAsJsonAsync("Gig", gig);

        public async Task UpdateGigAsync(Gig gig) =>
            await _http.PutAsJsonAsync($"Gig/{gig.Id}", gig);

        public async Task DeleteGigAsync(int id) =>
            await _http.DeleteAsync($"Gig/{id}");
    }
}
