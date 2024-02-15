using ConsoleTables;
using DMARC_Reader.Entities;

namespace DMARC_Reader;

public static class Program
{
    public static void Main(string[] args)
    {
        if (args.Length == 0)
            WriteProgramInfo();
        
        string dumpPath = args[0];

        if (!Path.Exists(dumpPath))
            WriteError("path \"{0}\" is not exist!", new object[] { dumpPath });
        
        WriteDmarcReportTable(dumpPath);
    }

    public static void WriteDmarcReportTable(string reportPath)
    {
        ConsoleTable consoleTable = new ConsoleTable("IP", "Hostname", "Count", "Disposition", "DKIM", "SPF");
        ReportReader reader = new ReportReader(reportPath);

        foreach (Record record in reader.Read())
            consoleTable.AddRow(record.Ip, record.Hostname, record.Count, record.Disposition, record.Dkim, record.Spf);
        
        Console.WriteLine();
        consoleTable.Write(Format.MarkDown);
    }

    public static void WriteProgramInfo()
    {
        Console.WriteLine();
        Console.WriteLine(
            " ____  __  __    _    ____   ____      ____                _           \n" +
            "|  _ \\|  \\/  |  / \\  |  _ \\ / ___|    |  _ \\ ___  __ _  __| | ___ _ __ \n" +
            "| | | | |\\/| | / _ \\ | |_) | |   _____| |_) / _ \\/ _` |/ _` |/ _ \\ '__|\n" +
            "| |_| | |  | |/ ___ \\|  _ <| |__|_____|  _ <  __/ (_| | (_| |  __/ |   \n" +
            "|____/|_|  |_/_/   \\_\\_| \\_\\\\____|    |_| \\_\\___|\\__,_|\\__,_|\\___|_|");
        Console.WriteLine();

        Console.WriteLine("DMARC-Reader Version: 1.1");
        Console.WriteLine("CLI utility for viewing DMARC records in table form.");
        Console.WriteLine("Created special for Apels1n-NET.");
        Console.WriteLine();
        
        Console.WriteLine("Usage: dmarcrd <path to xml dump file>");
        Console.WriteLine();
        
        Console.WriteLine("(C) MG Inc. 2024 (C) Raleyph 2024");
        Console.WriteLine();
        
        Environment.Exit(0);
    }

    public static void WriteError(string errorText, object[] args)
    {
        Console.WriteLine($"dmarcrd: {errorText}", args);
        Console.WriteLine();
        
        Environment.Exit(-1);
    }
}
