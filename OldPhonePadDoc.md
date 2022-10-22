<h1>Class OldPhonePadServices </h1>
  <ul>
    <li>Turn any keypad input into alphabetical letters</li>
  </ul>
  <h3> Key Mapping </h3>
  <ul>
  <li>
  <h4>Action Key</h4>
  </li>
<table>
<thead>
  <tr>
    <th>Key</th>
    <th>Actions</th>
  </tr>
</thead>
<tbody>
  <tr>
    <td>*</td>
    <td>Perform backspace action</td>
  </tr>
  <tr>
    <td> </td>
    <td>Whitespace,pause for a second</td>
  </tr>
  <tr>
    <td>#</td>
    <td>Sent or End of input</td>
  </tr>
</tbody>
</table>
  <li>
  <h4>Number Key</h4>
  </li>
<table>
<thead>
  <tr>
    <th>Key</th>
    <th>Alphabetical letters</th>
  </tr>
</thead>
<tbody>
  <tr>
    <td>0</td>
    <td>WhiteSpace</td>
  </tr>
  <tr>
    <td>1</td>
    <td>& ' (</td>
  </tr>
  <tr>
    <td>2</td>
    <td>A B C</td>
  </tr>
  <tr>
    <td>3</td>
    <td>D E F</td>
  </tr>
  <tr>
    <td>4</td>
    <td>G H I</td>
  </tr>
  <tr>
    <td>5</td>
    <td>J K L</td>
  </tr>
  <tr>
    <td>6</td>
    <td>M N O</td>
  </tr>
  <tr>
    <td>7</td>
    <td>P Q R S</td>
  </tr>
  <tr>
    <td>8</td>
    <td>T U V</td>
  </tr>
  <tr>
    <td>9</td>
    <td>W X Y Z</td>
  </tr>
</tbody>
</table>
</ul>
<h2>Methods</h2>

<h3>ValidateInput(string )</h3>
<span>Validate the user input against the key mapping.<br>Input string must contain only action keys, number keys and must end with an action key ( # ).</span>

#####

    public static bool ValidateInput(string input);

<h4>Parameters</h4>

#####
    input String
    
The string for user input.

<h4>Return</h4>
<a href="https://learn.microsoft.com/en-us/dotnet/api/system.boolean?view=net-6.0">Boolean</a>

----

<h3>OldPhonePad(string )</h3>
<span>Transform keypad input into alphabetical letters according to key mapping.<br/>
Input between `[0-9]` will compute a letter via a circle through the key mapping.<br/>
The input "*" will be translated as backspace and delete the previous letter from the sequence.<br/>
The input " "(WhiteSpace) is used as a pause, to type two characters from after each other.<br/> 
"#" is used for the line ending character.
</span>

#####

    public static string OldPhonePad(string input)

<h4>Parameters</h4>

#####
    input String
    
The string for user input.

<h4>Return</h4>
<a href="https://learn.microsoft.com/en-us/dotnet/api/system.string?view=net-6.0">String</a>

<h4>Example</h4>

#####
    using OldPhonePad.Services;
    
    string userInput = "222 2 8#"
    var alphabeticLetter = OldPhonePadServices.OldPhonePad(userInput);
    Console.WriteLine(alphabeticLetter);
    //Output => CAT
    
    userInput = “4433555 555666#”;
    alphabeticLetter = OldPhonePadServices.OldPhonePad(userInput);
    Console.WriteLine(alphabeticLetter);
    //Output => HELLO
    
    //Input  => 227*#
    //Output => B
    
    //Input  => 20220222#
    //Output => A B C
    
