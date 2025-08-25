using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Collections.Generic;

// Harici kütüphaneler
using UglyToad.PdfPig;
using DocumentFormat.OpenXml.Packaging;

// Projenin enum'u
using WinForms_FileAnalyzer.Enums;

namespace WinForms_FileAnalyzer.Models
{
    public static class FileReader
    {
        public static Task<string> ReadAsync(FileType type, string path)
        {
            switch (type)
            {
                case FileType.Txt:
                    return ReadTxtAsync(path);

                case FileType.Docx:
                    return ReadDocxAsync(path);

                case FileType.Pdf:
                    return ReadPdfAsync(path);

                default:
                    throw new NotSupportedException($"Desteklenmeyen tür: {type}");
            }
        }


        public static Task<string> ReadTxtAsync(string path)
            => Task.Run(() => File.ReadAllText(path, Encoding.UTF8));

        public static Task<string> ReadDocxAsync(string path)
            => Task.Run(() =>
            {
                using (var doc = WordprocessingDocument.Open(path, false))
                {
                    var body = doc.MainDocumentPart.Document.Body;
                    return body?.InnerText ?? string.Empty;
                }
            });

        public static Task<string> ReadPdfAsync(string path)
            => Task.Run(() =>
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
}
    