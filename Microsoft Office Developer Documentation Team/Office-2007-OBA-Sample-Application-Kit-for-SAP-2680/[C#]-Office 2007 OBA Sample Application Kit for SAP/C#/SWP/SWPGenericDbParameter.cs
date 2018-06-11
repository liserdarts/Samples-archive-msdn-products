using System;
using System.Data;
using System.Data.Common;

namespace SoftwarePronto.CodeGenerator.DatabaseDriverCommon
{
    // We need to specify parameters but we do not know if they are
    // OracleParameter or SQLParameter, etc. So the CG creates a generic
    // parameter that can be used to create "any" underlying parameter type
    public class SWPGenericDbParameter : DbParameter
    {
        private DbType _dbType;

        private ParameterDirection _direction;

        private bool _isNullable = false;

        private string _parameterName;

        private int _size = 0;

        private string _sourceColumn = string.Empty;

        private bool _sourceColumnNullMapping = false;

        private DataRowVersion _dataRowVersion = DataRowVersion.Current;

        private object _value = null;

        public SWPGenericDbParameter(
            string parameterName,
            DbType dbType,
            ParameterDirection direction,
            int size,
            object value)
        {
            _parameterName = parameterName;
            _dbType = dbType;
            _direction = direction;
            _size = size;
            _value = value;
        }

        public SWPGenericDbParameter(
            string parameterName,
            DbType dbType,
            ParameterDirection direction,
            int size)
        {
            _parameterName = parameterName;
            _dbType = dbType;
            _direction = direction;
            _size = size;
        }

        // ugly name but it does explain clearly what is going on
        public void Assign_ParameterName_DBType_Direction_Size_Value(
            DbParameter parameter)
        {
            parameter.ParameterName = _parameterName;
            parameter.DbType = _dbType;
            parameter.Direction = _direction;
            parameter.Size = _size;
            parameter.Value = _value;
        }

        public override DbType DbType
        {
            get
            {
                return _dbType;
            }

            set
            {
                _dbType = value;
            }
        }

        public override ParameterDirection Direction
        {
            get
            {
                return _direction;
            }
            set
            {
                _direction = value;
            }
        }

        public override bool IsNullable
        {
            get
            {
                return _isNullable;
            }
            set
            {
                _isNullable = value;
            }
        }

        public override string ParameterName
        {
            get
            {
                return _parameterName;
            }
            set
            {
                _parameterName = value;
            }
        }

        public override int Size
        {
            get
            {
                return _size;
            }
            set
            {
                _size = value;
            }
        }

        public override string SourceColumn
        {
            get
            {
                return _sourceColumn;
            }
            set
            {
                _sourceColumn = value;
            }
        }

        public override bool SourceColumnNullMapping
        {
            get
            {
                return _sourceColumnNullMapping;
            }
            set
            {
                _sourceColumnNullMapping = value;
            }
        }

        public override DataRowVersion SourceVersion
        {
            get
            {
                return _dataRowVersion;
            }
            set
            {
                _dataRowVersion = value;
            }
        }

        public override object Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }

        public override void ResetDbType()
        {
        }
    }
}
