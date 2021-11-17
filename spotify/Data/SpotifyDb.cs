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

    public class AvailableDevices : IDBObjectStore
    {
        public IDBIndex Id { get; }
        public IDBIndex Type { get; }


        public AvailableDevices(IDBDatabase database) : base(database)
        {
            Name = "AvailableDevices";
            KeyPath = "id";

            Id = new IDBIndex(this, "id", "id");
            Type = new IDBIndex(this, "type", "type");
        }
    }

    public class SpotifyDb : IDBDatabase
    {
        public LikedSongs LikedSongs { get; }
        public AvailableDevices AvailableDevices { get; }

        public SpotifyDb(IJSRuntime jsRuntime) : base(jsRuntime)
        {
            Name = "SpotifyDb";
            Version = 3;

            LikedSongs = new LikedSongs(this);
            AvailableDevices = new AvailableDevices(this);
        }
    }
}
