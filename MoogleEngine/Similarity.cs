using System.Linq;
namespace MoogleEngine;
public class Similarity
{
    public Similarity(Dictionary<string,Dictionary<string,float>> Universe, Dictionary<string,float> Everyword, Query propiedadesdelquery)
    {
        this.DocumentosRelacionados = new Dictionary<string,float>();
        EncontrarPosiblesResultados(Universe,propiedadesdelquery);
    }

    public Dictionary<string,float> DocumentosRelacionados = new Dictionary<string, float>{};
    Dictionary<string,float> PosiblesResultados = new Dictionary<string,float>();

    void EncontrarPosiblesResultados(Dictionary<string,Dictionary<string,float>> Universe,Query propiedadesdelquery)
    {
        foreach (string Tittle in Universe.Keys)
        {
            bool aciertos = false;
            bool ban = true;
            foreach(string palabrasdelquery in propiedadesdelquery.QuerycomoDocumento.Keys) 
            {

                    foreach (string baneadas in propiedadesdelquery.Banned)
                    {
                        if(Universe[Tittle].ContainsKey(baneadas))
                        ban = false;
                    }
                    foreach(string asunto in propiedadesdelquery.Subject)
                    {
                        if (!Universe[Tittle].ContainsKey(asunto))
                        ban = false;
                    }
                    if (Universe[Tittle].ContainsKey(palabrasdelquery))
                    {
                        aciertos = true;
                    }
            }
            if(aciertos && ban)
            {
                PosiblesResultados.Add(Tittle,0);
            }
        }

        AsociarScore(PosiblesResultados,Universe,propiedadesdelquery);
    }
    void AsociarScore(Dictionary<string,float> Documentos,Dictionary<string,Dictionary<string,float>> Universe,Query propiedadesdelquery)
    {
        foreach (string Tittle in Documentos.Keys)
        {
            Documentos[Tittle] = similitudelcoseno(Tittle,Universe,propiedadesdelquery.QuerycomoDocumento);
        }

        var intermedio = Documentos.OrderBy(x => x.Value).Take(10);
        DocumentosRelacionados = intermedio.ToDictionary(x => x.Key, x => x.Value);
    }
    public static float similitudelcoseno(string Titulo,Dictionary<string,Dictionary<string,float>> Universe,Dictionary<string,float> QuerycomoDocumento)
    {
        float producto = 0;
        foreach (string palabra in QuerycomoDocumento.Keys.Intersect(Universe[Titulo].Keys))
        {
            producto += QuerycomoDocumento[palabra] * Universe[Titulo][palabra];
        }

        float mag1 = ((float)Math.Sqrt(QuerycomoDocumento.Values.Sum(x => x * x)));
        float mag2 =  ((float)Math.Sqrt(Universe[Titulo].Values.Sum(x => x * x)));

        float Cosine = producto/(mag1 * mag2);

        return Cosine;
    }
}