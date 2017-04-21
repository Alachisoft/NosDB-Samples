// ===============================================================================
// Alachisoft (R) NosDB Sample Code.
// ===============================================================================
// Copyright © Alachisoft.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================

using NosDB.Common.Triggers;
using NosDB.Common;

namespace NosDB.Samples
{
    /// ******************************************************************************
    /// <summary>
    /// A sample trigger that validates the "UnitPrice" attribute in Product Collection.
    /// 
    /// Requirements:
    ///     1. A NosDB database and collection
    ///     2. This assembly to be built and deployed on the NosDB server
    /// </summary>
    public class ProductValidationTrigger : IDatabaseTrigger
    {
        private const string UNIT_PRICE = "UnitPrice";

        /// <summary>
        /// The method to be called prior to the operation performed
        /// </summary>
        /// <param name="context">Contains the information about the operation performed and the document it is to be performed on</param>
        public bool PreTrigger(TriggerContext context)
        {
            // The context contains the type of operation to be performed in the 
            // TriggerAction property i.e. if a document is being inserted, updated or deleted.
            // A switch statement can be applied to perform a specific action on each of 
            // the operation type. The context also contains the document upon which the
            // operation is going to be performed, hence providing the opportunity to perform an
            // automated modification on the document beforehand. 

            switch (context.TriggerAction)
            {
                // This case is executed before inserting a new document
                case TriggerAction.PreInsert:
                    return IsValidUnitPrice(context.EventDocument);

                // This case is executed before updating an existing document
                case TriggerAction.PreUpdate:

                    return IsValidUnitPrice(context.EventDocument);

                // This case is executed before deleting an existing document
                case TriggerAction.PreDelete:

                    break;
            }
            // The return statement controls the behavior of operation. 
            // A return false will cause the operation to fail. 
            return true;
        }

        /// <summary>
        /// The method to be called after to the operation performed
        /// </summary>
        /// <param name="context">Contains the information about the operation performed and the document it is performed on</param>

        public void PostTrigger(TriggerContext context)
        {
            switch (context.TriggerAction)
            {
                // This case is executed after inserting a new document
                case TriggerAction.PostInsert:

                    break;

                // This case is executed after updating an existing document
                case TriggerAction.PostUpdate:

                    break;

                // This case is executed after deleting an existing document
                case TriggerAction.PostDelete:

                    break;
            }
        }

        /// <summary>
        /// This method validates the value of "UnitPrice".
        /// </summary>
        /// <param name="eventDocument">
        /// Document which is being inserted, updated or deleted.
        /// </param>
        /// <returns>
        /// Boolean: true if the "UnitPrice" is valid and false if it is not.
        /// </returns>
        private bool IsValidUnitPrice(IJSONDocument eventDocument)
        {
            try
            {
                //Validate "UnitPrice" attribute exists in document and is not NULL.
                if (!eventDocument.Contains(UNIT_PRICE) || eventDocument[UNIT_PRICE] == null)
                    return false;

                double unitPrice = eventDocument.GetAsDouble(UNIT_PRICE);

                //Validates the value of "UnitPrice" attribute.
                if (unitPrice <= 0.0)
                    return false;
            }
            catch
            {
                return false;
            }
            return true;
        }
    }

}
