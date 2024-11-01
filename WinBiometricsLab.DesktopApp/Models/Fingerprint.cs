﻿using System.ComponentModel;
using WinBiometricDotNet;
using WinBiometricsLab.Core;
using WinBiometricsLab.Core.Results;

namespace WinBiometricsLab.DesktopApp.Models
{
    public class Fingerprint : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(
                [System.Runtime.CompilerServices.CallerMemberName]
            string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                RaisePropertyChanged(nameof(FullName));
            }
        }

        private FunctionType _assignedFunction;
        public FunctionType AssignedFunction
        {
            get => _assignedFunction;
            set
            {

                _assignedFunction = value;
                RaisePropertyChanged(nameof(FullName));

            }
        }

        public FingerPosition Position { get; set; }

        public string FullName => $"{Name} (Position: {Position}, Function: {AssignedFunction})";
    }
}
