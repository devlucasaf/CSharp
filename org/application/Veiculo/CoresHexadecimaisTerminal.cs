namespace CSharpRepositorio.org.application.Veiculo;

public static class CoresHexadecimaisTerminal
{
    public const string RESET = "\u001b[0m";

    public static string CorHexadecimal(int r, int g, int b)
    {
        return "\u001b[38;2;" + r + ";" + g + ";" + b + "m";
    }

    public static readonly string WHITE         = CorHexadecimal(255, 250, 250); // 1
    public static readonly string LEMON_CHIFFON = CorHexadecimal(255, 250, 205); // 2
    public static readonly string ROYAL_BLUE    = CorHexadecimal(65, 105, 225);  // 3
    public static readonly string AQUA_MARINE   = CorHexadecimal(127, 255, 212); // 4
    public static readonly string GOLD          = CorHexadecimal(255, 215, 0);   // 5
    public static readonly string FOREST_GREEN  = CorHexadecimal(34, 139, 34);   // 6
    public static readonly string SPRING_GREEN  = CorHexadecimal(0, 255, 127);   // 7
    public static readonly string CYAN          = CorHexadecimal(0, 255, 255);   // 8
    public static readonly string BEIGE         = CorHexadecimal(245, 245, 220); // 9
    public static readonly string FIRE_BRICK    = CorHexadecimal(178, 34, 34);   // 10
    public static readonly string RED           = CorHexadecimal(255, 0, 0);     // 11
    public static readonly string DARK_VIOLET   = CorHexadecimal(148, 0, 211);   // 12
    public static readonly string DEEP_SKY_BLUE = CorHexadecimal(0, 191, 255);   // 13
    public static readonly string DODGER_BLUE   = CorHexadecimal(24, 116, 205);  // 14
    public static readonly string SLATE_BLUE    = CorHexadecimal(71, 60, 139);   // 15
    public static readonly string TOMATO        = CorHexadecimal(255, 99, 71);   // 12
}