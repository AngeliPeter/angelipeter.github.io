using Blazored.LocalStorage;
using spotify.Interfaces;
using SpotifyAPI.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace spotify.Data
{
    public class Spotify : ISpotify
    {
        public NavigationManager MyNavigationManager { get; set; }

        private string ClientId = "b98639a71452460780fd041649a6f64e";
        public SpotifyClient SpotifyObject { get; set; }
        public ISyncLocalStorageService localStorageService;

        private string RequestNewToken()
        {
            Code.Init();

            localStorageService.SetItem("verifier", Code.CodeVerifier);

            var loginRequest = new LoginRequest(
            new Uri($"{MyNavigationManager.BaseUri}callback"),
            ClientId,
            LoginRequest.ResponseType.Code
            )
            {
                CodeChallengeMethod = "S256",
                CodeChallenge = Code.CodeChallenge,
                Scope = new[] {
                    Scopes.UserReadRecentlyPlayed,
                    Scopes.UserReadPlaybackPosition,
                    Scopes.UserTopRead,
                    Scopes.UserLibraryRead,
                    Scopes.UserLibraryModify,
                    Scopes.PlaylistModifyPrivate,
                    Scopes.PlaylistReadPrivate,
                    Scopes.UserFollowRead,
                    Scopes.PlaylistModifyPublic,
                    Scopes.UserReadPrivate,
                    Scopes.UserReadEmail,
                    Scopes.AppRemoteControl,
                    Scopes.Streaming,
                    Scopes.UserReadCurrentlyPlaying,
                    Scopes.UserModifyPlaybackState,
                    Scopes.UserReadPlaybackState,
                    Scopes.PlaylistReadCollaborative,
                    Scopes.UserFollowModify
                }
            };

            return loginRequest.ToUri().ToString();
        }

        public async Task<(bool Success, string RedirectURL, SpotifyClient SpotifyObject)> Init(bool check = false)
        {
            PKCETokenResponse PKCE = localStorageService.GetItem<PKCETokenResponse>("PKCE");

            Console.WriteLine(" --- INIT --- ");

            if (PKCE is null)
            {
                Console.WriteLine(" --- PKCE null --- ");
                return (false, RequestNewToken(), null);
            }

            Console.WriteLine(" --- DateTime.UtcNow --- ");
            Console.WriteLine(DateTime.UtcNow);
            Console.WriteLine(" --- PKCE.CreatedAt.AddSeconds(PKCE.ExpiresIn) --- ");
            Console.WriteLine(PKCE.CreatedAt.AddSeconds(PKCE.ExpiresIn));

            if (DateTime.UtcNow > (PKCE.CreatedAt.AddSeconds(PKCE.ExpiresIn)))
            {
                try
                {
                    Console.WriteLine(" --- PKCE EXPIRED --- ");
                    var newResponse = await new OAuthClient().RequestToken(new PKCETokenRefreshRequest(ClientId,
                    PKCE.RefreshToken));
                    localStorageService.SetItem<PKCETokenResponse>("PKCE", newResponse);
                    SpotifyObject = new SpotifyClient(newResponse.AccessToken);
                }
                catch
                {
                    Console.WriteLine(" --- PKCE EXPIRED ERROR --- ");
                    return (false, RequestNewToken(), null);
                }
            }
            else
            {
                Console.WriteLine(" --- PKCE OK --- ");
                if (!check)
                {
                    SpotifyObject = new SpotifyClient(PKCE.AccessToken);
                }
                else
                {
                    SpotifyObject = null;
                }
            }

            return (true, "none", SpotifyObject);
        }
        public Spotify(ISyncLocalStorageService _localStorageService, NavigationManager _MyNavigationManager)
        {
            localStorageService = _localStorageService;
            MyNavigationManager = _MyNavigationManager;
        }
    }
}
