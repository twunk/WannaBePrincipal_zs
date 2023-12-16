using Google.Cloud.Firestore;

namespace WannaBePrincipal.Models
{
    public class UserRepository
    {
        private readonly FirestoreDb db;
        
        public UserRepository(string project)
        {
            db = FirestoreDb.Create(project);
        }

        /// <summary>
        /// Add new user to a db.
        /// </summary>
        /// <param name="user">Document with data of the entry.</param>
        public async Task<string> AddUser(UserData user)
        {
            CollectionReference collRef = db.Collection("users");
            var createResponse = await collRef.AddAsync(user.ToDictionary());
            return createResponse.Id;
        }

        /// <summary>
        /// Returns with 1 user's data.
        /// </summary>
        /// <param name="id">ID of the user.</param>
        /// <returns>Returns with true if the user is exists.</returns>
        public async Task<UserData> GetUser(string userID)
        {
            // check if it exists
            DocumentReference docRef = db.Collection("users").Document(userID.ToString());
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

            if (!snapshot.Exists)
            {
                return null;
            }
            else
            {
                return snapshot.ConvertTo<UserData>();
            }
        }

        /// <summary>
        /// Edit user's data in db.
        /// </summary>
        /// <param name="id">ID of the user.</param>
        /// <param name="user">Document with data of the entry.</param>
        /// <returns>Returns with true if the user is exists.</returns>
        public async Task<bool> EditUser(string userID, UserData user)
        {
            // check if it exists
            DocumentReference docRef = db.Collection("users").Document(userID.ToString());
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

            if (!snapshot.Exists)
            {
                return false;
            }
            else
            {
                // modify data
                WriteResult editResult = await docRef.SetAsync(user.ToDictionary());

                return true;
            }
        }

        /// <summary>
        /// Delete user's data in db.
        /// </summary>
        /// <param name="id">ID of the user.</param>
        /// <returns>Returns with true if the user was existed.</returns>
        public async Task<bool> DeleteUser(string userID)
        {
            // check if it exists
            DocumentReference docRef = db.Collection("users").Document(userID.ToString());
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

            if (!snapshot.Exists)
            {
                return false;
            }
            else
            {
                // modify data
                WriteResult editResult = await docRef.DeleteAsync();

                return true;
            }
        }

        /// <summary>
        /// List all users.
        /// </summary>
        /// <returns>A <see cref="CollectionReference"/> with all the users.</returns>
        public async Task<List<Dictionary<string, object>>> GetUsersFromDB()
        {
            List<Dictionary<string, object>> docs = [];

            Query allCitiesQuery = db.Collection("users");
            QuerySnapshot allCitiesQuerySnapshot = await allCitiesQuery.GetSnapshotAsync();
            foreach (DocumentSnapshot documentSnapshot in allCitiesQuerySnapshot.Documents)
            {
                Console.WriteLine("Document data for {0} document:", documentSnapshot.Id);
                Dictionary<string, object> docData = documentSnapshot.ToDictionary();
                docs.Add(docData);
            }

            return docs;
        }

        /// <summary>
        /// Delete all users.
        /// </summary>
        public async Task DeleteCollection(string collectionName, int batchSize = 100)
        {
            CollectionReference collectionReference = db.Collection(collectionName);
            QuerySnapshot snapshot = await collectionReference.Limit(batchSize).GetSnapshotAsync();
            IReadOnlyList<DocumentSnapshot> documents = snapshot.Documents;
            while (documents.Count > 0)
            {
                foreach (DocumentSnapshot document in documents)
                {
                    Console.WriteLine("Deleting document {0}", document.Id);
                    await document.Reference.DeleteAsync();
                }
                snapshot = await collectionReference.Limit(batchSize).GetSnapshotAsync();
                documents = snapshot.Documents;
            }
            Console.WriteLine("Finished deleting all documents from the collection.");
        }
    }
}
