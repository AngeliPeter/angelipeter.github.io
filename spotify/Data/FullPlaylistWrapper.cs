using SpotifyAPI.Web;

namespace spotify.Data
{
    public class FullPlaylistWrapper : FullPlaylist
    {
        new public Paging<PlaylistTrack<FullTrack>> Tracks
        {
            get;
            set;
        }

    }
}
