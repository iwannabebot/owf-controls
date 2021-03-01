using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OWFControls.Demo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DataTable unboundGridSource = new DataTable();
            unboundGridSource.Columns.Add("Selected", typeof(bool));
            unboundGridSource.Columns.Add("Dosage", typeof(int));
            unboundGridSource.Columns.Add("Drug", typeof(string));
            unboundGridSource.Columns.Add("Diagnosis", typeof(string));
            unboundGridSource.Columns.Add("Date", typeof(DateTime));
            unboundGridSource.Rows.Add(false, 25, "Drug A", "Disease A", DateTime.Now);
            unboundGridSource.Rows.Add(false, 50, "Drug Z", "Problem Z", DateTime.Now);
            unboundGridSource.Rows.Add(false, 10, "Drug Q", "Disorder Q", DateTime.Now);
            unboundGridSource.Rows.Add(true, 21, "Medicine A", "Diagnosis A", DateTime.Now);
            owfGrid1.DataSource = unboundGridSource;
        }
    }
}
