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
            double parcelExcessWeight;
            ParcelCost parcelCost = new();
            Parcel parcel = new();

            switch (userSelect)
            {
                case 1:
                    /*  Välj i menyn att du vill beräkna frakt för ett paket.
     Mata in avsändarens namn, paketets vikt i kg, innehållets värde i kronor, om avsändaren är medlem och om paketet ska försäkras.
     Programmet beräknar kostnaden och skriver ut ett fraktkvitto! */
                    parcel = GetParcelInfo(parcel);

                    Console.WriteLine($"userName är: {parcel.UserName}\nparcelWeight är: {parcel.Weight}\nparcelValue är: {parcel.Value}\nisMember är: {parcel.IsMember}\nhasInsurance är: {parcel.HasInsurance}");

                    parcelCost.SetBaseFee();
                    Console.WriteLine($"BaseFee är: {parcelCost.BaseFee}");
                    parcelExcessWeight = GetParcelExcessWeight(parcel.Weight, parcel.IsMember);
                    Console.WriteLine($"parcelExcessWeight är: {parcelExcessWeight}");
                    parcelCost.SetExcessWeightCost(parcelExcessWeight);
                    Console.WriteLine($"parcelExcessWeightFee är: {parcelCost.ExcessWeight}");

                    parcelCost.SetHeavyWeightCost(parcelExcessWeight);
                    Console.WriteLine($"heavyWeightCost är: {parcelCost.HeavyWeight}");

                    parcelCost.SetInsuranceCost(parcel.HasInsurance, parcel.Value);
                    Console.WriteLine($"insuranceCost är: {parcelCost.Insurance:0.##}");

                    parcelCost.SetTotalCost(parcelCost);
                    Console.WriteLine($"totalCost är: {parcelCost.TotalCost}");



                    PrintReceipt(parcel, parcelCost);
                    break;
                case 2:



                    /* Gör klart menyval 2. Användaren ska få ange ett filnamn och programmet ska beräkna frakten för varje paket i filen.

    Varje rad innehåller:
    namn;vikt;värde;medlem;försäkring;land */

                    //läsa varje rad
                    string[] rowsAllInfo = File.ReadAllLines("fraktsedel.txt");
                    // skicka in i metoderna

                    for (int i = 0; i < rowsAllInfo.Length; i++)
                    {
                        string[] row = rowsAllInfo[i].Split(";");

                        parcelCost.SetBaseFee();
                        parcel.UserName = row[0];
                        parcel.Weight = double.Parse(row[1]);
                        parcel.Value = decimal.Parse(row[2]);
                        parcel.IsMember = row[3].Trim() == "ja";
                        parcel.HasInsurance = row[4].Trim() == "ja";
                        parcelExcessWeight = GetParcelExcessWeight(parcel.Weight, parcel.IsMember);
                        parcelCost.SetExcessWeightCost(parcelExcessWeight);
                        parcelCost.SetInsuranceCost(parcel.HasInsurance, parcel.Value);
                        parcelCost.SetHeavyWeightCost(parcelExcessWeight);
                        parcelCost.SetTotalCost(parcelCost);
                        bool isFirstLoop = i == 0;

                        PrintReceipt(parcel, parcelCost, isFirstLoop, true);
                    }


                    break;
                case 3:
                    return;
                default:
                    Console.WriteLine("Ogiltigt val.");
                    break;


            }


        }
    }

    static private void PrintReceipt(Parcel parcel, ParcelCost parcelCost, bool isFirstLoop = false, bool printToFile = false)
    {
        string receiptFileName = "kvitto.txt";

        string dataToPrint = $"FRAKTKVITTO\n-----------------------------\nAvsändare: {parcel.UserName}\nVikt: {parcel.Weight} kg\nInnehållets värde: {parcel.Value} kr\nMedlem: {parcel.IsMember}\nFörsäkring: {parcel.HasInsurance}\nGrundavgift: {parcelCost.BaseFee} kr\nViktavgift: {parcelCost.ExcessWeight} kr\nTunggodstillägg: {parcelCost.HeavyWeight} kr\nFörsäkringsavgift: {parcelCost.Insurance:0.##} kr\nTotalt att betala: {parcelCost.TotalCost} kr\n-----------------------------\n\n";

        if (printToFile)
        {

            if (isFirstLoop && File.Exists(receiptFileName))
            {
                File.Delete(receiptFileName);
            }

            File.AppendAllText(receiptFileName, dataToPrint);

        }
        else Console.WriteLine(dataToPrint);
    }
    static private Parcel GetParcelInfo(Parcel parcel)
    {

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
    private sealed class ParcelCost
    {
        public int BaseFee { get; set; }
        public decimal ExcessWeight { get; set; }
        public decimal HeavyWeight { get; set; }
        public decimal Insurance { get; set; }
        public decimal TotalCost { get; set; }

        public void SetTotalCost(ParcelCost parcelCost)
        {
            decimal totalCost = 0;
            foreach (var cost in parcelCost.GetType().GetProperties())
            {
                if (cost.Name != nameof(TotalCost) && (cost.PropertyType == typeof(int) || cost.PropertyType == typeof(decimal) || cost.PropertyType == typeof(float) || cost.PropertyType == typeof(double)))
                {
                    totalCost += Convert.ToDecimal(cost.GetValue(parcelCost));
                }
            }


            TotalCost = totalCost;
        }

        public void SetBaseFee()
        {
            int baseFee = 49;
            BaseFee = baseFee;
        }
        public void SetExcessWeightCost(double parcelExcessWeight)
        {
            int weightFee = 10;

            decimal parcelExcessWeightCost = (decimal)parcelExcessWeight * weightFee;

            ExcessWeight = parcelExcessWeightCost;

        }
        public void SetHeavyWeightCost(double parcelExcessWeight)
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

            HeavyWeight = heavyWeightCost;
        }
        public void SetInsuranceCost(bool hasInsurance, decimal parcelValue)
        {
            //Om paketet ska försäkras kostar försäkringen 1 % av innehållets värde. Annars är försäkringsavgiften 0 kr.
            if (hasInsurance)
            {
                decimal insuranceFee = 0.01m;
                decimal insuranceCost = parcelValue * insuranceFee;
                Insurance = insuranceCost;
            }
            else Insurance = 0;
        }


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

