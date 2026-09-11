namespace LAB1;

static public class Program
{
    static void Main()
    {
        while (true)
        {
            Console.WriteLine("VÄLKOMMEN TILL LASSES LAST 1.0\n");
            Console.WriteLine("1) Beräkna frakt för ett paket");
            Console.WriteLine("2) Beräkna frakt för flera paket från fil");
            Console.WriteLine("3) Avsluta\n");
            Console.Write("Val: ");

            string? input = Console.ReadLine() ?? "";
            int userSelect = int.Parse(input);
            if (userSelect == 1)
            {
                /*  Välj i menyn att du vill beräkna frakt för ett paket.
 Mata in avsändarens namn, paketets vikt i kg, innehållets värde i kronor, om avsändaren är medlem och om paketet ska försäkras.
 Programmet beräknar kostnaden och skriver ut ett fraktkvitto! */
                Parcel parcel = GetParcelInfo();
                Console.WriteLine($"userName är: {parcel.UserName}\nparcelWeight är: {parcel.Weight}\nparcelValue är: {parcel.Value}\nisMember är: {parcel.IsMember}\nhasInsurance är: {parcel.HasInsurance}");

                int baseFee = GetBaseFee();
                Console.WriteLine($"BaseFee är: {baseFee}");
                double parcelExcessWeight = GetParcelExcessWeight(parcel.Weight, parcel.IsMember);
                Console.WriteLine($"parcelExcessWeight är: {parcelExcessWeight}");
                decimal parcelExcessWeightCost = GetParcelExcessWeightCost(parcelExcessWeight);
                Console.WriteLine($"parcelExcessWeightFee är: {parcelExcessWeightCost}");

                decimal heavyWeightCost = GetHeavyWeightCost(parcelExcessWeight);
                Console.WriteLine($"heavyWeightCost är: {heavyWeightCost}");

                decimal insuranceCost = GetInsuranceCost(parcel.HasInsurance, parcel.Value);
                Console.WriteLine($"insuranceCost är: {insuranceCost:0.##}");

                decimal totalCost = GetTotalCost(baseFee, parcelExcessWeightCost, heavyWeightCost, insuranceCost);
                Console.WriteLine($"totalCost är: {totalCost}");



                PrintReceipt(parcel.UserName, parcel.Weight, parcel.Value, parcel.IsMember, parcel.HasInsurance, baseFee, parcelExcessWeightCost, insuranceCost, heavyWeightCost, totalCost);
            }
            else if (userSelect == 2)
            {

                Console.WriteLine("hade jag haft mer tid hade jag gjort denna del. Men tanke är att jag skulle kunna återanvända mina metoder i bästa fall och bara lägga in logik för att hämta data från fil istället för en user //Fredrik programmerare");
            }
            else if (userSelect == 3)
            {

                break;
            }
        }
    }

    static private void PrintReceipt(string sender, double weight, decimal value, bool isMember, bool hasInsurance, int baseFee, decimal weightFee, decimal insuranceFee, decimal heavyGoodsFee, decimal totalCost)
    {
        Console.WriteLine($"FRAKTKVITTO\n-----------------------------\nAvsändare: {sender}\nVikt: {weight} kg\nInnehållets värde: {value} kr\nMedlem: {isMember}\nFörsäkring: {hasInsurance}\nGrundavgift: {baseFee} kr\nViktavgift: {weightFee} kr\nTunggodstillägg: {heavyGoodsFee} kr\nFörsäkringsavgift: {insuranceFee:0.##} kr\nTotalt att betala: {totalCost} kr\n-----------------------------");
    }
    static private Parcel GetParcelInfo()
    {
        Parcel parcel = new();
        //user prompts
        string userNamePrompt = "Var god ange ditt namn: ";
        string parcelWeightPrompt = "Var god ange paketets vikt i kg: ";
        string parcelValuePrompt = "Var god ange paketets värde i kronor: ";
        string isMemberPrompt = "Är du medlem?";
        string hasInsurancePrompt = "Vill du försäkra paketet?";
        string confirmPrompt = "tryck (y) för ja eller (n) för nej";


        Console.Write(userNamePrompt);
        /*  Mata in avsändarens namn */
        string userName = Console.ReadLine() ?? "";
        parcel.UserName = userName;

        /* paketets vikt i kg */
        Console.Write(parcelWeightPrompt);
        string parcelWeightInput = Console.ReadLine() ?? "";
        parcel.Weight = double.Parse(parcelWeightInput);

        /* innehållets värde i kronor */
        Console.Write(parcelValuePrompt);
        string parcelValueInput = Console.ReadLine() ?? "";
        parcel.Value = decimal.Parse(parcelValueInput);

        /* om avsändaren är medlem  */
        Console.WriteLine(isMemberPrompt);
        Console.Write(confirmPrompt);

        parcel.IsMember = GetConfirmation();


        /* om paketet ska försäkras. */
        Console.WriteLine(hasInsurancePrompt);
        Console.Write(confirmPrompt);

        parcel.HasInsurance = GetConfirmation();


        return parcel;

    }
    private static bool GetConfirmation()
    {
        const string errorMessage = "Var god välj endast y eller n.";

        while (true)
        {
            string confirm = (Console.ReadLine() ?? "").ToLowerInvariant();

            if (confirm == "y")
            {
                return true;
            }

            if (confirm == "n")
            {
                return false;
            }

            Console.WriteLine(errorMessage);
        }
    }
    static private int GetBaseFee()
    {
        int baseFee = 49;
        return baseFee;
    }
    static private double GetParcelExcessWeight(double parcelWeight, bool isMember)
    {
        //De första 2 kg ingår i grundavgiften.
        //ta viktvärde minus 2
        //För medlemmar ingår istället de första 5 kg.

        if (isMember)
        {
            if (parcelWeight >= 5)
            {
                return parcelWeight - 5;
            }
            else return 0;
        }
        else
        {
            if (parcelWeight >= 2)
            {
                return parcelWeight - 2;
            }
            else return 0;
        }



    }
    static private decimal GetParcelExcessWeightCost(double parcelExcessWeight)
    {
        int weightFee = 10;

        decimal parcelExcessWeightCost = (decimal)parcelExcessWeight * weightFee;

        return parcelExcessWeightCost;

    }
    static private decimal GetHeavyWeightCost(double parcelExcessWeight)
    {
        int heavyWeightThreshold = 20;
        int heavyWeightFee = 30;
        decimal heavyWeightCost;

        //räkna ut vikten över tungviktsgränsen för att kunna lägga på avgiften för tungvikt
        if (parcelExcessWeight >= heavyWeightThreshold)
        {
            double weightOverHeavyWeightThreshold = parcelExcessWeight - heavyWeightThreshold;
            heavyWeightCost = (decimal)weightOverHeavyWeightThreshold * heavyWeightFee;
        }


        else heavyWeightCost = 0;

        return heavyWeightCost;
    }
    static private decimal GetInsuranceCost(bool hasInsurance, decimal parcelValue)
    {
        //Om paketet ska försäkras kostar försäkringen 1 % av innehållets värde. Annars är försäkringsavgiften 0 kr.
        if (hasInsurance)
        {
            decimal insuranceFee = 0.01m;
            decimal insuranceCost = parcelValue * insuranceFee;
            return insuranceCost;
        }
        else return 0;
    }
    static private decimal GetTotalCost(int baseFee, decimal parcelExcessWeightCost, decimal heavyWeightCost, decimal insuranceCost)
    {
        //här hade jag hellre skickat in ett objekt och beräknat summa av property value, om det är möjligt vet jag inte. Men jag hade utforskat det om jag hade haft mer tid. Nu fick det bli en array som jag beräknar summan på. Nackdelen är att den inte kan växa dynamiskt utan att behöva lägga till variabler i arrayen.
        decimal[] allCosts = [baseFee, parcelExcessWeightCost, heavyWeightCost, insuranceCost];
        decimal totalCost = allCosts.Sum();
        return totalCost;
    }
    private sealed class Parcel
    {
        public string UserName { get; set; } = "";
        public double Weight { get; set; }
        public decimal Value { get; set; }
        public bool IsMember { get; set; }
        public bool HasInsurance { get; set; }

    }
}

