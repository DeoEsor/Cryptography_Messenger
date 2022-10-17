using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using CryptoDesktop.Annotations;

namespace CryptoDesktop.MVVM.Model;

public sealed class ContactModel : INotifyPropertyChanged
{
    private string _username;
    private string _status;
    private string _imageSource;
    public string Password { get; set; }
    public string Color { get; set; }
    public int Id { get; set; }
    
    public ObservableCollection<MessageModel> Messages { get; set; }
    
    public string LastMessage
    {
        get
        {
            if (Messages.Count == 0)
                return String.Empty;
            
            var str = Messages.Last().Message.Type == Message.MessageType.String
                ? Encoding.UTF8.GetString(Messages.Last().Message.MessageData)
                : "File";
            return str;
        }
    }

    public string Username
    {
        get => _username;
        set
        {
            _username = value;
            OnPropertyChanged();
        }
    }

    public string ImageSource
    {
        get => _imageSource;
        set
        {
            _imageSource = value;
            OnPropertyChanged();
        }
    }

    public string Status
    {
        get => _status;
        set
        {
            _status = value;
            OnPropertyChanged();
        }
    }

    
    public event PropertyChangedEventHandler? PropertyChanged;

    [NotifyPropertyChangedInvocator]
    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}