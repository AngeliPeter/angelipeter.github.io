﻿using Blazored.LocalStorage;
using spotify.Interfaces;
using SpotifyAPI.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace spotify.Data
{
    public class Spotify : ISpotify
    {
        private string ClientId = "b98639a71452460780fd041649a6f64e";
        public SpotifyClient SpotifyObject { get; set; }
        public ISyncLocalStorageService localStorageService;

        private string RequestNewToken()
        {
            Code.Init();

            localStorageService.SetItem("verifier", Code.CodeVerifier);

            Console.WriteLine(Code.CodeVerifier);

            var loginRequest = new LoginRequest(
            new Uri("https://localhost:44313/callback"),
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

        public async Task<(bool Success, string RedirectURL, SpotifyClient SpotifyObject)> Init()
        {
            PKCETokenResponse PKCE = localStorageService.GetItem<PKCETokenResponse>("PKCE");

            if (PKCE is null)
            {
                return (false, RequestNewToken(), null);
            }

            if (DateTime.UtcNow > (PKCE.CreatedAt.AddSeconds(PKCE.ExpiresIn)))
            {
                try
                {
                    var newResponse = await new OAuthClient().RequestToken(new PKCETokenRefreshRequest(ClientId,
                    PKCE.RefreshToken));
                    localStorageService.SetItem<PKCETokenResponse>("PKCE", newResponse);
                    SpotifyObject = new SpotifyClient(newResponse.AccessToken);
                }
                catch
                {
                    return (false, RequestNewToken(), null);
                }
            }
            else
            {
                SpotifyObject = new SpotifyClient(PKCE.AccessToken);
            }

            return (true, "none", SpotifyObject);
        }
        public Spotify(ISyncLocalStorageService _localStorageService)
        {
            localStorageService = _localStorageService;
        }
    }
}
