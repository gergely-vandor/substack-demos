// filesPath is the path where this executable is running
string filesPath = Path.Combine(Directory.GetCurrentDirectory());

//files will be a string array containing the full path of all (txt) files in the folder where we are running.
//We filter for txt files
//This array is useful when we want to run the app manually, without putting it in a memoQ template.
//This way it processes TXT files in the folder where it is running, and all its subfolders
//And then we don't need to use any command line arguments
string[] files = Directory.GetFiles(filesPath, "*.txt", SearchOption.AllDirectories);

//However, if we are running it in memoQ automation, then there are at least two command line arguments, inputFile and outputFile
//Then we overwrite (reassign) our array with the single file that is specified in the first command line argument, which is args[0] 
//(google "zero indexing" if you wonder why the first item in the array is [0])
//In this case, we still have an array, but with a single string in it (just one file to process).
//So, the program checks if there were any command line arguments
if (args.Length > 0)
    files = new string[] { args[0] };
//Now, we go through the files. In the case of memoq automation, there is just one.
foreach (string file in files)
{

    string textFromFile = File.ReadAllText(file);

    //we keep replacing double spaces with single spaces
    //before each replace, we check if the text still has any double spaces
    while (textFromFile.Contains("  "))
    {
        textFromFile = textFromFile.Replace("  ", " ");
    }
    
    //args[1] is the second argument, outputFile
    //if it exists, because the tool was run with command line arguments, we copy the file there
    if (args.Length > 0)
        File.WriteAllText(args[1], textFromFile);
    //or if we are running locally, without command line arguments, we just overwrite the original file
    else
        File.WriteAllText(file, textFromFile);
}