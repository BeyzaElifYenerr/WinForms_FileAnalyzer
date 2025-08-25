using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Extensions.Logging;
using WinForms_FileAnalyzer.Enums;
using WinForms_FileAnalyzer.Models;

namespace WinForms_FileAnalyzer
{
    public partial class MainForm : Form
    {
        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {
          
        }

        private void topPanel_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void btnExportJson_Click(object sender, EventArgs e)
        {
            ExportWordsToJson();
        }


        public MainForm()
        {
            InitializeComponent();
        }

        private class AnalysisResult
        {
            public List<(string Word, int Count)> topWords { get; set; } = new List<(string Word, int Count)>();
            public Dictionary<char, int> punct { get; set; } = new Dictionary<char, int>();

            public int uniqueWordCount { get; set; } = 0;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            cboFileType.Items.AddRange(new object[] { ".txt", ".docx", ".pdf" });
            btnLoad.Enabled = false;
            progressBar.Minimum = 0;
            progressBar.Maximum = 100;
            statusLabel.Text = "Hazır";
        }

        private void ExportWordsToJson()
        {
            List<WordCount> wordList = new List<WordCount>();

            foreach (DataGridViewRow row in dgvWords.Rows)
            {
                if (row.IsNewRow) continue;

                string word = row.Cells[0].Value?.ToString();
                int count = int.TryParse(row.Cells[1].Value?.ToString(), out int c) ? c : 0;

                wordList.Add(new WordCount { Word = word, Count = count });
            }

            // Dosya seç
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "JSON Files (*.json)|*.json";
                sfd.FileName = "word_counts.json";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    string json = JsonConvert.SerializeObject(wordList, Formatting.Indented);
                    File.WriteAllText(sfd.FileName, json);

                    MessageBox.Show("Veriler JSON olarak kaydedildi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void cboFileType_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnLoad.Enabled = cboFileType.SelectedItem != null;
        }

        private async void btnLoad_Click(object sender, EventArgs e)
        {
            if (cboFileType.SelectedItem == null) return;
            string ext = cboFileType.SelectedItem.ToString();

            using (OpenFileDialog ofd = new OpenFileDialog())
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

                    progressBar.Value = 20;

                    FileType fileType;
                    switch (ext)
                    {
                        case ".txt":
                            fileType = FileType.Txt;
                            break;
                        case ".docx":
                            fileType = FileType.Docx;
                            break;
                        case ".pdf":
                            fileType = FileType.Pdf;
                            break;
                        default:
                            throw new NotSupportedException("Bu uzantı desteklenmiyor.");
                    }

                    string content = await FileReader.ReadAsync(fileType, ofd.FileName);

                    progressBar.Value = 60;
                    statusLabel.Text = "Analiz ediliyor...";

                    var result = AnalyzeText(content);
                    progressBar.Value = 90;

                    dgvWords.DataSource = result.topWords
                        .Select(x => new { Word = x.Word, Count = x.Count })
                        .ToList();

                    dgvPunct.DataSource = result.punct
                        .Select(x => new { Punctuation = x.Key.ToString(), Count = x.Value })
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
                }
                finally
                {
                    await Task.Delay(300);
                    progressBar.Value = 0;
                }
            }


            (List<(string Word, int Count)> topWords, Dictionary<char, int> punct) AnalyzeText(string content)
            {
                var punct = new Dictionary<char, int>();
                foreach (char ch in content)
                    if (char.IsPunctuation(ch))
                        punct[ch] = punct.TryGetValue(ch, out var c) ? c + 1 : 1;

                var tokens = Regex.Split(content.ToLowerInvariant(), @"[^a-zöçşığü0-9]+")
                                  .Where(t => !string.IsNullOrWhiteSpace(t));

                var stop = new HashSet<string>(new[] {
                "ve", "ile", "ama", "ancak", "fakat", "veya", "ya", "de", "da", "ki", "mi", "mı", "mu", "mü", "için", "gibi"
            }, StringComparer.OrdinalIgnoreCase);

                var filtered = tokens.Where(t => !stop.Contains(t) && !Regex.IsMatch(t, @"^\d+$"));

                var counts = filtered.GroupBy(w => w)
                                     .Select(g => (Word: g.Key, Count: g.Count()))
                                     .OrderByDescending(x => x.Count)
                                     .ThenBy(x => x.Word)
                                     .ToList();

                return (counts, punct);
            }
        }
    }
}
