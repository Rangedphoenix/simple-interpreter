# simple-interpreter

This is an application that I developed for one of my previous classes, and is a relatively simple look at an interpreter. It is designed to be able to execute specific commands in real time, and has some basic functionality one would expect from a programming language.

Syntax:

All statements declared within this interpreter begin with a base command. This is the root command that will decide what the statement that is currently being executed will do. There are several keywords, including:

-var : For commands dealing with variable creation.
-print: For displaying values in the log window.
-assign: Assigns values to previously declared variables
-add/subtract/multiply/divide/mod: Performs mathematic operations on numeric variables. (Note: Mod is not implemented yet.)
-concat: Join two strings together. (Not yet implemented)
-search: Search for a specific value within the library. (Not yet implemented)


After the basic keyword is declared, the program will then begin to branch, depending on what keyword is used. After this, the syntax remains similar, but may have slight variations.

-var: Uses the syntax of var varType varName value(Optional)
At present, the following data types are available: string, int, double, bool, char.

-print: print varToPrint(If a variable is not found here, it will simply print out everything beyond the print keyword.)

-assign: assign value to varName. (Note that data types are strictly enforced)

-add/subtract/multiply/divide/mod: mathCommand var1 var2 var3(optional)
Note: var3 stores the results of the operation between var1 and var2. If it is not declared, the result will be stored in var1 instead. This may change in the future.

-join: Syntax on this command has not entirely been decided, however, it will likely end up looking similar to the math commands, and use a join var1 var2 var3(optional) syntax.

-search: Again, not presently implemented, but it will probably have a syntax similar to search varType for value.
