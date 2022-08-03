namespace HTTPAnimalCrossing.DTO{
    using System.Text.Json.Serialization;
    public class Fish
    {
        public int id {get;set;}
        [JsonPropertyName("file-name")]
        public string? filename {get;set;}
        [JsonIgnore]
        public string? name {get;set;}
        [JsonIgnore]
        public string? availability {get;set;}
        public string? shadow {get;set;}
        public int price {get;set;}
        [JsonPropertyName("price-cj")]
        public int jsprice {get;set;}
        [JsonPropertyName("catch-phrase")]
        public string? catch_phrase {get;set;}
        [JsonPropertyName("museum-phrase")]
        public string? museum_phrase {get;set;}
        [JsonPropertyName("image_uri")]
        public string? image_uri {get;set;}
        [JsonPropertyName("icon_uri")]
        public string? icon_uri {get;set;}

    }
    
    public class AllFish{
        public List<Fish> FishList {get;set;}
        public List<Fish> GetFishList(){
            return this.FishList;
        }
    }
}