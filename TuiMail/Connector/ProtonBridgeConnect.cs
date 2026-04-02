using MailKit.Net.Imap;
using MailKit.Security;
using MailKit;
using MimeKit;

namespace TuiMail.Connector;

public class ProtonBridgeConnect
{
    private static ProtonBridgeConnect _instance = null;

    private ProtonBridgeConnect()
    {
    }

    public ImapClient Connect(string user, string password)
    {
        try
        {
            Logger.Logger.GetInstance().Debug("Connect");

            var imap = new ImapClient();
            imap.Connect("127.0.0.1", 1143, SecureSocketOptions.StartTls);
            Logger.Logger.GetInstance().Debug("ImapClient connected");

            imap.Authenticate(user, password);

            if (!imap.IsAuthenticated)
            {
                Logger.Logger.GetInstance().Debug("Failed to authenticate");
                return null;
            }

            Logger.Logger.GetInstance().Debug("Connected successfully");
            return imap;
        }
        catch (Exception ex)
        {
            Logger.Logger.GetInstance().Debug($"Connection error: {ex.Message}");
            return null;
        }
    }

    public List<MimeMessage> GetMessages(ImapClient imap)
    {
        try
        {
            if (imap == null || !imap.IsConnected)
            {
                Logger.Logger.GetInstance().Debug("IMAP client not connected");
                return new List<MimeMessage>();
            }

            imap.Inbox.Open(FolderAccess.ReadOnly);
            var messages = new List<MimeMessage>();
            for (int i = 0; i < imap.Inbox.Count; i++)
            {
                messages.Add(imap.Inbox.GetMessage(i));
            }
            return messages;
        }
        catch (Exception ex)
        {
            Logger.Logger.GetInstance().Debug($"Error getting messages: {ex.Message}");
            return new List<MimeMessage>();
        }
    }

    public static ProtonBridgeConnect GetInstance()
    {
        if (_instance == null)
        {
            _instance = new ProtonBridgeConnect();
        }
        return _instance;
    }
}
