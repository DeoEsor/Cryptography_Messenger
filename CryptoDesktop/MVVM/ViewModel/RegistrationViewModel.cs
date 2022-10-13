using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CryptoDesktop.Annotations;
using CryptoDesktop.gRPC;
using CryptoDesktop.MVVM.Commands;
using CryptoDesktop.MVVM.Model;
using CryptoDesktop.UserControls;
using DryIoc;
using Grpc.Core;

namespace CryptoDesktop.MVVM.ViewModel;

public sealed class RegistrationViewModel : INotifyPropertyChanged
{
    public Window Owner { get; set; }
    public AuthClient CryptoClient { get; set; }
    public ICommand LoginCommand { get; set; }
    public ICommand RegisterCommand { get; set; }
    public string UserName { get; set; } = "Username";
    public RegistrationViewModel(Window owner)
    {
        Owner = owner;
        CryptoClient = App.Container.Resolve<AuthClient>();
        LoginCommand = new RelayCommand(Login);
        RegisterCommand = new RelayCommand(Register);
    }

    private async void Register(object obj)
    {
        if (obj is not PasswordBox passwordBox) return;
        var a =App.Container.Resolve<MainWindow>();
        var reply = await CryptoClient.Register(UserName, passwordBox.Password);
        
        if (reply.StatusCode != (int)StatusCode.OK)
        {
            var messageBox = reply.StatusCode switch
            {
                (int)StatusCode.Aborted => new MessageBoxCustom("Something with server, try later", MessageType.Error,
                    MessageButtons.Ok).ShowDialog(),
                (int)StatusCode.Unauthenticated => new MessageBoxCustom("This username is defined try another", MessageType.Error,
                    MessageButtons.Ok).ShowDialog(),
                _ => throw new NotImplementedException()
            };

            return;
        }

        var chat = App.Container.Resolve<ChatViewModel>();
        chat.User = new ContactModel
        {
            Username = reply.User.Username,
            ImageSource = reply.User.ImageSource,
            Id = (int)reply.User.Id,
            Color = reply.User.Color,
            Status = "Online"
        };
        a.DataContext = chat;
        a.Show();
        Owner.Close();
    }

    private async void Login(object obj)
    {
        if (obj is not PasswordBox passwordBox) return;
        var a =App.Container.Resolve<MainWindow>();
        var reply = await CryptoClient.AuthAsync(UserName, passwordBox.Password);
        
        if (reply.StatusCode != (int)StatusCode.OK)
        {
                // ignored
        }

        var chat = App.Container.Resolve<ChatViewModel>();
        chat.User = new ContactModel
        {
            Username = reply.User.Username,
            ImageSource = reply.User.ImageSource,
            Id = (int)reply.User.Id,
            Color = reply.User.Color,
            Status = "Online"
        };
        a.DataContext = chat;
        a.Show();
        Owner.Close();
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    [NotifyPropertyChangedInvocator]
    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}