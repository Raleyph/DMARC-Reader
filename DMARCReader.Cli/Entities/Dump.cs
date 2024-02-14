namespace DMARC_Reader.Entities;

public class Dump
{
    public string Ip { get; }
    public int Count { get; }
    public string Disposition { get; }
    public string Dkim { get; }
    public string Spf { get; }
    
    public Dump(string ip, int count, string disposition, string dkim, string spf)
    {
        Ip = ip;
        Count = count;
        Disposition = disposition;
        Dkim = dkim;
        Spf = spf;
    }
}
