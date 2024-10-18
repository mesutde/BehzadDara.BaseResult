using Microsoft.AspNetCore.Http;

namespace BehzadDara.BaseResult;

public class Result<T> : Result
{
    public T? Value { get; private set; }

    public void SetValue(T value)
    {
        Value = value;
    }
}

public class Result
{
    #region Properties
    public bool IsSuccess { get; set; }
    public int StatusCode { get; set; }
    public Dictionary<string, string[]> ValidationErrors { get; set; } = [];
    public List<string> ErrorMessages { get; set; } = [];
    public List<string> SuccessMessages { get; set; } = [];
    #endregion

    #region Messages
    public void AddSuccessMessage(string message)
    {
        if (!string.IsNullOrEmpty(message) && !SuccessMessages.Contains(message))
        {
            SuccessMessages.Add(message);
        }
    }

    public void AddErrorMessage(string message)
    {
        if (!string.IsNullOrEmpty(message) && !ErrorMessages.Contains(message))
        {
            ErrorMessages.Add(message);
        }
    }

    public void AddValidationErrors(Dictionary<string, string[]> ErrorMessages)
    {
        ValidationErrors = ErrorMessages;
    }
    #endregion

    #region HttpResults
    public void OK(string message = Messages.OK)
    {
        StatusCode = StatusCodes.Status200OK;
        Succeed(message);
    }

    public void NoContent()
    {
        StatusCode = StatusCodes.Status204NoContent;
        Succeed();
    }

    public void Redirect(string message = Messages.Redirect)
    {
        StatusCode = StatusCodes.Status308PermanentRedirect;
        Succeed(message);
    }

    public void BadRequest(Dictionary<string, string[]> ErrorMessages, string message = Messages.BadRequest)
    {
        StatusCode = StatusCodes.Status400BadRequest;
        Failed(message, ErrorMessages);
    }

    public void Unauthorized(string message = Messages.Unauthorized)
    {
        StatusCode = StatusCodes.Status401Unauthorized;
        Failed(message);
    }

    public void Forbidden(string message = Messages.Forbidden)
    {
        StatusCode = StatusCodes.Status403Forbidden;
        Failed(message);
    }

    public void NotFound(string message = Messages.NotFound)
    {
        StatusCode = StatusCodes.Status404NotFound;
        Failed(message);
    }

    public void MethodNotAllowed(string message = Messages.MethodNotAllowed)
    {
        StatusCode = StatusCodes.Status405MethodNotAllowed;
        Failed(message);
    }

    public void Conflict(string message = Messages.Conflict)
    {
        StatusCode = StatusCodes.Status409Conflict;
        Failed(message);
    }

    public void TooManyRequest(string message = Messages.TooManyRequest)
    {
        StatusCode = StatusCodes.Status429TooManyRequests;
        Failed(message);
    }

    public void NotImplemented(string message = Messages.NotImplemented)
    {
        StatusCode = StatusCodes.Status501NotImplemented;
        Failed(message);
    }

    public void InternalServerError(string message = Messages.InternalServerError)
    {
        StatusCode = StatusCodes.Status500InternalServerError;
        Failed(message);
    }
    #endregion

    #region IsSuccess
    private void Succeed(string message)
    {
        AddSuccessMessage(message);
        Succeed();
    }
    private void Succeed()
    {
        IsSuccess = true;
    }

    private void Failed(string message)
    {
        AddErrorMessage(message);
        Failed();
    }

    private void Failed()
    {
        IsSuccess = false;
    }

    private void Failed(string message, Dictionary<string, string[]> ErrorMessages)
    {
        Failed(message);
        AddValidationErrors(ErrorMessages);
    }
    #endregion

    #region Static Success
    public static Result Success(string message = Messages.OK)
    {
        var result = new Result();
        result.OK(message);
        return result;
    }

    public static Result<T> Success<T>(T value, string message = Messages.OK)
    {
        var result = new Result<T>();
        result.SetValue(value);
        result.OK(message);
        return result;
    }
    #endregion
}