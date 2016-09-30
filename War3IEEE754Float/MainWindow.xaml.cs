using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace War3IEEE754Float
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        bool enableEvent = true;

        private void TextBoxHexValue_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (enableEvent)
            {
                try
                {
                    if (TextBoxHexValue.Text.Count() == 8)
                    {
                        UInt32 hexValue = Convert.ToUInt32(TextBoxHexValue.Text, 16);
                        //uint signValue = hexValue >> 31;
                        //uint exponentValue = (hexValue & 0x7F800000) >> 23;
                        //uint mantissaValue = (hexValue & 0x7FFFFF);
                        //double floatValue = 0;
                        //int count = 0;
                        //uint position = 0x400000;
                        //while (mantissaValue != 0)
                        //{
                        //    count--;
                        //    if ((mantissaValue & position) != 0)
                        //    {
                        //        floatValue += Math.Pow(2, count);
                        //    }
                        //    mantissaValue = mantissaValue & (~position);
                        //    position = position >> 1;
                        //}
                        //floatValue = Math.Pow(-1, signValue) * (1 + floatValue) * Math.Pow(2, (exponentValue - 127));
                        float floatValue = BitConverter.ToSingle(BitConverter.GetBytes(hexValue), 0);
                        enableEvent = false;
                        TextBoxFloatValue.Text = floatValue.ToString();
                        enableEvent = true;
                    }
                }
                catch
                {
                    enableEvent = false;
                    TextBoxFloatValue.Text = "";
                    enableEvent = true;
                }
            }
            e.Handled = true;
        }

        private void TextBoxFloatValue_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (enableEvent)
            {
                try
                {
                    float floatValue = (float) Convert.ToDouble(TextBoxFloatValue.Text);
                    UInt32 hexValue = BitConverter.ToUInt32(BitConverter.GetBytes(floatValue), 0);
                    enableEvent = false;
                    TextBoxHexValue.Text = String.Format("{0:X}", hexValue);
                    enableEvent = true;
                }
                catch
                {
                    enableEvent = false;
                    TextBoxHexValue.Text = "";
                    enableEvent = true;
                }
            }
            e.Handled = true;
        }
        
        private void TextBoxOriginText_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            TextBoxOriginText.Text = "";
        }

        private void TextBoxOriginText_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            List<string> codeList = TextBoxOriginText.Text.Split('\n').ToList();
            string newCode = "";
            foreach (string select in codeList)
            {
                if (select.Count() > 11) newCode += select.Substring(11);
            }
            Clipboard.SetText(newCode);
        }
    }
}
