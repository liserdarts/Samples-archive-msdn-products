using System.Windows.Forms;

namespace Microsoft.SAPSK.ContosoTours.Helper
{
    public static class GridHelper
    {
        public static void ShowColumns(DataGridView grid, params string[] columns)
        {
            foreach (DataGridViewColumn column in grid.Columns)
            {
                column.Visible = false;
            }
            foreach (string col in columns)
            {
                grid.Columns[col].Visible = true;
            }
        }

        public static void HideColumns(DataGridView grid, params string[] columns)
        {
            foreach (DataGridViewColumn column in grid.Columns)
            {
                column.Visible = true;
            }
            foreach (string col in columns)
            {
                grid.Columns[col].Visible = false;
            }
        }

        public static void SetColumnTitle(DataGridView grid, string[] columns, string[] titles)
        {
            int i = 0;
            foreach (string col in columns)
            {
                grid.Columns[col].HeaderText = titles[i];
                i++;
            }
        }

        public static void SetWidthColumn(DataGridView grid, string[] columns, int[] width)
        {
            int i = 0;
            foreach (string col in columns)
            {
                grid.Columns[col].Width = width[i];
                i++;
            }
        }
    }
}
