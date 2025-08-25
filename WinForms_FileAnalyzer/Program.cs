using System;
using System.Windows.Forms;
using Microsoft.Extensions.Logging;
using Karambolo.Extensions.Logging.File;

namespace WinForms_FileAnalyzer
{
    internal static class Program
    {
        public static ILoggerFactory LoggerFactory { get; private set; }
        public static ILogger Logger { get; private set; }

        

        [STAThread]
        static void Main()
        {
            // --- LOGGING KURULUMU ---
            LoggerFactory = Microsoft.Extensions.Logging.LoggerFactory.Create(builder =>
            {
                builder
                    .SetMinimumLevel(LogLevel.Information)
                    .AddConsole()
                    .AddDebug()
                    .AddFile(o =>
                    {
                        // Logs klasörü
                        var logsDir = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
                        logsDir = System.IO.Path.GetFullPath(logsDir);
                        System.IO.Directory.CreateDirectory(logsDir);

                        o.RootPath = logsDir;

                        // 3.x API: dosyaları Files ile tanımla (formatter kullanmıyoruz → uyumlu)
                        o.Files = new[]
                        {
                            new LogFileOptions
                            {
                                // Günlük dosyası: Logs\app-YYYY-MM-DD.txt
                                Path = "app-<date:yyyy-MM-dd>.txt"
                            }
                        };
                    }); // <-- AddFile kapanışı
            });        // <-- LoggerFactory.Create(builder => ...) kapanışı

            Logger = LoggerFactory.CreateLogger("WinFormsApp");

            // --- WinForms başlangıcı ---
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                Logger.LogInformation("Uygulama başlatıldı.");

                using (var login = new LoginForm())
                {
                    var result = login.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        Logger.LogInformation("Kullanıcı giriş yaptı, MainForm açılıyor.");
                        Application.Run(new MainForm());
                    }
                    else
                    {
                        Logger.LogInformation("Giriş iptal edildi. Uygulama kapanıyor.");
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogCritical(ex, "Uygulama kritik bir hatayla sonlandı!");
                MessageBox.Show("Beklenmeyen bir hata oluştu. Ayrıntılar log dosyasına yazıldı.", "Kritik Hata");
            }
            finally
            {
                (LoggerFactory as IDisposable)?.Dispose();
            }
        }
    }
}
