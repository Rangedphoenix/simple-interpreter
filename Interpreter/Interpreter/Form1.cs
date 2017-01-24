using System;
using System.Linq;
using System.Windows.Forms;

/*
    A basic programming interpreter
    Programmed by: Brandon Barnhard
*/

namespace Interpreter
{
    public partial class Form1 : Form
    {
        string variableTypeFound;

        public Form1()
        {
            InitializeComponent();
        }

        private void ExeButton_Click(object sender, EventArgs e)
        {
            string command;
            string[] splitCommand;

            command = CommandBox.Text;

            //if nothing was entered into the command prompt
            if(command == "")
            {
                //do nothing
                return;
            }
            command = command.ToLower();

            splitCommand = command.Split(' ');

            /*this is for debugging usage.
            foreach(string s in splitCommand)
            {
                DebugLog.AppendText(s + "\n");
            }*/
            //this is where we will begin to separate out the necessary commands
            switch (splitCommand[0])
            {
                case "var":
                    createVar(splitCommand);
                    break;

                case "print":
                    print(splitCommand);
                    break;

                case "assign":
                    assignVar(splitCommand);
                    break;

                case "add":
                    math(splitCommand);
                    break;

                case "subtract":
                    math(splitCommand);
                    break;

                case "multiply":
                    math(splitCommand);
                    break;

                case "divide":
                    math(splitCommand);
                    break;

                case "mod":
                    math(splitCommand);
                    break;

                case "concat":
                    joinStrings(splitCommand);
                    break;

                /*REMOVED FUNCTIONALITY
                case "is":
                    isTrue(splitCommand);
                    break;
                */

                case "search":
                    search(splitCommand);
                    break;

                case "exit":
                    Application.Exit();
                    break;

                default:
                    DebugLog.AppendText("Command not recognized.\n");
                    break;
            }

            CommandBox.Text = "";
        }

        //module for variable creation
        private void createVar(string[] command)
        {
            //this variable seems useless now, but will come into play soon
            bool assign;

            //if the array is not long enough, there are missing entries.
            //cut this off now before it becomes a problem.
            //furthermore, we might as well remove excessive gobbedygook now.
            if(command.Length < 3 || command.Length > 4)
            {
                DebugLog.AppendText("Invalid variable declaration.\n"
                    + "Format is: var varType varName (optional)value");
                return;
            }

            //next we will validate the name immediately. If the name is already present, we will not continue.
            //this must be done for each variable type

            if (!checkStorage(command[2]))
            {
                return;
            }

            //validation is finally complete, we can now work on creating the variable.
            assign = (command.Length == 4);
            //just for fun though, we should find out if the user is assigning a value to the statement
            //first, we must
            //find out the data type the user wants to create
            switch (command[1])
            {
                //string module
                case "string":
                    Storage.stringNames.Add(command[2]);
                    DebugLog.AppendText("Variable " + Storage.stringNames.Last() + " created.\n");

                    if (assign)
                    {
                        Storage.stringVars.Add(command[3]);
                        DebugLog.AppendText("Value: " + Storage.stringVars.Last() + " assigned to " + Storage.stringNames.Last() + "\n");
                    }
                    else
                    {
                        Storage.stringVars.Add("");
                    }
                    break;

                    //integer module
                case "int":
                    Storage.intNames.Add(command[2]);
                    DebugLog.AppendText("Variable " + Storage.intNames.Last() + " created.\n");

                    if (assign)
                    {
                        int tempInt;
                        if(int.TryParse(command[3], out tempInt))
                        {
                            Storage.intVars.Add(tempInt);
                            DebugLog.AppendText("Value: " + Storage.intVars.Last() + " assigned to " + Storage.intNames.Last() + "\n");
                        }
                        else
                        {
                            DebugLog.AppendText("Parsing Error: Value provided was not a valid integer.\nValue will instead be set to zero.\n");
                            Storage.intVars.Add(0);
                        }
                    }
                    else
                    {
                        Storage.intVars.Add(0);
                    }
                    break;

                    //double module
                case "double":
                    Storage.doubleNames.Add(command[2]);
                    DebugLog.AppendText("Variable " + Storage.doubleNames.Last() + " created.\n");

                    if (assign)
                    {
                        double tempDouble;
                        if(double.TryParse(command[3], out tempDouble))
                        {
                            Storage.doubleVars.Add(tempDouble);
                            DebugLog.AppendText("Value: " + Storage.doubleVars.Last() + " assigned to " + Storage.doubleNames.Last() + "\n");
                        }
                        else
                        {
                            DebugLog.AppendText("Parsing Error: Value provided was not a valid double.\nValue will instead be set to zero.\n");
                            Storage.doubleVars.Add(0);
                        }
                    }
                    else
                    {
                        Storage.doubleVars.Add(0);
                    }

                    break;

                    //boolean module
                case "bool":
                    Storage.boolNames.Add(command[2]);
                    DebugLog.AppendText("Variable " + Storage.boolNames.Last() + " created.\n");

                    if (assign)
                    {
                        switch (command[3])
                        {
                            case "true":
                                Storage.boolVars.Add(true);
                                DebugLog.AppendText(Storage.boolNames.Last() + " is now " + Storage.boolVars.Last() + "\n");
                                break;

                            case "false":
                                Storage.boolVars.Add(false);
                                DebugLog.AppendText(Storage.boolNames.Last() + " is now " + Storage.boolVars.Last() + "\n");
                                break;

                            default:
                                Storage.boolVars.Add(false);
                                DebugLog.AppendText("Invalid assignment. Boolean variables must be assigned true or false."
                                    + "\nSetting to false as default.\n");
                                break;
                        }
                    }
                    else
                    {
                        Storage.boolVars.Add(false);
                    }
                    break;
                    
                    //char module
                case "char":
                    Storage.charNames.Add(command[2]);
                    DebugLog.AppendText("Variable " + Storage.charNames.Last() + " created.\n");

                    if (assign)
                    {
                        char tempChar;
                        if(char.TryParse(command[3], out tempChar))
                        {
                            Storage.charVars.Add(tempChar);
                            DebugLog.AppendText("Value " + Storage.charVars.Last() + " added to " + Storage.charNames.Last() + "\n");
                        }
                        else
                        {
                            DebugLog.AppendText("Parsing Error. Char variable must be of type char.\n");
                            Storage.charVars.Add(' ');
                        }
                    }
                    else
                    {
                        Storage.charVars.Add(' ');
                    }
                    break;

                default:
                    DebugLog.AppendText("Invalid variable type declaration.\nMust be of type string, "
                        + "int, bool, double, or char.\n");
                    break;
            }
        }

        //module for printing
        private void print(string[] command)
        {
            //if there isn't anything to print...
            if(command.Length < 2)
            {
                DebugLog.AppendText("Printing error: Nothing found to print.\n");
                return;
            }
            int indexOfVar;

            indexOfVar = findVariableIndex(command[1]);

            //if a variable is found
            if(indexOfVar != -1)
            {
                switch (variableTypeFound)
                {
                    case "string":
                        OutputLog.AppendText(Storage.stringVars[indexOfVar] + "\n");
                        break;

                    case "int":
                        OutputLog.AppendText(Storage.intVars[indexOfVar] + "\n");
                        break;

                    case "double":
                        OutputLog.AppendText(Storage.doubleVars[indexOfVar] + "\n");
                        break;

                    case "bool":
                        OutputLog.AppendText(Storage.boolVars[indexOfVar] + "\n");
                        break;

                    case "char":
                        OutputLog.AppendText(Storage.charVars[indexOfVar] + "\n");
                        break;

                    default:
                        DebugLog.AppendText("A wtf error has occurred.\n");
                        break;
                }
            }
            else
            {
                for(int i = 1; i < command.Length; i++)
                {
                    OutputLog.AppendText(command[i] + " ");
                }
                OutputLog.AppendText("\n");
            }
        }

        //module for variable assignment
        private void assignVar(string[] command)
        {
            //let's turn down invalid syntax immediately
            if(command.Length != 4)
            {
                DebugLog.AppendText("Assignment Error: Correct format is:\n\"Assign value to varName\"\n");
                return;
            }
            if(command[2] != "to")
            {
                DebugLog.AppendText("Assignment Error: Correct format is:\n\"Assign value to varName\"\n");
                return;
            }
            int indexOfVar;

            indexOfVar = findVariableIndex(command[3]);


            //final validation check. If the variable isn't found, it will return
            if (indexOfVar == -1)
            {
                DebugLog.AppendText("Variable " + command[3] + " not found.\n");
                return;
            }

            switch (variableTypeFound)
            {
                case "string":
                    Storage.stringVars[indexOfVar] = command[1];
                    DebugLog.AppendText("Value " + Storage.stringVars[indexOfVar] + " assigned to " + Storage.stringNames[indexOfVar] + ".\n");
                    break;

                case "int":
                    int tempInt;

                    if (int.TryParse(command[1], out tempInt))
                    {
                        Storage.intVars[indexOfVar] = tempInt;
                        DebugLog.AppendText("Value " + Storage.intVars[indexOfVar] + " assigned to " + Storage.intNames[indexOfVar] + ".\n");
                    }
                    else
                    {
                        DebugLog.AppendText("Parsing Error: Assignment failed.\n");
                    }
                    break;

                case "double":
                    double tempDouble;

                    if (double.TryParse(command[1], out tempDouble))
                    {
                        Storage.doubleVars[indexOfVar] = tempDouble;
                        DebugLog.AppendText("Value " + Storage.doubleVars[indexOfVar] + " assigned to " + Storage.doubleNames[indexOfVar] + ".\n");
                    }
                    else
                    {
                        DebugLog.AppendText("Parsing Error: Assignment failed.\n");
                    }
                    break;

                case "bool":
                    bool tempBool;

                    if (bool.TryParse(command[1], out tempBool))
                    {
                        Storage.boolVars[indexOfVar] = tempBool;
                        DebugLog.AppendText("Value " + Storage.boolVars[indexOfVar] + " assigned to " + Storage.boolNames[indexOfVar] + ".\n");
                    }
                    else
                    {
                        DebugLog.AppendText("Parsing Error: Assignment failed.\n");
                    }
                    break;

                case "char":
                    char tempChar;

                    if (char.TryParse(command[1], out tempChar))
                    {
                        Storage.charVars[indexOfVar] = tempChar;
                        DebugLog.AppendText("Value " + Storage.charVars[indexOfVar] + " assigned to " + Storage.charNames[indexOfVar] + ".\n");
                    }
                    else
                    {
                        DebugLog.AppendText("Parsing Error: Assignment failed.\n");
                    }
                    break;

                default:
                    DebugLog.AppendText("A wtf error has occurred.\n");
                    break;                
            }
        }

        //module for mathematics TODO: Figure out a way to differentiate doubles and ints
        private void math(string[] command)
        {
            if(command.Length != 3 && command.Length != 4)
            {
                DebugLog.AppendText("Invalid math statement. Format is:\n" +
                    "Add/subtract/multiply/divide var1 var2 (optional)var3\n");
            }
            int indexOfVar1, indexOfVar2, indexOfVar3 = -2;

            indexOfVar1 = findVariableIndex(command[1]);
            if(variableTypeFound != "double" || variableTypeFound != "int")
            {
                DebugLog.AppendText("Error: Math operations cannot be performed on non-numbers.\n");
                return;
            }

            bool var1IsDouble = (indexOfVar1 != -1 && variableTypeFound == "double");

            indexOfVar2 = findVariableIndex(command[2]);
            bool var2IsDouble = (indexOfVar2 != -1 && variableTypeFound == "double");

            bool var3IsDouble;
            if(command.Length == 4)
            {
                indexOfVar3 = findVariableIndex(command[3]);
                var3IsDouble = (indexOfVar3 != -1 && variableTypeFound == "double");
            }
            else
            {
                var3IsDouble = false;
            }

            if(indexOfVar1 == -1 || indexOfVar2 == -1)
            {
                DebugLog.AppendText("Math error. Unable to locate one or more variables.\n");
                return;
            }

            if(indexOfVar3 == -1)
            {
                DebugLog.AppendText("Math error. " + command[3] + " not found.\n");
                return;
            }

            //this is where the math begins.
            switch (command[0])
            {
                //format is var1 - var2 = var3
                case "add":
                    if(indexOfVar3 != -2)
                    {
                        if (var3IsDouble)
                        {
                            if (var1IsDouble)//if var1 is a double
                            {   //if all values are doubles
                                if (var2IsDouble)
                                {
                                    Storage.doubleVars[indexOfVar3] = Storage.doubleVars[indexOfVar1] + Storage.doubleVars[indexOfVar2];
                                }
                                else//if var 2 is an int
                                {
                                    Storage.doubleVars[indexOfVar3] = Storage.doubleVars[indexOfVar1] + (double) Storage.intVars[indexOfVar2];
                                }
                            }
                            else//if var1 is an int
                            {
                                if (var2IsDouble)
                                {
                                    Storage.doubleVars[indexOfVar3] = (double)Storage.intVars[indexOfVar1] + Storage.doubleVars[indexOfVar2];
                                }
                                else//if both var1 and var 2 are ints
                                {
                                    Storage.doubleVars[indexOfVar3] = (double)Storage.intVars[indexOfVar1] + (double)Storage.intVars[indexOfVar2];
                                }
                            }
                        }
                        else//if var3 is an int
                        {
                            if (var1IsDouble)//if var1 is a double
                            {   //if all values are doubles
                                if (var2IsDouble)
                                {
                                    Storage.intVars[indexOfVar3] = (int)(Storage.doubleVars[indexOfVar1] + Storage.doubleVars[indexOfVar2]);
                                }
                                else//if var 2 is an int
                                {
                                    Storage.intVars[indexOfVar3] = (int)(Storage.doubleVars[indexOfVar1] + (double)Storage.intVars[indexOfVar2]);
                                }
                            }
                            else//if var1 is an int
                            {
                                if (var2IsDouble)
                                {
                                    Storage.intVars[indexOfVar3] = (int)((double)Storage.intVars[indexOfVar1] + Storage.doubleVars[indexOfVar2]);
                                }
                                else//if both var1 and var 2 are ints
                                {
                                    Storage.intVars[indexOfVar3] = (int)((double)Storage.intVars[indexOfVar1] + (double)Storage.intVars[indexOfVar2]);
                                }
                            }
                        }
                    }
                    else//if var 3 is not present, we will instead assign the value to var1 instead.
                    {
                        if (var1IsDouble)
                        {
                            if (var2IsDouble)//if both vars are doubles
                            {
                                Storage.doubleVars[indexOfVar1] += Storage.doubleVars[indexOfVar2];
                            }
                            else//if var2 is an int
                            {
                                Storage.doubleVars[indexOfVar1] += (double)Storage.intVars[indexOfVar2];
                            }
                        }
                        else//if var1 is an int
                        {
                            if (var2IsDouble)
                            {
                                Storage.intVars[indexOfVar1] = (int)((double)Storage.intVars[indexOfVar1] + Storage.intVars[indexOfVar2]);
                            }
                            else//if both variables are ints
                            {
                                Storage.intVars[indexOfVar1] += Storage.intVars[indexOfVar2];
                            }
                        }
                    }
                    break;

                case "subtract":
                    if (indexOfVar3 != -2)
                    {
                        if (var3IsDouble)
                        {
                            if (var1IsDouble)//if var1 is a double
                            {   //if all values are doubles
                                if (var2IsDouble)
                                {
                                    Storage.doubleVars[indexOfVar3] = Storage.doubleVars[indexOfVar1] - Storage.doubleVars[indexOfVar2];
                                }
                                else//if var 2 is an int
                                {
                                    Storage.doubleVars[indexOfVar3] = Storage.doubleVars[indexOfVar1] - (double)Storage.intVars[indexOfVar2];
                                }
                            }
                            else//if var1 is an int
                            {
                                if (var2IsDouble)
                                {
                                    Storage.doubleVars[indexOfVar3] = (double)Storage.intVars[indexOfVar1] - Storage.doubleVars[indexOfVar2];
                                }
                                else//if both var1 and var 2 are ints
                                {
                                    Storage.doubleVars[indexOfVar3] = (double)Storage.intVars[indexOfVar1] - (double)Storage.intVars[indexOfVar2];
                                }
                            }
                        }
                        else//if var3 is an int
                        {
                            if (var1IsDouble)//if var1 is a double
                            {   //if all values are doubles
                                if (var2IsDouble)
                                {
                                    Storage.intVars[indexOfVar3] = (int)(Storage.doubleVars[indexOfVar1] - Storage.doubleVars[indexOfVar2]);
                                }
                                else//if var 2 is an int
                                {
                                    Storage.intVars[indexOfVar3] = (int)(Storage.doubleVars[indexOfVar1] - (double)Storage.intVars[indexOfVar2]);
                                }
                            }
                            else//if var1 is an int
                            {
                                if (var2IsDouble)
                                {
                                    Storage.intVars[indexOfVar3] = (int)((double)Storage.intVars[indexOfVar1] - Storage.doubleVars[indexOfVar2]);
                                }
                                else//if both var1 and var 2 are ints
                                {
                                    Storage.intVars[indexOfVar3] = (int)((double)Storage.intVars[indexOfVar1] - (double)Storage.intVars[indexOfVar2]);
                                }
                            }
                        }
                    }
                    else//if var 3 is not present, we will instead assign the value to var1 instead.
                    {
                        if (var1IsDouble)
                        {
                            if (var2IsDouble)//if both vars are doubles
                            {
                                Storage.doubleVars[indexOfVar1] -= Storage.doubleVars[indexOfVar2];
                            }
                            else//if var2 is an int
                            {
                                Storage.doubleVars[indexOfVar1] -= (double)Storage.intVars[indexOfVar2];
                            }
                        }
                        else//if var1 is an int
                        {
                            if (var2IsDouble)
                            {
                                Storage.intVars[indexOfVar1] = (int)((double)Storage.intVars[indexOfVar1] - Storage.intVars[indexOfVar2]);
                            }
                            else//if both variables are ints
                            {
                                Storage.intVars[indexOfVar1] -= Storage.intVars[indexOfVar2];
                            }
                        }
                    }
                    break;

                case "multiply":
                    if (indexOfVar3 != -2)
                    {
                        if (var3IsDouble)
                        {
                            if (var1IsDouble)//if var1 is a double
                            {   //if all values are doubles
                                if (var2IsDouble)
                                {
                                    Storage.doubleVars[indexOfVar3] = Storage.doubleVars[indexOfVar1] * Storage.doubleVars[indexOfVar2];
                                }
                                else//if var 2 is an int
                                {
                                    Storage.doubleVars[indexOfVar3] = Storage.doubleVars[indexOfVar1] * (double)Storage.intVars[indexOfVar2];
                                }
                            }
                            else//if var1 is an int
                            {
                                if (var2IsDouble)
                                {
                                    Storage.doubleVars[indexOfVar3] = (double)Storage.intVars[indexOfVar1] * Storage.doubleVars[indexOfVar2];
                                }
                                else//if both var1 and var 2 are ints
                                {
                                    Storage.doubleVars[indexOfVar3] = (double)Storage.intVars[indexOfVar1] * (double)Storage.intVars[indexOfVar2];
                                }
                            }
                        }
                        else//if var3 is an int
                        {
                            if (var1IsDouble)//if var1 is a double
                            {   //if all values are doubles
                                if (var2IsDouble)
                                {
                                    Storage.intVars[indexOfVar3] = (int)(Storage.doubleVars[indexOfVar1] * Storage.doubleVars[indexOfVar2]);
                                }
                                else//if var 2 is an int
                                {
                                    Storage.intVars[indexOfVar3] = (int)(Storage.doubleVars[indexOfVar1] * (double)Storage.intVars[indexOfVar2]);
                                }
                            }
                            else//if var1 is an int
                            {
                                if (var2IsDouble)
                                {
                                    Storage.intVars[indexOfVar3] = (int)((double)Storage.intVars[indexOfVar1] * Storage.doubleVars[indexOfVar2]);
                                }
                                else//if both var1 and var 2 are ints
                                {
                                    Storage.intVars[indexOfVar3] = (int)((double)Storage.intVars[indexOfVar1] * (double)Storage.intVars[indexOfVar2]);
                                }
                            }
                        }
                    }
                    else//if var 3 is not present, we will instead assign the value to var1 instead.
                    {
                        if (var1IsDouble)
                        {
                            if (var2IsDouble)//if both vars are doubles
                            {
                                Storage.doubleVars[indexOfVar1] *= Storage.doubleVars[indexOfVar2];
                            }
                            else//if var2 is an int
                            {
                                Storage.doubleVars[indexOfVar1] *= (double)Storage.intVars[indexOfVar2];
                            }
                        }
                        else//if var1 is an int
                        {
                            if (var2IsDouble)
                            {
                                Storage.intVars[indexOfVar1] = (int)((double)Storage.intVars[indexOfVar1] * Storage.intVars[indexOfVar2]);
                            }
                            else//if both variables are ints
                            {
                                Storage.intVars[indexOfVar1] *= Storage.intVars[indexOfVar2];
                            }
                        }
                    }
                    break;

                case "divide":
                    //first things first, with division, we have to look for any divide by zero errors
                    if (var2IsDouble)
                    {
                        if(Storage.doubleVars[indexOfVar2] == 0)
                        {
                            DebugLog.AppendText("Divide by zero error. Unable to complete operation.\n");
                            return;
                        }
                    }
                    else
                    {
                        if(Storage.intVars[indexOfVar2] == 0)
                        {
                            DebugLog.AppendText("Divide by zero error. Unable to complete operation.\n");
                            return;
                        }
                    }

                    if (indexOfVar3 != -2)
                    {
                        if (var3IsDouble)
                        {
                            if (var1IsDouble)//if var1 is a double
                            {   //if all values are doubles
                                if (var2IsDouble)
                                {
                                    Storage.doubleVars[indexOfVar3] = Storage.doubleVars[indexOfVar1] / Storage.doubleVars[indexOfVar2];
                                }
                                else//if var 2 is an int
                                {
                                    Storage.doubleVars[indexOfVar3] = Storage.doubleVars[indexOfVar1] / (double)Storage.intVars[indexOfVar2];
                                }
                            }
                            else//if var1 is an int
                            {
                                if (var2IsDouble)
                                {
                                    Storage.doubleVars[indexOfVar3] = (double)Storage.intVars[indexOfVar1] / Storage.doubleVars[indexOfVar2];
                                }
                                else//if both var1 and var 2 are ints
                                {
                                    Storage.doubleVars[indexOfVar3] = (double)Storage.intVars[indexOfVar1] / (double)Storage.intVars[indexOfVar2];
                                }
                            }
                        }
                        else//if var3 is an int
                        {
                            if (var1IsDouble)//if var1 is a double
                            {   //if all values are doubles
                                if (var2IsDouble)
                                {
                                    Storage.intVars[indexOfVar3] = (int)(Storage.doubleVars[indexOfVar1] / Storage.doubleVars[indexOfVar2]);
                                }
                                else//if var 2 is an int
                                {
                                    Storage.intVars[indexOfVar3] = (int)(Storage.doubleVars[indexOfVar1] / (double)Storage.intVars[indexOfVar2]);
                                }
                            }
                            else//if var1 is an int
                            {
                                if (var2IsDouble)
                                {
                                    Storage.intVars[indexOfVar3] = (int)((double)Storage.intVars[indexOfVar1] / Storage.doubleVars[indexOfVar2]);
                                }
                                else//if both var1 and var 2 are ints
                                {
                                    Storage.intVars[indexOfVar3] = (int)((double)Storage.intVars[indexOfVar1] / (double)Storage.intVars[indexOfVar2]);
                                }
                            }
                        }
                    }
                    else//if var 3 is not present, we will instead assign the value to var1 instead.
                    {
                        if (var1IsDouble)
                        {
                            if (var2IsDouble)//if both vars are doubles
                            {
                                Storage.doubleVars[indexOfVar1] /= Storage.doubleVars[indexOfVar2];
                            }
                            else//if var2 is an int
                            {
                                Storage.doubleVars[indexOfVar1] /= (double)Storage.intVars[indexOfVar2];
                            }
                        }
                        else//if var1 is an int
                        {
                            if (var2IsDouble)
                            {
                                Storage.intVars[indexOfVar1] = (int)((double)Storage.intVars[indexOfVar1] / Storage.intVars[indexOfVar2]);
                            }
                            else//if both variables are ints
                            {
                                Storage.intVars[indexOfVar1] /= Storage.intVars[indexOfVar2];
                            }
                        }
                    }
                    break;

                case "mod":
                    placeholder();
                    break;

                default:
                    DebugLog.AppendText("A wtf error has occurred.\n");
                    break;
            }
        }

        private void joinStrings(string[] command)
        {
            placeholder();
        }

        /*REMOVED FUNCTIONALITY
        private void isTrue(string[] command)
        {
            placeholder();
        }*/

        private void search(string[] command)
        {
            placeholder();
        }


        //utility methods. Not used for anything with regards to user commands, but instead for in-program use

        public void placeholder()
        {
            DebugLog.AppendText("Feature not yet implemented.\n");
        }


        //method to search for variables within the storage class.
        public int findVariableIndex(string name)
        {
            int counter = 0;
            foreach (string s in Storage.stringNames)
            {
                if(name == s)
                {
                    variableTypeFound = "string";
                    return counter;
                }
                counter++;
            }

            counter = 0;

            foreach (string s in Storage.intNames)
            {
                if (name == s)
                {
                    variableTypeFound = "int";
                    return counter;
                }
                counter++;
            }

            counter = 0;

            foreach (string s in Storage.doubleNames)
            {
                if (name == s)
                {
                    variableTypeFound = "double";
                    return counter;
                }
                counter++;
            }

            counter = 0;

            foreach (string s in Storage.boolNames)
            {
                if (name == s)
                {
                    variableTypeFound = "bool";
                    return counter;
                }
                counter++;
            }

            counter = 0;

            foreach (string s in Storage.charNames)
            {
                if (name == s)
                {
                    variableTypeFound = "char";
                    return counter;
                }
                counter++;
            }

            return -1;
        }

        //this method is for checking for duplicate variable names
        private bool checkStorage(string str)
        {
            bool[] validated = new bool[5];
            //set all values to true so they remain true until a fail is detected
            for(int i = 0; i < validated.Length; i++)
            {
                validated[i] = true;
            }
            foreach (string s in Storage.stringNames)
            {
                validated[0] = validate(str, s);
            }

            foreach (string s in Storage.intNames)
            {
                validated[1] = validate(str, s);
                
            }

            foreach (string s in Storage.doubleNames)
            {
                validated[2] = validate(str, s);
            }

            foreach (string s in Storage.boolNames)
            {
                validated[3] = validate(str, s);
            }

            foreach (string s in Storage.charNames)
            {
                validated[4] = validate(str, s);
            }
            return (validated[0] && validated[1] && validated[2] && validated[3] && validated[4]);
        }

        //works with checkstorage to validate names.
        private bool validate(string str1, string str2)
        {
            if (str1 == str2)
            {
                DebugLog.AppendText("Error. Variable " + str1 + " already exists.\n");
                return false;
            }
            return true;
        }
    }
}
