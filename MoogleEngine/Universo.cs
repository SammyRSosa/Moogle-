using System.Text.RegularExpressions;
namespace MoogleEngine;
public class Universo
{
    public Universo()
    {
        this.Universe = new Dictionary<string, Dictionary<string, float>>();
        this.EveryWord = new Dictionary<string, float>();
        this.EveryDocument = Directory.GetFiles("../Content", "*.txt", SearchOption.AllDirectories);
        ParsearDocumentos();
        IDF();
    }
    public string[] EveryDocument { get; private set; }
    public Dictionary<string, Dictionary<string, float>> Universe { get; private set; }
    public Dictionary<string, float> EveryWord { get; private set; }
    private void ParsearDocumentos()
    {
        foreach (string Document in EveryDocument)
        {
            string[] WxDocument = GetWords(File.ReadAllText(Document));

            Dictionary<string, float> WordsCount = new Dictionary<string, float> { };

            foreach (string Word in WxDocument)
            {
                if (!this.EveryWord.ContainsKey(Word))
                {
                    EveryWord.Add(Word, 1);
                }

                if (!WordsCount.ContainsKey(Word))
                {
                    WordsCount.Add(Word, 1);
                    EveryWord[Word]++;
                }
                else
                {
                    WordsCount[Word]++;
                }
            }

            Universe.Add(Document, WordsCount);
        }
    }
    public static string[] GetWords(string Document)
    {
        
        string[] Words = Normalize(Document).Split(' ', StringSplitOptions.RemoveEmptyEntries);
        return Words;
    }
    public static string Normalize(string Document)
    {
        
        string DocumentoProcesado;

        DocumentoProcesado = Regex.Replace(Document.ToLower(), "á", "a")
        .Replace("é", "e")
        .Replace("í", "i")
        .Replace("ó", "o")
        .Replace("ú", "u")
        .Replace("ñ", "n");

        return Regex
           .Replace(DocumentoProcesado, @"[^a-z0-9 ]+", " ");
    }
    void IDF()
    {
        foreach (string palabra in EveryWord.Keys)
        {
            EveryWord[palabra] = ((float)Math.Log10(EveryDocument.Length/EveryWord[palabra]));
        }

        foreach (string padre in Universe.Keys)
        {
            foreach (string hijo in Universe[padre].Keys)
            {
                Universe[padre][hijo] = Universe[padre][hijo] * EveryWord[hijo];
            }
        }
    }
}