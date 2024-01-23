﻿namespace ProjectSimple.Application.Validations;

public class ValidationResult
{
    public bool IsValid => Errors.Count == 0;
    public List<string> Errors { get; set; } = [];
}
