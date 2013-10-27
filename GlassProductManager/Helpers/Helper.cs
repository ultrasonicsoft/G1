using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GlassProductManager
{
    internal class Helper
    {
        internal static void ShowInformationMessageBox(string message, string caption = null)
        {
            caption = caption ?? "Glass Product Manager";
            MessageBox.Show(message, caption, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        internal static void ShowErrorMessageBox(string message, string caption = null)
        {
            caption = caption ?? "Glass Product Manager";
            MessageBox.Show(message, caption, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        internal static MessageBoxResult ShowQuestionMessageBox(string message, string caption = null, MessageBoxButton buttons = MessageBoxButton.YesNoCancel)
        {
            caption = caption ?? "Glass Product Manager";
            return MessageBox.Show(message, caption, buttons, MessageBoxImage.Question);
        }
    }
}
