using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

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

        internal static bool IsNumberOnly(TextBox input)
        {
            bool result = false;
            if (Regex.IsMatch(input.Text, @"^\d+$"))
            {
                input.Style = null;
                result = true;
            }
            else
            {
                Style textBoxErrorStyle;
                FrameworkElement frameworkElement;
                frameworkElement = new FrameworkElement();
                //textBoxNormalStyle = (Style)frameworkElement.TryFindResource("textBoxNormalStyle");
                textBoxErrorStyle = (Style)frameworkElement.TryFindResource("textBoxErrorStyle");
                input.Style = textBoxErrorStyle;
            }
            return result;
        }
    }
}
