namespace MoogleEngine;
public static class Moogle
{
    public static SearchResult Query(string query, Universo universo) {
        // Modifique este método para responder a la búsqueda
        Query input = new Query(query, universo.EveryWord);
        Similarity simil = new Similarity(universo.Universe, universo.EveryWord, input);
        Snipet snip = new Snipet(simil.DocumentosRelacionados,input,universo.EveryWord);
        
        SearchItem[] items = GetResults(simil,snip);
        string suggestion = input.sugestion;

        return new SearchResult(items, suggestion);

            
        
    }
    private static string Name(string path) => path.Substring(0,path.Length-4).Split(new char[]{'\\', '/'}).Last();
    private static SearchItem[] GetResults(Similarity simil,Snipet snip)
    {
        int count = simil.DocumentosRelacionados.Count;
        SearchItem[] results = new SearchItem[count];

        for (int i = 0; i < count; i++)
        {
            var doc = simil.DocumentosRelacionados;

            string path = doc.Keys.ElementAt(i);
            float tfidf = doc.Values.ElementAt(i);

            string name = Name(path);
            string snnipet = snip.Respuesta[path];

            results[i] = new SearchItem(name,snnipet,tfidf);
        }

        return results;
    }
}
