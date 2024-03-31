using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using DynamicForms.Example.Shared;

namespace DynamicForms.Example.Avalonia.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
#pragma warning disable CA1822 // Mark members as static
    public string Greeting => "Welcome to Avalonia!";
#pragma warning restore CA1822 // Mark members as static

    public ExampleSettings Example { get; set; } = new();

    public MainWindowViewModel()
    {
        using var linkStream = File.OpenRead("yoshi.1.png");
        LinkSprite = Bitmap.DecodeToWidth(linkStream, 64);
        
        using var samusStream = File.OpenRead("rash.png");
        SamusSprite = Bitmap.DecodeToWidth(samusStream, 212);
        
        using var shipStream = File.OpenRead("Shak's Stash by Phiggle.png");
        ShipSprite = Bitmap.DecodeToWidth(shipStream, 248);

        Example.ButtonTest += (sender, args) =>
        {
            Console.WriteLine("Button pressed");
        };
    }

    public Bitmap LinkSprite { get; set; }
    public Bitmap SamusSprite { get; set; }
    public Bitmap ShipSprite { get; set; }
}