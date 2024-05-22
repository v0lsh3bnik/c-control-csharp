namespace MalDev;

public class Customization
{
    public string NL; // shortcut
    public string NORMAL;
    public string RED;
    public string GREEN;
    public string YELLOW;
    public string BLUE;
    public string MAGENTA;
    public string CYAN;
    public string GREY;
    public string BOLD;
    public string NOBOLD;
    public string UNDERLINE;
    public string NOUNDERLINE;
    public string REVERSE;
    public string NOREVERSE;

    public string INFO;
    public string SUCCESS;
    public string ERROR;
    
    public Customization()
    {
        NL = Environment.NewLine; // shortcut
        NORMAL = Console.IsOutputRedirected ? "" : "\x1b[39m";
        RED = Console.IsOutputRedirected ? "" : "\x1b[91m";
        GREEN = Console.IsOutputRedirected ? "" : "\x1b[92m";
        YELLOW = Console.IsOutputRedirected ? "" : "\x1b[93m";
        BLUE = Console.IsOutputRedirected ? "" : "\x1b[94m";
        MAGENTA = Console.IsOutputRedirected ? "" : "\x1b[95m";
        CYAN = Console.IsOutputRedirected ? "" : "\x1b[96m";
        GREY = Console.IsOutputRedirected ? "" : "\x1b[97m";
        BOLD = Console.IsOutputRedirected ? "" : "\x1b[1m";
        NOBOLD = Console.IsOutputRedirected ? "" : "\x1b[22m";
        UNDERLINE = Console.IsOutputRedirected ? "" : "\x1b[4m";
        NOUNDERLINE = Console.IsOutputRedirected ? "" : "\x1b[24m";
        REVERSE = Console.IsOutputRedirected ? "" : "\x1b[7m";
        NOREVERSE = Console.IsOutputRedirected ? "" : "\x1b[27m";

        INFO = $"{YELLOW}[*]{NORMAL}";
        SUCCESS = $"{GREEN}[+]{NORMAL}";
        ERROR = $"{RED}[-]{NORMAL}";
        // Console.WriteLine(
        // $"This is {RED}Red{NORMAL}, {GREEN}Green{NORMAL}, {YELLOW}Yellow{NORMAL}, {BLUE}Blue{NORMAL}, {MAGENTA}Magenta{NORMAL}, {CYAN}Cyan{NORMAL}, {GREY}Grey{NORMAL}! ");
        // Console.WriteLine(
        // $"This is {BOLD}Bold{NOBOLD}, {UNDERLINE}Underline{NOUNDERLINE}, {REVERSE}Reverse{NOREVERSE}! ");
    }
}