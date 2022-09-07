using System.Net.Http;

// HttpClient is intended to be instantiated once per application, rather than per-use. See Remarks.

HttpClient client = new HttpClient();

try
{
    HttpResponseMessage response = await client.GetAsync("https://www.timeanddate.no/vaer/?continent=europe&low=c");
    response.EnsureSuccessStatusCode();
    string responseHTML = await response.Content.ReadAsStringAsync();
    // Above three lines can be replaced with new helper method below
    // string responseBody = await client.GetStringAsync(uri);

    // Console.WriteLine(responseHTML);

    int startPos = responseHTML.IndexOf("zebra fw tb-theme");
    int slutPos = responseHTML.IndexOf("</table>");

    string tableHTML = responseHTML.Substring(startPos, slutPos - startPos);

    var cityArray = tableHTML.Split("<td>", StringSplitOptions.RemoveEmptyEntries);
    var tempArray = tableHTML.Split("td class=rbi>", StringSplitOptions.RemoveEmptyEntries);
    for (int i = 1; i < cityArray.Length; i++)
    {
        
        int startPosA = cityArray[i].IndexOf("\">") + 2;
        int slutPosA = cityArray[i].IndexOf("</a>");
        string cityName = cityArray[i].Substring(startPosA, slutPosA - startPosA);
        string cityTemp = tempArray[i].Substring(0, 2);

        Console.WriteLine(cityName + " " + cityTemp + " °C");
        
    }
   ;
    Console.ReadKey();
    
}

catch (HttpRequestException e)
{
    Console.WriteLine("\nException Caught!");
    Console.WriteLine("Message :{0} ", e.Message);
}
