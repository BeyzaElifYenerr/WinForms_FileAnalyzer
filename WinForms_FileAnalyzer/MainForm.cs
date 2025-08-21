using System;
using System.IO;
using UglyToad.PdfPig;
using DocumentFormat.OpenXml.Packaging;
using System.Drawing;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text;
using Microsoft.Extensions.Logging;

namespace WinForms_FileAnalyzer
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private class AnalysisResult
        {
            public List<(string Word, int Count)> topWords { get; set; }
                = new List<(string Word, int Count)>();  

            public Dictionary<char, int> punct { get; set; }
                = new Dictionary<char, int>();
            public int uniqueWordCount { get; set; } = 0;  
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cboFileType_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnLoad.Enabled = cboFileType.SelectedItem != null;
        }

        private void topPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            cboFileType.Items.AddRange(new object[] { ".txt", ".docx", ".pdf" });
            btnLoad.Enabled = false;
            progressBar.Minimum = 0;
            progressBar.Maximum = 100;
            statusLabel.Text = "Hazır";

            lblSummary.Text = "";
            lblSummary.AutoSize = false;
            lblSummary.Dock = DockStyle.Top;
            lblSummary.Height = 40;
            lblSummary.Font = new System.Drawing.Font(lblSummary.Font, System.Drawing.FontStyle.Bold);
            lblSummary.TextAlign = ContentAlignment.MiddleCenter;
            lblSummary.TextAlign = ContentAlignment.MiddleCenter;
        }

        private async void btnLoad_Click(object sender, EventArgs e)
        {
            if (cboFileType.SelectedItem == null) return;
            string ext = cboFileType.SelectedItem.ToString();

            using (var ofd = new OpenFileDialog())
            {
                ofd.Filter = $"{ext} files|*{ext}";
                ofd.Title = "Bir dosya seç";
                if (ofd.ShowDialog() != DialogResult.OK) return;

                try
                {
                    Program.Logger.LogInformation("Dosya yükleme işlemi başladı.");
                    progressBar.Value = 0;
                    statusLabel.Text = "Okunuyor...";
                    lblSummary.Text = "";

                    string content = "";

                    if (ext == ".txt")
                    {
                        progressBar.Value = 20;

                        content = await Task.Run(() => File.ReadAllText(ofd.FileName, Encoding.UTF8));
                        progressBar.Value = 60;
                    }
                    else if (ext == ".docx")
                    {
                        progressBar.Value = 20;
                        content = await ReadDocxAsync(ofd.FileName);
                        progressBar.Value = 60;
                    }
                    else if (ext == ".pdf")
                    {
                        progressBar.Value = 20;
                        content = await ReadPdfAsync(ofd.FileName);
                        progressBar.Value = 60;
                    }
                    else
                    {
                        MessageBox.Show("Bu uzantı desteklenmiyor.", "Bilgi");
                        return;
                    }

                    statusLabel.Text = "Analiz ediliyor...";
                    var result = AnalyzeText(content);
                    progressBar.Value = 90;


                    dgvWords.DataSource = result.topWords
                        .Select(x => new { Word = x.Word, Count = x.Count })
                        .ToList();

                    dgvPunct.DataSource = result.punct
                        .Select(kv => new { Punctuation = kv.Key.ToString(), Count = kv.Value })
                        .ToList();


                    lblSummary.Text = $"Farklı kelime: {result.topWords.Count}";

                    progressBar.Value = 100;
                    statusLabel.Text = "Bitti";
                    Program.Logger.LogInformation("Dosya başarıyla yüklendi.");
                }
                catch (Exception ex)
                {
                    statusLabel.Text = "Hata oluştu";
                    MessageBox.Show("Dosya okunamadı ya da analiz edilemedi.\n" + ex.Message, "Hata");
                    Program.Logger.LogError(ex, "Dosya yüklenirken hata oluştu.");
                    MessageBox.Show("Hata: " + ex.Message);
                }
                finally
                {
                    await Task.Delay(300);
                    progressBar.Value = 0;
                }
            }
        }
        private (List<(string Word, int Count)> topWords, Dictionary<char, int> punct) AnalyzeText(string content)
        {
            var punct = new Dictionary<char, int>();
            foreach (char ch in content)
                if (char.IsPunctuation(ch))
                    punct[ch] = punct.TryGetValue(ch, out var c) ? c + 1 : 1;

            var tokens = Regex.Split(content.ToLowerInvariant(), @"[^a-zöçşığü0-9]+")
                              .Where(t => !string.IsNullOrWhiteSpace(t));

            var stop = new HashSet<string>(new[] { "ve", "ile", "ama", "ancak", "fakat", "veya", "ya", "de", "da", "ki", "mi", "mı", "mu", "mü", "için", "gibi" },
                                           StringComparer.OrdinalIgnoreCase);

            var filtered = tokens.Where(t => !stop.Contains(t) && !Regex.IsMatch(t, @"^\d+$"));

            var counts = filtered.GroupBy(w => w)
                                 .Select(g => (Word: g.Key, Count: g.Count()))
                                 .OrderByDescending(x => x.Count)
                                 .ThenBy(x => x.Word)
                                 .ToList();

            return (counts, punct);
        }
        private Task<string> ReadPdfAsync(string path)
        {
            return Task.Run(() =>
            {
                var sb = new StringBuilder();
                using (var pdf = PdfDocument.Open(path))
                {
                    int n = pdf.NumberOfPages;
                    for (int i = 1; i <= n; i++)
                    {
                        var page = pdf.GetPage(i);
                        sb.AppendLine(page.Text);
                    }
                }
                return sb.ToString();
            });
        }
        private Task<string> ReadDocxAsync(string path)
        {
            return Task.Run(() =>
            {
                using (WordprocessingDocument doc = WordprocessingDocument.Open(path, false))
                {
                    var body = doc.MainDocumentPart.Document.Body;
                    return body.InnerText ?? string.Empty;
                }
            });
        }
    }
}
