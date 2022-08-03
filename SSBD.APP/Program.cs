namespace SSBD.APP{
    public class Program{
        public static async Task Main(string[] argv){
            ConsoleClient cc = new ConsoleClient("https://localhost:7074/api/");
            await cc.RunMain();
        }
    }
}