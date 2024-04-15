namespace Harmonic.Shared.Constants;

public class CONSTANTS
{
    public static readonly INT_CNT INT = new();
    public static readonly DATE_CNT DATE = new();
    public static readonly STRING_CNT STRING = new();
}

public class DATE_CNT
{
    public readonly string INVALIDA = "Forneça uma data válida";
}

public class INT_CNT
{
    public readonly int DEFAULT_MAX_VALUE = 45;
    public readonly string MESSAGE_MAIOR_QUE_ZERO = "Valor deve ser maior do que zero";
    public readonly string MESSAGE_VALOR_INCOMPLETO = "Valor Incompleto";
    public readonly string MESSAGE_VALOR_MAXIMO_FORNECIDO = "Valor fornecido é superior ao permitido";
    public readonly string MESSAGE_VAZIO = "Valor não pode ser vazio";
    public readonly string MESSAGE_INVALIDO = "Valor fornecido é inválido";
}

public class STRING_CNT
{
    public readonly string MESSAGE_VAZIO = "Não pode ser vazio";
    public readonly string MESSAGE_TAMANHO_EXCEDIDO = "Valor excedido";
}
