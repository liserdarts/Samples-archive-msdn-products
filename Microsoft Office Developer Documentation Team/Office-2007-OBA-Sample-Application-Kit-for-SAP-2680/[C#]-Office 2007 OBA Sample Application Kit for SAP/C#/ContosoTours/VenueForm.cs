using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Microsoft.SAPSK.ContosoTours.DAL;
using Microsoft.SAPSK.ContosoTours.Helper;
using Microsoft.SAPSK.ContosoTours.Properties;

namespace Microsoft.SAPSK.ContosoTours
{
    public partial class VenueForm : Form
    {
        enum EnumFormMode
        {
            AddMode,
            EditMode,
            CallbackAdd,
            ViewMode
        }

        EnumFormMode mode;

        private string _fileVenueImage = string.Empty;
        private string _fileFacilityImage = string.Empty;
        private string _fileLocationMapImage = string.Empty;
        private byte[] _venueImage = null;
        private byte[] _facilityImage = null;
        private byte[] _locationMapImage = null;

        private DataTable _dtVenue;

        public int _venueID = 0;
        public string _venueName = string.Empty;
        public bool _isChildForm = false;

        private int _currentVenueIndex = -1;
        private int _rowIndex = 0;

        private bool _isCellClick = false;

        public VenueForm()
        {
            InitializeComponent();
        }

        private void VenueForm_Load(object sender, EventArgs e)
        {
            #region create grid
            SAPEventReadWrite eventRW =
                new SAPEventReadWrite(Config._dbConnectionName);
            SAPVenueReadWrite venueRW =
                new SAPVenueReadWrite(Config._dbConnectionName);
            _dtVenue = new DataTable();
            //_dtVenue.Columns.Add("EditIco", typeof(byte[]));
            //_dtVenue.Columns.Add("DelIco", typeof(byte[]));
            _dtVenue.Columns.Add("VenueID", typeof(int));
            _dtVenue.Columns.Add("VenueName",typeof(string));
            _dtVenue.Columns.Add("VenueDescription", typeof(string));
            _dtVenue.Columns.Add("VenueStreet", typeof(string));
            _dtVenue.Columns.Add("VenueCity", typeof(string));
            _dtVenue.Columns.Add("VenueState", typeof(string));
            _dtVenue.Columns.Add("VenuePostalCode", typeof(string));
            _dtVenue.Columns.Add("VenueGeographicMap", typeof(byte[]));
            _dtVenue.Columns.Add("VenueFacilityMap", typeof(byte[]));
            _dtVenue.Columns.Add("VenueImage", typeof(byte[]));
            _dtVenue.Columns.Add("EditTag", typeof(bool));
            _dtVenue.Columns.Add("DelTag", typeof(bool));

            //byte[] editIco = UtilityHelper.BitmapToByte(Resources.icoedit);
            //byte[] delIco = UtilityHelper.BitmapToByte(Resources.icodelete);
            //byte[] editDisableIco = UtilityHelper.BitmapToByte(Resources.icoedit_disabled);
            //byte[] delDisableIco = UtilityHelper.BitmapToByte(Resources.icodelete_disabled);

            bool editAble = false;
            bool delAble = false;

            List<int> usedVenue = new List<int>();
            using (SAPDataReaderEvent rdrEvent =
                eventRW.ReaderSelectAll())
            {
                if (rdrEvent.DataReader != null &&
                    rdrEvent.DataReader.HasRows)
                {
                    while (rdrEvent.DataReader.Read())
                    {
                        usedVenue.Add(rdrEvent.VenueID);
                    } //while (rdrEvent.DataReader.Read());
                }
            }

            using (SAPDataReaderVenue rdrVenue =
                venueRW.ReaderSelectAll())
            {
                if (rdrVenue.DataReader != null &&
                    rdrVenue.DataReader.HasRows)
                {
                    while (rdrVenue.DataReader.Read())
                    {
                        //byte[] editImage = editIco;
                        //byte[] delImage = delIco;


                        bool tag = usedVenue.Contains(rdrVenue.VenueID);
                        delAble = !tag;
                        editAble = !tag;
                       
                        _dtVenue.Rows.Add(
                            //editImage,
                            //delImage,
                            rdrVenue.VenueID,
                            rdrVenue.VenueName,
                            rdrVenue.VenueDescription,
                            rdrVenue.VenueStreet,
                            rdrVenue.VenueCity,
                            rdrVenue.VenueState,
                            rdrVenue.VenuePostalCode,
                            rdrVenue.VenueGeographicMap,
                            rdrVenue.VenueFacilityMap,
                            rdrVenue.VenueImage,
                            editAble,
                            delAble);

                    } //while (rdrVenue.DataReader.Read());
                }
            }

            dataGridViewVenue.DataSource = _dtVenue;
            #endregion

            #region utilize grid
            GridHelper.HideColumns(
                dataGridViewVenue,
                SAPVenueReadWrite._venueIDColumnName,
                SAPVenueReadWrite._venueDescriptionColumnName,
                SAPVenueReadWrite._venueGeographicMapColumnName,
                SAPVenueReadWrite._venueFacilityMapColumnName,
                SAPVenueReadWrite._venueImageColumnName,
                "EditTag",
                "DelTag");
            string[] columns = new string[]
            {
                //"EditIco",
                //"DelIco",
                SAPVenueReadWrite._venueNameColumnName,
                SAPVenueReadWrite._venueStreetColumnName,
                SAPVenueReadWrite._venueCityColumnName,
                SAPVenueReadWrite._venueStateColumnName,
                SAPVenueReadWrite._venuePostalCodeColumnName
            };
            string[] titles = new string[]
            {
                //"",
                //"",
                "Venue",
                "Street",
                "City",
                "State",
                "Postal Code"
            };
            int[] gridWidth = new int[] 
            {
                //20,
                //20,
                200,
                100,
                100,
                100,
                100
            };
            GridHelper.SetColumnTitle(
                dataGridViewVenue,
                columns,
                titles);
            GridHelper.SetWidthColumn(
                dataGridViewVenue,
                columns,
                gridWidth);
            #endregion

            mode = EnumFormMode.ViewMode;
            UtilityHelper.SetToReadOnly(Controls);

            if (_isChildForm)
            {
                mode = EnumFormMode.CallbackAdd;
                buttonAddSave.Text = "Save";
                buttonCancel.Text = "Cancel";
                dataGridViewVenue.Enabled = false;
                UtilityHelper.ClearTextbox(Controls);
                UtilityHelper.EnableControls(Controls);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            if (mode == EnumFormMode.CallbackAdd)
            {
                DialogResult = DialogResult.Cancel;
                Close();
            }
            if (mode != EnumFormMode.ViewMode)
            {
                mode = EnumFormMode.ViewMode;
                buttonCancel.Text = "Close";
                buttonSave.Visible = false;
                if (_rowIndex > 0)
                {
                    FillTabContent(_rowIndex);
                }
                else if (_dtVenue.Rows.Count > 0)
                {
                    //default to 0
                    FillTabContent(0);
                }
                else
                {
                    UtilityHelper.ClearTextbox(Controls);
                    ClearImages();
                }
                UtilityHelper.SetToReadOnly(Controls);
                dataGridViewVenue.Enabled = true;
            }
            else
            {
                Close();
            }
        }

        private void buttonAddSave_Click(object sender, EventArgs e)
        {
            if (mode == EnumFormMode.ViewMode)
            {
                mode = EnumFormMode.AddMode;
                buttonAddSave.Text = "Save";
                buttonCancel.Text = "Cancel";
                dataGridViewVenue.Enabled = false;
                UtilityHelper.ClearTextbox(Controls);
                UtilityHelper.EnableControls(Controls);
            }
            else
            {
                if (!IsValid())
                {
                    return;
                }

                SAPVenueReadWrite venueRW =
                    new SAPVenueReadWrite(Config._dbConnectionName);

                if (mode == EnumFormMode.EditMode)
                {
                    #region modify record
                    _locationMapImage = FileToByte(_fileLocationMapImage, _locationMapImage);
                    _facilityImage = FileToByte(_fileFacilityImage, _facilityImage);
                    _venueImage = FileToByte(_fileVenueImage, _venueImage);
                    venueRW.Update(
                        _venueID,
                        textBoxName.Text.Trim(),
                        textBoxDescription.Text.Trim(),
                        textBoxStreet.Text.Trim(),
                        textBoxCity.Text.Trim(),
                        textBoxState.Text.Trim(),
                        textBoxPostalCode.Text.Trim(),
                        _locationMapImage,
                        _facilityImage,
                        _venueImage);
                    _dtVenue.Rows[_rowIndex][SAPVenueReadWrite._venueNameColumnName] =
                        textBoxName.Text.Trim();
                    _dtVenue.Rows[_rowIndex][SAPVenueReadWrite._venueDescriptionColumnName] =
                        textBoxDescription.Text.Trim();
                    _dtVenue.Rows[_rowIndex][SAPVenueReadWrite._venueStreetColumnName] =
                        textBoxStreet.Text.Trim();
                    _dtVenue.Rows[_rowIndex][SAPVenueReadWrite._venueCityColumnName] =
                        textBoxCity.Text.Trim();
                    _dtVenue.Rows[_rowIndex][SAPVenueReadWrite._venueStateColumnName] =
                        textBoxState.Text.Trim();
                    _dtVenue.Rows[_rowIndex][SAPVenueReadWrite._venuePostalCodeColumnName] =
                        textBoxPostalCode.Text.Trim();
                    _dtVenue.Rows[_rowIndex][SAPVenueReadWrite._venueGeographicMapColumnName] =
                        _locationMapImage;
                    _dtVenue.Rows[_rowIndex][SAPVenueReadWrite._venueImageColumnName] =
                        _facilityImage;
                    _dtVenue.Rows[_rowIndex][SAPVenueReadWrite._venueFacilityMapColumnName] =
                        _venueImage;
                    #endregion
                }
                else
                {
                    #region insert record
                    _locationMapImage = FileToByte(_fileLocationMapImage, null);
                    _facilityImage = FileToByte(_fileFacilityImage, null);
                    _venueImage = FileToByte(_fileVenueImage, null);
                    venueRW.Insert(
                        textBoxName.Text.Trim(),
                        textBoxDescription.Text.Trim(),
                        textBoxStreet.Text.Trim(),
                        textBoxCity.Text.Trim(),
                        textBoxState.Text.Trim(),
                        textBoxPostalCode.Text.Trim(),
                        _locationMapImage,
                        _facilityImage,
                        _venueImage,
                        out _venueID);

                    if (mode == EnumFormMode.CallbackAdd)
                    {
                        _venueName = textBoxName.Text.Trim();
                        DialogResult = DialogResult.OK;
                        Close();
                        return;
                    }
                    else
                    {
                        byte[] editIco = UtilityHelper.BitmapToByte(Resources.icoedit);
                        byte[] delIco = UtilityHelper.BitmapToByte(Resources.icodelete);

                        _dtVenue.Rows.Add(
                            editIco,
                            delIco,
                            _venueID,
                            textBoxName.Text.Trim(),
                            textBoxDescription.Text.Trim(),
                            textBoxStreet.Text.Trim(),
                            textBoxCity.Text.Trim(),
                            textBoxState.Text.Trim(),
                            textBoxPostalCode.Text.Trim(),
                            _locationMapImage,
                            _facilityImage,
                            _venueImage,
                            false);
                    }
                    #endregion
                }
                mode = EnumFormMode.ViewMode;
                dataGridViewVenue.Enabled = true;
                buttonCancel.Text = "Close";
                buttonAddSave.Text = "Add";
                UtilityHelper.SetToReadOnly(Controls);
            }
        }

        private void linkLabelVenueImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog.ShowDialog(this);
            pictureBoxVenueImage.ImageLocation = openFileDialog.FileName;
            _fileVenueImage = openFileDialog.FileName;
        }

        private void linkLabelLocationMap_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog.ShowDialog(this);
            pictureBoxLocationMap.ImageLocation = openFileDialog.FileName;
            _fileLocationMapImage = openFileDialog.FileName;
        }

        private void linkLabelFacilityImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog.ShowDialog(this);
            pictureBoxFacilityImage.ImageLocation = openFileDialog.FileName;
            _fileFacilityImage = openFileDialog.FileName;
        }

        private void checkBoxFacilityImage_CheckedChanged(object sender, EventArgs e)
        {
            ResizeImage(checkBoxFacilityImage.Checked, panelFacilityImage, pictureBoxFacilityImage);
        }

        private void checkBoxLocationMap_CheckedChanged(object sender, EventArgs e)
        {
            ResizeImage(checkBoxLocationMap.Checked, panelVenueImage, pictureBoxLocationMap);
        }

        private void checkBoxVenueImage_CheckedChanged(object sender, EventArgs e)
        {
            ResizeImage(checkBoxVenueImage.Checked, panelVenueImage, pictureBoxVenueImage);
        }

        private void ResizeImage(bool resize, Panel panelImage, PictureBox pictureImage)
        {
            if (resize)
            {
                pictureImage.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureImage.Size = new Size(panelImage.Width, panelImage.Height);
            }
            else
            {
                pictureImage.SizeMode = PictureBoxSizeMode.AutoSize;
            }
        }

        private byte[] FileToByte(string fileName, byte[] imageSelected)
        {
            if (fileName.Length > 0 && File.Exists(fileName))
            {
                return UtilityHelper.FileToByte(fileName);
            }
            else if (_venueID > 0)
            {
                if (imageSelected == null)
                {
                    return UtilityHelper.BitmapToByte(Resources.blank);
                }
                else
                {
                    return imageSelected;
                }
            }
            else
            {
                return UtilityHelper.BitmapToByte(Resources.blank);
            }
        }

        private void dataGridViewVenue_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //_isCellClick = true;
            _currentVenueIndex=dataGridViewVenueCommand(e);
        }

        private void FillTabContent(int index)
        {
            DataRow dr = _dtVenue.Rows[index];
            _rowIndex = index;
            _venueID = Convert.ToInt32(dr[SAPVenueReadWrite._venueIDColumnName]);
            textBoxName.Text = ((string)dr[SAPVenueReadWrite._venueNameColumnName]).Trim();
            textBoxDescription.Text = ((string)dr[SAPVenueReadWrite._venueDescriptionColumnName]).Trim();
            textBoxStreet.Text = ((string)dr[SAPVenueReadWrite._venueStreetColumnName]).Trim();
            textBoxCity.Text = ((string)dr[SAPVenueReadWrite._venueCityColumnName]).Trim();
            textBoxState.Text = ((string)dr[SAPVenueReadWrite._venueStateColumnName]).Trim();
            textBoxPostalCode.Text = ((string)dr[SAPVenueReadWrite._venuePostalCodeColumnName]).Trim();
            pictureBoxLocationMap.Image =
                UtilityHelper.ByteToImage((byte[])dr[SAPVenueReadWrite._venueGeographicMapColumnName]);
            pictureBoxVenueImage.Image =
                UtilityHelper.ByteToImage((byte[])dr[SAPVenueReadWrite._venueImageColumnName]);
            pictureBoxFacilityImage.Image =
                UtilityHelper.ByteToImage((byte[])dr[SAPVenueReadWrite._venueFacilityMapColumnName]);
        }

        private void ClearImages()
        {
            pictureBoxFacilityImage.Image =
                Resources.blank;
            pictureBoxLocationMap.Image =
                Resources.blank;
            pictureBoxVenueImage.Image =
                Resources.blank;
        }

        private bool IsValid()
        {
            bool isValid = true;

            errorProvider.Clear();
            if (textBoxName.Text.Trim().Length == 0)
            {
                errorProvider.SetError(textBoxName, "Venue Name is required.");
                isValid = false;
            }

            if (textBoxDescription.Text.Trim().Length == 0)
            {
                errorProvider.SetError(textBoxDescription, "Description is required.");
                isValid = false;
            }

            if (textBoxStreet.Text.Trim().Length == 0)
            {
                errorProvider.SetError(textBoxStreet, "Street is required.");
                isValid = false;
            }

            if (textBoxCity.Text.Trim().Length == 0)
            {
                errorProvider.SetError(textBoxCity, "City is required.");
                isValid = false;
            }

            if (textBoxState.Text.Trim().Length == 0)
            {
                errorProvider.SetError(textBoxState, "State is required.");
                isValid = false;
            }

            if (textBoxPostalCode.Text.Trim().Length == 0)
            {
                errorProvider.SetError(textBoxPostalCode, "Postal Code is required.");
                isValid = false;
            }

            return isValid;
        }

        private void dataGridViewVenue_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            //_isCellClick = false;
            _currentVenueIndex= dataGridViewVenueCommand(e);
        }

        private int dataGridViewVenueCommand(DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                FillContent(e.RowIndex);
                return e.RowIndex;
            }

            return -1;
        }

        private void FillContent(int rowIndex)
        {
            if (_dtVenue.Rows.Count > rowIndex)
            {
                FillTabContent(rowIndex);
            } 
        }

        private void buttonNew_Click(object sender, EventArgs e)
        {
            if (mode == EnumFormMode.ViewMode)
            {
                mode = EnumFormMode.AddMode;

                buttonSave.Text = "Save";
                buttonSave.Visible = true;
                buttonCancel.Text = "Cancel";
                dataGridViewVenue.Enabled = false;
                UtilityHelper.ClearTextbox(Controls);
                UtilityHelper.EnableControls(Controls);
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (_currentVenueIndex >= 0 && 
                mode== EnumFormMode.ViewMode)
            {
                bool editOk = (bool)_dtVenue.Rows[_currentVenueIndex]["EditTag"];
                if ((bool)_dtVenue.Rows[_currentVenueIndex]["EditTag"])
                {
                    ClearImages();
                    UtilityHelper.ClearTextbox(Controls);
                    UtilityHelper.EnableControls(Controls);
                    FillTabContent(_currentVenueIndex);
                    dataGridViewVenue.Enabled = false;
                    buttonSave.Visible = true;
                    buttonCancel.Text = "Cancel";
                    mode = EnumFormMode.EditMode;
                }
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (!IsValid())
            {
                return;
            }

            SAPVenueReadWrite venueRW =
                new SAPVenueReadWrite(Config._dbConnectionName);

            if (mode == EnumFormMode.EditMode)
            {
                #region modify record
                _locationMapImage = FileToByte(_fileLocationMapImage, _locationMapImage);
                _facilityImage = FileToByte(_fileFacilityImage, _facilityImage);
                _venueImage = FileToByte(_fileVenueImage, _venueImage);
                venueRW.Update(
                    _venueID,
                    textBoxName.Text.Trim(),
                    textBoxDescription.Text.Trim(),
                    textBoxStreet.Text.Trim(),
                    textBoxCity.Text.Trim(),
                    textBoxState.Text.Trim(),
                    textBoxPostalCode.Text.Trim(),
                    _locationMapImage,
                    _facilityImage,
                    _venueImage);
                _dtVenue.Rows[_rowIndex][SAPVenueReadWrite._venueNameColumnName] =
                    textBoxName.Text.Trim();
                _dtVenue.Rows[_rowIndex][SAPVenueReadWrite._venueDescriptionColumnName] =
                    textBoxDescription.Text.Trim();
                _dtVenue.Rows[_rowIndex][SAPVenueReadWrite._venueStreetColumnName] =
                    textBoxStreet.Text.Trim();
                _dtVenue.Rows[_rowIndex][SAPVenueReadWrite._venueCityColumnName] =
                    textBoxCity.Text.Trim();
                _dtVenue.Rows[_rowIndex][SAPVenueReadWrite._venueStateColumnName] =
                    textBoxState.Text.Trim();
                _dtVenue.Rows[_rowIndex][SAPVenueReadWrite._venuePostalCodeColumnName] =
                    textBoxPostalCode.Text.Trim();
                _dtVenue.Rows[_rowIndex][SAPVenueReadWrite._venueGeographicMapColumnName] =
                    _locationMapImage;
                _dtVenue.Rows[_rowIndex][SAPVenueReadWrite._venueImageColumnName] =
                    _facilityImage;
                _dtVenue.Rows[_rowIndex][SAPVenueReadWrite._venueFacilityMapColumnName] =
                    _venueImage;
                #endregion
            }
            else
            {
                #region insert record
                _locationMapImage = FileToByte(_fileLocationMapImage, null);
                _facilityImage = FileToByte(_fileFacilityImage, null);
                _venueImage = FileToByte(_fileVenueImage, null);
                venueRW.Insert(
                    textBoxName.Text.Trim(),
                    textBoxDescription.Text.Trim(),
                    textBoxStreet.Text.Trim(),
                    textBoxCity.Text.Trim(),
                    textBoxState.Text.Trim(),
                    textBoxPostalCode.Text.Trim(),
                    _locationMapImage,
                    _facilityImage,
                    _venueImage,
                    out _venueID);

                if (mode == EnumFormMode.CallbackAdd)
                {
                    _venueName = textBoxName.Text.Trim();
                    DialogResult = DialogResult.OK;
                    Close();
                    return;
                }
                else
                {
                    //byte[] editIco = UtilityHelper.BitmapToByte(Resources.icoedit);
                    //byte[] delIco = UtilityHelper.BitmapToByte(Resources.icodelete);

                    _dtVenue.Rows.Add(
                        //editIco,
                        //delIco,
                        _venueID,
                        textBoxName.Text.Trim(),
                        textBoxDescription.Text.Trim(),
                        textBoxStreet.Text.Trim(),
                        textBoxCity.Text.Trim(),
                        textBoxState.Text.Trim(),
                        textBoxPostalCode.Text.Trim(),
                        _locationMapImage,
                        _facilityImage,
                        _venueImage,
                        true,
                        true);
                }
                #endregion
            }
            mode = EnumFormMode.ViewMode;
            dataGridViewVenue.Enabled = true;
            buttonCancel.Text = "Close";
            buttonAddSave.Text = "Add";
            UtilityHelper.SetToReadOnly(Controls);
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (_currentVenueIndex >= 0 &&
                mode == EnumFormMode.ViewMode)
            {
                DataRow dr = _dtVenue.Rows[_currentVenueIndex];
                if (!(bool)dr["DelTag"])
                {
                    return;
                }
                if (Message.DeleteMessage((string)dr[SAPVenueReadWrite._venueNameColumnName])
                    == DialogResult.Yes)
                {
                    SAPVenueReadWrite venueRW =
                        new SAPVenueReadWrite(Config._dbConnectionName);
                    venueRW.Delete(Convert.ToInt32(dr[SAPVenueReadWrite._venueIDColumnName]));
                    _dtVenue.Rows.Remove(dr);
                    dr = _dtVenue.Rows[_currentVenueIndex];
                }

            }
        }

        
    }
}