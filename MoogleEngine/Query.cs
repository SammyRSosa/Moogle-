namespace MoogleEngine;
public class Query
{
    public Dictionary<string,float> Wanted{get; private set;}
    public List<string> Banned{get; private set;}
    public List<string> Subject{get; private set;}
    public Dictionary<string,float> QuerycomoDocumento{get; private set;}
    public string sugestion{get; private set;}
    private string[] Arrayinput;
    public Query(string input, Dictionary<string,float> EveryWord)
    {
        this.QuerycomoDocumento = new Dictionary<string, float>();
        this.Arrayinput = input.ToLower().Split(' ',StringSplitOptions.RemoveEmptyEntries);
        this.Wanted = new Dictionary<string, float>();
        this.Banned = new List<string>();
        this.Subject = new List<string>();
        this.Process(input,EveryWord); 
        this.sugestion = SacarSugerencia(this.Arrayinput, EveryWord);
        GetWanted(Wanted,QuerycomoDocumento);
    }

    void Process(string input,Dictionary<string,float> EveryWord)
    {
        for (int i = 0; i < this.Arrayinput.Length; i++)
        {
            switch (this.Arrayinput[i][0])
            {
                case '^':
                    Arrayinput[i] = Universo.Normalize(this.Arrayinput[i].Substring(1,this.Arrayinput[i].Length-1));
                    
                    if (!QuerycomoDocumento.ContainsKey(this.Arrayinput[i]))
                    {
                        QuerycomoDocumento.Add(this.Arrayinput[i],1);                        
                    }
                    else
                    {
                        QuerycomoDocumento[this.Arrayinput[i]]++;
                    }

                    Subject.Add(this.Arrayinput[i]);
                break;

                case '!':
                    this.Arrayinput[i] = Universo.Normalize(this.Arrayinput[i].Substring(1,this.Arrayinput[i].Length-1));

                    if (!QuerycomoDocumento.ContainsKey(this.Arrayinput[i]))
                    {
                        QuerycomoDocumento.Add(this.Arrayinput[i],1);                        
                    }
                    else
                    {
                        QuerycomoDocumento[this.Arrayinput[i]]++;
                    }
                    
                    Banned.Add(this.Arrayinput[i]);
                    break;

                case '*':
                    int count = 0;
                    for (int j = 0; j < this.Arrayinput[i].Length; j++)
                    {
                        if (this.Arrayinput[i][j] == '*')
                        {
                            count++;
                        }
                    }

                    this.Arrayinput[i] = this.Arrayinput[i].Substring(count,Arrayinput[i].Length-count);
                    this.Arrayinput[i] = Universo.Normalize(this.Arrayinput[i]);

                     if (QuerycomoDocumento.ContainsKey(this.Arrayinput[i]))
                    {
                        QuerycomoDocumento[this.Arrayinput[i]]++;    
                    }
                    else
                    {
                        QuerycomoDocumento.Add(this.Arrayinput[i],1); 
                    }
                    Wanted.Add(this.Arrayinput[i],count);
                    break;

                default:
                    Arrayinput[i] = Universo.Normalize(Arrayinput[i]);
                    if (!QuerycomoDocumento.ContainsKey(this.Arrayinput[i]))
                    {
                        QuerycomoDocumento.Add(this.Arrayinput[i],1);                        
                    }
                    else
                    {
                        QuerycomoDocumento[this.Arrayinput[i]]++;
                    }
                    break;
            }            
        }
        
    }
   
    string SacarSugerencia(string[] Arrayimput,Dictionary<string,float> EveryWord)
    {
        string sug = "";
        foreach (string palabranoencontrad in Arrayimput)
        {
            if (!EveryWord.ContainsKey(palabranoencontrad))
            {
                QuerycomoDocumento.Remove(palabranoencontrad);
                sug += " " + (PalabraMasCercana(palabranoencontrad,EveryWord));
                break;
            }
            else
            {
                sug += " " + palabranoencontrad;
            }
        }

        return sug;
    }

    string PalabraMasCercana(string asunto,Dictionary<string,float> EveryWord)
    {
        float valormaximo = int.MaxValue;
        string result = "";
        foreach(string palabrareal in EveryWord.Keys)
        {  
           float valorcercano = DistanciaDeLevenshtein(asunto,palabrareal);

           if (valorcercano < valormaximo)
           {
                result = palabrareal;
                valormaximo = valorcercano; 
           }
        } 

        return result;
    }


       private static float DistanciaDeLevenshtein(string s, string t)
        {
            float[,] distance = new float[s.Length + 1, t.Length + 1];

            for (int i = 0; i <= s.Length; i++)
            {
                distance[i, 0] = i;
            }

            for (int j = 0; j <= t.Length; j++)
            {
                distance[0, j] = j;
            }

            for (int i = 1; i <= s.Length; i++)
            {
                for (int j = 1; j <= t.Length; j++)
                {
                    int cost = (s[i - 1] == t[j - 1]) ? 0 : 1;

                    distance[i, j] = Math.Min(
                        Math.Min(distance[i - 1, j] + 1, distance[i, j - 1] + 1),
                        distance[i - 1, j - 1] + cost
                    );
                }
            }

            return distance[s.Length, t.Length];
        }
     
     
    private void GetWanted(Dictionary<string,float> Wanted,Dictionary<string,float> QuerycomoDocumento)
    {
        foreach (string item in Wanted.Keys)
        {
            QuerycomoDocumento[item] *= Wanted[item];
        }
    }
}