using SpotifyAPI.Web;
using System.Collections.Generic;

namespace spotify.Data
{
    public class ArtistWrapper
    {
        public string Id { get; set; }
        public FullArtist Artist { get; set; }
        public List<SimpleAlbum> Albums { get; set; }
        public List<SimpleAlbum> Eps { get; set; }
        public List<FullTrack> TopTracks { get; set; }
    }
}
