using GalaSoft.MvvmLight.Ioc;
using PokeDaniel.Services;
using PokeDaniel.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokeDaniel
{
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            SimpleIoc.Default.Register<MainViewModel>();

            SimpleIoc.Default.Register<IPokeClient, PokeClient>();
            SimpleIoc.Default.Register<AudioService>();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public MainViewModel Main
        {
            get
            {
                return SimpleIoc.Default.GetInstance<MainViewModel>();
            }
        }
    }
}
