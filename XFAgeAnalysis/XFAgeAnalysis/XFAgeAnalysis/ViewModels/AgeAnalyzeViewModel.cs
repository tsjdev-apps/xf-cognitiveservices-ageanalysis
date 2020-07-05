using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;
using XFAgeAnalysis.Interfaces;

namespace XFAgeAnalysis.ViewModels
{
    public class AgeAnalyzeViewModel : INotifyPropertyChanged
    {
        private readonly IDataService _dataService;
        private readonly IPhotoService _photoService;


        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set { _isLoading = value; OnPropertyChanged(); CheckCommandsStates(); }
        }

        private byte[] _imageBytes;
        public byte[] ImageBytes
        {
            get => _imageBytes;
            set { _imageBytes = value; OnPropertyChanged(); CheckCommandsStates(); }
        }

        private int? _analyzedAge;
        public int? AnalyzedAge
        {
            get => _analyzedAge;
            set { _analyzedAge = value; OnPropertyChanged(); CheckCommandsStates(); }
        }


        private Command _takePictureCommand;
        public Command TakePictureCommand => _takePictureCommand ?? (_takePictureCommand = new Command(async () => await TakePictureAsync(), CanTakeOrPickPicture));

        private Command _pickPictureCommand;
        public Command PickPictureCommand => _pickPictureCommand ?? (_pickPictureCommand = new Command(async () => await PickPictureAsync(), CanTakeOrPickPicture));

        private Command _analyzePictureCommand;
        public Command AnalyzePictureCommand => _analyzePictureCommand ?? (_analyzePictureCommand = new Command(async () => await AnalyzePictureAsync(), CanAnalyzeImage));


        public AgeAnalyzeViewModel(IDataService dataService, IPhotoService photoService)
        {
            _dataService = dataService;
            _photoService = photoService;
        }


        private async Task TakePictureAsync()
        {
            if (IsLoading)
                return;

            ImageBytes = null;
            AnalyzedAge = null;
            IsLoading = true;

            try
            {
                ImageBytes = await _photoService.TakePictureAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{GetType().Name} | {nameof(TakePictureAsync)} | {ex}");
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async Task PickPictureAsync()
        {
            if (IsLoading)
                return;

            ImageBytes = null;
            AnalyzedAge = null;
            IsLoading = true;

            try
            {
                ImageBytes = await _photoService.PickPictureAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{GetType().Name} | {nameof(PickPictureAsync)} | {ex}");
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async Task AnalyzePictureAsync()
        {
            if (IsLoading)
                return;

            IsLoading = true;
            AnalyzedAge = null;

            try
            {
                var age = await _dataService.AnalyzePictureAsync(ImageBytes);
                if (age.HasValue)
                    AnalyzedAge = (int)age.Value;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{GetType().Name} | {nameof(AnalyzePictureAsync)} | {ex}");
                throw;
            }
            finally
            {
                IsLoading = false;
            }
        }

        private void CheckCommandsStates()
        {
            TakePictureCommand.ChangeCanExecute();
            PickPictureCommand.ChangeCanExecute();
            AnalyzePictureCommand.ChangeCanExecute();
        }

        private bool CanTakeOrPickPicture()
        {
            return !IsLoading;
        }

        private bool CanAnalyzeImage()
        {
            return ImageBytes != null && !IsLoading;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
