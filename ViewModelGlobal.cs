using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp;

public partial class ViewModelGlobal : ObservableObject
{
    //esta property es global, común a todos los view models. La usamos para saber si se están haciendo peticiones 
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    bool isBusy;

    public bool IsNotBusy => !IsBusy;

    //public event PropertyChangedEventHandler PropertyChanged;

    //public void RaisePropertyChanged([CallerMemberName] string propertyName = null)
    //{ 
    //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    //}
}
