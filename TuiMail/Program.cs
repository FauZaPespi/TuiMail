using Terminal.Gui;

Application.Init();

Colors.Base = new ColorScheme()
{
    Normal = new Terminal.Gui.Attribute(Color.Gray, Color.Black),
    Focus = new Terminal.Gui.Attribute(Color.Black, Color.Gray),
    HotNormal = new Terminal.Gui.Attribute(Color.BrightMagenta, Color.Black),
    HotFocus = new Terminal.Gui.Attribute(Color.BrightMagenta, Color.Gray),
};

var fieldScheme = new ColorScheme()
{
    Normal = new Terminal.Gui.Attribute(Color.Gray, Color.DarkGray),
    Focus = new Terminal.Gui.Attribute(Color.Black, Color.Gray),
};

var mainWindow = new Window("TuiMail - v1.0")
{
    X = 0,
    Y = 0,
    Width = Dim.Fill(),
    Height = Dim.Fill(),
    ColorScheme = Colors.Base
};

var asciiArt = @"
  _______    _ __  __       _ _ 
 |__   __|  (_)  \/  |     (_) |
    | |_   _ _| \  / | __ _ _| |
    | | | | | | |\/| |/ _` | | |
    | | |_| | | |  | | (_| | | |
    |_|\__,_|_|_|  |_|\__,_|_|_|
";

var mainLabel = new Label(asciiArt)
{
    X = Pos.Center(),
    Y = 1,
    Width = Dim.Fill(),
    Height = 6,
    TextAlignment = TextAlignment.Centered
};

var usrFrame = new FrameView(" Username ") 
{
    X = Pos.Center(),
    Y = Pos.Bottom(mainLabel) + 2,
    Width = 32,
    Height = 3, 
};

var usrInput = new TextField("")
{
    X = 0,
    Y = 0,
    Width = Dim.Fill(),
    ColorScheme = fieldScheme
};
usrFrame.Add(usrInput);


var pwdFrame = new FrameView(" Password ")
{
    X = Pos.Center(),
    Y = Pos.Bottom(usrFrame) + 1,
    Width = 32,
    Height = 3,
};

var pwdInput = new TextField("")
{
    X = 0,
    Y = 0,
    Width = Dim.Fill(),
    Secret = true,
    ColorScheme = fieldScheme
};
pwdFrame.Add(pwdInput);


var btnLogin = new Button("Login")
{
    X = Pos.Center(),
    Y = Pos.Bottom(pwdFrame) + 2
};

btnLogin.Clicked += () =>
{
    var user = usrInput.Text.ToString();
    var pass = pwdInput.Text.ToString();

    if (string.IsNullOrWhiteSpace(user))
    {
        MessageBox.ErrorQuery("Error", "Please enter a username.", "OK");
        usrInput.SetFocus();
    }
    else
    {
        MessageBox.Query("TuiMail", $"Logging in as {user}...", "Continue");
    }
};

mainWindow.Add(mainLabel, usrFrame, pwdFrame, btnLogin);

Application.Top.Add(mainWindow);
Application.Run();
Application.Shutdown();