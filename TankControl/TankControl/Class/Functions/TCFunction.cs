using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TankControl.Class.Functions
{
    public static class TCFunction
    {
        public static void MessageBoxError(string message)
        {
            message += ", Print screen error dan kirim ke adiwena23@gmail.com";

            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void MessageBoxFail(string message)
        {
            message += ", Lihat setting atau hubungi teknisi";

            MessageBox.Show(message, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }
}
