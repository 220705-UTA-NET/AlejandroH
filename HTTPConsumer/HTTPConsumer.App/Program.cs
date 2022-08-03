// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");
using System;
using System.Text.Json;
namespace HTTPConsumer.App
{
	public class Program{
		static async Task Main(){
		//send request to http://jsonplaceholder.typicode./com/post/1
		HttpClient client = new HttpClient();
		//string? uri = "http://jsonplaceholder.typicode.com/posts/1";
		string? uri = "http://jsonplaceholder.typicode.com/posts";
		string res = await client.GetStringAsync(uri);
		//System.Console.WriteLine(res);

		//DTO.Post? post = JsonSerializer.Deserialize<List<DTO.Post>>(res);
		List<DTO.Post>? newPosts = JsonSerializer.Deserialize<List<DTO.Post>>(res);
		//if(post != null)
			//System.Console.WriteLine(post.title);
		int line =1 ;
		foreach(var item in newPosts){
			System.Console.WriteLine(line++ + " "+ item.title);
		}

	}
	}
}
