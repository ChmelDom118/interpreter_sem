namespace InterpreterGUI
{
    public partial class Form1 : Form
    {
        private static Dictionary<string, Color> keywords = new Dictionary<string, Color>()
        {
            { "declare", Color.DarkOrange },
            { "begin", Color.DarkOrange },
            { "end", Color.DarkOrange },
            { "while", Color.DarkOrange },
            { "do", Color.DarkOrange },
            { "repeat", Color.DarkOrange },
            { "until", Color.DarkOrange },
            { "for", Color.DarkOrange },
            { "if", Color.DarkOrange },
            { "then", Color.DarkOrange },
            { "elseif", Color.DarkOrange },
            { "else", Color.DarkOrange },
            { "function", Color.DarkOrange },
            { "return", Color.DarkOrange },
            { "execute", Color.DarkOrange },
            { "integer", Color.DodgerBlue },
            { "double", Color.DodgerBlue },
            { "string", Color.DodgerBlue },
            { "boolean", Color.DodgerBlue },
            { "true", Color.Olive },
            { "false", Color.Olive },
        };

        public Form1()
        {
            InitializeComponent();

            richTextBoxCode.KeyUp += RichTextBoxCode_KeyUp;

            foreach (ToolStripItem item in fileToolStripMenuItem.DropDownItems)
            {   
                item.ForeColor = Color.FromArgb(64, 64, 64);
                item.BackColor = Color.FromArgb(240, 240, 240);
            }

            fileToolStripMenuItem.DropDownOpened += FileToolStripMenuItem_DropDownOpened;
            fileToolStripMenuItem.DropDownClosed += FileToolStripMenuItem_DropDownClosed;
        }

        private void RichTextBoxCode_KeyUp(object? sender, KeyEventArgs e)
        {
            int selectionStart = richTextBoxCode.SelectionStart;
            richTextBoxCode.Rtf = Highlight(richTextBoxCode.Text, richTextBoxCode.Font);
            richTextBoxCode.SelectionStart = selectionStart;
        }

        private void FileToolStripMenuItem_DropDownOpened(object? sender, EventArgs e)
        {
            fileToolStripMenuItem.ForeColor = Color.FromArgb(64, 64, 64);
        }

        private void FileToolStripMenuItem_DropDownClosed(object? sender, EventArgs e)
        {
            fileToolStripMenuItem.ForeColor = Color.FromArgb(240, 240, 240);
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog.ShowDialog();

            richTextBoxCode.Text = File.ReadAllText(openFileDialog.FileName);
            int selectionStart = richTextBoxCode.SelectionStart;
            richTextBoxCode.Rtf = Highlight(richTextBoxCode.Text, richTextBoxCode.Font);
            richTextBoxCode.SelectionStart = selectionStart;
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog.ShowDialog();
            File.WriteAllText(openFileDialog.FileName, richTextBoxCode.Text);
        }

        private void RunToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBoxOutput.Text = "Run";
        }

        private void RichTextBoxCode_TextChanged(object sender, EventArgs e)
        {

        }

        private string Highlight(string text, Font font)
        {
            RichTextBox newRichTextBox = new RichTextBox();

            newRichTextBox.Text = text;
            newRichTextBox.Font = font;
            newRichTextBox.ForeColor = Color.FromArgb(240, 240, 240);

            foreach (var entry in keywords)
            {
                int pointer = 0;
                while ((pointer = newRichTextBox.Find(entry.Key, pointer, RichTextBoxFinds.WholeWord)) != -1)
                {
                    newRichTextBox.Select(pointer, entry.Key.Length);
                    newRichTextBox.SelectionFont = new Font(font.FontFamily, font.Size, FontStyle.Bold);
                    newRichTextBox.SelectionColor = entry.Value;
                    pointer += 1;
                }
            }

            return newRichTextBox.Rtf;
        }
    }
}