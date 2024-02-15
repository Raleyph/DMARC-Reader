using System.Xml;
using System.Net;
using System.Net.Sockets;

namespace DMARC_Reader.Entities;

public class ReportReader
{
    public XmlDocument Document { get; }

    public ReportReader(string dumpPath)
    {
        Document = new XmlDocument();
        Document.Load(dumpPath);
    }

    public List<Record> Read()
    {
        List<Record> records = new List<Record>(); 
        XmlNodeList rows = Document.GetElementsByTagName("row");

        foreach (XmlNode row in rows)
        {
            string ip = "";
            string hostname;
            int count = 0;
            string disposition = "";
            string dkim = "";
            string spf = "";

            foreach (XmlNode rowNode in row.ChildNodes)
            {
                string nodeName = rowNode.Name;
                string nodeText = rowNode.InnerText;
                
                if (nodeName == "source_ip")
                    ip = nodeText;
                else if (nodeName == "count")
                    count = Convert.ToInt32(nodeText);
                else if (nodeName == "policy_evaluated")
                {
                    foreach (XmlNode evaluate in rowNode.ChildNodes)
                    {
                        string evaluateName = evaluate.Name;
                        string evaluateText = evaluate.InnerText;
                        
                        if (evaluateName == "disposition")
                            disposition = evaluateText;
                        else if (evaluateName == "dkim")
                            dkim = evaluateText;
                        else if (evaluateName == "spf")
                            spf = evaluateText;
                    }
                }
            }

            try
            {
                hostname = Dns.GetHostEntry(ip).HostName;
            }
            catch (SocketException)
            {
                hostname = "none";
            }
            
            records.Add(new Record(ip, hostname, count, disposition, dkim, spf));
        }

        return records;
    }
}
