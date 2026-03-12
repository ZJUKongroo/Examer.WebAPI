// Copyright (c) ZJUKongroo. All Rights Reserved.

namespace Examer.Helpers;

public class MailConfig
{
    public string From { get; set; } = string.Empty;
    public string ActivateAccountSubject { get; set; } = string.Empty;
    public string ActivateAccountBody { get; set; } = string.Empty;
    public string ResetPasswordSubject { get; set; } = string.Empty;
    public string ResetPasswordBody { get; set; } = string.Empty;
}
