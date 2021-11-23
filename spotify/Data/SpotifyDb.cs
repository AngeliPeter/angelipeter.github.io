using BlazorIndexedDbJs;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace spotify.Data
{
    public class LikedSongs : IDBObjectStore
    {
        public IDBIndex AddedAt { get; }
        public IDBIndex Track { get; }

        public LikedSongs(IDBDatabase database) : base(database)
        {
            Name = "LikedSongs";
            KeyPath = "id";
            AutoIncrement = true;

            AddedAt = new IDBIndex(this, "addedAt", "addedAt");
            Track = new IDBIndex(this, "track", "track");
        }
    }

    public class SavedPlaylists : IDBObjectStore
    {
        public IDBIndex Id { get; }
        public IDBIndex _Name { get; }
        public IDBIndex Images { get; }


        public SavedPlaylists(IDBDatabase database) : base(database)
        {
            Name = "SavedPlaylists";
            KeyPath = "id";
            AutoIncrement = true;
            _Name = new IDBIndex(this, "name", "name");
            Images = new IDBIndex(this, "images", "images");
        }
    }

    public class SpotifyDb : IDBDatabase
    {
        public LikedSongs LikedSongs { get; }
        public SavedPlaylists SavedPlaylists { get; }

        public SpotifyDb(IJSRuntime jsRuntime) : base(jsRuntime)
        {
            Name = "SpotifyDb";
            Version = 5;

            LikedSongs = new LikedSongs(this);
            SavedPlaylists = new SavedPlaylists(this);
        }
    }
}
