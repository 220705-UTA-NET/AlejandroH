namespace SSBD.APP.Models{
    public class MyOrder{
        public int Id {get;set;}
        public string Name {get;set;}
        //public ITEM[] Item {get;set;}
        public string[] Items{get;set;}
        public string[] AddOns {get;set;}
        public MyOrder(){

        }
        public MyOrder(int id, string name, string[] items, string[] addons){
            this.Id = id;
            this.Name = name;
            this.Items = items;
            this.AddOns = addons;
        }
        public string PrintOrder()
        {
            string myorder = $"Id: {this.Id}\nName: {this.Name}\nItems: ";
            for(int i = 0; i < this.Items.Count(); i++){
                myorder = myorder + this.Items[i] + " ";
                if(i == this.Items.Count()-1){
                    myorder = myorder + this.AddOns[i] + "\n";
                }
                else{
                    myorder = myorder + this.AddOns[i] + ",\n";
                }
            }
            return myorder;        }
    }
}
