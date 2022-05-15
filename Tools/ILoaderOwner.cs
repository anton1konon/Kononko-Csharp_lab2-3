using System.Windows;

namespace Task2.Tools
{
    internal interface ILoaderOwner 
    {
        Visibility LoaderVisibility { get; set; }
        bool IsControlEnabled { get; set; }
    }
}
