using GigTracker.Models;
using System.Net.Http.Json;

namespace GigTracker.Frontend.Services
{
    public class BandService(HttpClient http)
    {
        private readonly HttpClient _http = http;

        public async Task<List<Band>> GetBandsAsync() =>
            await _http.GetFromJsonAsync<List<Band>>("Band") ?? [];

        public async Task<Band?> GetBandAsync(int id) =>
            await _http.GetFromJsonAsync<Band>($"Band/{id}");

        public async Task CreateBandAsync(Band band) =>
            await _http.PostAsJsonAsync("Band", band);

        public async Task UpdateBandAsync(Band band) =>
            await _http.PutAsJsonAsync($"Band/{band.Id}", band);

        public async Task DeleteBandAsync(int id) =>
            await _http.DeleteAsync($"Band/{id}");
    }
}