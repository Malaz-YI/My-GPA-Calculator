using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyGPA_Calc
{
    public partial class mzTxtBox : TextBox
    {
        private Color _bottomBorderColor = Color.Black;
        private Color _onFocusColor = Color.Blue;


        public mzTxtBox()
        {
            BorderStyle = BorderStyle.None;
            AutoSize = false;

            // Add a label to the custom control
            Controls.Add(new Label
            {
                Height = 2,
                Dock = DockStyle.Bottom,
                BackColor = _bottomBorderColor
            }
            );
            InitializeComponent();
        }
        [Browsable(false)]
        public new BorderStyle BorderStyle
        {
            get { return base.BorderStyle; }
            set { base.BorderStyle = value; }
        }

        public Color BottomBorderColor
        {
            get { return _bottomBorderColor;}
            set
            {
                _bottomBorderColor = value;
                Controls[0].BackColor = _bottomBorderColor;
            }
        }

        public Color BottomBorderOnFocusColor
        {
            get { return _onFocusColor; }
            set { _onFocusColor = value; }
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }

        private void mzTxtBox_Enter(object sender, EventArgs e)
        {
            Controls[0].BackColor = _onFocusColor;
        }

        private void mzTxtBox_Leave(object sender, EventArgs e)
        {
            Controls[0].BackColor = _bottomBorderColor;
        }
    }
}
