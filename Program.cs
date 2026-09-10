namespace LAB1;

static public class Program
{
    static void Main()
    {
        // Ok nått är fel med progammet, det startar inte ens. 
        // Hinner inte fixa, har möte med chefen om 5 min. Mvh Pelle Programmerare

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
                // Bra kod för att beräkna kommer jag att skriva här.
                // Fixar efter fikapausen. /Pelle
            }
            else if (userSelect == 2)
            {
                Console.WriteLine("DET HÄR VALET ÄR INTE IMPLEMENTERAT ÄNNU! GE MIG MER BETALT SÅ FIXAR JAG DET JAG LOVAR.");
            }
            else if (userSelect == 3)
            {
                //Här ska progarmmet avslutas men vet inte exakt hur. Kanske inte är så viktigt heller. //Pelle Programmerare
                break;
            }
        }
    }
}

