using AutoMapper;
using F2022A3THH.Data;
using F2022A3THH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

// ************************************************************************************
// WEB524 Project Template V1 == 2227-fb9b7406-3b07-4d67-b8d8-7a7b34d526ab
//
// By submitting this assignment you agree to the following statement.
// I declare that this assignment is my own work in accordance with the Seneca Academic
// Policy. No part of this assignment has been copied manually or electronically from
// any other source (including web sites) or distributed to other students.
// ************************************************************************************

namespace F2022A3THH.Controllers
{
    public class Manager
    {
        // Reference to the data context
        private DataContext ds = new DataContext();

        // AutoMapper instance
        public IMapper mapper;

        public Manager()
        {
            // If necessary, add more constructor code here...

            // Configure the AutoMapper components
            var config = new MapperConfiguration(cfg =>
            {
                // Define the mappings below, for example...
                // cfg.CreateMap<SourceType, DestinationType>();
                // cfg.CreateMap<Product, ProductBaseViewModel>();
                cfg.CreateMap<Album, AlbumBaseViewModel>();
                cfg.CreateMap<Artist, ArtistBaseViewModel>();
                cfg.CreateMap<MediaType, MediaTypeBaseViewModel>();

                // tracks
                cfg.CreateMap<Track, TrackBaseViewModel>();
                cfg.CreateMap<Track, TrackWithDetailViewModel>();
                cfg.CreateMap<TrackWithDetailViewModel, Track>();
                cfg.CreateMap<TrackAddViewModel, Track>();
                cfg.CreateMap<TrackAddFormViewModel, TrackAddViewModel>();

                // playlists
                cfg.CreateMap<Playlist, PlaylistBaseViewModel>();
                cfg.CreateMap<Playlist, PlaylistWithDetailViewModel>();
                cfg.CreateMap<Playlist, PlaylistEditTracksViewModel>();
                cfg.CreateMap<PlaylistWithDetailViewModel, PlaylistEditTracksFormViewModel>();
            });

            mapper = config.CreateMapper();

            // Turn off the Entity Framework (EF) proxy creation features
            // We do NOT want the EF to track changes - we'll do that ourselves
            ds.Configuration.ProxyCreationEnabled = false;

            // Also, turn off lazy loading...
            // We want to retain control over fetching related objects
            ds.Configuration.LazyLoadingEnabled = false;
        }

        // Add your methods below and call them from controllers. Ensure that your methods accept
        // and deliver ONLY view model objects and collections. When working with collections, the
        // return type is almost always IEnumerable<T>.
        //
        // Remember to use the suggested naming convention, for example:
        // ProductGetAll(), ProductGetById(), ProductAdd(), ProductEdit(), and ProductDelete().

        // Artist methods
        public IEnumerable<ArtistBaseViewModel> ArtistGetAll()
        {
            var artists = ds.Artists.OrderBy(a => a.Name);

            return artists == null ? null : mapper.Map<IEnumerable<Artist>, IEnumerable<ArtistBaseViewModel>>(artists);
        }


        // Album methods
        public IEnumerable<AlbumBaseViewModel> AlbumGetAll()
        {
            var albums = ds.Albums.OrderBy(a => a.Title);
            return albums == null ? null : mapper.Map<IEnumerable<Album>, IEnumerable<AlbumBaseViewModel>>(albums);
        }


        // MediaType methods
        public IEnumerable<MediaTypeBaseViewModel> MediaTypeGetAll()
        {
            var mediaTypes = ds.MediaTypes.OrderBy(m => m.Name);
            return mediaTypes == null ? null : mapper.Map<IEnumerable<MediaType>, IEnumerable<MediaTypeBaseViewModel>>(mediaTypes);
        }


        // Track methods
        public IEnumerable<TrackWithDetailViewModel> TrackGetAllWithDetail()
        {
            var tracks = ds.Tracks.Include("Album")
                                  .Include("Album.Artist")
                                  .Include("MediaType")
                                  .OrderBy(t => t.Name);

            return mapper.Map<IEnumerable<Track>, IEnumerable<TrackWithDetailViewModel>>(tracks);
        }


        public TrackWithDetailViewModel TrackGetById(int id)
        {
            var track = ds.Tracks.Include("Album")
                                  .Include("Album.Artist")
                                  .Include("MediaType")
                                  .FirstOrDefault(t => t.TrackId == id);

            return track == null ? null : mapper.Map<Track, TrackWithDetailViewModel>(track);
        }


        public TrackWithDetailViewModel TrackAdd(TrackAddViewModel newTrack)
        {
            // Attempt to find the associated object
            var album = ds.Albums.Find(newTrack.AlbumId);
            var mediaType = ds.MediaTypes.Find(newTrack.MediaTypeId);

            if (album == null)
            {
                return null;
            }
            else
            {
                // Attempt to add the new track
                var addedTrack = ds.Tracks.Add(mapper.Map<TrackAddViewModel, Track>(newTrack));

                // Set the associated track property
                addedTrack.Album = album;
                addedTrack.MediaType = mediaType;
                ds.SaveChanges();

                return (addedTrack == null) ? null : mapper.Map<Track, TrackWithDetailViewModel>(addedTrack);
            }
        }

        // Playlist methods
        public IEnumerable<PlaylistBaseViewModel> PlaylistGetAll()
        {
            var playlists = ds.Playlists.Include("Tracks")
                                        .OrderBy(p => p.Name);

            return playlists == null ? null : mapper.Map<IEnumerable<Playlist>, IEnumerable<PlaylistBaseViewModel>>(playlists);
        }

        public PlaylistWithDetailViewModel PlaylistGetById(int id)
        {
            var playlist = ds.Playlists.Include("Tracks")
                                         .SingleOrDefault(p => p.PlaylistId == id);

            return playlist == null ? null : mapper.Map<Playlist, PlaylistWithDetailViewModel>(playlist);
        }

        public PlaylistEditTracksViewModel PlaylistEditTracks(PlaylistEditTracksViewModel newTrack)
        {
            // Attempt to fetch the object
            var o = ds.Playlists.Include("Tracks")
                                .SingleOrDefault(t => t.PlaylistId == newTrack.PlaylistId);

            if (o == null)
            {
                // Problem - object was not found, so return
                return null;
            }
            else
            {
                // Update the object with the incoming values

                // First, clear out the existing collection
                o.Tracks.Clear();

                // Then, go through the incoming items
                // For each one, add to the fetched object's collection
                foreach (var item in newTrack.TrackIds)
                {
                    var a = ds.Tracks.Find(item);
                    o.Tracks.Add(a);
                }
                // Save changes
                ds.SaveChanges();

                return mapper.Map<Playlist, PlaylistEditTracksViewModel>(o);
            }
        }
    }
}