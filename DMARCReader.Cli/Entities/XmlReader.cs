using System.Xml;

namespace DMARC_Reader.Entities;

public class XmlReader
{
    public XmlDocument Document { get; }

    public XmlReader(string dumpPath)
    {
        Document = new XmlDocument();
        Document.Load(dumpPath);
    }

    public List<Dump> Read()
    {
        List<Dump> dumps = new List<Dump>(); 
        XmlNodeList rows = Document.GetElementsByTagName("row");

        foreach (XmlNode row in rows)
        {
            string ip = "";
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
            
            dumps.Add(new Dump(ip, count, disposition, dkim, spf));
        }

        return dumps;
    }
}
