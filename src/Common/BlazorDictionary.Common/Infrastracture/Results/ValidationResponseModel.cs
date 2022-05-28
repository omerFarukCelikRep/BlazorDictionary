﻿using System.Text.Json.Serialization;

namespace BlazorDictionary.Common.Infrastracture.Results;
public class ValidationResponseModel
{
    public IEnumerable<string> Errors { get; set; }

    public ValidationResponseModel(IEnumerable<string> errors)
    {
        Errors = errors;
    }

    public ValidationResponseModel(string message) : this(new List<string>() { message }) { }

    [JsonIgnore]
    public string FlattenErrors => Errors is not null ? string.Join(Environment.NewLine, Errors) : string.Empty;
}
