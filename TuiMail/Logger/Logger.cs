namespace TuiMail.Logger;

public class Logger
{
    private static Logger _instance = null;
    private static readonly object _lock = new object();
    private static readonly string LogDirectory = "./logs/";

    private Logger()
    {
        if (!Directory.Exists(LogDirectory))
        {
            Directory.CreateDirectory(LogDirectory);
        }
    }

    public static Logger GetInstance()
    {
        lock (_lock)
        {
            if (_instance == null)
            {
                _instance = new Logger();
            }
            return _instance;
        }
    }

    public void Debug(string message)
    {
        try
        {
            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss_fff");
            string logMessage = $"DEBUG AT {DateTime.Now}{Environment.NewLine}{message}{Environment.NewLine}";
            string logFilePath = Path.Combine(LogDirectory, $"{timestamp}_logfile.log");
            File.AppendAllText(logFilePath, logMessage);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to write log: {ex.Message}");
        }
    }
}