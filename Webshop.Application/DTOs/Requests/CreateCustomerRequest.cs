﻿namespace Webshop.Application.DTOs.Requests;

public class CreateCustomerRequest
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string StreetAdress { get; set; } = string.Empty;
    public int PostalCode { get; set; }
    public string City { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;
}
