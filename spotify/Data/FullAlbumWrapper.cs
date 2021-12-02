using SpotifyAPI.Web;
using System.Collections.Generic;

namespace spotify.Data
{
    public class FullAlbumWrapper : FullAlbum
    {
        public List<FullTrack> FullTracks { get; set; }
    }
}
