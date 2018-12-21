using System.Collections.Generic;

namespace MedicalSys.DataAccessor
{
    /// <summary>
    /// Interface IDAO
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDAO<T> where T : IDataObject
    {
        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>List{`0}.</returns>
        List<T> GetAll();
        /// <summary>
        /// Updates the specified data object.
        /// </summary>
        /// <param name="dataObject">The data object.</param>
        void Update(T dataObject);
        /// <summary>
        /// Deletes the specified data object.
        /// </summary>
        /// <param name="dataObject">The data object.</param>
        void Delete(T dataObject);
        /// <summary>
        /// Inserts the specified data object.
        /// </summary>
        /// <param name="dataObject">The data object.</param>
        void Insert(T dataObject);
    }
}
