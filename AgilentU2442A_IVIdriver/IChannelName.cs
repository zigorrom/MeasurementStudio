using System;
namespace AgilentU2442A_IVIdriver
{
    interface IChannelName
    {
        //string Alias { get; set; }
        string NativeName { get; }// set; }
        ChannelEnum ChannelIdentifier { get;}// set; }
    }
}
