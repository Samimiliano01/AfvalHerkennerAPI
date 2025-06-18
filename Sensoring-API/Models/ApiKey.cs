namespace Sensoring_API.Models
{
    /// <summary>
    /// Represents an API key with an associated role, used to manage access permissions for the application.
    /// </summary>
    public class ApiKey
    {
        /// <summary>
        /// This field is the primary key in the database and is also the hashed key that's used for authentication
        /// </summary>
        public required string Key {  get; set; }

        /// <summary>
        /// Specifies the role associated with the API key, used to determine the level of access or permissions granted to the key.
        /// </summary>
        public required string Role { get; set; }
    }
}
