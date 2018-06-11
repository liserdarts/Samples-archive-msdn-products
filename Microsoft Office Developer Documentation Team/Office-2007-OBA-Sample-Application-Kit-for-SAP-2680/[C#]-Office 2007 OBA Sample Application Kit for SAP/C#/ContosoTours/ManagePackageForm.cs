using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using Microsoft.Office.Tools;
using Microsoft.SAPSK.ContosoTours.DAL;
using Microsoft.SAPSK.ContosoTours.Helper;
using Microsoft.SAPSK.ContosoTours.Properties;

namespace Microsoft.SAPSK.ContosoTours
{
    public partial class ManagePackageForm : Form
    {
        private List<PackageList> _sourcePackage;

        //private bool _isCellClick = false;
        private int _packageCurrentIndex = -1;
        
        public ManagePackageForm()
        {
            InitializeComponent();
        }

        private void ManagePackage_Load(object sender, EventArgs e)
        {
            #region Populate Grid
            _sourcePackage =
                new List<PackageList>();
            
            List<PackageList> usedPackage = new List<PackageList>();
            List<PackageList> unUsedPackage = new List<PackageList>();

            SAPEventAttendeeReadWrite eventAttendeeRW =
                new SAPEventAttendeeReadWrite(Config._dbConnectionName);

            SAPPackageReadWrite packageRW =
                new SAPPackageReadWrite(Config._dbConnectionName);

            using (SAPDataReaderPackage rdrPackage =
                packageRW.ReaderSelectAll())
            {
                if (rdrPackage.DataReader != null &&
                    rdrPackage.DataReader.HasRows)
                {
                    SAPDataSetEventAttendee.EventAttendeeDataTable dtEventAttendee =
                        eventAttendeeRW.SelectAll().EventAttendee;
                    while (rdrPackage.DataReader.Read())
                    {
                        PackageList item = new PackageList();
                        item.PackageName = rdrPackage.PackageName;
                        item.PackageDescription = rdrPackage.PackageDescription;
                        item.PackagePhoto = rdrPackage.PackageImage;
                        item.PackageID = rdrPackage.PackageID;

                        DataRow[] rows =
                            dtEventAttendee.Select("packageid = " + item.PackageID.ToString());

                        if (rows != null && rows.Length > 0)
                        {
                            item.PackageTag = true;
                            usedPackage.Add(item);
                        }
                        else
                        {
                            item.PackageTag = false;
                            unUsedPackage.Add(item);
                        }

                    } //while (rdrPackage.DataReader.Read());

                    _sourcePackage.AddRange(unUsedPackage);
                    _sourcePackage.AddRange(usedPackage);
                }
            }

            //SAPEventAttendeeReadWrite

            #endregion

            #region Create Data Grid
            dataGridViewPackage.VirtualMode = true;

            DataGridViewTextBoxColumn columnName =
                new DataGridViewTextBoxColumn();
            columnName.HeaderText = "Name";
            columnName.Name = "ColumnName";
            columnName.Width = 100;
            dataGridViewPackage.Columns.Add(columnName);

            DataGridViewTextBoxColumn columnDesc =
                new DataGridViewTextBoxColumn();
            columnDesc.HeaderText = "Description";
            columnDesc.Name = "ColumnDescription";
            columnDesc.Width = 150;
            columnDesc.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewPackage.Columns.Add(columnDesc);

            //create icon edit column
            DataGridViewImageColumn editColumn =
                new DataGridViewImageColumn();
            editColumn.Image = Resources.icoedit;
            editColumn.Width = 25;
            editColumn.Name = "ColumnEdit";
            editColumn.HeaderText = string.Empty;
            editColumn.SortMode = DataGridViewColumnSortMode.NotSortable;

            //create icond delete column
            DataGridViewImageColumn deleteColumn = new DataGridViewImageColumn();
            deleteColumn.Image = Resources.icodelete;
            deleteColumn.Width = 25;
            deleteColumn.Name = "ColumnDelete";
            deleteColumn.HeaderText = string.Empty;
            deleteColumn.SortMode = DataGridViewColumnSortMode.NotSortable;

            dataGridViewPackage.Columns.Insert(2, editColumn);
            editColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            
            dataGridViewPackage.Columns.Insert(3, deleteColumn);
            deleteColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            
            #endregion

            dataGridViewPackage.RowCount = _sourcePackage.Count;

            pictureBoxNewPackage.Image = Resources.iconew;

            //set first image on poster panel
            if (_sourcePackage.Count > 0)
            {
                pictureBoxPoster.Image = 
                    UtilityHelper.ByteToImage(_sourcePackage[0].PackagePhoto);                
            }
        }

        private void pictureBoxNewPackage_Click(object sender, EventArgs e)
        {
            
        }

        private void dataGridViewPackage_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //_isCellClick = true;
            _packageCurrentIndex=dataGridViewPackageCommand(e);
        }

        private void dataGridViewPackage_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            if (_sourcePackage.Count == 0)
            {
                return;
            }

            PackageList currentPackage =
                _sourcePackage[e.RowIndex];

            switch (dataGridViewPackage.Columns[e.ColumnIndex].Name)
            {
                case "ColumnName":
                    e.Value = currentPackage.PackageName;
                    break;

                case "ColumnDescription":
                    e.Value = currentPackage.PackageDescription;
                    break;

                case "ColumnPoster":
                    e.Value = currentPackage.PackagePhoto;
                    break;

                case "ColumnEdit":
                    if (currentPackage.PackageTag)
                    {
                        e.Value = Resources.icoedit_disabled;
                    }
                    else
                    {
                        e.Value = Resources.icoedit;
                    }
                    break;
                case "ColumnDelete":
                    if (currentPackage.PackageTag)
                    {
                        e.Value = Resources.icodelete_disabled;
                    }
                    else
                    {
                        e.Value = Resources.icodelete;
                    }
                    break;

            }
        }

        private void dataGridViewPackage_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            /*
            switch (dataGridViewPackage.Columns[e.ColumnIndex].Name)
            {
                case "ColumnEdit":
                    dataGridViewPackage.Cursor = Cursors.Hand;
                    //panelTipEdit.Visible = true;
                    break;
                case "ColumnDelete":
                    dataGridViewPackage.Cursor = Cursors.Hand;
                    break;                
            }
            */
        }

        private void dataGridViewPackage_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            /*
            switch (dataGridViewPackage.Columns[e.ColumnIndex].Name)
            {
                case "ColumnEdit":
                    dataGridViewPackage.Cursor = Cursors.Default;
                    break;
                case "ColumnDelete":
                    dataGridViewPackage.Cursor = Cursors.Default;
                    break;
            }
            */
        }

        private int dataGridViewPackageCommand(DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                /*
                switch (e.ColumnIndex)
                {
                   case 0: //edit column
                        if (!_isCellClick)
                        {
                            ShowPoster(e.RowIndex);
                            return;
                        }
                        PackageList currentPackage =
                            _sourcePackage[e.RowIndex];

                        PackageForm newPackageForm = new PackageForm();

                        if (currentPackage.PackageTag)
                        {
                            newPackageForm._isReadOnly = true;
                            //return;
                        }

                        newPackageForm._packageID = currentPackage.PackageID;
                        if (newPackageForm.ShowDialog(this) == DialogResult.OK)
                        {
                            if (newPackageForm._packageItem != null)
                            {
                                _sourcePackage[e.RowIndex] = newPackageForm._packageItem;
                            }
                        }
                        newPackageForm.Dispose();
                        break;
                    case 1:
                        if (!_isCellClick)
                        {
                            ShowPoster(e.RowIndex);
                            return;
                        }
                        if (_sourcePackage.Count > 0)
                        {
                            PackageList deletedPackage =
                                _sourcePackage[e.RowIndex];

                            if (deletedPackage.PackageTag)
                            {
                                return;
                            }

                            if (Message.DeleteMessage(deletedPackage.PackageName)
                                == DialogResult.Yes)
                            {
                                SAPPackageEventMapReadWrite eventMapRW =
                                    new SAPPackageEventMapReadWrite(Config._dbConnectionName);

                                SAPDataSetPackageEventMap ds =
                                    eventMapRW.SelectByPackageID(deletedPackage.PackageID);

                                foreach (SAPDataSetPackageEventMap.PackageEventMapRow row in ds.PackageEventMap.Rows)
                                {
                                    eventMapRW.Delete(row.PackageEventMapID);
                                }

                                SAPPackageReadWrite packageRW =
                                    new SAPPackageReadWrite(Config._dbConnectionName);

                                packageRW.Delete(deletedPackage.PackageID);
                                _sourcePackage.RemoveAt(e.RowIndex);
                                dataGridViewPackage.RowCount = _sourcePackage.Count;
                            }
                        }
                        break;
                    default:
                        ShowPoster(e.RowIndex);
                        break;
                }*/
                ShowPoster(e.RowIndex);
                return e.RowIndex;
            }
            return - 1;
        }

        private void ShowPoster(int rowIndex)
        {
            if (_sourcePackage.Count > 0 && rowIndex > -1)
            {
                pictureBoxPoster.Image =
                    UtilityHelper.ByteToImage(_sourcePackage[rowIndex].PackagePhoto);
            }
        }

        private void dataGridViewPackage_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            //_isCellClick = false;
            _packageCurrentIndex=dataGridViewPackageCommand(e);
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonNew_Click(object sender, EventArgs e)
        {
            PackageForm newPackageForm = new PackageForm();
            if (newPackageForm.ShowDialog(this) == DialogResult.OK)
            {
                if (newPackageForm._packageItem != null)
                {
                    _sourcePackage.Add(newPackageForm._packageItem);
                    dataGridViewPackage.RowCount = _sourcePackage.Count;
                }
            }
            newPackageForm.Dispose();
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (_packageCurrentIndex >= 0)
            {
                PackageList currentPackage =
                                           _sourcePackage[_packageCurrentIndex];

                PackageForm newPackageForm = new PackageForm();

                if (currentPackage.PackageTag)
                {
                    newPackageForm._isReadOnly = true;
                    //return;
                }

                newPackageForm._packageID = currentPackage.PackageID;
                if (newPackageForm.ShowDialog(this) == DialogResult.OK)
                {
                    if (newPackageForm._packageItem != null)
                    {
                        _sourcePackage[_packageCurrentIndex] = newPackageForm._packageItem;
                    }
                }
                newPackageForm.Dispose();
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (_sourcePackage.Count > 0)
            {
                PackageList deletedPackage =
                    _sourcePackage[_packageCurrentIndex];

                if (deletedPackage.PackageTag)
                {
                    return;
                }

                if (Message.DeleteMessage(deletedPackage.PackageName)
                    == DialogResult.Yes)
                {
                    SAPPackageEventMapReadWrite eventMapRW =
                        new SAPPackageEventMapReadWrite(Config._dbConnectionName);

                    SAPDataSetPackageEventMap ds =
                        eventMapRW.SelectByPackageID(deletedPackage.PackageID);

                    foreach (SAPDataSetPackageEventMap.PackageEventMapRow row in ds.PackageEventMap.Rows)
                    {
                        eventMapRW.Delete(row.PackageEventMapID);
                    }

                    SAPPackageReadWrite packageRW =
                        new SAPPackageReadWrite(Config._dbConnectionName);

                    packageRW.Delete(deletedPackage.PackageID);
                    _sourcePackage.RemoveAt(_packageCurrentIndex);
                    dataGridViewPackage.RowCount = _sourcePackage.Count;
                }
            }

        }

    }
}