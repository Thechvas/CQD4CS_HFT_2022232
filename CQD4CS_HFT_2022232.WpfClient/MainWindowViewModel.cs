using CQD4CS_HFT_2022232.Models;
using CQD4CS_HFT_2022232.Models.DTOs;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace CQD4CS_HFT_2022232.WpfClient
{
    public class MainWindowViewModel : ObservableRecipient
    {
        public RestCollection<Artist> Artists { get; set; }
        public RestCollection<Song> Songs { get; set; }
        public RestCollection<Festival> Festivals { get; set; }
        public RestService rest {  get; set; }

        private Artist selectedArtist;

        public Artist SelectedArtist
        {
            get { return selectedArtist; }
            set
            {
                if (value != null)
                {
                    selectedArtist = new Artist()
                    {
                        Name = value.Name,
                        Id = value.Id,
                        NumOfAlbums = value.NumOfAlbums,
                        FestivalId = value.FestivalId
                         
                    };
                    OnPropertyChanged();
                    (DeleteArtistCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }

        private Song selectedSong;

        public Song SelectedSong
        {
            get { return selectedSong; }
            set
            {
                if (value != null)
                {
                    selectedSong = new Song()
                    {
                        Title = value.Title,
                        Id = value.Id,
                        Genre = value.Genre,
                        Length = value.Length,
                        ArtistId = value.ArtistId
                    };
                    OnPropertyChanged();
                    (DeleteSongCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }

        private Festival selectedFestival;

        public Festival SelectedFestival
        {
            get { return selectedFestival; }
            set
            {
                if (value != null)
                {
                    selectedFestival = new Festival()
                    {
                        Name = value.Name,
                        Id = value.Id,
                        Duration = value.Duration,
                        Location = value.Location
                    };
                    OnPropertyChanged();
                    (DeleteFestivalCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }
        private string longestSongOfArtist;
        public string LongestSongOfArtist
        {
            get { return longestSongOfArtist; }
            set
            {
                if (longestSongOfArtist != value)
                {
                    longestSongOfArtist = value;
                    OnPropertyChanged(nameof(LongestSongOfArtist));
                }
            }
        }

        private string festivalWithMostArtists;

        public string FestivalWithMostArtists
        {
            get { return festivalWithMostArtists; }
            set
            {
                if (festivalWithMostArtists != value)
                {
                    festivalWithMostArtists = value;
                    OnPropertyChanged(nameof(FestivalWithMostArtists));
                }
            }
        }

        private string artistWithMostAlbums;

        public string ArtistWithMostAlbums
        {
            get { return artistWithMostAlbums; }
            set
            {
                if (artistWithMostAlbums != value)
                {
                    artistWithMostAlbums = value;
                    OnPropertyChanged(nameof(ArtistWithMostAlbums));
                }
            }
        }

        private int totalDurationOfFestival;

        public int TotalDurationOfFestival
        {
            get { return totalDurationOfFestival; }
            set
            {
                if (totalDurationOfFestival != value)
                {
                    totalDurationOfFestival = value;
                    OnPropertyChanged(nameof(TotalDurationOfFestival));
                }
            }
        }

        private List<AlbumInfo> albumStatistics;

        public List<AlbumInfo> AlbumStatistics
        {
            get { return albumStatistics; }
            set
            {
                if (albumStatistics != value)
                {
                    albumStatistics = value;
                    OnPropertyChanged(nameof(AlbumStatistics));
                }
            }
        }

        private List<ArtistInfo> artistStatistics;

        public List<ArtistInfo> ArtistStatistics
        {
            get { return artistStatistics; }
            set
            {
                if (artistStatistics != value)
                {
                    artistStatistics = value;
                    OnPropertyChanged(nameof(ArtistStatistics));
                }
            }
        }

        private string specificSongFinder;

        public string SpecificSongFinder
        {
            get { return specificSongFinder; }
            set
            {
                if (specificSongFinder != value)
                {
                    specificSongFinder = value;
                    OnPropertyChanged(nameof(SpecificSongFinder));
                }
            }
        }

        public List<string> GenreList { get; set; } = new List<string> { "Pop", "RnB", "Hip-Hop", "EDM", "Dance" };

        private string selectedGenre;

        public string SelectedGenre
        {
            get { return selectedGenre; }
            set
            {
                if (selectedGenre != value)
                {
                    selectedGenre = value;
                    OnPropertyChanged(nameof(SelectedGenre));
                }
            }
        }

        


        public ICommand CreateArtistCommand { get; set; }
        public ICommand DeleteArtistCommand { get; set; }
        public ICommand UpdateArtistCommand { get; set; }

        public ICommand CreateSongCommand { get; set; }
        public ICommand DeleteSongCommand { get; set; }
        public ICommand UpdateSongCommand { get; set; }

        public ICommand CreateFestivalCommand { get; set; }
        public ICommand DeleteFestivalCommand { get; set; }
        public ICommand UpdateFestivalCommand { get; set; }

        //non cruds
        public ICommand FestivalWithMostArtistsCommand { get; set; }
        public ICommand LongestSongOfArtistCommand { get; set; }
        public ICommand ArtistWithMostAlbumsCommand { get; set; }
        public ICommand TotalDurationOfFestivalCommand { get; set; }
        public ICommand AlbumStatisticsCommand { get; set; }
        public ICommand ArtistStatisticsCommand { get; set; }
        public ICommand SpecificSongFinderCommand { get; set; }

        public string GetFestivalWithMostArtists()
        {
            FestivalWithMostArtists = rest.GetSingle<string>("Stat/FestivalWithMostArtists");
            return FestivalWithMostArtists;
        }
        
        public string GetLongestSongOfArtist(string artistName)
        {
            LongestSongOfArtist = rest.GetSingle<string>($"Stat/LongestSongOfArtist?artistName={artistName}");
            return LongestSongOfArtist;
        }

        public string GetArtistWithMostAlbums(string festivalLocation)
        {
            ArtistWithMostAlbums = rest.GetSingle<string>($"Stat/ArtistWithMostAlbums?festivalLocation={festivalLocation}");
            return ArtistWithMostAlbums;
        }
        
        public int GetTotalDurationOfFestival(int festivalId)
        {
            TotalDurationOfFestival = rest.GetSingle<int>($"Stat/TotalDurationOfFestival?festivalId={festivalId}");
            return TotalDurationOfFestival;
        }

        public List<AlbumInfo> GetAlbumStatistics()
        {
            AlbumStatistics = rest.Get<AlbumInfo>("Stat/AlbumStatistics");
            return AlbumStatistics;
        }

        public List<ArtistInfo> GetArtistStatistics()
        {
            ArtistStatistics = rest.Get<ArtistInfo>("Stat/ArtistStatistics");
            return ArtistStatistics;
        }

        public string GetSpecificSongFinder(string artistName, string genreName)
        {
            SpecificSongFinder = rest.GetSingle<string>($"Stat/SpecificSongFinder?artistName={artistName}&genreName={genreName}");
            return SpecificSongFinder;
        }

        public MainWindowViewModel()
        {
            rest = new RestService("http://localhost:36286/");
            Artists = new RestCollection<Artist>("http://localhost:36286/", "artist", "hub");
            Songs = new RestCollection<Song>("http://localhost:36286/", "song", "hub");
            Festivals = new RestCollection<Festival>("http://localhost:36286/", "festival", "hub");
            selectedGenre = GenreList.FirstOrDefault();

            CreateArtistCommand = new RelayCommand(() =>
            {
                Artists.Add(new Artist()
                {
                    Name = SelectedArtist.Name
                });
            });

            CreateSongCommand = new RelayCommand(() =>
            {
                Songs.Add(new Song()
                {
                    Title = SelectedSong.Title
                });
            });

            CreateFestivalCommand = new RelayCommand(() =>
            {
                Festivals.Add(new Festival()
                {
                    Name = SelectedFestival.Name,
                    Duration = SelectedFestival.Duration
                });
            });



            UpdateArtistCommand = new RelayCommand(() =>
            {
                Artists.Update(SelectedArtist);
            });

            UpdateSongCommand = new RelayCommand(() =>
            {
                Songs.Update(SelectedSong);
            });

            UpdateFestivalCommand = new RelayCommand(() =>
            {
                Festivals.Update(SelectedFestival);
            });



            DeleteArtistCommand = new RelayCommand(() =>
            {
                Artists.Delete(SelectedArtist.Id);
            },
            () =>
            {
                return SelectedArtist != null;
            });

            DeleteSongCommand = new RelayCommand(() =>
            {
                Songs.Delete(SelectedSong.Id);
            },
            () =>
            {
                return SelectedSong != null;
            });

            DeleteFestivalCommand = new RelayCommand(() =>
            {
                Festivals.Delete(SelectedFestival.Id);
            },
            () =>
            {
                return SelectedFestival != null;
            });

            //non cruds

            FestivalWithMostArtistsCommand = new RelayCommand(() =>
            {
                GetFestivalWithMostArtists();
            },
            () =>
            {
                return Festivals != null;
            });

            LongestSongOfArtistCommand = new RelayCommand(() =>
            {
                GetLongestSongOfArtist(SelectedArtist.Name);
            },
            () =>
            {
                return SelectedArtist != null;
            });

            ArtistWithMostAlbumsCommand = new RelayCommand(() =>
            {
                GetArtistWithMostAlbums(SelectedFestival.Location);
            },
            () =>
            {
                return SelectedFestival != null;
            });


            TotalDurationOfFestivalCommand = new RelayCommand(() =>
            {
                GetTotalDurationOfFestival(SelectedFestival.Id);
            },
            () =>
            {
                return SelectedFestival != null;
            });

            AlbumStatisticsCommand = new RelayCommand(() =>
            {
                GetAlbumStatistics();
            },
            () =>
            {
                return Artists != null;
            });

            ArtistStatisticsCommand = new RelayCommand(() =>
            {
                GetArtistStatistics();
            },
            () =>
            {
                return Songs != null;
            });

            SpecificSongFinderCommand = new RelayCommand(() =>
            {
                GetSpecificSongFinder(SelectedArtist.Name, SelectedGenre);
            },
            () =>
            {
                return SelectedArtist != null;
            });

            SelectedArtist = new Artist();
            SelectedSong = new Song();
            SelectedFestival = new Festival(3);

        }
    }
}
