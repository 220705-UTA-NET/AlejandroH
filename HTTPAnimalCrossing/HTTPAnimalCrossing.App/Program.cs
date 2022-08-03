// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");
using System;
using System.Text.Json;
namespace HTTPAnimalCrossing
{
	public class Program{
		static async Task Main(){
		/* the following code works with collection	
		//send request to http://jsonplaceholder.typicode./com/post/1
		HttpClient client = new HttpClient();
		//string? uri = "http://jsonplaceholder.typicode.com/posts/1";
		//string? uri = "http://acnhapi.com/v1/fish/2";
		string? uri = "http://acnhapi.com/v1/fish";
		string res = await client.GetStringAsync(uri);
		//System.Console.WriteLine(res);

		//DTO.Fish? fish = JsonSerializer.Deserialize<DTO.Fish>(res);
		List<DTO.Fish>? allFish = JsonSerializer.Deserialize<List<DTO.Fish>>(res);
		//if(fish != null)
			//System.Console.WriteLine(fish.filename);
		int line =1 ;
		foreach(var item in allFish){
			System.Console.WriteLine(line++ + " "+ item.filename);
		}
		*/
		HttpClient client = new HttpClient();
		//string? uri = "http://jsonplaceholder.typicode.com/posts/1";
		//string? uri = "http://acnhapi.com/v1/fish/2";
		string? uri = "http://acnhapi.com/v1/fish";
		string res = await client.GetStringAsync(uri);
		//System.Console.WriteLine(res);

		//DTO.Fish? fish = JsonSerializer.Deserialize<DTO.Fish>(res);
		//var allFish = JsonSerializer.Deserialize<DTO.FishResponse>(res);
		//System.Console.WriteLine(allFish.allFish.FishList[0].filename);
		var listresponse = JsonSerializer.Deserialize<Dictionary<string,DTO.Fish>>(res);
		List<DTO.Fish> fishList = listresponse.Values.ToList();
		//List<DTO.Fish>? fishList = allFish.GetFishList();
		System.Console.WriteLine(listresponse.Count);
		//System.Console.WriteLine(allFish.FishList[0].filename);
		//if(fish != null)
			//System.Console.WriteLine(fish.filename);
		//int line =1 ;
		//foreach(var item in fishList){
		//	System.Console.WriteLine(line++ + " "+ item.filename);
		//}
		foreach(var item in fishList){
			System.Console.WriteLine(item.filename);
		}
	}
	}
}
