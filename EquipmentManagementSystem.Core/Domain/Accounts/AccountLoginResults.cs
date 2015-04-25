namespace EquipmentManagementSystem.Core.Domain.Accounts
{
    /// <summary>
    /// Represents the account login result enumeration
    /// </summary>
    public enum AccountLoginResults
    {
        /// <summary>
        /// Login successful
        /// </summary>
        Successful = 1,
        /// <summary>
        /// Account dies not exist (email or username)
        /// </summary>
        AccountNotExist = 2,
        /// <summary>
        /// Wrong password
        /// </summary>
        WrongPassword = 3,
        /// <summary>
        /// Account have not been activated
        /// </summary>
        NotActive = 4,
        /// <summary>
        /// Account has been deleted 
        /// </summary>
        Deleted = 5,
        /// <summary>
        /// Account not registered 
        /// </summary>
        NotRegistered = 6,
    }
}
