using System.Text.RegularExpressions;

namespace Identity;

public class Cpf
{
    public string Value { get; }
    public string Masked { get; }
    public bool IsValid { get; }

    public Cpf(string cpf)
    {
        cpf = string.Join("", cpf.Where(char.IsNumber));
        if (cpf.Length != 11)
        {
            throw new ArgumentException("CPF deve ter 11 caracteres", nameof(cpf));
        }

        Value = cpf;
        Masked = Regex.Replace(cpf, @"(\d{3})(\d{3})(\d{3})(\d{2})", "$1.$2.$3-$4", RegexOptions.Compiled, TimeSpan.FromMicroseconds(2));
        IsValid = Validate(cpf);
    }

    private static bool Validate(string cpf)
    {
        return Validate(cpf, 9, 0) && Validate(cpf, 10, 1);
    }

    private static bool Validate(string cpf, int countChar, int digit)
    {
        var numbers = cpf[0..countChar];
        var digitValidator = cpf[9..];
        var reverseIndex = numbers.Length + 1;
        var sum = numbers.Aggregate(seed: 0, (atual, next) => atual + int.Parse($"{next}") * reverseIndex--);
        var mod11 = sum % 11;
        var mod11result = mod11 < 2 ? 0 : 11 - mod11;

        return digitValidator[digit] == char.Parse($"{mod11result}");
    }

    public static implicit operator Cpf(string cpf)
        => new(cpf);

    public static implicit operator string(Cpf cpf)
        => cpf.Masked;

    public static implicit operator bool(Cpf cpf)
        => cpf.IsValid;
}