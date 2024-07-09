class Program{
    public static void Main(string[] args){
        Console.WriteLine("BEGIN TEXT:");
        int target = 0;
        int guess = 0;
        int count = 0;

        Random rand = new Random();
        target = rand.Next(1000);
        Console.WriteLine(target);

        while(true){
            Console.WriteLine("PROMPT TEXT:");
            if(!Int32.TryParse(Console.ReadLine(), out guess)){
                continue;
            }
            count++;
            if(target == guess){
                Console.WriteLine("Right guess");
                break;
            }
            if(target > guess){
                Console.WriteLine("Too Small");
            }else{
                Console.WriteLine("Too Large");
            }
        }
    }
}