using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Windows.Forms;
using Microsoft.BusinessData.MetadataModel;
using Microsoft.BusinessData.MetadataModel.Collections;
using Microsoft.BusinessData.Runtime;
using Microsoft.Office.BusinessApplications.Runtime.Deployment;
using Microsoft.Office.BusinessData.MetadataModel;

namespace BCSPowerPointAddin
{
    public partial class BCSChartInfo : UserControl
    {
        Dictionary<IEntity, DataTable> EntityDataLookup = new Dictionary<IEntity, DataTable>();
        Dictionary<String, IEntity> EntityLookup = new Dictionary<String, IEntity>();
        public event EventHandler<ChartDataEventArgs> m_InsertChart;
        public event EventHandler<ChartDataEventArgs> m_UpdateChart;
        public BCSChartInfo()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Click event of the btnInsert control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnInsert_Click(object sender, EventArgs e)
        {
            String EntityName = ectList.SelectedItem as String;
            if (String.IsNullOrWhiteSpace(EntityName))
                return;
            IEntity Entity = EntityLookup[EntityName];
            if (chkCategories.Items.Count == 0)
            {
                System.Windows.Forms.MessageBox.Show("Select at least one category in the checkbox");
                return;
            }
            ChartDataEventArgs args = GetChartDataObject(Entity);
            //Rasise the event in a thread safe manner
            EventHandler<ChartDataEventArgs> temp = Interlocked.CompareExchange(ref m_InsertChart, null, null);
            if (temp != null) temp(this, args);
        }

        /// <summary>
        /// Gets the chart data object.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        private ChartDataEventArgs GetChartDataObject(IEntity entity)
        {
            ChartDataObject chartObject = new ChartDataObject();
            chartObject.Data = EntityDataLookup[entity];
            chartObject.Entity = ectList.SelectedItem as IEntity;
            chartObject.SeriesName = seriesList.SelectedItem as string;
            chartObject.XValues = new string[chkCategories.CheckedItems.Count];

            int counter = 0;
            foreach (string item in chkCategories.CheckedItems)
            {
                chartObject.XValues[counter] = item;
                counter++;
            }
            ChartDataEventArgs args = new ChartDataEventArgs();
            args.ChartDataObject = chartObject;
            return args;
        }
        /// <summary>
        /// Populates the Combo box with the available ECTs.
        /// </summary>
        public void PopulateControls()
        {
            RemoteSharedFileBackedMetadataCatalog Catalog = new RemoteSharedFileBackedMetadataCatalog();
            IEntityInstanceEnumerator InstanceEnumerator = null;
            INamespacedEntityDictionaryDictionary entDictDict = Catalog.GetEntities("*");
            foreach (INamedEntityDictionary entDict in entDictDict.Values)
            {
                foreach (IEntity entity in entDict.Values)
                {
                    EntityLookup[entity.Name] = entity;
                    ectList.Items.Add(entity.Name);
                    
                }
            }
            //Fill the ECT combobox
            if (EntityLookup != null && EntityLookup.Count > 0)
            {
                foreach (IEntity entity in EntityLookup.Values)
                {
                    InstanceEnumerator = entity.FindFiltered(
                                        entity.GetDefaultFinderFilters(),
                                        entity.GetMethodInstances(MethodInstanceType.Finder)[0].Value.Name,
                                        entity.GetLobSystem().GetLobSystemInstances()[0].Value,
                                        OperationMode.Online);
                    System.Data.DataTable dt = Catalog.Helper.CreateDataTable(InstanceEnumerator);
                    //System.Data.DataTable dt = Globals.ThisAddIn.GetBCSDataTest();
                    if(dt!=null)
                        EntityDataLookup[entity] = dt;

                }
                //ectList.DataSource = EntityCollection;
                ectList.SelectedIndex = 0;
                //ectList.SelectedItem = EntityCollection[0];
                ectList.SelectedIndexChanged += new EventHandler(comboBox1_SelectedIndexChanged);
            }
        }
        /// <summary>
        /// Handles the SelectedIndexChanged event of the comboBox1 control.
        /// This will populate the series name combo and the categories checbox list control
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            IEntity entity = EntityLookup[(String)ectList.SelectedItem];
            //Ge the tDataTable using helper
            if (EntityDataLookup.Count > 0)
            {
                System.Data.DataTable dt = EntityDataLookup[entity];

                foreach (DataColumn column in dt.Columns)
                {
                    seriesList.Items.Add(column.ColumnName);
                    chkCategories.Items.Add(column.ColumnName);
                }
                if (seriesList.Items.Count > 0)
                    seriesList.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Handles the Click event of the btnUpdate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            String EntityName = ectList.SelectedItem as String;
            if (String.IsNullOrWhiteSpace(EntityName))
                return;
            IEntity Entity = EntityLookup[EntityName];
            if (chkCategories.Items.Count == 0)
            {
                System.Windows.Forms.MessageBox.Show("Select at least one category in the checkbox");
                return;
            }
            ChartDataEventArgs args = GetChartDataObject(Entity);

            EventHandler<ChartDataEventArgs> temp = Interlocked.CompareExchange(ref m_UpdateChart, null, null);
            if (temp != null) temp(this, args);
        }

    }
}
