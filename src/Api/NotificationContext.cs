﻿using Custom_Architecture.Enums;
using System.Collections.ObjectModel;

namespace Custom_Architecture;

public class NotificationContext
{
    public IReadOnlyCollection<ErrorMessage> ErrorMessages { get => new ReadOnlyCollection<ErrorMessage>(_errors); }
    public bool IsValid { get => _errors.Count == 0; }

    private readonly IList<ErrorMessage> _errors = new List<ErrorMessage>();

    public void AddNotification(ErrorMessage errorMessage)
    {
        _errors.Add(errorMessage);
    }

    public void AddNotification(string errorCode, string message)
    {
        _errors.Add(new()
        {
            ErrorType = ErrorType.Validation,
            ErrorCode = errorCode,
            Message = message
        });
    }

    public void AddNotification(string errorCode, string message, ErrorType errorType)
    {
        _errors.Add(new()
        {
            ErrorType = errorType,
            ErrorCode = errorCode,
            Message = message
        });
    }
}
