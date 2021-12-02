using Microsoft.AspNetCore.Components;

namespace spotify.Utils
{
    public class NavigationUtils
    {
        NavigationManager NavManager;
        public NavigationUtils(NavigationManager _NavManager)
        {
            NavManager = _NavManager;
        }

        public void ClickedLikedSong()
        {
            NavManager.NavigateTo("/saved");
        }

        public void ClickedOnArtist(string id)
        {
            NavManager.NavigateTo($"/artist?artistId={id}");
        }
        public void ClickedOnAlbum(string id)
        {
            NavManager.NavigateTo($"/album?albumId={id}");
        }

        public void NavigateToPlaylist(string id)
        {
            NavManager.NavigateTo($"/playlist?playlistId={id}");
        }

        public void ClickedMainPage()
        {
            NavManager.NavigateTo("/");
        }
    }
}
