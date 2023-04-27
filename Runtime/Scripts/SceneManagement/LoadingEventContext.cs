using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;

namespace Common.Runtime.SceneManagement
{
    public sealed class LoadingEventContext : INotifyPropertyChanged
    {
        private bool _isCompleted;
        private float _progress = 0;
        private string _loadingText = "";
        
        public bool IsCompleted
        {
            get => this._isCompleted;
            private set
            {
                this._isCompleted = value;
                this.OnPropertyChanged();
            }
        }

        public float Progress
        {
            get => this._progress;
            set
            {
                this._progress = value;
                this.OnPropertyChanged();
            }
        }

        public string LoadingText
        {
            get => this._loadingText;
            set
            {
                this._loadingText = value;
                this.OnPropertyChanged();
            }
        }
        
        public void Complete()
        {
            this.Progress = 1;
            this.IsCompleted = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}