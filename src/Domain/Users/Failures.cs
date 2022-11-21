﻿namespace MultiProject.Delivery.Domain.Users;

public static class Failures
{
    public static Error InvalidPhoneNumber => Error.Validation(nameof(InvalidPhoneNumber));
    public static Error UserDoesNotMeetRole => Error.Validation(nameof(UserDoesNotMeetRole));
}