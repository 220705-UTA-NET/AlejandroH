// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");
namespace  MyFirstDotNet
{
    public class Program{
        public static void Main(string[] argv){
            System.Console.WriteLine("hello all");
            char myc = '⚡';
            myc = '\u26F0';
            System.Console.WriteLine(myc);
            myc++;
            for(int i = 0; i < 100; i++){
                myc++;
                System.Console.WriteLine(myc);
            }
        }
    }
}