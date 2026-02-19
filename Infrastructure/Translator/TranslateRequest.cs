namespace Infrastructure.Translator
{
    public partial class LibreTranslator
    {
        public record TranslateRequest(string q, string source, string target, string format = "text", int alternatives = 0);
    }
}