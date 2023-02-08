using System.Reflection.Metadata;
using telepawn_cli;
using TP;
using TP.Shared;
using System;


//Check if LOG Dir exists
if (!Directory.Exists(AppContext.BaseDirectory + "Logs/"))
    Directory.CreateDirectory(AppContext.BaseDirectory + "Logs/");

File.AppendAllText(AppContext.BaseDirectory + "Logs/current.txt", "---- Started " + DateTime.Now.ToString() + " ----\n");
Console.WriteLine("Hello, World!");

TPConfiguration config = new TPConfiguration("5300250014:AAHjYSoJpK9yYNug6kkPMuBqBX06lw5d_Wg", "main");

TelePawn tp = new TelePawn(config);
Kernel32.Init(tp);

string input;
while(tp.IsRunning)
{
    input = Console.ReadLine()!;
    if (input == null) continue;

    Console.WriteLine(input);


    if (input.Equals("exit"))
        break;
    Thread.Sleep(200);
}

tp.StopSafely();


