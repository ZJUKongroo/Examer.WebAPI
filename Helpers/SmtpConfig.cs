// Copyright (c) ZJUKongroo. All Rights Reserved.

namespace Examer.Helpers;

public class SmtpConfig
{
    public string Host { get; set; } = string.Empty;
    public int Port { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public bool EnableSsl { get; set; }
};
