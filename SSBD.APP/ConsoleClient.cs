using System.Text.Json;
using System.Security.Cryptography;
using System.Net.Security;
using System.Net;
using System.Security.Cryptography.X509Certificates;
//using System.Security.Cryptography.X509Certificates;
namespace SSBD.APP{
    public class ConsoleClient{
        private HttpClient client;// = new HttpClient();
        private char globalMenuIndex = '1';
        Models.MyOrder myOrder;
        private string uri = "http://192.168.1.23:5090/api/";
        private List<Item>? ItemList; 
        public ConsoleClient(string uri){
            client = new HttpClient();
            MakeMenu();
            this.uri = uri;
            myOrder = new Models.MyOrder();
        }
        private string GetEndpoint(string end){
            return this.uri+end;
        }
        public async Task RunMain(string newuri = ""){
            if(newuri.Length > 3){
                this.uri = newuri;
            }
            PrintWelcome();
            bool done = false;
            while(!done){
            char input = getChar("Enter 1 for new order\nEnter 2 for checking status of existing order\nEnter anything else to quit");
                switch(input){
                    case '1':
                    await MakeNewOrder();
                    break;
                    case '2':
                    await CheckStatus();
                    break;
                }
                if(input < '1' || input > '2'){
                    done = true;
                }
            }
            System.Console.WriteLine("Thank for your this console system");
        }
        public async Task MakeNewOrder(){
            int OrderDone = 4;
            System.Console.WriteLine("\n\n");
            string name = getString("Can I have name for the order",2);
            myOrder.Name = name;
            if(myOrder == null){
                System.Console.WriteLine("WHY ARE YOU NULL");
            }
            List<string> items = new List<string>();
            List<string> addons = new List<string>();
            while(OrderDone > 1 && OrderDone < 5){
                OrderDone = 1;
                PrintMenu(0,18);
                AddItem(items,addons);
                char input = getChar("Will this complete your order?");
                if('n' == input || 'N' == input){
                    OrderDone = MakeChange(items,addons);
                }
                myOrder.Items = items.ToArray();
                myOrder.AddOns = addons.ToArray();
                //OrderDone = true;
            }
            System.Console.WriteLine("order contains");
            //string sendto = GetEndpoint("submitorder");//for some reason this string gets updated to http and its port
            string sendto = "http://192.168.1.23:5090/api/submitorder";
            System.Console.WriteLine(sendto);
            string jsonorder = JsonSerializer.Serialize(myOrder);
            System.Console.WriteLine(jsonorder);
            var data = new StringContent(jsonorder, System.Text.Encoding.UTF8, "application/json");
            System.Console.WriteLine("Sending your order through the system... ");

            var responseMessage = await client.PostAsync(sendto, data);
            var result = await responseMessage.Content.ReadAsStreamAsync();
            myOrder = JsonSerializer.Deserialize<Models.MyOrder>(result);
            System.Console.WriteLine(myOrder.PrintOrder());
        }
        public async Task CheckStatus(){
            int orderid;
            string input = getString("Enter the id number of you order",1);
            Int32.TryParse(input,out orderid);
            string sendto = "http://192.168.1.23:5090/api/getorder/" + orderid;
            System.Console.WriteLine(sendto);
            string response = await client.GetStringAsync(sendto);
            Models.MyResponse response_obj = JsonSerializer.Deserialize<Models.MyResponse>(response);
            Models.MyOrder order;// = new Models.MyOrder();
            order = response_obj.order;
            System.Console.WriteLine(order.PrintOrder());
        }
        public void PrintWelcome(){
            System.Console.WriteLine("Welcome to SSBD");
            System.Console.WriteLine("Super Simple Burgers Drivein");
        }
        public void PrintMenu(int start, int end){
            System.Console.Write("\n");
            for(int i = start; i <= end; i++){
                System.Console.Write(globalMenuIndex+ " " + ItemList[i].Name);
                System.Console.Write(" at ");
                System.Console.WriteLine(ItemList[i].Price);
                globalMenuIndex++;
            }
        }
        private void AddItem(List<string> items,List<string> addons){
            System.Console.WriteLine("Please enter the number on the left of the menu to place item in the order\nPress N or n for sides");
            //PrintMenu(0,18);
            bool validinput = false;
            char input;
            int menupage = 1;
            while(!validinput){
                input = getChar("Enter number corresponding to the item in the menu\nPress N or n to cycle between Menu, Sides, and Drinks");
                if(input >= '1' && input <= '9'){
                    validinput = true;
                    int index;
                    int offset = -1;
                    switch(menupage){
                        case 1:
                        offset = -1;
                        break;
                        case 2:
                        offset = 8;
                        break;
                        case 3:
                        offset = 13;
                        break;
                    }
                    Int32.TryParse(input.ToString(),out index);
                    items.Add(ItemList![index+offset].Name);
                    if(menupage<3){
                        System.Console.WriteLine("\nYou have added " + items.Last());
                        addons.Add(getString("Please enter additional information for this item\nYou can say things like extra cheese or double meat"));
                    }
                    else{
                        string s = getString("Medium or Large");
                        if(s== "Medium"){
                            addons.Add("Medium");    
                        }
                        else{
                            addons.Add("Large");
                        }
                    }
                }
                else if(input == 'n' || input == 'N'){
                    validinput = false;
                    menupage = (menupage >=3)? 1:menupage+1;
                    globalMenuIndex = '1';
                    switch(menupage){
                        case 1:
                        System.Console.WriteLine("s~Burger~s");
                        PrintMenu(0,8);
                        System.Console.WriteLine("Additional toppings add 0.25");
                        System.Console.WriteLine("Additional meats add 1.00");
                        break;
                        case 2:
                        System.Console.WriteLine("s~Sides~s");
                        globalMenuIndex = '1';
                        PrintMenu(9,14);
                        System.Console.WriteLine("Additional toppings add 0.25");
                        break;
                        case 3:
                        System.Console.WriteLine("s~Sides~s");
                        globalMenuIndex = '1';
                        PrintMenu(15,18);
                        //System.Console.WriteLine("Additional toppings add 0.25");
                        break;
                    }
                }
                else{
                    if(input < '1' || input > '9'){
                        System.Console.WriteLine("\nInvalid input. Please try again");
                        validinput = false;
                    }
                }
                if(validinput){
                    input = getChar("\nWould you like to enter another item?");
                    if(input == 'y' || input == 'Y'){
                        validinput = false;
                        globalMenuIndex = '1';
                        PrintMenu(0,8);
                    }
                }
            }
        }
        private char getChar(string? prompt){
            if(prompt != null){
                System.Console.WriteLine(prompt);
            }
            return System.Console.ReadKey().KeyChar;
        }
        private int MakeChange(List<string> items,List<string> addons){
            int intinput = 2;
            char cinput;
            while(intinput > 1 && intinput < 4){
                cinput = getChar("Press 1 to complete order\nPress 2 to change the addons of an item.\nPress 3 to remove an item\n\nPress 4 to add new item\nPress 5 to quit");
                Int32.TryParse(cinput.ToString(),out intinput);
                if(intinput == 2){
                    System.Console.WriteLine("Your order contains");
                    for(int i = 0; i < items.Count(); i++){
                        System.Console.WriteLine(i+1 + " " +items[i] + " with " + addons[i]);
                    }
                    cinput = getChar("Which item should be make changes to\nEnter anything else to keep it as is");
                    Int32.TryParse(cinput.ToString(),out intinput);
                    if(intinput < items.Count()){
                        string add = getString("What would you like to add to this item");
                        addons[intinput] = add;
                    }
                    
                    intinput = 2;
                }
                if(intinput == 3){ 
                    System.Console.WriteLine("Your order contains");
                    for(int i = 0; i < items.Count(); i++){
                        System.Console.WriteLine(i+1 + " " +items[i] + " with " + addons[i]);
                    }
                    cinput = getChar("Which item should be remove\nEnter anything else to keep it as is");
                    Int32.TryParse(cinput.ToString(),out intinput);
                    if(intinput < items.Count()){
                        addons.RemoveAt(intinput);
                    }
                    intinput = 3;   
                }
            }
            return intinput;
        }
        public void PrintMyOrder(){
            if(myOrder == null)
            return;
            myOrder.PrintOrder();
        }
        private string getString(string? prompt, int? atleast=2){
            bool meetsLength = false;
            string response = "";
            while(!meetsLength){
                if(prompt != null){
                    System.Console.WriteLine(prompt);
                }
                response = Console.ReadLine();
                if(response.Length >= atleast){
                    meetsLength = true;
                }
                else{
                    System.Console.WriteLine("input does not meed minimum length");
                }
            }
            return response;
        }
        public string GetNameForOrder(){
            if(myOrder != null){
                return this.myOrder.Name;
            }
            else{
                return new String("N/A");
            }
        }
        private void MakeMenu(){
            ItemList = new List<Item>(){
                new Item("Classic Single Burger",4.25M),
                new Item("Classic Double Burger",5.00M),
                new Item("Classic Triple Burger",6.00M),
                new Item("Single Cheese Burger",3.5M),
                new Item("Double Cheese Burger",4.00M),
                new Item("Triple Cheese Burger",4.50M),
                new Item("Veggie Burger",5.50M),
                new Item("Chicken Burger",6.00M),
                new Item("Chicken Fried Steak",6.25M),
                new Item("Fries",1.50M),
                new Item("Onion Rings",1.50M),
                new Item("Fried Chicken Pieces",3.00M),
                new Item("Fried Cheese Sticks",4.50M),
                new Item("Fried Zucchini",3.00M),
                new Item("Coca-Cola",1.50M),
                new Item("Pepsi",1.50M),
                new Item("Sprite",1.50M),
                new Item("Dr. Pepper",1.50M),
                new Item("Lemonade",1.50M)
            };
        }
    }
}
