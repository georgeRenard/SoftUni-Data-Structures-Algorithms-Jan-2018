using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

class Program
{
    static void Main(string[] args)
    {

        ITextEditor editor = new TextEditor();
        Dictionary<string, bool> users = new Dictionary<string, bool>();

        string line = string.Empty;
        Regex regex = new Regex("\"(.*)\"");

        while ((line = Console.ReadLine()) != "end")
        {
            Match match = regex.Match(line);
            string[] commandArgs =
                line.Split(new char[] {' '}
                , StringSplitOptions.RemoveEmptyEntries);
            try
            {
                switch (commandArgs[0])
                {
                    case "login":
                        users[commandArgs[1]] = true;
                        editor.Login(commandArgs[1]);
                        break;
                    case "logout":
                        users[commandArgs[1]] = false;
                        editor.Logout(commandArgs[1]);
                        break;
                    case "users":
                        if (commandArgs.Length == 2)
                        {
                            editor.Users(commandArgs[1]);
                            break;
                        }
                        editor.Users();
                        break;
                }

                string username = commandArgs[0];
                if (!(users.ContainsKey(username) && users[username]))
                {
                    continue;
                }

                string str = match.Groups[1].Value;
                switch (commandArgs[1])
                {
                    case "insert":
                        editor.Insert(username, int.Parse(commandArgs[2]), str);
                        break;
                    case "prepend":
                        editor.Prepend(username, str);
                        break;
                    case "substring":
                        editor.Substring(username,
                            int.Parse(commandArgs[2]),
                            int.Parse(commandArgs[3]));
                        break;
                    case "delete":
                        editor.Delete(username,
                            int.Parse(commandArgs[2]),
                            int.Parse(commandArgs[3]));
                        break;
                    case "clear":
                        editor.Clear(username);
                        break;
                    case "length":
                        Console.WriteLine(editor.Length(username));
                        break;
                    case "print":
                        Console.WriteLine(editor.Print(username));
                        break;
                    case "undo":
                        editor.Undo(username);
                        break;
                }
            }
            catch (Exception ex)
            {
            }
        }

    }

    
}
