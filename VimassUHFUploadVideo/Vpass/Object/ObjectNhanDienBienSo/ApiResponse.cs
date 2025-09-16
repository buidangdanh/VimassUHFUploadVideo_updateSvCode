using Newtonsoft.Json;
using System;
using System.Collections.Generic;

public class ApiResponse
{
    [JsonProperty("results")]
    public List<Result> Results { get; set; }
}

public class Result
{
    [JsonProperty("box")]
    public Box Box { get; set; }

    [JsonProperty("plate")]
    public string Plate { get; set; }

    [JsonProperty("vehicle")]
    public Vehicle Vehicle { get; set; }
}

public class Box
{
    [JsonProperty("xmin")]
    public int Xmin { get; set; }

    [JsonProperty("ymin")]
    public int Ymin { get; set; }

    [JsonProperty("xmax")]
    public int Xmax { get; set; }

    [JsonProperty("ymax")]
    public int Ymax { get; set; }
}
public class Vehicle
{
    [JsonProperty("score")]
    public double Score { get; set; }

    [JsonProperty("type")]
    public String Type  { get; set; }

    [JsonProperty("box")]
    public Box Box { get; set; }

   
}
