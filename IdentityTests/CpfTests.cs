using Identity;

namespace IdentityTests;

public class CpfTests
{
    [Theory(DisplayName = "Gerar exceção se não contiver 11 caracteres")]
    [InlineData("1234567890")]
    [InlineData("012345678901")]
    public void Test01(string cpfInput)
    {
        Assert.Throws<ArgumentException>(() => new Cpf(cpfInput));
    }

    [Theory(DisplayName = "Contem apenas números mesmo que seja informado outros caracteres")]
    [InlineData("123.456.789-00", "12345678900")]
    [InlineData("012.345.678/90", "01234567890")]
    public void Test02(string cpfInput, string finalValue)
    {
        var value = new Cpf(cpfInput).Value;
        Assert.Equal(finalValue, value);
    }

    [Theory(DisplayName = "Valor deve poder ser usado formatado com a máscara '###.###.###-##'")]
    [InlineData("123.456.789-00", "123.456.789-00")]
    [InlineData("012.345.678/90", "012.345.678-90")]
    public void Test03(string cpfInput, string finalValue)
    {
        var value = new Cpf(cpfInput).Masked;
        Assert.Equal(finalValue, value);
    }

    [Theory(DisplayName = "CPF deve ser válido")]
    [InlineData("24535170070")]
    [InlineData("71179874056")]
    [InlineData("00383021014")]
    public void Test04(string cpfInput)
    {
        var value = new Cpf(cpfInput);
        Assert.True(value.IsValid);
    }

    [Theory(DisplayName = "Conversão implícita de string para objeto válido")]
    [InlineData("24535170070")]
    [InlineData("71179874056")]
    [InlineData("00383021014")]
    public void Test05(string cpfInput)
    {
        Cpf cpf = cpfInput;
        Assert.True(cpf);
    }

    [Theory(DisplayName = "Conversão implícita de string para objeto com máscara")]
    [InlineData("24535170070", "245.351.700-70")]
    [InlineData("71179874056", "711.798.740-56")]
    [InlineData("00383021014", "003.830.210-14")]
    public void Test06(string cpfInput, string masked)
    {
        Cpf cpf = cpfInput;
        Assert.Equal(masked, cpf);
    }
}