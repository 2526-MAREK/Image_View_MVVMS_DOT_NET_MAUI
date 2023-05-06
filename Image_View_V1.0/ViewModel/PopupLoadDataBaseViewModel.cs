/**
* @file PopupLoadDataBaseViewModel.cs
* @brief Contains the PopupLoadDataBaseViewModel class, which represents the popup window for loading images from the database.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
* @brief Represents the popup window for loading images from the database.
*/
namespace Image_View_V1._0.ViewModel
{
    /**
    * @brief Represents the popup window for loading images from the database.
    */
    partial class PopupLoadDataBaseViewModel : INotifyPropertyChanged
    {

        /*[ObservableProperty]
        public IEnumerable<ImageToProcess> imageAfterProcessList;
        public PopupLoadDataBaseViewModel(IEnumerable<ImageToProcess> imageAfterProcessList)
        {
            this.imageAfterProcessList = imageAfterProcessList;
        }
        [RelayCommand]
        public async Task LoadImages()
        {

        }*/
       


        private ObservableCollection<IEnumerable<ImageToProcess>> _imageAfterProcessList;
        public ObservableCollection<IEnumerable<ImageToProcess>> imageAfterProcessList
        {
            get { return _imageAfterProcessList; }
            set
            {
                _imageAfterProcessList = value;
                OnPropertyChanged(nameof(imageAfterProcessList));
            }
        }

        // Implementacja interfejsu INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public PopupLoadDataBaseViewModel(IEnumerable<ImageToProcess> imageAfterProcessList)
        {
            ObservableCollection<ImageToProcess> imageCollection = new ObservableCollection<ImageToProcess>(imageAfterProcessList); // utwórz nową kolekcję

            ObservableCollection<IEnumerable<ImageToProcess>> collection = new ObservableCollection<IEnumerable<ImageToProcess>>(); // utwórz kolekcję zawierającą kolekcje obrazów
            collection.Add(imageCollection);
            this.imageAfterProcessList = collection;
        }
    }
}
