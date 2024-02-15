namespace DMARC_Reader.Entities;

public class Record
{
    public string Ip { get; }
    public string Hostname { get; }
    public int Count { get; }
    public string Disposition { get; }
    public string Dkim { get; }
    public string Spf { get; }
    
    public Record(string ip, string hostname, int count, string disposition, string dkim, string spf)
    {
        Ip = ip;
        Hostname = hostname;
        Count = count;
        Disposition = disposition;
        Dkim = dkim;
        Spf = spf;
    }
}
