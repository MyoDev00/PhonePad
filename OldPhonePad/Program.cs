using OldPhonePad.Services;

string userInput = "";
do
{
    Console.WriteLine("Enter input =>");
    userInput = Console.ReadLine() ?? "";

    if (!string.IsNullOrEmpty(userInput))
    {
        if (OldPhonePadServices.ValidateInput(userInput))
        {
            string alphabeticLetter = "";
            alphabeticLetter = OldPhonePadServices.OldPhonePad(userInput);

            Console.WriteLine("Alphabetic Letter => ");
            Console.WriteLine(alphabeticLetter);
        }
        else
        {
            Console.WriteLine("Invalid input,input be number,white space or * and must be end with #");
        }
    }

    Console.WriteLine("Do you want to continue? Y/N or y/n");
    userInput = Console.ReadLine() ?? "";

    Console.WriteLine("----------------------------------------------");
} while (userInput.ToLower() == "y");
