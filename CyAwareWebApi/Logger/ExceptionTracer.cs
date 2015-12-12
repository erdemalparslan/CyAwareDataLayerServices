
using System;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Web;
using System.Web.Http.Tracing;
using CyAwareWebApi.Models;

public class ExceptionTracer : ITraceWriter
{
    private CyAwareContext db;

    public void Trace(HttpRequestMessage request, string category, TraceLevel level, Action<TraceRecord> traceAction)
    {
        TraceRecord rec = new TraceRecord(request, category, level);
        if (level == TraceLevel.Error || level == TraceLevel.Fatal)
        {
            traceAction(rec);
            WriteTrace(rec);
        }
    }

    protected void WriteTrace(TraceRecord record)
    {
        try
        {
            SysLog log = new SysLog() { message = record.Message, severity = "Error", source = record.Category.Split(':')[1], apiMethod = record.Category.Split(':')[0] };
            db = new CyAwareContext();
            db.Configuration.ProxyCreationEnabled = false;
            db.SysLogs.Add(log);
            db.SaveChangesAsync();
        }
        catch (Exception)
        {
            var uriPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
            var path = new Uri(uriPath).LocalPath + "/../Logs/Exceptions.log";
            File.AppendAllText(path, DateTime.Now + " - " + record.Category + " - " + record.Message + "\r\n");
        }

    }
}