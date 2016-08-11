using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechRadar.Shared.Enums;

namespace TechRadar.DataLayer.Entities
{
    public class SearchParamEntity
    {
        public SearchParamEntity()
        {
            //initialized to avoid null execption
            Operation = DBOperation.EqualTo;
            SortBy = SortOperation.None;
            LogicalOperation = LogicalOperation.None;
            PairOperation = PairOperation.None;
        }
        public SearchParamEntity(string modelFieldName, string fieldValue, DBOperation operation, LogicalOperation logicalOperation, SortOperation sortby, PairOperation pairOperation)
        {
            ModelFieldName = modelFieldName;
            FieldValue = fieldValue;
            Operation = operation;
            PairOperation = pairOperation;
            LogicalOperation = logicalOperation;
            SortBy = sortby;
        }
        /// <summary>
        /// Gets or sets the model field name.
        /// </summary>
        public string ModelFieldName { get; set; }
        /// <summary>
        /// Gets or sets the model field value.
        /// </summary>
        public string FieldValue { get; set; }
        /// <summary>
        /// Gets or sets the database operation.
        /// </summary>
        public DBOperation Operation { get; set; }
        public LogicalOperation LogicalOperation { get; set; }
        public SortOperation SortBy { get; set; }
        public PairOperation PairOperation { get; set; }


    }
}
