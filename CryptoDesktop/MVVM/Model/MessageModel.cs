﻿using System;

namespace CryptoDesktop.MVVM.Model;

public class MessageModel
{
    public CryptoDesktop.MVVM.Model.ContactModel Owner { get; set; }
    public string Username => Owner.Username;
    public string UsernameColor => Owner.Color; 
    public string ImageSource => Owner.ImageSource;
    public Message Message { get; set; }
    
    
    public DateTime Time { get; set; }
    public bool IsNativeOrigin { get; set; }
    public bool IsFirstMessage { get; set; }
}