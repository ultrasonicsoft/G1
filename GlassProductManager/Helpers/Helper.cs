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
                SetToDefaultStyle(input);
                result = true;
            }
            else
            {
                Style textBoxErrorStyle;
                FrameworkElement frameworkElement;
                frameworkElement = new FrameworkElement();
                //textBoxNormalStyle = (Style)frameworkElement.TryFindResource("textBoxNormalStyle");
                textBoxErrorStyle = (Style)frameworkElement.TryFindResource("textBoxNumericErrorStyle");
                input.Style = textBoxErrorStyle;
            }
            return result;
        }

        private static void SetToDefaultStyle(TextBox input)
        {
            Style txtNormalStyle;
            FrameworkElement frameworkElement;
            frameworkElement = new FrameworkElement();
            txtNormalStyle = (Style)frameworkElement.TryFindResource("DefaultTextBox");
            input.Style = txtNormalStyle;
        }

        internal static bool IsNonEmpty(TextBox input)
        {
            bool result = false;
            if (false == string.IsNullOrEmpty(input.Text))
            {
                SetToDefaultStyle(input);

                result = true;
            }
            else
            {
                Style textBoxErrorStyle;
                FrameworkElement frameworkElement;
                frameworkElement = new FrameworkElement();
                textBoxErrorStyle = (Style)frameworkElement.TryFindResource("textBoxEmptyErrorStyle");
                input.Style = textBoxErrorStyle;
            }
            return result;
        }

        internal static bool IsValidPhone(TextBox input)
        {
            bool result = false;

            if (input.Text == string.Empty)
            {
                SetToDefaultStyle(input);
                return true;
            }
            if (Regex.IsMatch(input.Text, @"^[01]?[- .]?(\([2-9]\d{2}\)|[2-9]\d{2})[- .]?\d{3}[- .]?\d{4}$"))
                //if (Regex.IsMatch(input.Text, @"/^\(?(\d{3})\)?[- ]?(\d{3})[- ]?(\d{4})$/"))
            {
                SetToDefaultStyle(input);
                result = true;
            }
            else
            {
                Style textBoxErrorStyle;
                FrameworkElement frameworkElement;
                frameworkElement = new FrameworkElement();
                textBoxErrorStyle = (Style)frameworkElement.TryFindResource("textBoxPhoneErrorStyle");
                input.Style = textBoxErrorStyle;
            }
            return result;
        }

        internal static bool IsValidEmail(TextBox input)
        {
            bool result = false;
            if (input.Text == string.Empty)
            {
                SetToDefaultStyle(input);
                return true;
            }

            if (Regex.IsMatch(input.Text, @"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$"))
            {
                SetToDefaultStyle(input);
                result = true;
            }
            else
            {
                Style textBoxErrorStyle;
                FrameworkElement frameworkElement;
                frameworkElement = new FrameworkElement();
                textBoxErrorStyle = (Style)frameworkElement.TryFindResource("textBoxEmailErrorStyle");
                input.Style = textBoxErrorStyle;
            }
            return result;
        }

        internal static bool IsValidCurrency(TextBox input)
        {
            bool result = false;

            if (input.Text == "0.00")
            {
                SetToDefaultStyle(input);
                return true;
            }

            if (false == string.IsNullOrEmpty(input.Text) && Regex.IsMatch(input.Text, @"^[0-9]*(?:\.[0-9]*)?$"))
            {
                SetToDefaultStyle(input);
                
                result = true;
            }
            else
            {
                Style textBoxErrorStyle;
                FrameworkElement frameworkElement;
                frameworkElement = new FrameworkElement();
                textBoxErrorStyle = (Style)frameworkElement.TryFindResource("textBoxCurrencyErrorStyle");
                input.Style = textBoxErrorStyle;
            }
            return result;
        }

        internal static bool IsValidCurrency(string input)
        {
            bool result = false;

            if (input == "0.00")
            {
                return true;
            }

            if (false == string.IsNullOrEmpty(input) && Regex.IsMatch(input, @"^[0-9]*(?:\.[0-9]*)?$"))
            {
                result = true;
            }
            return result;
        }

    }
}
