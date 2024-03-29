﻿using SpotifyAPI.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace spotify.Interfaces
{
    public interface ISpotify
    {
        public Task<(bool Success, string RedirectURL, SpotifyClient SpotifyObject)> Init(bool check = false);
    }
}
