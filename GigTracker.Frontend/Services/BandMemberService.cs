using GigTracker.Models;
using System.Net.Http.Json;

namespace GigTracker.Frontend.Services
{
    public class BandMemberService(HttpClient http)
    {
        private readonly HttpClient _http = http;

        public async Task<List<BandMember>> GetBandMembersAsync() =>
            await _http.GetFromJsonAsync<List<BandMember>>("BandMember") ?? [];

        public async Task<BandMember?> GetBandMemberAsync(int id) =>
            await _http.GetFromJsonAsync<BandMember>($"BandMember/{id}");

        public async Task CreateBandMemberAsync(BandMember bandMember) =>
            await _http.PostAsJsonAsync("BandMember", bandMember);

        public async Task UpdateBandMemberAsync(BandMember bandMember) =>
            await _http.PutAsJsonAsync($"BandMember/{bandMember.Id}", bandMember);

        public async Task DeleteBandMemberAsync(int id) =>
            await _http.DeleteAsync($"BandMember/{id}");
    }
}