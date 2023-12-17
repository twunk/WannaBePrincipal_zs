using Google.Cloud.Firestore;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace WannaBePrincipal.Models
{
    public class UserRepository : IUserModel
    {
        private readonly string projectString = "able-source-200515";
        
        /// <summary>
        /// Add new user to a db.
        /// </summary>
        /// <param name="user">Document with data of the entry.</param>
        public async Task<string> AddUser(UserData user)
        {
            FirestoreDb db = FirestoreDb.Create(projectString);
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
            FirestoreDb db = FirestoreDb.Create(projectString);
            DocumentReference docRef = db.Collection("users").Document(userID.ToString());
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

            if (!snapshot.Exists)
            {
                throw new KeyNotFoundException($"User with ID {userID} not found.");
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
            FirestoreDb db = FirestoreDb.Create(projectString);
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
            FirestoreDb db = FirestoreDb.Create(projectString);
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
        public async Task<List<UserData>> GetUsersFromDB()
        {
            List<UserData> docs = [];

            FirestoreDb db = FirestoreDb.Create(projectString);
            Query allCitiesQuery = db.Collection("users");
            QuerySnapshot allCitiesQuerySnapshot = await allCitiesQuery.GetSnapshotAsync();
            foreach (DocumentSnapshot documentSnapshot in allCitiesQuerySnapshot.Documents)
            {
                Console.WriteLine("Document data for {0} document:", documentSnapshot.Id);
                
                Dictionary<string, object> docData = documentSnapshot.ToDictionary();
                string json = JsonConvert.SerializeObject(docData);
                UserData? user = JsonConvert.DeserializeObject<UserData>(json);
                if(user == null)
                {
                    throw new DataMisalignedException("Something wrong in db.");
                }
                user.Id = documentSnapshot.Id;

                docs.Add(user);
            }

            return docs;
        }

        /// <summary>
        /// Delete all users.
        /// </summary>
        public async Task DeleteCollection(string collectionName, int batchSize = 100)
        {
            FirestoreDb db = FirestoreDb.Create(projectString);
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
