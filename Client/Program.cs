using System;

class Program
{
    static async Task Main()
    {
        HttpClient httpClient = new HttpClient();
        HttpResponseMessage response = await httpClient.GetAsync("https://localhost:7050/api/Joke?jokeCategorie=1");
        string data = await response.Content.ReadAsStringAsync();
        Console.WriteLine(data);
        //Console.WriteLine("What ");
    }
}