using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace AppTextAnalytics.Models
{
    public class CarouselItem : INotifyPropertyChanged
    {
        private double _position;
        private double _rotation;
        private double _scale;

        public CarouselItem()
        {
            Scale = 1;
        }

        public double Position
        {
            get { return _position; }
            set
            {
                _position = value;
                OnPropertyChanged();
            }
        }

        public double Rotation
        {
            get { return _rotation; }
            set
            {
                _rotation = value;
                OnPropertyChanged();
            }
        }

        public double Scale
        {
            get { return _scale; }
            set
            {
                _scale = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
