using CQD4CS_HFT_2022232.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CQD4CS_HFT_2022232.WpfClient
{
    public class MainWindowViewModel : ObservableRecipient
    {
        public RestCollection<Artist> Artists { get; set; }
        public RestCollection<Song> Songs { get; set; }
        public RestCollection<Festival> Festivals { get; set; }
        public RestService rest {  get; set; }

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

        public string FestivalWithMostArtists { get; set; }

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


        public ICommand CreateArtistCommand { get; set; }
        public ICommand DeleteArtistCommand { get; set; }
        public ICommand UpdateArtistCommand { get; set; }

        public ICommand CreateSongCommand { get; set; }
        public ICommand DeleteSongCommand { get; set; }
        public ICommand UpdateSongCommand { get; set; }

        public ICommand CreateFestivalCommand { get; set; }
        public ICommand DeleteFestivalCommand { get; set; }
        public ICommand UpdateFestivalCommand { get; set; }

        public ICommand LongestSongOfArtistCommand { get; set; }


        public string getLongestSongOfArtist(string artistName)
        {

            LongestSongOfArtist = rest.GetSingle<string>($"Stat/LongestSongOfArtist?artistName={artistName}");
            
            return LongestSongOfArtist;
        }

        public MainWindowViewModel()
        {
            rest = new RestService("http://localhost:36286/");
            Artists = new RestCollection<Artist>("http://localhost:36286/", "artist", "hub");
            Songs = new RestCollection<Song>("http://localhost:36286/", "song", "hub");
            Festivals = new RestCollection<Festival>("http://localhost:36286/", "festival", "hub");
            FestivalWithMostArtists = new RestService("http://localhost:36286/").GetSingle<string>("Stat/FestivalWithMostArtists");
            //if (SelectedArtist != null)
            //{
            //    LongestSongOfArtist = new RestService("http://localhost:36286/")
            //        .GetSingle<string>($"Stat/LongestSongOfArtist?artistName={SelectedArtist.Name}");
            //}



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

            LongestSongOfArtistCommand = new RelayCommand(() =>
            {
                getLongestSongOfArtist(SelectedArtist.Name);
                
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
