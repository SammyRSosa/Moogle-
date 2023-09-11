using System.Text.RegularExpressions;
namespace MoogleEngine;

public class Snipet
{
    public Dictionary<string,string> Respuesta{get; private set;}

    public Snipet(Dictionary<string,float> Resultados, Query Propiedadesdelquery, Dictionary<string,float> EveryWord)
    {
        this.Respuesta = new Dictionary<string, string>();
        Dictionary<string,string> Snippet = EncontrarSnippet(Resultados,Propiedadesdelquery,EveryWord);
    }

    Dictionary<string,string> EncontrarSnippet(Dictionary<string,float> Resultados,Query propiedadesdelquery,Dictionary<string,float> EveryWord)
    {
        foreach (string Texto in Resultados.Keys)
        {

            Dictionary<string,Dictionary<string,float>> UniversodeSnipets = new Dictionary<string, Dictionary<string, float>>();
            Dictionary<string,float> posibles = new Dictionary<string, float>();
            string Todo = File.ReadAllText(Texto);
            
            string[] palabrasdeltexto = Todo.Split(' ',StringSplitOptions.RemoveEmptyEntries);

            string[] Trozos = new string[palabrasdeltexto.Length/20 + 1];

            for (int i = 0; i < palabrasdeltexto.Length; i++)
            {
                Trozos[i/20] += " " + palabrasdeltexto[i];
            }

            foreach (string trocitos in Trozos)
            {
                if(trocitos != null)
                {
                    string[] palabrasdeltrocito = Universo.GetWords(trocitos);

                    Dictionary<string,float> TFsdelSnippet = new Dictionary<string, float>();

                    foreach (string palabra in palabrasdeltrocito)
                    {

                        if(TFsdelSnippet.ContainsKey(palabra))
                        {
                            TFsdelSnippet[palabra]++;
                        }
                        else
                        {
                            TFsdelSnippet.Add(palabra,1);
                        }
                    }

                    foreach (string item in TFsdelSnippet.Keys)
                    {
                        TFsdelSnippet[item] *= EveryWord[item]; 
                    }
                    
                    if(!UniversodeSnipets.ContainsKey(trocitos))
                    UniversodeSnipets.Add(trocitos,TFsdelSnippet);
                    else
                    UniversodeSnipets[trocitos]= TFsdelSnippet;
                }
            }

            foreach (string eso in UniversodeSnipets.Keys)
            {
                posibles.Add(eso,Similarity.similitudelcoseno(eso,UniversodeSnipets,propiedadesdelquery.QuerycomoDocumento));
            }

            string a = MejorSnippets(posibles);

            Respuesta.Add(Texto,a);
        }
        return Respuesta;
    }
    static string MejorSnippets(Dictionary<string,float> keyValuePairs)
    {
        float bas = float.MaxValue;
        string resp = "";

        foreach (string key in keyValuePairs.Keys)
        {   
            if(keyValuePairs[key] < bas)
            {
                resp = key;
                bas = keyValuePairs[key];
            }
        }
        return resp;
    }
}