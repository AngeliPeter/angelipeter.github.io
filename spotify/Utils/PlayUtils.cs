using spotify.Data;
using spotify.Interfaces;
using SpotifyAPI.Web;
using System.Collections.Generic;

namespace spotify.Utils
{
    public class PlayUtils
    {
        public async void ClickedAlbumPlay(string albumUri, SpotifyClient spotifyClient)
        {
            PlayerResumePlaybackRequest alb = new PlayerResumePlaybackRequest();
            alb.ContextUri = albumUri;
            await spotifyClient.Player.ResumePlayback(alb);
        }

        public async void ClickedPlay(string songUri, SpotifyClient spotifyClient)
        {
            List<string> trackList = new List<string>();
            trackList.Add(songUri);
            var track = new PlayerResumePlaybackRequest();
            track.Uris = trackList;
            await spotifyClient.Player.ResumePlayback(track);
        }
    }
}
