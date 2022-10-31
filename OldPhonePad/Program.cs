using OldPhonePad.IServices;
using OldPhonePad.Services;

string userInput = "";
IOldPhonePadServices oldPhonePadServices = new OldPhonePadServices();
do
{
    Console.WriteLine("Enter input =>");
    userInput = Console.ReadLine() ?? "";

    if (!string.IsNullOrEmpty(userInput))
    {
        if (oldPhonePadServices.ValidateInput(userInput))
        {
            string alphabeticLetter = "";
            alphabeticLetter = oldPhonePadServices.ConvertKeyAsAlphabetic(userInput);

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
