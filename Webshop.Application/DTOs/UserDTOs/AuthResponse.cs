﻿namespace Webshop.Application.DTOs.UserDTOs;

public class AuthResponse
{
    public string Token { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
}
